using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Calculator
{
    public class TextWindow
    {
        // bitness constants
        private const int LONG = 64;
        private const int INT = 32;
        private const int SHORT = 16;
        private const int BYTE = 8;

        // Programmer mode + small window
        private bool SMALL_WINDOW_OUTPUT_ON_PROGRAMMER = false;

        // Calc bitness
        private const int MAX_BITNESS = LONG;

        // Window max chars this is JUST for scientific mode
        public const int MAX_CHARACTERS = 49;

        // Main page
        private Calc_MainPage _mainPage = null;

        // Current Page
        public CalculatorView currentView { get; set; }

        private bool _isProgrammerDec = false;
        public bool isProgrammerDec { set { _isProgrammerDec = value; } }

        private TextBlock _window = null;
        private bool _isMainWindow;
        private bool _clearOnNextUse;
        public bool clearOnNextUse 
        { 
            get { return _clearOnNextUse; } 
            set { _clearOnNextUse = value; } 
        }

        // for previous inputs
        public enum InputType { OperatorDouble = 1, OperatorSingle = 2, Int = 3, Double = 4, Rand = 5, Symbol = 5, Paran = 6, Negative = 7, NULL = 100 }
        private List<InputValue> _inputSequence = null;

        public TextWindow(TextBlock window, bool isMainWindow, Calc_MainPage mainPage)
        {
            _window = window;
            _inputSequence = new List<InputValue>();
            _clearOnNextUse = true;
            _isMainWindow = isMainWindow;
            _mainPage = mainPage;
        }

        #region Set/Add

        public void Set(string txt, InputType type, bool clearOnNextUse)
        {
            // clear input sequence
            _inputSequence.Clear();

            // add to input sequence both scientific and programmer
            AddInputSequence(txt, type, false, true);

            // update the screen text
            Redraw();

            // say the window has nothing in it
            _clearOnNextUse = clearOnNextUse;

            // update the recent list
            MakeRecent();
        }

        public void SetNoTranslation(string txt)
        {
            // clear input sequence
            _inputSequence.Clear();

            // add to input sequence both scientific and programmer
            AddInputSequence(txt, InputType.NULL, false, true);

            // update the screen text
            _window.Text = txt;

            // say the window has nothing in it
            _clearOnNextUse = true;

            // update the recent list
            MakeRecent();
        }

        public void Add(string txt, InputType type)
        {
            // if the user is trying to enter in more characters that what is supported
            if (_isMainWindow && GetTextFromList().Length > MAX_CHARACTERS)
                return;

            bool hasParan = false;
            if (_clearOnNextUse)
            {
                _inputSequence.Clear();
                _clearOnNextUse = false;
            }

            // if our current value and our last value are the "same"
            if (!_isMainWindow && (type == InputType.Int || type == InputType.Double) && (this.GetLastInputType() == InputType.Int || this.GetLastInputType() == InputType.Double))
            {
                // remove the previous element and now add a new one with updated text
                InputValue value = _inputSequence.Last();
                _inputSequence.Remove(_inputSequence.Last());
                txt = value.inputData + txt;
            }

            // if we have a single operator, simply remove the last inputvalue and change it
            else if (type == InputType.OperatorSingle)
            {
                // if this is isn't a paran than it's not part of a larger expression
                if (_inputSequence.Last().inputData != Calc_MainPage.CLOSE_PARAN)
                {
                    InputValue value = _inputSequence[_inputSequence.Count - 1];
                    _inputSequence.RemoveAt(_inputSequence.Count - 1);

                    //if (value.hasParan)
                    //    txt = txt + value.inputData;
                    //else

                    //_inputSequence.Add(new InputValue(InputType.OperatorSingle, txt, hasParan));
                    //_inputSequence.Add(new InputValue(InputType.OperatorSingle, Calc_MainPage.OPEN_PARAN, hasParan));
                    //_inputSequence.Add(value);
                    //_inputSequence.Add(new InputValue(InputType.OperatorSingle, Calc_MainPage.CLOSE_PARAN, hasParan));

                    txt = txt + Calc_MainPage.OPEN_PARAN + value.inputData + Calc_MainPage.CLOSE_PARAN;
                }

                // otherwise it's part of a larger expression like (2+3) or worse (((2+4)-5)*2)
                else
                {
                    // here we'll need to walk back up the inputsequence and determine where the 
                    // corresponding open paran is then use that entire expression with this one

                    // init with one since we know the last character is a paran
                    int closeParan = 1;

                    // we'll start this off at zero
                    int openParan = 0;

                    // as we pop off elements we'll add them to this list
                    Stack<InputValue> tempInputSequence = new Stack<InputValue>();

                    // add the last element to the new list
                    tempInputSequence.Push(_inputSequence.Last());
                    _inputSequence.Remove(_inputSequence.Last());

                    // while we have not found the first paran
                    while (openParan < closeParan)
                    {
                        // if it's an open or close paran
                        if (_inputSequence.Last().inputData == Calc_MainPage.OPEN_PARAN)
                            openParan++;
                        else if (_inputSequence.Last().inputData == Calc_MainPage.CLOSE_PARAN)
                            closeParan++;

                        // in all cases pop the value and add it to the list
                        tempInputSequence.Push(_inputSequence.Last());
                        _inputSequence.Remove(_inputSequence.Last());
                    }

                    // at this point we've got the full expression to be wrapped in the temp sequence

                    // first add the operator to the mix
                    //_inputSequence.Add(new InputValue(InputType.OperatorSingle, txt, hasParan));

                    // then push everything back to the input sequence
                    while (tempInputSequence.Count > 0)
                        txt += tempInputSequence.Pop().inputData;
                        //_inputSequence.Add();
                }
            }

            // add to input sequence
            AddInputSequence(txt, type, hasParan, false);

            // redraw grid
            Redraw();

            // update the recent list
            MakeRecent();
        }

        private void AddInputSequence(string txt, InputType type, bool hasParan, bool isSet)
        {
            // if we're in a programmer mode
            if (_isMainWindow && _mainPage.isProgrammer() && isValue(type))
            {
                // get our decimal converted value
                string dec = GetTextFromList();

                // first get the original expression
                string expOrig = GetExpression(dec, false);

                // add onto it the new value
                string expLong = expOrig + GetExpression(txt, true);

                //string binRep
                expOrig += GetExpression(txt, false);

                // get the new decimal expression
                dec = GetDecimal(expLong, true);

                // if there was an overflow exception converting out value
                if (dec == CalcViews.Programmer.EXCEPTION_STR)
                    return;
                else if (dec == string.Empty)
                    dec = "0";

                // for current bitness make sure the value isn't too big
                // Here we're right shifting since this stuff is signed
                if ((currentView as ProgrammerView).currentBits == Calculator.CalcViews.Programmer.ProgrammerCalcBits.Byte && (long.Parse(dec) >> 1) > sbyte.MaxValue ||
                    (currentView as ProgrammerView).currentBits == Calculator.CalcViews.Programmer.ProgrammerCalcBits.Word && (long.Parse(dec) >> 1) > short.MaxValue ||
                    (currentView as ProgrammerView).currentBits == Calculator.CalcViews.Programmer.ProgrammerCalcBits.Dword && (long.Parse(dec) >> 1) > int.MaxValue ||
                    (currentView as ProgrammerView).currentBits == Calculator.CalcViews.Programmer.ProgrammerCalcBits.Qword && (long.Parse(dec) >> 1) > long.MaxValue)
                    return;

                // now that we validated, get the real SIGNED value 
                dec = GetDecimal(expOrig, false);

                if (dec == string.Empty)
                    dec = "0";

                // clear input sequence
                _inputSequence.Clear();

                // re-add to the sequence
                _inputSequence.Add(new InputValue(InputType.Int, dec, hasParan));
            }

            // if we're in scientific or stat mode
            else 
                _inputSequence.Add(new InputValue(type, txt, hasParan));
        }

        #endregion

        #region Remove

        public bool RemoveLast()
        {
            if (_isMainWindow)
            {
                // is the window empty or have a 0 character in it
                if (_window.Text == string.Empty ||
                _inputSequence.Count <= 0 ||
                (_inputSequence.Count == 1 && _inputSequence.First().inputData == "0") ||
                GetLastInputType() == InputType.NULL)
                {
                    Set("0", InputType.Int, true);
                    return false;
                }


                // if we're in programmer mode we need to first validate which mode we're in
                // so that we can delete proper amounts of characters
                if (_mainPage.isProgrammer())
                {
                    string expression = GetExpression(GetTextFromList(), false);

                    // trim off the last character
                    expression = expression.Remove(expression.Length - 1);

                    Set(GetDecimal(expression, false), InputType.Int, false);
                }
                else
                {
                    // Remove the last element from the list
                    _inputSequence.RemoveAt(_inputSequence.Count - 1);

                    // if the main window had 1 char which was NOT "0"
                    if (_inputSequence.Count <= 0)
                        Set("0", InputType.Int, true);
                }
            }

            else
            {
                // is the window empty or have a 0 character in it
                if (_window.Text == string.Empty ||
                _inputSequence.Count <= 0 ||
                GetLastInputType() == InputType.NULL)
                {
                    Set(string.Empty, InputType.NULL, true);
                    return false;
                }

                // Remove the last element from the list
                _inputSequence.RemoveAt(_inputSequence.Count - 1);
            }

            // Set our current Window and type
            Redraw();

            // update the recent list
            MakeRecent();

            return true;
        }

        public bool RemoveFirst(bool makeRecent)
        {
            // As with all remove ops, check if it's empty first
            if (_window.Text == string.Empty || (_inputSequence.Count <= 1 && _window.Text.Length <= 1) || 
                GetLastInputType() == InputType.NULL)
            {
                if (_isMainWindow)
                    Set("0", InputType.Int, false);
                else
                    Set(string.Empty, InputType.NULL, true);

                return false;
            }

            // Remove the last element from the list
            if (_mainPage.isProgrammer())
            {
                // get our decimal converted value
                string dec = GetTextFromList();

                // first get the original expression
                string exp = GetExpression(dec, false);

                // add onto it the new value
                exp = exp.Substring(1);

                // get the new decimal expression
                dec = GetDecimal(exp, false);

                // re-add to the sequence
                Set(dec, InputType.Int, false);
            }
            else
                _inputSequence.RemoveAt(0);

            // Set our current Window and type
            Redraw();

            // update the recent list
            if(makeRecent)
                MakeRecent();

            return true;
        }

        #endregion

        #region Get

        private string GetExpression(string dec, bool asLong)
        {
            string exp = dec;

            if (!_mainPage.isProgrammer())
                return exp;

            if (asLong)
            {
                if ((currentView as ProgrammerView).calcState == Calc_MainPage.CalcStateValues.Hex)
                    exp = CalcViews.Programmer.dec2Long(dec, 16);
                else if ((currentView as ProgrammerView).calcState == Calc_MainPage.CalcStateValues.Oct)
                    exp = CalcViews.Programmer.dec2Long(dec, 8);
                else if ((currentView as ProgrammerView).calcState == Calc_MainPage.CalcStateValues.Bin)
                    exp = CalcViews.Programmer.dec2Long(dec, 2);
            }

            else
            {
                if ((currentView as ProgrammerView).calcState == Calc_MainPage.CalcStateValues.Hex)
                    exp = CalcHelpers.dec2Hex(dec, (currentView as ProgrammerView).currentBits);
                else if ((currentView as ProgrammerView).calcState == Calc_MainPage.CalcStateValues.Oct)
                    exp = CalcHelpers.dec2Oct(dec, (currentView as ProgrammerView).currentBits);
                else if ((currentView as ProgrammerView).calcState == Calc_MainPage.CalcStateValues.Bin)
                    exp = CalcHelpers.dec2Bin(dec, (currentView as ProgrammerView).currentBits);
            }

            return exp;
        }

        // always returns "64-bit" representation
        public List<int> GetBinary()
        {
            if (!_mainPage.isProgrammer())
                return new List<int>();

            string bin = CalcHelpers.dec2Bin(Get(), (currentView as ProgrammerView).currentBits).PadLeft(MAX_BITNESS, '0');

            List<int> binArray = new List<int>();

            foreach (char c in bin.ToCharArray())
                binArray.Add(int.Parse(c.ToString()));

            return binArray;
        }

        public string GetDecimal(List<int> bin)
        {
            if (!_mainPage.isProgrammer())
                return string.Empty;

            string exp = string.Empty;
            foreach (int b in bin)
                exp += b.ToString();

            return CalcHelpers.bin2Dec(exp, (currentView as ProgrammerView).currentBits);
        }

        private string GetDecimal(string exp, bool asLong)
        {
            string dec = exp;

            if (!_mainPage.isProgrammer())
                return dec;

            if (asLong)
            {
                if ((currentView as ProgrammerView).calcState == Calc_MainPage.CalcStateValues.Hex)
                    dec = CalcViews.Programmer.hex2Long(exp);
                else if ((currentView as ProgrammerView).calcState == Calc_MainPage.CalcStateValues.Oct)
                    dec = CalcViews.Programmer.oct2Long(exp);
                else if ((currentView as ProgrammerView).calcState == Calc_MainPage.CalcStateValues.Bin)
                    dec = CalcViews.Programmer.bin2Long(exp);
            }

            else
            {
                if ((currentView as ProgrammerView).calcState == Calc_MainPage.CalcStateValues.Hex)
                    dec = CalcHelpers.hex2Dec(exp, (currentView as ProgrammerView).currentBits);
                else if ((currentView as ProgrammerView).calcState == Calc_MainPage.CalcStateValues.Oct)
                    dec = CalcHelpers.oct2Dec(exp, (currentView as ProgrammerView).currentBits);
                else if ((currentView as ProgrammerView).calcState == Calc_MainPage.CalcStateValues.Bin)
                    dec = CalcHelpers.bin2Dec(exp, (currentView as ProgrammerView).currentBits);
            }

            return dec;
        }

        private string GetTextFromListForWindow()
        {
            // for the small window, only print stuff if we're in scientific mode
            if (!_isMainWindow)
            {
                // if we're not in programmer mode OR we are and the flag is set
                if (!_mainPage.isProgrammer() ||
                    (_mainPage.isProgrammer() && SMALL_WINDOW_OUTPUT_ON_PROGRAMMER))
                    return GetTextFromList();
                else
                    return string.Empty;
            }

            // if we're not in a normal numbers view return the normal text
            if (_mainPage.isProgrammer())
                return GetExpression(GetTextFromList(), false);

            // otherwise return scientific notation which include commas in the text
            else
                return GetTextFromList();
        }

        public string GetDecValueForWindow()
        {
            return GetTextFromList();
        }

        private string GetTextFromList()
        {
            string temp = string.Empty;

            foreach (InputValue v in _inputSequence)
                temp += v.inputData;

            return temp;
        }

        private void SeperateIntFromDecimal(out string intExp, out string decimalExp)
        {
            bool dotFound = false;
            intExp = decimalExp = string.Empty;

            foreach (InputValue v in _inputSequence)
            {
                if (v.inputData == ".")
                    dotFound = true;

                if (dotFound)
                    decimalExp += v.inputData;
                else
                    intExp += v.inputData;
            }
        }

        public string Get()
        {
            return GetTextFromList();
        }

        public string GetWindowText()
        {
            return GetTextFromListForWindow();
        }

        public string Get(int index)
        {
            return _inputSequence[index].inputData;
        }

        public InputValue GetLast()
        {
            return _inputSequence.Last();
        }

        public InputType GetLastInputType()
        {
            if (_inputSequence.Count > 0)
                return _inputSequence.Last().inputType;
            return InputType.NULL;
        }

        #endregion

        #region State

        public void MakeRecent()
        {
            Calc_MainPage.recentWindow = this;
        }

        public void Negate()
        {
            if (_inputSequence.Count > 0)
            {
                // first check to see if we're already negated
                if (_inputSequence[0].inputData.StartsWith("-"))
                {
                    // if this is the single negation operator remove it
                    if (_inputSequence[0].inputData.Length == 1)
                        _inputSequence.RemoveAt(0);
                    else
                    {
                        // get the old data and modify it
                        string data = _inputSequence[0].inputData.Substring(1);
                        InputType type = _inputSequence[0].inputType;
                        bool hasParan = _inputSequence[0].hasParan;

                        // remove it
                        _inputSequence.RemoveAt(0);

                        // re add to the list
                        _inputSequence.Insert(0, new InputValue(type, data, hasParan));
                    }
                }
                else
                    _inputSequence.Insert(0, new InputValue(InputType.Negative, "-", false));

                // redraw the content
                Redraw();
            }
        }

        public bool isDouble()
        {
            foreach (InputValue i in _inputSequence)
            {
                if (i.inputType == InputType.Double)
                    return true;
            }

            if (GetTextFromList().Contains("."))
                return true;
            return false;
        }

        public bool isValue()
        {
            InputType it = GetLastInputType();

            return isValue(it);
        }

        public bool isValue(InputType it)
        {
            return it == InputType.Int || it == InputType.Double || it == InputType.Rand || 
                   it == InputType.Symbol;
        }

        #endregion

        #region Redraw

        private string changeBitnessHelper(string bin, string dec, int MAX, CalcViews.Programmer.ProgrammerCalcBits currentBits, CalcViews.Programmer.ProgrammerCalcBits prevBits)
        {
            string retVal = string.Empty;

            // First we'll resize the bin string to have the right number of bits
            // per the prevBits value
            int prevMax = (prevBits == CalcViews.Programmer.ProgrammerCalcBits.Qword) ? LONG :
                (prevBits == CalcViews.Programmer.ProgrammerCalcBits.Dword) ? INT :
                (prevBits == CalcViews.Programmer.ProgrammerCalcBits.Word) ? SHORT : BYTE;

            int count = prevMax - bin.Length;
            for (int i = 0; i < count; i++)
                bin = "0" + bin;

            if (bin.StartsWith("1"))
            {
                count = MAX - bin.Length;
                string tempExp = string.Empty;

                for (int i = 0; i < count; i++)
                    tempExp += "1";

                return CalcHelpers.bin2Dec(tempExp + bin, (currentView as ProgrammerView).currentBits);
            }

            return dec;
        }

        public void ChangeBitness(CalcViews.Programmer.ProgrammerCalcBits prevBits)
        {
            if (!_mainPage.isProgrammer())
            {
                _mainPage.initCalcError();
                return;
            }

            string dec = Get();
            string bin = CalcViews.Programmer.dec2Long(dec,2);

            // next if the user has chosen a specific bitness
            if ((currentView as ProgrammerView).currentBits == CalcViews.Programmer.ProgrammerCalcBits.Qword)
            {
                // ASSUME we're comming from a lower order bit
                dec = changeBitnessHelper(bin, dec, LONG, (currentView as ProgrammerView).currentBits, prevBits);
            }
            else if ((currentView as ProgrammerView).currentBits == CalcViews.Programmer.ProgrammerCalcBits.Dword)
            {
                // if we're going down in bitness
                if (bin.Length > INT)
                {
                    bin = bin.Substring(bin.Length - INT);
                    dec = CalcHelpers.bin2Dec(bin, (currentView as ProgrammerView).currentBits);
                }

                // if we're going up in bitness
                else
                    dec = changeBitnessHelper(bin, dec, INT, (currentView as ProgrammerView).currentBits, prevBits);
            }
            else if ((currentView as ProgrammerView).currentBits == CalcViews.Programmer.ProgrammerCalcBits.Word)
            {
                // if we're going down in bitness
                if (bin.Length > SHORT)
                {
                    bin = bin.Substring(bin.Length - SHORT);
                    dec = CalcHelpers.bin2Dec(bin, (currentView as ProgrammerView).currentBits);
                }

                // if we're going up in bitness
                else
                    dec = changeBitnessHelper(bin, dec, SHORT, (currentView as ProgrammerView).currentBits, prevBits);
            }
            else if ((currentView as ProgrammerView).currentBits == CalcViews.Programmer.ProgrammerCalcBits.Byte)
            {
                // if we're going down in bitness
                if (bin.Length > BYTE)
                {
                    bin = bin.Substring(bin.Length - BYTE);
                    //dec = mainPage.bin2Dec(bin);
                }

                // can't go smaller than byte
                dec = CalcHelpers.bin2Dec(bin, (currentView as ProgrammerView).currentBits);
            }

            Set(dec, GetLastInputType(), true);
        }

        public void Redraw()
        {
            // TBD : BINARY
            _window.Text = GetTextFromListForWindow();

            if (_isMainWindow && _mainPage.isProgrammer())
            {
                List<int> binArray = GetBinary();

                (currentView as ProgrammerView).Redraw(binArray);
            }
        }

        #endregion
    }
}
