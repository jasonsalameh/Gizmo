using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.DataTransfer.ShareTarget;
using Windows.ApplicationModel.Search;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Printing;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.System.Threading;
using Windows.UI;
using Windows.UI.ApplicationSettings;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Printing;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Notepad
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Notepad_MainPage : Page 
    {
        public DispatcherTimer recentFilesTimer;
        private DispatcherTimer saveTimer, updateTimer, displayTimer;
        IStorageItem currentFile;
        bool textChanged, prepared, snapped, isSharing, readyForDisplay, displayAddToMRU, displayRefreshData;
        IStorageItem displayFile;
        DataTransferManager manager;
        ShareOperation shareOperation = null;

        //For Printing
        //private PrintManager printManager;
        //private PrintDocument printDocument;
        //private List<UIElement> printPages = null;
        //private IPrintDocumentSource printDocumentSource;

        private NotepadData data;
        private Grid previousLVItem;
        private int numToLoad, searchIndex;
        const int ITEMS_IN_FULL_SCREEN = 25;
        const int ITEMS_IN_SNAPPED_SCREEN = 3;
        //TBD:private SearchPane searchPane;
        const int UPDATE_TIMER = 10;

        SolidColorBrush white = Helpers.GetBrush(Helpers.Colors.White);
        SolidColorBrush blue = Helpers.GetBrush(Helpers.Colors.MetroBlue);
        SolidColorBrush green = Helpers.GetBrush(Helpers.Colors.MetroGreen);

        public Notepad_MainPage()
        {
            InitializeComponent();
            currentFile = null;
            textChanged = false;
            prepared = false;
            previousLVItem = null;
            numToLoad = ITEMS_IN_FULL_SCREEN;
            searchIndex = 0;
            snapped = false;
            isSharing = true;
            readyForDisplay = false;
            displayAddToMRU = false;
            displayRefreshData = false;
            displayFile = null;

            recentFilesTimer = new DispatcherTimer();
            recentFilesTimer.Interval = TimeSpan.FromMilliseconds(UPDATE_TIMER);
            recentFilesTimer.Tick += timer_Tick;

            displayTimer = new DispatcherTimer();
            displayTimer.Interval = TimeSpan.FromMilliseconds(UPDATE_TIMER);
            displayTimer.Tick += displayTimer_Tick;

            //Don't start the timer, it will be called from the navigation function when not sharing
            //recentFilesTimer.Start();

            //InitializePrinting();

            //Layout
            //Window.Current.SizeChanged += Current_SizeChanged; 
            this.SizeChanged += Current_SizeChangedUAP;

            //Suspend
            Utilities.App.Current.Suspending += new SuspendingEventHandler(App_Suspending);

            //set up a timer to save periodically
            saveTimer = new DispatcherTimer();
            saveTimer.Interval = TimeSpan.FromMinutes(5);
            saveTimer.Tick += saveTimer_Tick;
            saveTimer.Start();

            updateTimer = new DispatcherTimer();
            updateTimer.Interval = TimeSpan.FromMilliseconds(UPDATE_TIMER);
            updateTimer.Tick += t_Tick;

            //Initialize TitleRow
            App.ScheduleOnNextRender(delegate
            {
                List<Breadcrumb> breadcrumbs = new List<Breadcrumb>();
                breadcrumbs.Add(new Breadcrumb(App.Apps.MainPage));
                breadcrumbs.Add(new Breadcrumb(App.Apps.Notepad, true));
                TitleRow.SetupBreadcrumbs(breadcrumbs);                
                this.recentSpacer.Height = this.contentLine.Height;
            });

            //TBD:SettingsPane.GetForCurrentView().CommandsRequested += Settings_CommandsRequested;

        }


        //#region print code

        //private void InitializePrinting()
        //{
        //    //Create new print document
        //    printDocument = new PrintDocument();
        //    printDocumentSource = printDocument.DocumentSource;
        //    printDocument.AddPages += printDocument_AddPages;
        //    printDocument.Paginate += printDocument_Paginate;
        //    printDocument.GetPreviewPage += printDocument_GetPreviewPage;

        //    // Get the print manager.
        //    printManager = PrintManager.GetForCurrentView();
        //    printManager.PrintTaskRequested += printManager_PrintTaskRequested;
        //}

        //private void UnintializePrinting()
        //{
        //    printManager = PrintManager.GetForCurrentView();
        //    printManager.PrintTaskRequested -= printManager_PrintTaskRequested;
        //}

        //void printManager_PrintTaskRequested(PrintManager sender, PrintTaskRequestedEventArgs args)
        //{
        //    PrintTask printTask = args.Request.CreatePrintTask("Print Page Title", GetPrintSource);
        //}

        //private void GetPrintSource(PrintTaskSourceRequestedArgs e)
        //{
        //    e.SetSource(printDocumentSource);
        //}

        //void printDocument_GetPreviewPage(object sender, GetPreviewPageEventArgs e)
        //{
        //    printDocument.SetPreviewPage(e.PageNumber, printPages[e.PageNumber - 1]);
        //}

        //void printDocument_Paginate(object sender, PaginateEventArgs e)
        //{
        //    printPages = new List<UIElement>();
        //    GetPrintPages(e.PrintTaskOptions);

        //    printDocument.SetPreviewPageCount(printPages.Count, Windows.UI.Xaml.Printing.PreviewPageCountType.Final);

        //    printDocument.SetPreviewPageCount(printPages.Count, PreviewPageCountType.Intermediate);

        //}

        //// Create a printable version of the requested print range
        //void GetPrintPages(Windows.Graphics.Printing.PrintTaskOptions options)
        //{
        //    // Get the dimensions of the page
        //    Windows.Graphics.Printing.PrintPageDescription pageDescription = options.GetPageDescription(0);

        //    double textWidth = pageDescription.PageSize.Width;
        //    double textHeight = pageDescription.PageSize.Height;

        //    // Set the page margins to 5% of the page size. If the ImageableRect is smaller than  
        //    // the requested print area, then use the ImageableRect.
        //    double marginWidth = Math.Max(pageDescription.PageSize.Width - pageDescription.ImageableRect.Width,
        //                                  pageDescription.PageSize.Width * .05);

        //    double marginHeight = Math.Max(pageDescription.PageSize.Height - pageDescription.ImageableRect.Height,
        //                                   pageDescription.PageSize.Height * .05);

        //    // Create a StackPanel to hold the page contents. Page contents are in a 
        //    // RichTextBlock. If there is additional page content, it will overflow
        //    // into a RichTextBlockOverflow.
        //    StackPanel printPage = new StackPanel();
        //    RichTextBlock rtb = new RichTextBlock();

        //    printPage.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
        //    printPage.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;

        //    // Set content margins
        //    rtb.Margin = new Thickness(marginWidth / 2, marginHeight / 2, marginWidth / 2, marginHeight / 2);
        //    rtb.Width = textWidth - marginWidth;
        //    rtb.Height = textHeight - marginHeight;

        //    // Get the text content from the PrintAreaPanel and add to first printable page
        //    //GetDocumentText(rtb, contentStack);
        //    printPage.Children.Add(rtb);
        //    printPages.Add(printPage);

        //    // Add first printable page to Canvas to test for multiple page content
        //    PrintCanvas.Children.Clear();
        //    PrintCanvas.Children.Add(printPage);

        //    // Add additional page content, if it exists
        //    AddOverflowContent(rtb, textWidth, textHeight, marginWidth, marginHeight);
        //}

        //// Add a RichTextBlockOverflow to determine if there are more pages to print.
        //void AddOverflowContent(FrameworkElement rtb, double textWidth, double textHeight, double marginWidth, double marginHeight)
        //{
        //    // A RichTextBlockOverflow to test for additional pages of content
        //    RichTextBlockOverflow rtbOverflow = new RichTextBlockOverflow();

        //    if (rtb is RichTextBlock)
        //    {
        //        RichTextBlock rtbCurrent = rtb as RichTextBlock;
        //        rtbCurrent.OverflowContentTarget = rtbOverflow;
        //    }
        //    else
        //    {
        //        RichTextBlockOverflow rtbOCurrent = rtb as RichTextBlockOverflow;
        //        rtbOCurrent.OverflowContentTarget = rtbOverflow;
        //    }

        //    // Set content dimensions and create the RichTextBlockOverflow page
        //    rtbOverflow.Width = textWidth - marginWidth;
        //    rtbOverflow.Height = textHeight - marginHeight;
        //    rtbOverflow.Margin =
        //        new Thickness(marginWidth / 2, marginHeight / 2, marginWidth / 2, marginHeight / 2);

        //    StackPanel overflowPage = new StackPanel();
        //    overflowPage.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
        //    overflowPage.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;

        //    overflowPage.Children.Add(rtbOverflow);

        //    // Add the page to the test canvas and redraw to determine if there is additional content
        //    PrintCanvas.Children.Add(overflowPage);
        //    PrintCanvas.InvalidateMeasure();
        //    PrintCanvas.UpdateLayout();

        //    // If additional content exists, add it to the print output and check for more
        //    if (rtbOverflow.HasOverflowContent)
        //    {
        //        // Add the overflow page to the printable content
        //        printPages.Add(overflowPage);
        //        AddOverflowContent(rtbOverflow, textWidth, textHeight, marginWidth, marginHeight);
        //    }
        //    else
        //    {
        //        // Add the last page of overflow content
        //        if (rtb is RichTextBlock)
        //            printPages.Add(overflowPage);
        //    }
        //}

        //// Get a copy of the content in the print area panel
        //void GetDocumentText(RichTextBlock mainBlock, StackPanel panel)
        //{
        //    // Get the content from the print area panel
        //    foreach (TextBox tb in panel.Children)
        //    {
        //        // Create a new TextBlock with the paragraph text
        //        Windows.UI.Xaml.Documents.Paragraph newPara =
        //            new Windows.UI.Xaml.Documents.Paragraph();
        //        newPara.FontSize = tb.FontSize;
        //        newPara.FontWeight = tb.FontWeight;
        //        //newPara.LineHeight = tb.LineHeight;
        //        newPara.Foreground = tb.Foreground;
        //        newPara.TextAlignment = tb.TextAlignment;
        //        Windows.UI.Xaml.Documents.Run runText =
        //            new Windows.UI.Xaml.Documents.Run();
        //        runText.Text = tb.Text;
        //        newPara.Inlines.Add(runText);

        //        // Add the TextBlock to the StackPanel that is returned
        //        mainBlock.Blocks.Add(newPara);
        //    }
        //}

        //void printDocument_AddPages(object sender, AddPagesEventArgs e)
        //{
        //    for (int i = 0; i < printPages.Count; i++)
        //    {
        //        printDocument.AddPage(printPages[i]);
        //    }

        //    printDocument.AddPagesComplete();
        //}
        //#endregion

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //Sharing
            try
            {
                manager = DataTransferManager.GetForCurrentView();
                manager.DataRequested += new TypedEventHandler<DataTransferManager, DataRequestedEventArgs>(manager_DataRequested);
            }
            catch { }

            //Search
            //TBD:
            //try
            //{
            //    searchPane = SearchPane.GetForCurrentView();
            //    searchPane.QuerySubmitted += new TypedEventHandler<SearchPane, SearchPaneQuerySubmittedEventArgs>(searchPane_QuerySubmitted);
            //    searchPane.QueryChanged += new TypedEventHandler<SearchPane, SearchPaneQueryChangedEventArgs>(searchPane_QueryChanged);
            //}
            //catch { }

            MainPage_ChangeViewState();

            // show help
            Utilities.Common.ShowHelp.ShowHelpMessage(App.Apps.Notepad);
        }

        public TextBlock LoadingTxt
        {
            get
            {
                return this.txtLoading;
            }

            set
            {
                this.txtLoading = value;
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            PromptToSave("Do you want to save these changes?", false, false);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {           
            //unregister for Share
            try
            {
                manager.DataRequested -= manager_DataRequested;
            }
            catch { }

            //unregister for search
            //TBD:
            //try
            //{
            //    searchPane.QueryChanged -= searchPane_QueryChanged;
            //    searchPane.QuerySubmitted -= searchPane_QuerySubmitted;
            //}
            //catch { }

            ////unregister for settings
            //try
            //{
            //   SettingsPane.GetForCurrentView().CommandsRequested -= Settings_CommandsRequested;
            //}
            //catch { }
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

        async void saveTimer_Tick(object sender, object e)
        {
            await SaveFile(false, false);
        }

        void timer_Tick(object sender, object e)
        {
            isSharing = false;
            recentFilesTimer.Stop();
            UpdateData(UPDATE_TIMER);
            //Set the cursor to highlight the title
            this.txtNoteName.Focus(Windows.UI.Xaml.FocusState.Keyboard);
            this.txtNoteName.SelectionStart = this.txtNoteName.Text.Length;
            this.txtNoteName.SelectionLength = 0;
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
            MainPage_ChangeViewState();
        }

        void MainPage_ChangeViewState()
        {
            this.TitleRow.ChangeViewState();

            if (App.isLandscape())
            {
                VisualStateManager.GoToState(this, "Fill", false);
                //this.ItemGridView.ItemsPanel = (ItemsPanelTemplate)Resources["StoreFrontGridItemsPanelTemplate"];
                this.ContentRoot.ColumnDefinitions[0].Width = new GridLength(320);
                this.numToLoad = ITEMS_IN_FULL_SCREEN;
                snapped = false;
                UpdateData(UPDATE_TIMER);
            }
            else if (App.isPortrait())
            {
                VisualStateManager.GoToState(this, "Full", false);
                //this.ItemGridView.ItemsPanel = (ItemsPanelTemplate)Resources["StoreFrontGridItemsPanelTemplate"];
                this.ContentRoot.ColumnDefinitions[0].Width = new GridLength(320);
                this.numToLoad = ITEMS_IN_FULL_SCREEN;
                snapped = false;
                UpdateData(UPDATE_TIMER);
            }
            else if (App.isSnapped())
            {
                VisualStateManager.GoToState(this, "Snapped", false);
                //this.ItemGridView.ItemsPanel = (ItemsPanelTemplate)Resources["StoreFrontGridItemsPanelTemplateSnapped"];
                this.ContentRoot.ColumnDefinitions[0].Width = new GridLength(0);
                this.numToLoad = ITEMS_IN_SNAPPED_SCREEN;
                snapped = true;
                UpdateData(UPDATE_TIMER);
            }
        }

        //TBD:
        //void searchPane_QueryChanged(SearchPane sender, SearchPaneQueryChangedEventArgs args)
        //{
        //    SearchForText(args.QueryText, false);
        //}

        //void searchPane_QuerySubmitted(SearchPane sender, SearchPaneQuerySubmittedEventArgs args)
        //{            
        //    SearchForText(args.QueryText, false);
        //    this.searchIndex = this.txtContent.SelectionStart + args.QueryText.Length;
        //}

        private void SearchForText(string query, bool triedStartingFromTop)
        {
            if (query == String.Empty)
                return;

            try
            {
                this.txtContent.Focus(Windows.UI.Xaml.FocusState.Pointer);
                this.txtContent.SelectionStart = this.txtContent.Text.IndexOf(query, searchIndex, StringComparison.CurrentCultureIgnoreCase);
                
                if (this.txtContent.SelectionStart != -1)
                    this.txtContent.SelectionLength = query.Length;                
                
                if (this.txtContent.SelectionStart == -1 && !triedStartingFromTop )
                {
                    searchIndex = 0;
                    SearchForText(query, true);
                }                
            }
            catch //(Exception e)
            {
                searchIndex = 0;
                SearchForText(query, true);
            }

        }

        async public void App_Suspending(object sender, object e)
        {
            SuspendingOperation op = (e as SuspendingEventArgs).SuspendingOperation;
            SuspendingDeferral deferral = op.GetDeferral();

            //Begin 5 seconds

            await SaveFile(false, false);

            //End 5 seconds
            deferral.Complete();
        }


        #region Sharing Code
        public async void displaySharingText(ShareTargetActivatedEventArgs args)
        {     
            //Handle saving the existing file before moving on to this
            if (args.Kind == ActivationKind.ShareTarget)
            {
                PromptToSave("Do you want to save these changes before accepting shared data?", false, false);

                this.shareOperation = args.ShareOperation;
                this.txtNoteName.Text = shareOperation.Data.Properties.Title;
                string description = shareOperation.Data.Properties.Description;
                if (description != "")
                    this.txtContent.Text = description + Environment.NewLine;

                if (shareOperation.Data.Contains(StandardDataFormats.Text))
                {
                    string text = await shareOperation.Data.GetTextAsync(StandardDataFormats.Text);
                    if (text != null)
                        this.txtContent.Text += text;
                }

                this.btnNewNote.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                //this.btnSave.Visibility = Windows.UI.Xaml.Visibility.Visible;
                this.btnSaveAs.Opacity = 1;                
            }

            Window.Current.Content = this;
            Window.Current.Activate();
        }

        private void manager_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            var properties = args.Request.Data.Properties;
            properties.Title = this.txtNoteName.Text;
            properties.Description = "Sharing a Note";
            args.Request.Data.SetText(this.txtContent.Text);
        }
        #endregion

        private void txtNoteName_GotFocus(object sender, RoutedEventArgs e)
        {
            this.txtNoteName.SelectionStart = this.txtNoteName.Text.Length;
            this.txtNoteName.SelectionLength = 0;
            
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.txtContent.Focus(Windows.UI.Xaml.FocusState.Keyboard);
        }

        private void txtContent_GotFocus(object sender, RoutedEventArgs e)
        {
            this.txtNoteName.SelectionStart = 0;
            this.txtNoteName.SelectionLength = 0;
        }

        async void displayTimer_Tick(object sender, object e)
        {
            if (!readyForDisplay)
                return;

            this.btnSaveAs.Opacity = 1;
            //this.btnSave.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            this.txtNoteName.Text = displayFile.Name;

            textChanged = false;
            prepared = true;

            StorageFile sFile = displayFile as StorageFile;

            try
            {
                this.txtContent.Text = await FileIO.ReadTextAsync(sFile);

                this.txtContent.SelectionLength = 0;
                this.txtContent.SelectionStart = this.txtContent.Text.Length;
                this.currentFile = displayFile;
                if (displayAddToMRU)
                    AddFileToMRUList(currentFile, displayRefreshData);
                textChanged = false;
                displayTimer.Stop();
            }
            catch (Exception)
            {
                prepareForNewFile("The structure of this file is unsupported.", displayFile.Name);
                displayTimer.Stop();
            }
        }

        internal void DisplayFile(IStorageItem file, bool addToMRU, bool refreshData)
        {
            //this is the hackiest, shittiest code I've written in a long time - but WinRT is forcing my hand without proper async support.
            readyForDisplay = false;
            //Save the current file
            PromptToSave("Do you want to save these changes before opening a new file?", false, false);            

            this.displayFile = file;
            this.displayAddToMRU = addToMRU;
            this.displayRefreshData = refreshData;

            displayTimer.Start();
            
        }



        private void AddFileToMRUList(IStorageItem file, bool refreshData)
        {
            StorageApplicationPermissions.MostRecentlyUsedList.Add(file);

            //refresh the grid view
            if (refreshData)
                UpdateData(UPDATE_TIMER);
        }

        private void UpdateData(int interval)
        {
            if (!updateTimer.IsEnabled)
                updateTimer.Start();
        }

        async void t_Tick(object sender, object e)
        {
            (sender as DispatcherTimer).Stop();
            data = new NotepadData(this);
            await data.init(numToLoad);
        }

        public void RefreshGridView()
        {
            this.ItemGridView.ItemsSource = data.Collection;
            if (data.ItemCount > 0 && !isSharing)
            {
                //unhide the grid view and other UI
                this.ItemGridView.Visibility = Windows.UI.Xaml.Visibility.Visible;                
                this.txtRecent.Visibility = Windows.UI.Xaml.Visibility.Visible;
                
                if (!snapped)
                    this.ContentRoot.ColumnDefinitions[0].Width = new GridLength(320, GridUnitType.Auto);

                App.ScheduleOnNextRender(delegate()
                {
                    this.imgLine.Width = col1.ActualWidth - 60;                
                });
                
            }
            else if (isSharing)
            {
                this.ItemGridView.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                this.txtRecent.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                
                this.ContentRoot.ColumnDefinitions[0].Width = new GridLength(0);

                App.ScheduleOnNextRender(delegate()
                {
                    this.imgLine.Width = col1.ActualWidth - 60;
                });                              
            }
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            await SaveFile(true, false);
        }

        async internal Task WriteFile(IStorageItem file, bool prepare)
        {
            try
            {
                StorageFile sampleFile = file as StorageFile;

                await FileIO.WriteTextAsync(sampleFile, txtContent.Text);
                if (prepare)
                    prepareForNewFile();
                readyForDisplay = true;
                
            }
            catch (Exception) {}
        }

        async public Task SaveFile(bool promptToSave, bool prepare)
        {
            if (currentFile == null && promptToSave && textChanged)
            {
                SaveFileAs(prepare);
                return;
            }
            else if (currentFile == null && !promptToSave && textChanged)
            {
                //Do nothing. We can't save because we don't have the document library capability!
            }
            else if (currentFile != null && textChanged)
            {
                this.txtNoteName.Text = currentFile.Name;
                await WriteFile(currentFile, prepare);
                textChanged = false;
                this.btnSaveAs.Opacity = 1;
                
            }
        }

        async internal void SaveFileAs(bool prepare)
        {
            //if (snapped)
            //{                
            //    bool unsnap = Windows.UI.ViewManagement.ApplicationView.TryUnsnap();
            //    if (!unsnap)
            //        return;
            //    else
            //        snapped = false;
            //}
            FileSavePicker savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            // Dropdown of file types the user can save the file as            
            savePicker.FileTypeChoices.Add("Plain Text", new List<string>() { ".txt" });
            // Default extension if the user does not select a choice explicitly from the dropdown
            savePicker.DefaultFileExtension = ".txt";
            // Default file name if the user does not type one in or select a file to replace
            savePicker.SuggestedFileName = this.txtNoteName.Text;
            StorageFile savedItem = await savePicker.PickSaveFileAsync();

            if (savedItem != null)
            {
                await WriteFile(savedItem, prepare);
                if (!prepare)
                {
                    prepared = true;
                    textChanged = false;
                    currentFile = savedItem;
                    this.txtNoteName.Text = savedItem.Name;
                }
                this.btnSaveAs.Opacity = 1;
                //add file to MRU list
                AddFileToMRUList(savedItem, true);
            }
        }

        private async void PromptToSave(string messageSuffix, bool showCancel, bool prepare)
        {
            if (currentFile != null && textChanged)
            {
                await SaveFile(true, prepare);
            }
            //do logic here for prompting
            else if (textChanged && !(this.txtContent.Text == "" && this.txtNoteName.Text == "Untitled"))
            {
                var boxAsk = new MessageDialog("There are unsaved changes to \"" + this.txtNoteName.Text + "\". " + messageSuffix);
                UICommand btnS = new UICommand("Yes", new UICommandInvokedHandler(DoNothing));
                UICommand btnNoS = new UICommand("No", new UICommandInvokedHandler(DoNothing));
                UICommand btnC = null;
                if (showCancel)
                    btnC = new UICommand("Cancel", new UICommandInvokedHandler(DoNothing));
                boxAsk.Commands.Add(btnS);
                boxAsk.Commands.Add(btnNoS);
                if (showCancel)
                    boxAsk.Commands.Add(btnC);
                boxAsk.DefaultCommandIndex = 0;
                var a = await boxAsk.ShowAsync();
                switch (a.Label)
                {
                    case "Yes":
                        await SaveFile(true, prepare);
                        break;
                    case "No":
                        if (prepare)
                            prepareForNewFile();
                        readyForDisplay = true;
                        break;
                    default:
                        break;
                }
            }
            else if (prepare)
            {
                prepareForNewFile();
                App.ScheduleOnNextRender(delegate()
                {
                    this.textChanged = false;
                    prepared = true;
                });
            }
            else
                readyForDisplay = true;
        }

        private void btnSaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileAs(false);
        }

        private void btnNewNote_Click(object sender, RoutedEventArgs e)
        {
            PromptToSave("Would you like to save these changes before creating a new note?", true, true);
        }

        public static string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == ' ')
                    sb.Append(c);
            }
            if (sb.Length > 0)
                return sb.ToString();
            else
                return "Untitled";
        }

        #region Prepare For New File Functions
        private void prepareForNewFile()
        {
            //Prepare for new file
            this.currentFile = null;
            textChanged = false;
            prepared = true;
            searchIndex = 0;

            //clear UI
            this.txtContent.Text = "";
            this.txtNoteName.Text = "Untitled";
            
            //reset cursor            
            this.txtContent.Focus(Windows.UI.Xaml.FocusState.Keyboard);
            this.txtContent.SelectionStart = 0;
            this.txtContent.SelectionLength = 0;

            this.txtNoteName.Focus(Windows.UI.Xaml.FocusState.Keyboard);
            this.txtNoteName.SelectionStart = this.txtNoteName.Text.Length;
            this.txtNoteName.SelectionLength = 0;
                        

            //hide save buttons
            //this.btnSave.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            this.btnSaveAs.Opacity = 0;
            //show new note button
            this.btnNewNote.Visibility = Windows.UI.Xaml.Visibility.Visible;

            //reset the selection on the GridView
            this.ItemGridView.SelectedIndex = -1;
            if (previousLVItem != null)
            {
                try
                {
                    previousLVItem.Background = white;
                }
                catch { }
            }
        }

        private void prepareForNewFile(string message)
        {
            prepareForNewFile();
            this.txtContent.Text = message;
            prepared = true;
        }

        private void prepareForNewFile(string message, string noteName)
        {
            prepareForNewFile(message);
            this.txtNoteName.Text = noteName;
            prepared = true;
        }
        #endregion
  
        private void DoNothing(object command)
        {
            //do nothing, such a stupid hack
        }

        private void txtContent_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (prepared)
            {
                prepared = false;
                textChanged = false;
            }
            else
            {
                textChanged = true;
                this.btnSaveAs.Opacity = 1;
            }
        }

        private void txtNoteName_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.currentFile = null;

            if (prepared)
                textChanged = false;
            else
                this.textChanged = true;
        }

        private async void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            await PrintManager.ShowPrintUIAsync();
        }

        private void clearMRU()
        {
            StorageApplicationPermissions.MostRecentlyUsedList.Clear();
            UpdateData(UPDATE_TIMER);
        }

        async internal void checkMRU()
        {
            AccessListEntryView entries = StorageApplicationPermissions.MostRecentlyUsedList.Entries;
            StringBuilder outputText = new StringBuilder("The MRU list contains the following items:\n\n");

            foreach (AccessListEntry entry in entries)
            {
                try
                {
                    StorageFile storageFile = await StorageApplicationPermissions.MostRecentlyUsedList.GetFileAsync(entry.Token);
                    outputText.Append(storageFile.DisplayName + "\n");
                }
                catch { }
            }
            this.txtContent.Text += outputText.ToString();
        }

        private void ItemGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.searchIndex = 0;
            LoadSelectedFile((NotepadItem)e.ClickedItem);
            this.txtContent.Focus(Windows.UI.Xaml.FocusState.Keyboard);
        }

        async private void LoadSelectedFile(NotepadItem item)
        {
            StorageFile sampleFile = await StorageApplicationPermissions.MostRecentlyUsedList.GetFileAsync(item.Token);
            DisplayFile(sampleFile, false, false);
        }

        private void Grid_PointerEntered(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            var g = sender as Grid;
            if (g != previousLVItem)
                g.Background = blue;
        }

        private void Grid_PointerExited(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            var g = sender as Grid;
            if (g != previousLVItem)
                g.Background = white;
        }

        private void Grid_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (previousLVItem != null)
            {
                try
                {
                    previousLVItem.Background = white;
                }
                catch { }
            }
            previousLVItem = sender as Grid;
            previousLVItem.Background = green;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            clearMRU();
            this.txtContent.Focus(Windows.UI.Xaml.FocusState.Keyboard);
            //checkMRU();
            //RefreshGridView();
        }

        private void btnOpenNote_Click(object sender, RoutedEventArgs e)
        {
            OpenNote();
        }

        async private void OpenNote()
        {
            //if (snapped)
            //{
            //    bool unsnap = Windows.UI.ViewManagement.ApplicationView.TryUnsnap();
            //    if (!unsnap)
            //        return;
            //    else
            //        snapped = false;
            //}
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.List;
            openPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            openPicker.FileTypeFilter.Add(".txt");

            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file == null)
                return;

            prepared = true;
            DisplayFile(file, true, false);


            UpdateData(UPDATE_TIMER);
        }

        private void txtContent_KeyUp_1(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Tab)
            {
                this.txtContent.Text += '\t';
                this.txtContent.SelectionStart = this.txtContent.Text.Length;
                this.txtContent.SelectionLength = 0;
                e.Handled = true;
            }
        }

    }
}
