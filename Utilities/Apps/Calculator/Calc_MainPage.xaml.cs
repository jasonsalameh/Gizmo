using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;
using System.CodeDom.Compiler;
using Windows.UI.ViewManagement;
using System.Reflection;
using Windows.System;
using Windows.UI.Core;
using System.Numerics;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Search;
using System.Globalization;
using Windows.UI.ApplicationSettings;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media.Animation;
using Utilities;
using Calculator.CalcViews;
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Calculator
{
    public sealed partial class Calc_MainPage : Page
    {
        // Most recent drawing window
        public static TextWindow recentWindow = null;

        // So we can make using input keys work (from keyboard) we
        // need to maintain which "equals" key is the current one
        private Button _equalKey = null;

        // Drawing Windows
        private TextWindow _smallWindow = null;
        private TextWindow _mainWindow = null;

        // Paranthesis
        public static int _numParans = 0;
        public const string OPEN_PARAN = "(";
        public const string CLOSE_PARAN = ")";

        // Prev operator
        private string _prevOperator = string.Empty;
        private TextWindow.InputType _prevInputType = TextWindow.InputType.NULL;

        // Memory
        private ObservableCollection<MemoryItem> _memoryItems;
        private const int MAX_MEMORY_ITEMS = 10;

        // App Bar
        private const int APP_BAR_DELAY_TIME_LONG = 2500;
        private const int APP_BAR_DELAY_TIME_SHORT = 1500;

        // Evaluation
        private EvaluationEngine _eval = new EvaluationEngine();
        public EvaluationEngine Eval
        {
            get { return _eval; }
        }

        // State
        private const char DELIMETER = '#';

        // Search
        //private SearchPane _searchPane = null;

        // Calc States
        private App.Apps calcState = App.Apps.Scientific;
        private App.Apps prevState;
        public enum CalcStateValues { Deg = 20, Rad = 19, Hex = 8, Dec = 7, Oct = 6, Bin = 5 }
        private CalcStateValues calcStateValue;

        // so that when you change views you don't loose your previous setting in the calc
        public static CalcStateValues programmerCalcState = CalcStateValues.Hex;
        public static CalcStateValues scientificCalcState = CalcStateValues.Deg;

        // Current Page
        public CalculatorView currentView;

        public Calc_MainPage()
        {
            this.InitializeComponent();
        }

        #region On Navigated To

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //TBD: SettingsPane.GetForCurrentView().CommandsRequested += Settings_CommandsRequested;

            Utilities.Common.ShowHelp.ShowHelpMessage(App.Apps.Calculator);
        }

        public void Initialize(App.Apps state)
        {
            // set the initial calc settings
            prevState = calcState;
            calcState = state;

            InitEval();

            // Init drawing boxes
            _smallWindow = new TextWindow(OutputWindow, false, this);
            _mainWindow = new TextWindow(OutputWindowMain, true, this);

            // for character inputs
            CoreWindow.GetForCurrentThread().CharacterReceived += CharacterReceived;

            // set viewstate handler
            //Window.Current.SizeChanged += Current_SizeChanged;
            this.SizeChanged += Current_SizeChangedUAP;


            // init calc view
            MainPage_ViewStateChanged();

            // init the calc to zeroz
            initCalc();

            // init the search function
            initSearch();

            // Init our list of currently active memory items
            _memoryItems = new ObservableCollection<MemoryItem>();

            // bind the grid to our new list
            MemoryGrid.ItemsSource = _memoryItems;

            // state handler for changing state
            LoadState();
            Windows.Storage.ApplicationData.Current.DataChanged += State_DataChanged;

            App.ScheduleOnNextRender(delegate
            {
                List<Breadcrumb> breadcrumbs = new List<Breadcrumb>();
                breadcrumbs.Add(new Breadcrumb(App.Apps.MainPage));
                breadcrumbs.Add(new Breadcrumb(App.Apps.Calculator));
                breadcrumbs.Add(new Breadcrumb(calcState, true));
                TitleRow.SetupBreadcrumbs(breadcrumbs);
            });
        }


        public void InitEval()
        {
            // set function handlers for our evaltuator
            _eval.ProcessSymbol += ProcessSymbol;
            _eval.ProcessFunction += ProcessFunction;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            try
            {

                this.UnregisterSearch();
                //TBD:SettingsPane.GetForCurrentView().CommandsRequested -= Settings_CommandsRequested;

                CoreWindow.GetForCurrentThread().CharacterReceived -= CharacterReceived;
                //Window.Current.SizeChanged -= Current_SizeChanged;
                this.SizeChanged += Current_SizeChangedUAP;

                Windows.Storage.ApplicationData.Current.DataChanged -= State_DataChanged;
            }
            catch { }
        }

        //TBD:
        //public void Settings_CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        //{
        //    SettingsCommand about = new SettingsCommand("About", "About", (x) =>
        //    {
        //        MainPage.ShowPopup(new Utilities.Settings.About(), this.ActualHeight, "About");
        //    });

        //    SettingsCommand feedback = new SettingsCommand("Feedback", "Report a bug", (x) =>
        //    {
        //        (Application.Current as App).Feedback();
        //    });

        //    args.Request.ApplicationCommands.Add(about);
        //    args.Request.ApplicationCommands.Add(feedback);

        //}

        #endregion

        #region Publics

        public static bool IsCalculator(App.Apps app)
        {
            if (app == App.Apps.Calculator ||
                app == App.Apps.Programmer ||
                app == App.Apps.Scientific ||
                app == App.Apps.Statistics)
                return true;

            return false;
        }

        #endregion

        #region View

        private void SetGridMargin(double value)
        {
            Thickness th = new Thickness(value, 0, value, value);
            CalcViewGrid.Margin = th;
        }

        public void SetPage()
        {
            // first remove the current page from the grid
            CalcViewGrid.Children.Remove(currentView as Page);

            double margin = 20;

            CalcStateViewBox.Margin = new Thickness(24, 0, 0, 0);
            CalcStateColumn.Width = new GridLength(90);

            // Small Screen Settings
            if (App.isSmallScreen())
                currentView = new CalcViews.SmallScientific(this);

            // Portrait Settings
            else if (calcState == App.Apps.Programmer && App.isPortrait())
                currentView = new CalcViews.PortraitProgrammer(this);
            else if (calcState == App.Apps.Programmer && App.isSnapped())
                currentView = new CalcViews.SnappedProgrammer(this);
            else if (calcState == App.Apps.Statistics && App.isPortrait())
                currentView = new CalcViews.PortraitStatistical(this);
            else if (calcState == App.Apps.Statistics && App.isSnapped())
                currentView = new CalcViews.SnappedStatistical(this);
            else if (calcState == App.Apps.Scientific && App.isPortrait())
                currentView = new CalcViews.PortraitScientific(this);
            else if (calcState == App.Apps.Scientific && App.isSnapped())
                currentView = new CalcViews.SnappedScientific(this);


            // Landscape Settings
            else if (calcState == App.Apps.Programmer)
                currentView = new CalcViews.Programmer(this);
            else if (calcState == App.Apps.Statistics)
                currentView = new CalcViews.Statistical(this);
            else
                currentView = new CalcViews.Scientific(this);

            // specific UI things for snapped mode
            if (App.isSnapped())
            {
                CalcStateViewBox.Margin = new Thickness(2, 0, 0, 0);
                CalcStateColumn.Width = new GridLength(30);
                margin = 5;
            }

            if (App.isPortrait())
                margin = 5;

            // reset margin for new view
            SetGridMargin(margin);

            // reset the current view
            _mainWindow.currentView = currentView;

            // re-add the page
            CalcViewGrid.Children.Add(currentView as Page);

            // setup equal key for focus
            _equalKey = currentView.GetEqualsKey();

            // reset the equals key focus when we change view
            CalcHelpers.equalsKeyFocus(currentView.GetEqualsKey());
        }

        private void Current_SizeChangedUAP(object sender, SizeChangedEventArgs e)
        {
            
            App.UpdateViewModelFromSize(e.NewSize);

            if (App.CurrentView == App.PreviousView)
                return;

            Current_SizeChanged();
        }

        private void Current_SizeChanged()
        {
            MainPage_ViewStateChanged();
        }

        private void MainPage_ViewStateChanged()
        {
            this.TitleRow.ChangeViewState();

            CloseAppBars();

            // Appbar
            MemoryAppBar.Visibility = Windows.UI.Xaml.Visibility.Visible;
            MemoryAppBar.IsEnabled = true;

            if (isProgrammer())
            {
                this.calcState = App.Apps.Programmer;

                SetPage();
                ProgrammerSettings_Click(Calc_MainPage.programmerCalcState);
            }
            else if (isStatistics())
            {
                this.calcState = App.Apps.Statistics;

                SetPage();
                Stat_Add_Button_Click("0");
            }
            else
            {
                this.calcState = App.Apps.Scientific;
                //ScientificSettings.Visibility = Windows.UI.Xaml.Visibility.Visible;

                SetPage();
                ScientificSettings_Click(Calc_MainPage.scientificCalcState);
            }
            //}

            // reset the equals key focus when we change view
            CalcHelpers.equalsKeyFocus(currentView.GetEqualsKey());
        }

        #endregion

        #region AppBar

        private void CloseAppBars()
        {
            try
            {
                MemoryAppBar.IsOpen = false;
                //SettingsAppBar.IsOpen = false;
            }
            catch { }
        }

        #endregion

        #region Add to Expression

        private void addContentToExpression(string data, TextWindow.InputType currentInputType)
        {
            bool exception = false;
            try
            {
                #region Numbers

                #region Scientific

                if (_mainWindow.isValue(currentInputType) && !isProgrammer())
                {
                    // Here the user entered in a value after entering in another value like 55 or 55.55
                    if ((currentInputType == TextWindow.InputType.Int && recentWindow.GetLastInputType() == TextWindow.InputType.Double) ||
                        (currentInputType == TextWindow.InputType.Double && recentWindow.GetLastInputType() == TextWindow.InputType.Int) ||
                        (currentInputType == TextWindow.InputType.Int && recentWindow.GetLastInputType() == TextWindow.InputType.Int) ||
                        (currentInputType == TextWindow.InputType.Double && recentWindow.GetLastInputType() == TextWindow.InputType.Double))
                    {
                        _mainWindow.Add(data, currentInputType);
                    }

                    // Here the user entered in a regular value that is to be on its own so we put that into the main window
                    else if (currentInputType == TextWindow.InputType.Int || currentInputType == TextWindow.InputType.Double)
                    {
                        // Here the user corrected what he did last so what we need to do is remove the last element from the _smallWindow list
                        if (_smallWindow.GetLastInputType() == TextWindow.InputType.OperatorSingle || _smallWindow.isValue())
                        {
                            _smallWindow.RemoveLast();
                        }

                        // make sure to touch MAIN last so it's most recent
                        _mainWindow.Set(data, currentInputType, false);
                    }

                    else if (currentInputType == TextWindow.InputType.Rand || currentInputType == TextWindow.InputType.Symbol)
                    {
                        // Here the user corrected what he did last so what we need to do is remove the last element from the _smallWindow list
                        if (_smallWindow.GetLastInputType() == TextWindow.InputType.OperatorSingle || _smallWindow.isValue())
                        {
                            _smallWindow.RemoveLast();
                        }

                        // make sure to touch MAIN last so it's most recent
                        _mainWindow.Set(data, currentInputType, true);
                    }
                }

                #endregion

                #region Programmer

                else if (_mainWindow.isValue(currentInputType) && isProgrammer())
                {
                    // Here the user entered in a number after another
                    if (recentWindow.GetLastInputType() == TextWindow.InputType.Int)
                    {
                        _mainWindow.Add(CalcHelpers.toDec((currentView as ProgrammerView).calcState, data, (currentView as ProgrammerView).currentBits), TextWindow.InputType.Int);
                    }

                    // Here the user entered in a regular value that is to be on its own so we put that into the main window
                    else
                    {
                        // Here the user corrected what he did last so what we need to do is remove the last element from the _smallWindow list
                        if (_smallWindow.GetLastInputType() == TextWindow.InputType.OperatorSingle || _smallWindow.isValue())
                        {
                            _smallWindow.RemoveLast();
                        }

                        // make sure to touch MAIN last so it's most recent
                        _mainWindow.Set(data, currentInputType, false);
                    }
                }

                #endregion

                #endregion

                #region Parans

                else if (currentInputType == TextWindow.InputType.Paran && data == OPEN_PARAN)
                {
                    _smallWindow.Add(data, currentInputType);
                }

                // Here the user just closed the last paran in the expression so evaluate it
                //else if (currentInputType == TextWindow.InputType.Paran && data == CLOSE_PARAN && _numParans == 0 && recentWindow.isValue())
                //{
                //    // Hack 
                //    _numParans++;

                //    // if it was a valid value, use it in the expression
                //    if (_mainWindow.isValue() && recentWindow == _mainWindow)
                //        _smallWindow.Add(_mainWindow.Get(), _mainWindow.GetLastInputType());

                //    _smallWindow.Add(data, currentInputType);

                //    // Hack
                //    _numParans = 0;

                //    Equal_Button_Click(null, null);
                //}

                else if (currentInputType == TextWindow.InputType.Paran && data == CLOSE_PARAN && recentWindow.isValue())
                {
                    // if it was a valid value, use it in the expression
                    if (_mainWindow.isValue() && recentWindow == _mainWindow)
                        _smallWindow.Add(_mainWindow.Get(), _mainWindow.GetLastInputType());

                    _smallWindow.Add(data, currentInputType);

                    Equal_Button_Click(null, null, true);
                }

                #endregion

                #region Operators

                else if (currentInputType == TextWindow.InputType.OperatorDouble)
                {
                    // check to make sure the last character was not an open paran... if so we can't enter a double operator
                    if (_prevInputType == TextWindow.InputType.Paran &&
                        _smallWindow.GetLast().inputData == OPEN_PARAN)
                        return;

                    // only go in here if there really was two operators after eachother, we check this via the _mainwindow usedvalue var which tells me if 
                    // if we've already used whats in the mainwindow buffer
                    else if (currentInputType == TextWindow.InputType.OperatorDouble && currentInputType == recentWindow.GetLastInputType() && recentWindow == _smallWindow)
                    {
                        // if we could remove the last element ok proceed
                        if (_smallWindow.RemoveLast())
                            _smallWindow.Add(data, currentInputType);
                        else
                            Clear_Calc_Button_Click(null, null);
                    }

                    // here the user entered in a "weaker" operator via PEMDAS and there are no parans in the expression
                    // also validate the user didn't just enter in * then /
                    else if (currentInputType == TextWindow.InputType.OperatorDouble && _prevOperator != string.Empty && (CalcHelpers.compareOPs(data, _prevOperator) >= 0) && _numParans == 0)
                    {
                        // if it was a valid value, use it in the expression
                        if (_mainWindow.isValue() && recentWindow == _mainWindow)
                            _smallWindow.Add(_mainWindow.Get(), _mainWindow.GetLastInputType());

                        Equal_Button_Click(null, null);

                        _smallWindow.Add(data, currentInputType);
                    }

                    // just add the opeartor
                    else if (currentInputType == TextWindow.InputType.OperatorDouble)
                    {
                        // if it was a valid value, use it in the expression
                        if (_mainWindow.isValue() && recentWindow == _mainWindow)
                            _smallWindow.Add(_mainWindow.Get(), _mainWindow.GetLastInputType());

                        _smallWindow.Add(data, currentInputType);
                    }
                }

                // user entered in a single operator like n! or log
                else if (currentInputType == TextWindow.InputType.OperatorSingle)
                {
                    // if it was a valid value, use it in the expression
                    if (_mainWindow.isValue() && recentWindow == _mainWindow)
                        _smallWindow.Add(_mainWindow.Get(), _mainWindow.GetLastInputType());

                    _smallWindow.Add(data, currentInputType);

                    string inputData = _smallWindow.GetLast().inputData;
                    string tempExpression = _eval.Execute(inputData).ToString();

                    // here we set the input type to Null so that the user can't just continue to enter in additional values
                    _mainWindow.Set(tempExpression, TextWindow.InputType.NULL, false);
                    _smallWindow.MakeRecent();
                }

                #endregion

                #region Finishing Functions

                // incase the user types in zeros in the front of the expression
                CalcHelpers.cleanUpZeros(_mainWindow);

                // finally give focus back to the equals key
                CalcHelpers.equalsKeyFocus(currentView.GetEqualsKey());

                // remember the last input
                _prevInputType = currentInputType;

                #endregion
            }
            catch { exception = true; }
            if (exception)
            {
                initCalcError();
            }
        }

        #endregion

        #region Metro Memory Buttons

        public void Remove_Memory_Click(MemoryItem memItem)
        {
            try
            {
                // if this is last item, clear everything
                // TODO not sure why i have to do this instead 
                // of whats below... that just thows an exception
                if (_memoryItems.Count == 1)
                {
                    Clear_All_Memory_Click(null, null);
                    return;
                }

                // remove this specific item
                _memoryItems.Remove(memItem);
                SaveState();
            }

            catch { }

            // reset the equals key focus when we change view
            CalcHelpers.equalsKeyFocus(currentView.GetEqualsKey());
        }

        public void Use_Memory_Click(MemoryItem memItem)
        {
            try
            {
                // if we're not in scientific or stat mode you can't add doubles
                if (isProgrammer() && CalcHelpers.isDouble(memItem.Value))
                    return;

                // first clear whats already in the viewing mode (including small
                // window too)
                Clear_Calc_Button_Click_Memory_Use();

                // if the mainwindow is in programmer mode setup the memory so 
                // it comes back to the right state
                _mainWindow.Set(memItem.Value, TextWindow.InputType.Int, true);

                // close appbars
                CloseAppBars();
            }
            catch { initCalcError(); }

            // reset the equals key focus when we change view
            CalcHelpers.equalsKeyFocus(currentView.GetEqualsKey());

        }



        public void Add_Memory_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string mainWindowDecText = _mainWindow.GetWindowText();


                // first check this exact value doesn't already exist
                foreach (MemoryItem item in _memoryItems)
                {
                    // if we find a match
                    if (item.Text.CompareTo(mainWindowDecText) == 0)
                    {
                        // TODO ask the matching on to highlight
                        ScrollToView(item);
                        item.HighLightItem();
                        return;
                    }
                }

                // create a new item
                MemoryItem memItem = new MemoryItem();
                memItem.InitializeWindow(this);
                memItem.Value = _mainWindow.GetDecValueForWindow();
                memItem.Text = _mainWindow.GetWindowText();

                // easy if we're in scientific or statistical
                if (!isProgrammer())
                {
                    memItem.isProgrammer = false;
                    memItem.Type = Calc_MainPage.CalcStateValues.Dec;
                }

                // otherwise set additional parameters
                else
                {
                    memItem.isProgrammer = true;
                    memItem.Type = (currentView as ProgrammerView).calcState;
                }

                Add_Memory_Click(memItem);
                SaveState();
            }
            catch { }

            _mainWindow.clearOnNextUse = true;

            // reset the equals key focus when we change view
            CalcHelpers.equalsKeyFocus(currentView.GetEqualsKey());
        }

        public async void Add_Memory_Click(MemoryItem memItem)
        {
            // if we have too much stuff in memory start killing off
            if (_memoryItems.Count > MAX_MEMORY_ITEMS)
            {
                MemoryItem toRemove = _memoryItems.First();
                _memoryItems.Remove(toRemove);
            }

            // insert item
            _memoryItems.Add(memItem);

            // make sure we make the last item the most recent
            ScrollToView(memItem);

            await Task.Delay(APP_BAR_DELAY_TIME_SHORT);
            MemoryAppBar.IsOpen = false;

            //_mainWindow.clearOnNextUse = true;
        }

        private async void ScrollToView(MemoryItem memItem)
        {
            MemoryGrid.UpdateLayout();
            await Task.Delay(APP_BAR_DELAY_TIME_SHORT);
            MemoryGrid.ScrollIntoView(memItem);
        }

        public void Clear_All_Memory_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // clear the list
                _memoryItems.Clear();

                ClearState();

                CloseAppBars();
            }
            catch { }

            // reset the equals key focus when we change view
            CalcHelpers.equalsKeyFocus(currentView.GetEqualsKey());
        }

        private bool lostFocus;
        public async void Metro_Memory_Add_Button_Click(object sender, RoutedEventArgs e)
        {
            // open and close the appbar as feedback
            MemoryAppBar.IsOpen = true;

            Add_Memory_Click(null, null);

            // we'll use this to determine if the app bar closed on its own
            lostFocus = false;

            await Task.Delay(APP_BAR_DELAY_TIME_LONG);

            if (!lostFocus)
                MemoryAppBar.IsOpen = false;

            // reset the equals key focus when we change view
            CalcHelpers.equalsKeyFocus(currentView.GetEqualsKey());
        }

        private void MemoryAppBar_LostFocus(object sender, RoutedEventArgs e)
        {
            lostFocus = true;
        }

        public void Metro_Memory_Restore_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Use_Memory_Click(_memoryItems.Last());
            }
            catch { }
        }

        #endregion

        #region State

        private void SaveState()
        {
            Utilities.StateManager.SaveRoamingState(Utilities.App.Apps.Calculator, Utilities.StateManager.CALC_MEMORY_KEY, _memoryItems);
        }

        private void ClearState()
        {
            Utilities.StateManager.ClearRoamingState(Utilities.App.Apps.Calculator, Utilities.StateManager.CALC_MEMORY_KEY);
        }

        private void LoadState()
        {
            try
            {
                bool success = true;
                object retVal = Utilities.StateManager.LoadRoamingState(Utilities.App.Apps.Calculator, Utilities.StateManager.CALC_MEMORY_KEY, ref success, this);

                List<MemoryItem> list = retVal as List<MemoryItem>;

                foreach (MemoryItem mi in list)
                {
                    Add_Memory_Click(mi);
                }
            }
            catch { }
        }

        private void State_DataChanged(Windows.Storage.ApplicationData sender, object args)
        {
            //throw new NotImplementedException();
        }

        #endregion

        #region Key Input

        private async void OutputWindowMain_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            // Since command delegates are unique, no need to specify command Ids.
            var menu = new PopupMenu();
            menu.Commands.Add(new UICommand("Cut", (command) =>
            {
                CutCommand();
            }));
            menu.Commands.Add(new UICommand("Copy", (command) =>
            {
                CopyCommand();
            }));
            menu.Commands.Add(new UICommand("Paste", (command) =>
            {
                PasteCommand();
            }));

            var chosenCommand = await menu.ShowForSelectionAsync(GetElementRect((FrameworkElement)sender));
        }

        public static Rect GetElementRect(FrameworkElement element)
        {
            GeneralTransform buttonTransform = element.TransformToVisual(null);
            Point point = buttonTransform.TransformPoint(new Point());
            return new Rect(point, new Size(element.ActualWidth, element.ActualHeight));
        }


        private void CharacterReceived(CoreWindow sender, CharacterReceivedEventArgs args)
        {
            uint keyCode = args.KeyCode;

            int tempParse = -1;

            // if the value was 0-9
            if (int.TryParse(((char)keyCode).ToString(), out tempParse) && tempParse >= 0 && tempParse <= 9)
            {
                args.Handled = true;
                Value_Button_Click(((char)keyCode).ToString());
            }

            // if the value was a-f and we're in programmer hex mode
            else if (isProgrammer() && (currentView as ProgrammerView).calcState == Calc_MainPage.CalcStateValues.Hex && (char)keyCode >= 'a' && (char)keyCode <= 'f')
            {
                args.Handled = true;
                Value_Button_Click(((char)keyCode).ToString());
            }

            // if the value was + - * /
            else if (keyCode == '+' || keyCode == '-' || keyCode == '*' || keyCode == '/')
            {
                args.Handled = true;
                Operator_Double_Button_Click(((char)keyCode).ToString());
            }

            // if the value was ( )
            else if (keyCode == ')' || keyCode == '(')
            {
                args.Handled = true;
                Paran_Button_Click(((char)keyCode).ToString());
            }

            // if the value was .
            else if (keyCode == '.')
            {
                args.Handled = true;
                Dot_Button_Click(".");
            }

            // if the value was .
            else if (keyCode == '\b')
            {
                args.Handled = true;
                Delete_Last_Button_Click(null, null);
            }

            // if the value was enter
            else if (keyCode == '\r')
            {
                args.Handled = true;
                Equal_Button_Click(new object(), null);
            }

            // ctrl-c
            // if the text is '' yes there is actually stuff in there it's not empty
            // its the character 3
            else if (keyCode == '')
            {
                args.Handled = true;
                CopyCommand();
            }

            // ctrl-v
            // if the text is '' yes there is actually stuff in there it's not empty
            else if (keyCode == '')
            {
                args.Handled = true;
                PasteCommand();
            }

            // ctrl-x
            // cut command
            else if (keyCode == 24)
            {
                args.Handled = true;
                CutCommand();
            }

            // esc
            // if the text is '' Yes there is actually text in there
            else if (keyCode == '')
            {
                args.Handled = true;
                Clear_Calc_Button_Click(null, null);
            }

            if (args.Handled)
            {
                CloseAppBars();

                // reset the equals key focus when we change view
                CalcHelpers.equalsKeyFocus(currentView.GetEqualsKey());
            }
        }

        private void CopyCommand()
        {
            DataPackage dp = new DataPackage();
            dp.SetText(_mainWindow.Get());

            Clipboard.SetContent(dp);
        }

        private async void PasteCommand()
        {
            DataPackageView dpv = Clipboard.GetContent();

            Clear_Calc_Button_Click(null, null);

            string newText = (await dpv.GetTextAsync()).ToLower();

            insertToWindow(newText);
        }

        private void CutCommand()
        {
            DataPackage dp = new DataPackage();
            dp.SetText(_mainWindow.Get());

            Clipboard.SetContent(dp);

            Clear_Calc_Button_Click(null, null);
        }

        #endregion

        #region Values/Symbols/Dot

        public void Dot_Button_Click(object sender, RoutedEventArgs e)
        {
            string data = CalcHelpers.GetTextFromObject(sender);

            Dot_Button_Click(data);
        }

        private void Dot_Button_Click(string data)
        {
            // can't enter more than one dot if we're alredy a double
            if (_mainWindow.isDouble())
                return;

            addContentToExpression(data, TextWindow.InputType.Double);
        }

        public void Symbol_Button_Click(object sender, RoutedEventArgs e)
        {
            string data = CalcHelpers.GetTextFromObject(sender);

            if (data == "π")
                data = Math.PI.ToString();
            else if (data == "e")
                data = Math.E.ToString();

            addContentToExpression(data, TextWindow.InputType.Symbol);
        }

        // add a value to the expression string
        public void Value_Button_Click(object sender, RoutedEventArgs e)
        {
            string data = CalcHelpers.GetTextFromObject(sender);

            Value_Button_Click(data);
        }

        private void Value_Button_Click(string data)
        {
            addContentToExpression(data, TextWindow.InputType.Int);
        }

        // adding a new random value to the mix
        public void Rand_Button_Click(object sender, RoutedEventArgs e)
        {
            Random r = new Random();
            double d = r.NextDouble();

            addContentToExpression(d.ToString(), TextWindow.InputType.Rand);
        }

        #endregion

        #region Calc Settings

        public bool isScientific()
        {
            return this.calcState == App.Apps.Scientific;
        }

        public bool isProgrammer()
        {
            return this.calcState == App.Apps.Programmer;
        }

        public bool isStatistics()
        {
            return this.calcState == App.Apps.Statistics;
        }

        #region Programmer Calc Settings

        public void HexSettings_Click(object sender, RoutedEventArgs e)
        {
            ProgrammerSettings_Click(Calc_MainPage.CalcStateValues.Hex);
        }
        public void DecSettings_Click(object sender, RoutedEventArgs e)
        {
            ProgrammerSettings_Click(Calc_MainPage.CalcStateValues.Dec);
        }
        public void OctSettings_Click(object sender, RoutedEventArgs e)
        {
            ProgrammerSettings_Click(Calc_MainPage.CalcStateValues.Oct);
        }
        public void BinSettings_Click(object sender, RoutedEventArgs e)
        {
            ProgrammerSettings_Click(Calc_MainPage.CalcStateValues.Bin);
        }

        public void ProgrammerSettings_Click(Calc_MainPage.CalcStateValues mode)
        {
            // set the calcstate
            CalcState = mode.ToString();

            // set the state
            (currentView as ProgrammerView).calcState = mode;
            (currentView as ProgrammerView).ProgrammerSettings_Click(mode);

            // redraw whatever is on the mainWindow
            _mainWindow.Redraw();
        }


        #endregion

        #region Scientific / ScientificPortrait Calc Settings

        private void ScientificSettings_Click(Calc_MainPage.CalcStateValues mode)
        {
            // set the calcstate
            CalcState = mode.ToString();

            (currentView as ScientificView).scientificCalcState = mode;
        }

        #endregion

        #region Calc State Window

        private string CalcState
        {
            set
            {
                // if we're in the programmer calculator case
                if (value == Calc_MainPage.CalcStateValues.Hex.ToString() ||
                    value == Calc_MainPage.CalcStateValues.Dec.ToString() ||
                    value == Calc_MainPage.CalcStateValues.Oct.ToString() ||
                    value == Calc_MainPage.CalcStateValues.Bin.ToString())
                {

                    if (value == CalcStateValues.Hex.ToString())
                    {
                        CalcStateWindow.Text = Calc_MainPage.CalcStateValues.Hex.ToString();
                        calcStateValue = CalcStateValues.Hex;
                    }
                    else if (value == CalcStateValues.Dec.ToString())
                    {
                        CalcStateWindow.Text = Calc_MainPage.CalcStateValues.Dec.ToString();
                        calcStateValue = CalcStateValues.Dec;
                    }
                    else if (value == CalcStateValues.Oct.ToString())
                    {
                        CalcStateWindow.Text = Calc_MainPage.CalcStateValues.Oct.ToString();
                        calcStateValue = CalcStateValues.Oct;
                    }
                    else if (value == CalcStateValues.Bin.ToString())
                    {
                        CalcStateWindow.Text = Calc_MainPage.CalcStateValues.Bin.ToString();
                        calcStateValue = CalcStateValues.Bin;
                    }

                    // get the first character
                    if (App.isSnapped())
                        CalcStateWindow.Text = CalcStateWindow.Text.Substring(0, 1);
                }

                // if we're in the scientific calculator case
                else if (value == Calc_MainPage.CalcStateValues.Deg.ToString() ||
                    value == Calc_MainPage.CalcStateValues.Rad.ToString())
                {
                    if (value == CalcStateValues.Deg.ToString())
                    {
                        CalcStateWindow.Text = Calc_MainPage.CalcStateValues.Deg.ToString();
                        calcStateValue = CalcStateValues.Deg;
                    }
                    else if (value == CalcStateValues.Rad.ToString())
                    {
                        CalcStateWindow.Text = Calc_MainPage.CalcStateValues.Rad.ToString();
                        calcStateValue = CalcStateValues.Rad;
                    }

                    // get the first character
                    if (App.isSnapped())
                        CalcStateWindow.Text = CalcStateWindow.Text.Substring(0, 1);
                }

                // in the statistical calculator case
                else
                {
                    CalcStateWindow.Text = value;
                    calcStateValue = CalcStateValues.Dec;

                    // get the last character
                    if (App.isSnapped())
                        CalcStateWindow.Text = CalcStateWindow.Text[CalcStateWindow.Text.Length - 1].ToString();
                }
            }
        }


        private void CalcStateWindowTapped(object sender, TappedRoutedEventArgs e)
        {
            // in scientific mode
            if (calcState == App.Apps.Scientific)
            {
                if (calcStateValue == CalcStateValues.Deg)
                    ScientificSettings_Click(CalcStateValues.Rad);
                else
                    ScientificSettings_Click(CalcStateValues.Deg);
            }
            else if (calcState == App.Apps.Programmer)
            {
                if (calcStateValue == CalcStateValues.Hex)
                    ProgrammerSettings_Click(CalcStateValues.Dec);
                else if (calcStateValue == CalcStateValues.Dec)
                    ProgrammerSettings_Click(CalcStateValues.Oct);
                else if (calcStateValue == CalcStateValues.Oct)
                    ProgrammerSettings_Click(CalcStateValues.Bin);
                else
                    ProgrammerSettings_Click(CalcStateValues.Hex);
            }
        }

        #endregion

        #region Statistics Calc Settings

        public void Stat_Add_Button_Click(string data)
        {
            // set the main window to clear
            _mainWindow.clearOnNextUse = true;
            CalcState = "Cnt=" + data;
        }

        public void Stat_Init_Button_Click()
        {
            Stat_Add_Button_Click("0");
        }

        public string GetMainText()
        {
            return _mainWindow.Get();
        }

        public void SetMainText(string data)
        {
            initCalc();

            if (CalcHelpers.isDouble(data))
                _mainWindow.Set(data, TextWindow.InputType.Double, true);
            else
                _mainWindow.Set(data, TextWindow.InputType.Int, true);
        }

        #endregion

        #region Programmer Calc Bitness

        public void Change_Bitness_Click(CalcViews.Programmer.ProgrammerCalcBits bitness)
        {
            // we shouldn't be here if we're not in programmer mode
            if (!isProgrammer())
            {
                initCalcError();
                return;
            }

            CalcViews.Programmer.ProgrammerCalcBits prevBits = (currentView as ProgrammerView).currentBits;

            (currentView as ProgrammerView).currentBits = bitness;

            _mainWindow.ChangeBitness(prevBits);

            // redraw whatever is on the mainWindow
            _mainWindow.Redraw();
        }

        #endregion

        #endregion

        #region Negation

        public void Negate_Button_Click(object sender, RoutedEventArgs e)
        {
            try { recentWindow.Negate(); }
            catch { initCalcError(); }

            // reset the equals key focus when we change view
            CalcHelpers.equalsKeyFocus(currentView.GetEqualsKey());
        }

        #endregion

        #region Paran

        // add a value to the expression string
        public void Paran_Button_Click(object sender, RoutedEventArgs e)
        {
            string data = CalcHelpers.GetTextFromObject(sender);

            Paran_Button_Click(data);

        }

        private void Paran_Button_Click(string data)
        {
            // open paran
            if (data == OPEN_PARAN)
            {
                _numParans++;
            }

            // close paran (and there are more than 1 open paran)
            else if (data == CLOSE_PARAN && _numParans > 0)
            {
                // if the user entered an operator they shouldn't be able to close the paran
                if (recentWindow.GetLastInputType() == TextWindow.InputType.OperatorDouble && recentWindow == _smallWindow)
                    return;

                // if the user entered in a double like 55. they shouldn't be able to close the paran
                else if (_mainWindow.GetLastInputType() == TextWindow.InputType.Double && _mainWindow.GetLast().inputData.EndsWith(".") && recentWindow == _mainWindow)
                    return;

                _numParans--;
            }

            // otherwise they tried to close a paran without opening one
            else
                return;

            addContentToExpression(data, TextWindow.InputType.Paran);
        }

        #endregion

        #region Operators

        // add a operator to the expression string
        public void Operator_Single_Button_Click(object sender, RoutedEventArgs e)
        {
            string data = CalcHelpers.GetTextFromObject(sender);

            data = determineRealOp(data);

            if (isProgrammer() && data == "RGB")
            {
                try
                {
                    string hexCode = CalcHelpers.dec2Hex(long.Parse(_mainWindow.Get()).ToString(), (currentView as ProgrammerView).currentBits);

                    // should always be 6 characters long
                    if (hexCode.Length != 6)
                        throw new Exception();

                    string R = hexCode.Substring(0, 2);
                    string G = hexCode.Substring(2, 2);
                    string B = hexCode.Substring(4, 2);

                    _mainWindow.SetNoTranslation(
                        CalcHelpers.hex2Dec(R, (currentView as ProgrammerView).currentBits) + "-" +
                        CalcHelpers.hex2Dec(G, (currentView as ProgrammerView).currentBits) + "-" +
                        CalcHelpers.hex2Dec(B, (currentView as ProgrammerView).currentBits)
                        );

                }
                catch { initCalcError(); }
            }
            else
                addContentToExpression(data, TextWindow.InputType.OperatorSingle);
        }

        private string determineRealOp(string input)
        {
            string data = input;

            // Single operator
            if (data == "1/x")
                data = "inv";
            else if (data == "x²")
                data = "powtwo";
            else if (data == "x³")
                data = "powthree";
            else if (data == "%")
                data = "percent";
            else if (data == "eˣ")
                data = "epow";
            else if (data == "sinˉ¹")
                data = "asin";
            else if (data == "cosˉ¹")
                data = "acos";
            else if (data == "tanˉ¹")
                data = "atan";
            else if (data == "sinhˉ¹")
                data = "asinh";
            else if (data == "coshˉ¹")
                data = "acosh";
            else if (data == "tanhˉ¹")
                data = "atanh";
            else if (data == "log₂")
                data = "logtwo";
            else if (data == "2ˣ")
                data = "twopow";
            else if (data == "z→Q")
                data = "Q";
            else if (data == "Q→z")
                data = "Z";

            // Double Operator
            else if (data == "ₙPₖ")
                data = "P";
            else if (data == "ₙCₖ")
                data = "C";
            else if (data == "yˣ")
                data = "^";
            else if (data == "And")
                data = "&";
            else if (data == "Or")
                data = "|";
            else if (data == "Xor")
                data = "⊕";
            else if (data == "Lsh")
                data = "<";
            else if (data == "Rsh")
                data = ">";
            else if (data == "Mod")
                data = "%";

            return data;
        }

        // add a operator to the expression string
        public void Operator_Double_Button_Click(object sender, RoutedEventArgs e)
        {
            string data = CalcHelpers.GetTextFromObject(sender);

            data = determineRealOp(data);

            Operator_Double_Button_Click(data);
        }

        private void Operator_Double_Button_Click(string data)
        {
            addContentToExpression(data, TextWindow.InputType.OperatorDouble);

            _prevOperator = data;
        }

        #endregion

        #region Delete + Clear

        public void Delete_Last_Button_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.RemoveLast();

            // reset the equals key focus when we change view
            CalcHelpers.equalsKeyFocus(currentView.GetEqualsKey());
        }

        public void Clear_Calc_Button_Click(object sender, RoutedEventArgs e)
        {
            _smallWindow.Set(string.Empty, TextWindow.InputType.NULL, true);
            _mainWindow.Set("0", TextWindow.InputType.Int, false);
            _numParans = 0;
            _prevOperator = string.Empty;

            // reset the equals key focus when we change view
            CalcHelpers.equalsKeyFocus(currentView.GetEqualsKey());
        }

        // clears all but the small window
        public void Clear_Calc_Button_Click_Memory_Use()
        {
            _mainWindow.Set("0", TextWindow.InputType.Int, false);
            _numParans = 0;
            _prevOperator = string.Empty;

            // reset the equals key focus when we change view
            CalcHelpers.equalsKeyFocus(currentView.GetEqualsKey());
        }

        public void Clear_Error_Button_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.Set("0", TextWindow.InputType.Int, false);
            _numParans = 0;

            // since we just cleared main, small is now the most recent
            _smallWindow.MakeRecent();

            // reset the equals key focus when we change view
            CalcHelpers.equalsKeyFocus(currentView.GetEqualsKey());
        }

        public void initCalcError()
        {
            _smallWindow.Set(string.Empty, TextWindow.InputType.NULL, true);
            _mainWindow.Set("Err", TextWindow.InputType.NULL, true);
            _numParans = 0;
            _prevOperator = string.Empty;

            // reset the equals key focus when we change view
            CalcHelpers.equalsKeyFocus(currentView.GetEqualsKey());
        }

        private void initCalc()
        {
            Clear_Calc_Button_Click(null, null);

            // reset the equals key focus when we change view
            CalcHelpers.equalsKeyFocus(currentView.GetEqualsKey());
        }

        #endregion

        #region Equals

        public void Equal_Button_Click(object sender, RoutedEventArgs e, bool missingParan = false)
        {
            bool exception = false;

            try
            {
                // get the expression
                string tempExpression = _smallWindow.Get();

                string smallExp = string.Empty;

                // if this was the user that hit equal this means we have an expression 
                if (sender != null && _mainWindow.isValue() && recentWindow == _mainWindow)
                    tempExpression += _mainWindow.Get();

                smallExp = tempExpression;

                // for scenarios where the user is still typing in their expression
                string extraPrans = string.Empty;
                if (missingParan)
                    for (int i = 0; i < _numParans; i++)
                        extraPrans += ")";

                // get the evaluation (adding in the extra parans)
                tempExpression = _eval.Execute(tempExpression + extraPrans).ToString();

                _mainWindow.Set(tempExpression, tempExpression.Contains(".") ? TextWindow.InputType.Double : TextWindow.InputType.Int, true);

                // if the user hit enter update the small window 
                if (sender != null)
                {
                    _smallWindow.Set(string.Empty, TextWindow.InputType.NULL, true);
                    _mainWindow.MakeRecent();

                    // clear the calc
                    _numParans = 0;
                    _prevOperator = string.Empty;
                }
                else
                    _smallWindow.MakeRecent();
            }
            catch { exception = true; }
            if (exception)
            {
                initCalcError();
            }

            // reset the equals key focus when we change view
            CalcHelpers.equalsKeyFocus(currentView.GetEqualsKey());
        }

        #endregion

        #region Search

        private void initSearch()
        {
            //_searchPane = SearchPane.GetForCurrentView();

            //// When the user hits enter after they search
            //_searchPane.QuerySubmitted += searchPane_QuerySubmitted;

            //// While the user is typing this gets called to update the list
            //_searchPane.SuggestionsRequested += Utilities.MainPage.searchPane_SuggestionsRequested;
        }

        public void UnregisterSearch()
        {
            //_searchPane = SearchPane.GetForCurrentView();

            //_searchPane.QuerySubmitted -= searchPane_QuerySubmitted;
            //_searchPane.SuggestionsRequested -= Utilities.MainPage.searchPane_SuggestionsRequested;
        }

        public static string searchPane_SuggestionsRequested(string queryText)
        {
            // try to see if the user entered in something valid
            try
            {
                // get the expression 
                Calc_MainPage Page = new Calc_MainPage();
                Page.InitEval();
                return Page.Eval.Execute(queryText).ToString();
            }
            catch { }

            return string.Empty;
        }

        //TBD:
        //private void searchPane_QuerySubmitted(SearchPane sender, SearchPaneQuerySubmittedEventArgs args)
        //{
        //    // assume they entered in something valid
        //    insertToWindow(args.QueryText);
        //}

        public void insertToWindow(string data)
        {
            if (data == null ||
                data == string.Empty)
                return;

            long valueL = 0;
            double valueD = 0;
            bool exception = false;

            if (long.TryParse(data, out valueL))
                _mainWindow.Set(data, TextWindow.InputType.Int, false);
            else if (double.TryParse(data, out valueD))
                _mainWindow.Set(data, TextWindow.InputType.Double, false);
            else
            {
                try
                {
                    string tempExpression = _eval.Execute(data).ToString();

                    // here we set the input type to Null so that the user can't just continue to enter in additional values
                    _smallWindow.Set(data, TextWindow.InputType.NULL, true);
                    _mainWindow.Set(tempExpression, tempExpression.Contains(".") ? TextWindow.InputType.Double : TextWindow.InputType.Int, false);
                }
                catch { exception = true; }
                if (exception)
                {
                    initCalcError();
                }
            }

            _mainWindow.MakeRecent();
        }

        #endregion

        #region Process Text

        // Implement expression symbols
        private void ProcessSymbol(object sender, SymbolEventArgs e)
        {
            if (String.Compare(e.Name, "π") == 0)
            {
                e.Result = Math.PI;
            }
            else if (String.Compare(e.Name, "e") == 0)
            {
                e.Result = Math.E;
            }
            else if (String.Compare(e.Name.ToLower(), "infinity") == 0)
            {
                e.Result = double.PositiveInfinity;
            }
            else if (String.Compare(e.Name.ToLower(), "nan") == 0)
            {
                e.Result = double.NaN;
            }
            // Unknown symbol name
            else e.Status = SymbolStatus.UndefinedSymbol;
        }

        // Implement expression functions
        private void ProcessFunction(object sender, FunctionEventArgs e)
        {
            #region Standard

            if (String.Compare(e.Name, "abs") == 0)
            {
                if (e.Parameters.Count == 1)
                    e.Result = Math.Abs(e.Parameters[0]).ToString();
                else
                    e.Status = FunctionStatus.WrongParameterCount;
            }
            //else if (String.Compare(e.Name, "pow") == 0)
            //{
            //    if (e.Parameters.Count == 2)
            //        e.Result = Math.Pow(e.Parameters[0], e.Parameters[1]).ToString();
            //    else
            //        e.Status = FunctionStatus.WrongParameterCount;
            //}
            else if (String.Compare(e.Name, "epow") == 0)
            {
                if (e.Parameters.Count == 1)
                    e.Result = Math.Pow(Math.E, e.Parameters[0]).ToString();
                else
                    e.Status = FunctionStatus.WrongParameterCount;
            }
            else if (String.Compare(e.Name, "round") == 0)
            {
                if (e.Parameters.Count == 1)
                    e.Result = Math.Round(e.Parameters[0]).ToString();
                else
                    e.Status = FunctionStatus.WrongParameterCount;
            }
            else if (String.Compare(e.Name, "√") == 0)
            {
                if (e.Parameters.Count == 1)
                    e.Result = Math.Sqrt(e.Parameters[0]).ToString();
                else
                    e.Status = FunctionStatus.WrongParameterCount;
            }

            else if (String.Compare(e.Name, "ln") == 0)
            {
                if (e.Parameters.Count == 1)
                    e.Result = Math.Log(e.Parameters[0], 2).ToString();
                else
                    e.Status = FunctionStatus.WrongParameterCount;
            }
            else if (String.Compare(e.Name, "log") == 0)
            {
                if (e.Parameters.Count == 1)
                    e.Result = Math.Log(e.Parameters[0]).ToString();
                else
                    e.Status = FunctionStatus.WrongParameterCount;
            }
            else if (String.Compare(e.Name, "logtwo") == 0)
            {
                if (e.Parameters.Count == 1)
                    e.Result = Math.Log(e.Parameters[0], 2).ToString();
                else
                    e.Status = FunctionStatus.WrongParameterCount;
            }
            else if (String.Compare(e.Name, "inv") == 0)
            {
                if (e.Parameters.Count == 1)
                {
                    if (e.Parameters[0] == 0)
                    {
                        throw new DivideByZeroException();
                    }
                    e.Result = (1.0 / e.Parameters[0]).ToString();
                }
                else
                    e.Status = FunctionStatus.WrongParameterCount;
            }
            else if (String.Compare(e.Name, "percent") == 0)
            {
                if (e.Parameters.Count == 1)
                {
                    e.Result = (e.Parameters[0] / 100).ToString();
                }

                else
                    e.Status = FunctionStatus.WrongParameterCount;
            }
            else if (String.Compare(e.Name, "powtwo") == 0)
            {
                if (e.Parameters.Count == 1)
                    e.Result = (e.Parameters[0] * e.Parameters[0]).ToString();
                else
                    e.Status = FunctionStatus.WrongParameterCount;
            }
            else if (String.Compare(e.Name, "twopow") == 0)
            {
                if (e.Parameters.Count == 1)
                    e.Result = (Math.Pow(2, e.Parameters[0])).ToString();
                else
                    e.Status = FunctionStatus.WrongParameterCount;
            }
            else if (String.Compare(e.Name, "powthree") == 0)
            {
                if (e.Parameters.Count == 1)
                    e.Result = (e.Parameters[0] * e.Parameters[0] * e.Parameters[0]).ToString();
                else
                    e.Status = FunctionStatus.WrongParameterCount;
            }
            else if (String.Compare(e.Name, "x!") == 0)
            {
                if (e.Parameters.Count == 1)
                {
                    try
                    {
                        e.Result = factorial(e.Parameters[0]);
                    }
                    catch
                    {
                        e.Status = FunctionStatus.WrongParameterCount;
                    }
                }
                else
                    e.Status = FunctionStatus.WrongParameterCount;
            }

            #endregion

            #region Statistical

            else if (String.Compare(e.Name, "Z") == 0)
            {
                if (e.Parameters.Count == 1)
                {
                    if (e.Parameters[0] < 0 || e.Parameters[0] > 1)
                    {
                        e.Status = FunctionStatus.WrongParameterCount;
                    }
                    else
                    {
                        e.Result = Math.Abs(critz(e.Parameters[0])).ToString();
                    }
                }
                else
                    e.Status = FunctionStatus.WrongParameterCount;
            }

            else if (String.Compare(e.Name, "Q") == 0)
            {
                if (e.Parameters.Count == 1)
                {
                    if (Math.Abs(e.Parameters[0]) > Z_MAX)
                    {
                        //alert("Error: z value must be between -6 and 6.\nValues outside this range have probabilities\nwhich exceed the precision of calculation used in this page.");
                        e.Status = FunctionStatus.WrongParameterCount;
                    }
                    else
                    {
                        double qz = 1 - poz(Math.Abs(e.Parameters[0]));
                        e.Result = qz.ToString();
                    }
                }
                else
                    e.Status = FunctionStatus.WrongParameterCount;
            }

            #endregion

            #region Logic

            else if (String.Compare(e.Name, "Not") == 0)
            {
                if (e.Parameters.Count == 1)
                {
                    List<int> binRep = _mainWindow.GetBinary();

                    // next we'll do inversion
                    List<int> binNot = new List<int>();
                    foreach (int b in binRep)
                    {
                        if (b == 1)
                            binNot.Add(0);
                        else if (b == 0)
                            binNot.Add(1);
                        else
                        {
                            e.Result = "";
                            break;
                        }
                    }

                    e.Result = _mainWindow.GetDecimal(binNot);
                }
                else
                    e.Status = FunctionStatus.WrongParameterCount;
            }

            else if (String.Compare(e.Name, "RoL") == 0)
            {
                if (e.Parameters.Count == 1)
                {
                    e.Result = ((long)e.Parameters[0] << 1).ToString();
                }
                else
                    e.Status = FunctionStatus.WrongParameterCount;
            }

            else if (String.Compare(e.Name, "RoR") == 0)
            {
                if (e.Parameters.Count == 1)
                {
                    e.Result = ((long)e.Parameters[0] >> 1).ToString();
                }
                else
                    e.Status = FunctionStatus.WrongParameterCount;
            }

            #endregion

            #region Cos

            else if (String.Compare(e.Name, "cos") == 0)
            {
                if (e.Parameters.Count == 1)
                {
                    try
                    {
                        // choose for degrees vs radians
                        if (this.calcState == App.Apps.Scientific &&
                            (currentView as ScientificView).scientificCalcState == Calc_MainPage.CalcStateValues.Deg)
                            e.Result = Math.Cos(CalcHelpers.convertToRadians(e.Parameters[0])).ToString();
                        else
                            e.Result = Math.Cos(e.Parameters[0]).ToString();
                    }
                    catch
                    {
                        e.Result = Math.Cos(CalcHelpers.convertToRadians(e.Parameters[0])).ToString();
                    }
                }
                else
                    e.Status = FunctionStatus.WrongParameterCount;
            }
            else if (String.Compare(e.Name, "cosh") == 0)
            {
                // No difference for deg vs rad
                if (e.Parameters.Count == 1)
                    e.Result = Math.Cosh(e.Parameters[0]).ToString();
                else
                    e.Status = FunctionStatus.WrongParameterCount;
            }
            else if (String.Compare(e.Name, "acos") == 0)
            {
                if (e.Parameters.Count == 1)
                {
                    try
                    {
                        // choose for degrees vs radians
                        if (this.calcState == App.Apps.Scientific &&
                            (currentView as ScientificView).scientificCalcState == Calc_MainPage.CalcStateValues.Deg)
                            e.Result = CalcHelpers.convertToDegrees(Math.Acos(e.Parameters[0])).ToString();
                        else
                            e.Result = Math.Acos(e.Parameters[0]).ToString();
                    }
                    catch
                    {
                        e.Result = CalcHelpers.convertToDegrees(Math.Acos(e.Parameters[0])).ToString();
                    }
                }
                else
                    e.Status = FunctionStatus.WrongParameterCount;
            }
            else if (String.Compare(e.Name, "acosh") == 0)
            {
                // No difference for deg vs rad
                if (e.Parameters.Count == 1)
                    e.Result = Math.Log(e.Parameters[0] + Math.Sqrt(e.Parameters[0] * e.Parameters[0] - 1)).ToString();
                else
                    e.Status = FunctionStatus.WrongParameterCount;
            }

            #endregion

            #region Sin

            else if (String.Compare(e.Name, "sin") == 0)
            {
                if (e.Parameters.Count == 1)
                {
                    try
                    {
                        if (this.calcState == App.Apps.Scientific &&
                            (currentView as ScientificView).scientificCalcState == Calc_MainPage.CalcStateValues.Deg)
                            e.Result = Math.Sin(CalcHelpers.convertToRadians(e.Parameters[0])).ToString();
                        else
                            e.Result = Math.Sin(e.Parameters[0]).ToString();
                    }
                    catch
                    {
                        e.Result = Math.Sin(CalcHelpers.convertToRadians(e.Parameters[0])).ToString();
                    }
                }
                else
                    e.Status = FunctionStatus.WrongParameterCount;
            }
            else if (String.Compare(e.Name, "sinh") == 0)
            {
                if (e.Parameters.Count == 1)
                    e.Result = Math.Sinh(e.Parameters[0]).ToString();
                else
                    e.Status = FunctionStatus.WrongParameterCount;
            }
            else if (String.Compare(e.Name, "asin") == 0)
            {
                if (e.Parameters.Count == 1)
                {
                    try
                    {
                        if (this.calcState == App.Apps.Scientific &&
                            (currentView as ScientificView).scientificCalcState == Calc_MainPage.CalcStateValues.Deg)
                            e.Result = CalcHelpers.convertToDegrees(Math.Asin(e.Parameters[0])).ToString();
                        else
                            e.Result = Math.Asin(e.Parameters[0]).ToString();
                    }
                    catch
                    {
                        e.Result = CalcHelpers.convertToDegrees(Math.Asin(e.Parameters[0])).ToString();
                    }
                }
                else
                    e.Status = FunctionStatus.WrongParameterCount;
            }
            else if (String.Compare(e.Name, "asinh") == 0)
            {
                if (e.Parameters.Count == 1)
                {
                    double x;
                    int sign;
                    if (e.Parameters[0] == 0.0)
                    {
                        e.Result = e.Parameters[0].ToString();
                    }
                    else
                    {
                        if (e.Parameters[0] < 0.0)
                        {
                            sign = -1;
                            x = -e.Parameters[0];
                        }
                        else
                        {
                            sign = 1;
                            x = e.Parameters[0];
                        }
                        e.Result = (sign * Math.Log(x + Math.Sqrt(x * x + 1))).ToString();
                    }
                }
                else
                    e.Status = FunctionStatus.WrongParameterCount;
            }

            #endregion

            #region Tan

            else if (String.Compare(e.Name, "tan") == 0)
            {
                if (e.Parameters.Count == 1)
                {
                    try
                    {
                        if (this.calcState == App.Apps.Scientific &&
                            (currentView as ScientificView).scientificCalcState == Calc_MainPage.CalcStateValues.Deg)
                            e.Result = Math.Tan(CalcHelpers.convertToRadians(e.Parameters[0])).ToString();
                        else
                            e.Result = Math.Tan(e.Parameters[0]).ToString();
                    }
                    catch
                    {
                        e.Result = Math.Tan(CalcHelpers.convertToRadians(e.Parameters[0])).ToString();
                    }
                }
                else
                    e.Status = FunctionStatus.WrongParameterCount;
            }
            else if (String.Compare(e.Name, "tanh") == 0)
            {
                if (e.Parameters.Count == 1)
                    e.Result = Math.Tanh(e.Parameters[0]).ToString();
                else
                    e.Status = FunctionStatus.WrongParameterCount;
            }
            else if (String.Compare(e.Name, "atan") == 0)
            {
                if (e.Parameters.Count == 1)
                {
                    try
                    {
                        if (this.calcState == App.Apps.Scientific &&
                            (currentView as ScientificView).scientificCalcState == Calc_MainPage.CalcStateValues.Deg)
                            e.Result = CalcHelpers.convertToDegrees(Math.Atan(e.Parameters[0])).ToString();
                        else
                            e.Result = Math.Atan(e.Parameters[0]).ToString();
                    }
                    catch
                    {
                        e.Result = CalcHelpers.convertToDegrees(Math.Atan(e.Parameters[0])).ToString();
                    }
                }
                else
                    e.Status = FunctionStatus.WrongParameterCount;
            }
            else if (String.Compare(e.Name, "atanh") == 0)
            {
                if (e.Parameters.Count == 1)
                    e.Result = (Math.Log((1 + e.Parameters[0]) / (1 - e.Parameters[0])) * 0.5).ToString();
                else
                    e.Status = FunctionStatus.WrongParameterCount;
            }

            #endregion

            // Unknown function name
            else e.Status = FunctionStatus.UndefinedFunction;
        }

        #endregion

        #region Factorial

        public static string factorial(double x)
        {
            if (x > 5000)
            {
                throw new OverflowException();
            }
            double d = Math.Abs(x);
            if (Math.Floor(d) == d) return facInt((long)x);
            else return gamma(x + 1.0);
        }

        private static string facInt(long j)
        {
            BigInteger i = j;
            BigInteger d = 1;
            if (j < 0) i = Math.Abs(j);
            while (i > 1)
            {
                d *= i--;
            }
            if (j < 0) return (-d).ToString();
            else return d.ToString();
        }

        /// <summary>
        /// Returns the gamma function of the specified number.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private static string gamma(double x)
        {
            double[] P = {
						 1.60119522476751861407E-4,
						 1.19135147006586384913E-3,
						 1.04213797561761569935E-2,
						 4.76367800457137231464E-2,
						 2.07448227648435975150E-1,
						 4.94214826801497100753E-1,
						 9.99999999999999996796E-1
					 };
            double[] Q = {
						 -2.31581873324120129819E-5,
						 5.39605580493303397842E-4,
						 -4.45641913851797240494E-3,
						 1.18139785222060435552E-2,
						 3.58236398605498653373E-2,
						 -2.34591795718243348568E-1,
						 7.14304917030273074085E-2,
						 1.00000000000000000320E0
					 };

            double p, z;

            double q = Math.Abs(x);

            if (q > 33.0)
            {
                if (x < 0.0)
                {
                    p = Math.Floor(q);
                    if (p == q) throw new ArithmeticException("gamma: overflow");
                    //int i = (int)p;
                    z = q - p;
                    if (z > 0.5)
                    {
                        p += 1.0;
                        z = q - p;
                    }
                    z = q * Math.Sin(Math.PI * z);
                    if (z == 0.0) throw new ArithmeticException("gamma: overflow");
                    z = Math.Abs(z);
                    z = Math.PI / (z * stirf(q));

                    return (-z).ToString();
                }
                else
                {
                    return stirf(x).ToString();
                }
            }

            z = 1.0;
            while (x >= 3.0)
            {
                x -= 1.0;
                z *= x;
            }

            while (x < 0.0)
            {
                if (x == 0.0)
                {
                    throw new ArithmeticException("gamma: singular");
                }
                else if (x > -1.0E-9)
                {
                    return (z / ((1.0 + 0.5772156649015329 * x) * x)).ToString();
                }
                z /= x;
                x += 1.0;
            }

            while (x < 2.0)
            {
                if (x == 0.0)
                {
                    throw new ArithmeticException("gamma: singular");
                }
                else if (x < 1.0E-9)
                {
                    return (z / ((1.0 + 0.5772156649015329 * x) * x)).ToString();
                }
                z /= x;
                x += 1.0;
            }

            if ((x == 2.0) || (x == 3.0)) return z.ToString();

            x -= 2.0;
            p = polevl(x, P, 6);
            q = polevl(x, Q, 7);
            return (z * p / q).ToString();

        }

        /// <summary>
        /// Evaluates polynomial of degree N
        /// </summary>
        /// <param name="x"></param>
        /// <param name="coef"></param>
        /// <param name="N"></param>
        /// <returns></returns>
        private static double polevl(double x, double[] coef, int N)
        {
            double ans;

            ans = coef[0];

            for (int i = 1; i <= N; i++)
            {
                ans = ans * x + coef[i];
            }

            return ans;
        }

        /// <summary>
        /// Return the gamma function computed by Stirling's formula.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private const double SQTPI = 2.50662827463100050242E0;
        private static double stirf(double x)
        {
            double[] STIR = {
							7.87311395793093628397E-4,
							-2.29549961613378126380E-4,
							-2.68132617805781232825E-3,
							3.47222221605458667310E-3,
							8.33333333333482257126E-2,
		};
            double MAXSTIR = 143.01608;

            double w = 1.0 / x;
            double y = Math.Exp(x);

            w = 1.0 + w * polevl(w, STIR, 4);

            if (x > MAXSTIR)
            {
                /* Avoid overflow in Math.Pow() */
                double v = Math.Pow(x, 0.5 * x - 0.25);
                y = v * (v / y);
            }
            else
            {
                y = Math.Pow(x, x - 0.5) / y;
            }
            y = SQTPI * y * w;
            return y;
        }

        #endregion

        #region ZScore

        private const int Z_MAX = 6;                    // Maximum ±z value
        private const int ROUND_FLOAT = 6;              // Decimal places to round numbers

        /*  The following JavaScript functions for calculating normal and
            chi-square probabilities and critical values were adapted by
            John Walker from C implementations
            written by Gary Perlman of Wang Institute, Tyngsboro, MA
            01879.  Both the original C code and this JavaScript edition
            are in the public domain.  */

        /*  POZ  --  probability of normal z value

            Adapted from a polynomial approximation in:
                    Ibbetson D, Algorithm 209
                    Collected Algorithms of the CACM 1963 p. 616
            Note:
                    This routine has six digit accuracy, so it is only useful for absolute
                    z values <= 6.  For z values > to 6.0, poz() returns 0.0.
        */

        private double poz(double z)
        {
            double y, x, w;

            if (z == 0.0)
            {
                x = 0.0;
            }
            else
            {
                y = 0.5 * Math.Abs(z);
                if (y > (Z_MAX * 0.5))
                {
                    x = 1.0;
                }
                else if (y < 1.0)
                {
                    w = y * y;
                    x = ((((((((0.000124818987 * w
                             - 0.001075204047) * w + 0.005198775019) * w
                             - 0.019198292004) * w + 0.059054035642) * w
                             - 0.151968751364) * w + 0.319152932694) * w
                             - 0.531923007300) * w + 0.797884560593) * y * 2.0;
                }
                else
                {
                    y -= 2.0;
                    x = (((((((((((((-0.000045255659 * y
                                   + 0.000152529290) * y - 0.000019538132) * y
                                   - 0.000676904986) * y + 0.001390604284) * y
                                   - 0.000794620820) * y - 0.002034254874) * y
                                   + 0.006549791214) * y - 0.010557625006) * y
                                   + 0.011630447319) * y - 0.009279453341) * y
                                   + 0.005353579108) * y - 0.002141268741) * y
                                   + 0.000535310849) * y + 0.999936657524;
                }
            }
            return z > 0.0 ? ((x + 1.0) * 0.5) : ((1.0 - x) * 0.5);
        }


        /*  CRITZ  --  Compute critical normal z value to
                       produce given p.  We just do a bisection
                       search for a value within CHI_EPSILON,
                       relying on the monotonicity of pochisq().  */

        private double critz(double p)
        {
            double Z_EPSILON = 0.000001;     /* Accuracy of z approximation */
            double minz = -Z_MAX;
            double maxz = Z_MAX;
            double zval = 0.0;
            double pval;

            if (p < 0.0 || p > 1.0)
            {
                return -1;
            }

            while ((maxz - minz) > Z_EPSILON)
            {
                pval = poz(zval);
                if (pval > p)
                {
                    maxz = zval;
                }
                else
                {
                    minz = zval;
                }
                zval = (maxz + minz) * 0.5;
            }
            return (zval);
        }

        #endregion

        #region debug

        public static async void printdbg(string txt)
        {
            await new Windows.UI.Popups.MessageDialog(txt).ShowAsync();
        }

        #endregion
    }
}
