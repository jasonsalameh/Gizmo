using System;
using System.Collections.Generic;
using System.Globalization;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Calculator.CalcViews
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SnappedProgrammer : Page, CalculatorView, ProgrammerView
    {
        private Calc_MainPage _mainPage;

        // Bitness
        private Programmer.ProgrammerCalcBits currentBits = Programmer.ProgrammerCalcBits.Qword;
        Programmer.ProgrammerCalcBits ProgrammerView.currentBits { get { return currentBits; } set { currentBits = value; } }

        // State
        private Calc_MainPage.CalcStateValues calcStateInternal;
        Calc_MainPage.CalcStateValues ProgrammerView.calcState
        {
            get { return calcStateInternal; }
            set
            {
                calcStateInternal = value;
                Calc_MainPage.programmerCalcState = calcStateInternal;
            }
        }

        // Long converstion
        public const string EXCEPTION_STR = "exception";

        public SnappedProgrammer(Calc_MainPage mainPage)
        {
            this.InitializeComponent();

            this.calcStateInternal = Calc_MainPage.programmerCalcState;
            this._mainPage = mainPage;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame. 
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        #region Interface

        public Button GetEqualsKey()
        {
            return this.EqualProgrammer;
        }

        public void Add_Memory_Click(object sender, RoutedEventArgs e)
        {
            _mainPage.Add_Memory_Click(sender, e);
        }

        public void Clear_All_Memory_Click(object sender, RoutedEventArgs e)
        {
            _mainPage.Clear_All_Memory_Click(sender, e);
        }

        public void Metro_Memory_Add_Button_Click(object sender, RoutedEventArgs e)
        {
            _mainPage.Metro_Memory_Add_Button_Click(sender, e);
        }

        public void Metro_Memory_Restore_Button_Click(object sender, RoutedEventArgs e)
        {
            _mainPage.Metro_Memory_Restore_Button_Click(sender, e);
        }

        public void Dot_Button_Click(object sender, RoutedEventArgs e)
        {
            _mainPage.Dot_Button_Click(sender, e);
        }

        public void Symbol_Button_Click(object sender, RoutedEventArgs e)
        {
            _mainPage.Symbol_Button_Click(sender, e);
        }

        public void Value_Button_Click(object sender, RoutedEventArgs e)
        {
            _mainPage.Value_Button_Click(sender, e);
        }

        public void Rand_Button_Click(object sender, RoutedEventArgs e)
        {
            _mainPage.Rand_Button_Click(sender, e);
        }

        public void Negate_Button_Click(object sender, RoutedEventArgs e)
        {
            _mainPage.Negate_Button_Click(sender, e);
        }

        public void Paran_Button_Click(object sender, RoutedEventArgs e)
        {
            _mainPage.Paran_Button_Click(sender, e);
        }

        public void Operator_Single_Button_Click(object sender, RoutedEventArgs e)
        {
            _mainPage.Operator_Single_Button_Click(sender, e);
        }

        public void Operator_Double_Button_Click(object sender, RoutedEventArgs e)
        {
            _mainPage.Operator_Double_Button_Click(sender, e);
        }

        public void Delete_Last_Button_Click(object sender, RoutedEventArgs e)
        {
            _mainPage.Delete_Last_Button_Click(sender, e);
        }

        public void Clear_Calc_Button_Click(object sender, RoutedEventArgs e)
        {
            _mainPage.Clear_Calc_Button_Click(sender, e);
        }

        public void Clear_Error_Button_Click(object sender, RoutedEventArgs e)
        {
            _mainPage.Clear_Error_Button_Click(sender, e);
        }

        public void Equal_Button_Click(object sender, RoutedEventArgs e)
        {
            _mainPage.Equal_Button_Click(sender, e);
        }

        #endregion

        #region Programmer Calc Bitness

        public void QWord_Bitness_Click(object sender, RoutedEventArgs e)
        {
            Change_Bitness_Click(Programmer.ProgrammerCalcBits.Qword);
        }

        public void DWord_Bitness_Click(object sender, RoutedEventArgs e)
        {
            Change_Bitness_Click(Programmer.ProgrammerCalcBits.Dword);
        }

        public void Word_Bitness_Click(object sender, RoutedEventArgs e)
        {
            Change_Bitness_Click(Programmer.ProgrammerCalcBits.Word);
        }

        public void Byte_Bitness_Click(object sender, RoutedEventArgs e)
        {
            Change_Bitness_Click(Programmer.ProgrammerCalcBits.Byte);
        }

        public void Change_Bitness_Click(Programmer.ProgrammerCalcBits bitness)
        {
            if (bitness == Programmer.ProgrammerCalcBits.Qword)
            {
                // toggle button
                QWordButton.IsChecked = true;
                DWordButton.IsChecked = false;
                WordButton.IsChecked = false;
                ByteButton.IsChecked = false;
            }
            else if (bitness == Programmer.ProgrammerCalcBits.Dword)
            {
                QWordButton.IsChecked = false;
                DWordButton.IsChecked = true;
                WordButton.IsChecked = false;
                ByteButton.IsChecked = false;

                //QwordBitness.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                //DwordBitness.Visibility = Windows.UI.Xaml.Visibility.Visible;
                //WordBitness.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            else if (bitness == Programmer.ProgrammerCalcBits.Word)
            {
                QWordButton.IsChecked = false;
                DWordButton.IsChecked = false;
                WordButton.IsChecked = true;
                ByteButton.IsChecked = false;

                //QwordBitness.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                //DwordBitness.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                //WordBitness.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            else if (bitness == Programmer.ProgrammerCalcBits.Byte)
            {
                QWordButton.IsChecked = false;
                DWordButton.IsChecked = false;
                WordButton.IsChecked = false;
                ByteButton.IsChecked = true;

                //QwordBitness.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                //DwordBitness.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                //WordBitness.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }

            _mainPage.Change_Bitness_Click(bitness);
        }

        #endregion

        #region Programmer Calc Settings

        public void ProgrammerSettings_Click(Calc_MainPage.CalcStateValues mode)
        {
            if (mode == Calc_MainPage.CalcStateValues.Hex)
            {
                // Buttons
                HexSpecificButtons(true);
                DecimalSpecificButtons(true);
                OctalSpecificButtons(true);
            }
            else if (mode == Calc_MainPage.CalcStateValues.Dec)
            {
                // Buttons
                HexSpecificButtons(false);
                DecimalSpecificButtons(true);
                OctalSpecificButtons(true);
            }
            else if (mode == Calc_MainPage.CalcStateValues.Oct)
            {
                // Buttons
                HexSpecificButtons(false);
                DecimalSpecificButtons(false);
                OctalSpecificButtons(true);
            }
            else if (mode == Calc_MainPage.CalcStateValues.Bin)
            {
                // Buttons
                HexSpecificButtons(false);
                DecimalSpecificButtons(false);
                OctalSpecificButtons(false);
            }
        }

        // what turns off from Hex to Dec
        private void HexSpecificButtons(bool enabled)
        {
            AButton.IsEnabled = enabled;
            BButton.IsEnabled = enabled;
            CButton.IsEnabled = enabled;
            DButton.IsEnabled = enabled;
            EButton.IsEnabled = enabled;
            FButton.IsEnabled = enabled;
        }

        // what turns off from Dec to Oct
        private void DecimalSpecificButtons(bool enabled)
        {
            EightButton.IsEnabled = enabled;
            NineButton.IsEnabled = enabled;
        }

        // what turns off from Oct to Bin
        private void OctalSpecificButtons(bool enabled)
        {
            SevenButton.IsEnabled = enabled;
            SixButton.IsEnabled = enabled;
            FiveButton.IsEnabled = enabled;
            FourButton.IsEnabled = enabled;
            ThreeButton.IsEnabled = enabled;
            TwoButton.IsEnabled = enabled;
        }

        #endregion

        #region Redraw

        public void Redraw(List<int> binArray)
        {
            try
            {
                // byte
                BinaryFirst.Text = arrayToString(binArray.GetRange(32, 4));
                BinarySecond.Text = arrayToString(binArray.GetRange(36, 4));

                // word
                BinaryThird.Text = arrayToString(binArray.GetRange(40, 4));
                BinaryFourth.Text = arrayToString(binArray.GetRange(44, 4));

                // dword
                BinaryFifth.Text = arrayToString(binArray.GetRange(48, 4));
                BinarySixth.Text = arrayToString(binArray.GetRange(52, 4));
                BinarySeventh.Text = arrayToString(binArray.GetRange(56, 4));
                BinaryEight.Text = arrayToString(binArray.GetRange(60, 4));

                // qword
                BinaryFirstUp.Text = arrayToString(binArray.GetRange(0, 4));
                BinarySecondUp.Text = arrayToString(binArray.GetRange(4, 4));
                BinaryThirdUp.Text = arrayToString(binArray.GetRange(8, 4));
                BinaryFourthUp.Text = arrayToString(binArray.GetRange(12, 4));
                BinaryFifthUp.Text = arrayToString(binArray.GetRange(16, 4));
                BinarySixthUp.Text = arrayToString(binArray.GetRange(20, 4));
                BinarySeventhUp.Text = arrayToString(binArray.GetRange(24, 4));
                BinaryEightUp.Text = arrayToString(binArray.GetRange(28, 4));
            }
            catch
            {
                BinaryFirst.Text = string.Empty;
                BinarySecond.Text = string.Empty;
                BinaryThird.Text = string.Empty;
                BinaryFourth.Text = string.Empty;
                BinaryFifth.Text = string.Empty;
                BinarySixth.Text = string.Empty;
                BinarySeventh.Text = string.Empty;
                BinaryEight.Text = string.Empty;

                BinaryFirstUp.Text = string.Empty;
                BinarySecondUp.Text = string.Empty;
                BinaryThirdUp.Text = string.Empty;
                BinaryFourthUp.Text = string.Empty;
                BinaryFifthUp.Text = string.Empty;
                BinarySixthUp.Text = string.Empty;
                BinarySeventhUp.Text = string.Empty;
                BinaryEightUp.Text = string.Empty;
            }
        }

        private string arrayToString(List<int> arr)
        {
            string retString = string.Empty;
            foreach (int i in arr)
                retString += i.ToString();

            return retString;
        }

        #endregion

    }
}
