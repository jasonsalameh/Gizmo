using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Calculator.CalcViews;
using System.Globalization;

namespace Calculator
{
    public static class CalcHelpers
    {
        #region Helpers

        public static bool isDouble(string value)
        {
            if (value.Contains("."))
                return true;
            return false;
        }

        public async static void equalsKeyFocus(Button equalKey)
        {
            await equalKey.Dispatcher.RunAsync(CoreDispatcherPriority.High,  ()=>
            {
                equalKey.Focus(Windows.UI.Xaml.FocusState.Pointer);
            });
        }

        public static void cleanUpZeros(TextWindow window)
        {
            string temp = window.Get();
            if (temp.Length > 1 && temp[0] == '0' && temp[1] != '.')
            {
                window.RemoveFirst(false);
            }
        }

        // using PEMDAS
        // return : -x currentOp < prevOP
        //           0 currentOp == prevOp
        //           y currentOp > prevOp
        public static int compareOPs(string currentOP, string prevOP)
        {
            int returnValue = determineOPValue(prevOP) - determineOPValue(currentOP);
            return returnValue;
        }

        public static int determineOPValue(string op)
        {
            switch (op)
            {
                // Hack this is for the initial case where there is nothing previous
                case "":
                case EvaluationEngine.UnaryMinus:
                    return 30;
                case Calc_MainPage.OPEN_PARAN:
                case Calc_MainPage.CLOSE_PARAN:
                    return 0;
                case "^":
                    return 15;
                case "*":
                case "/":
                case "÷":
                case "×":

                // logical cases
                case "&":
                case "|":
                case "⊕":
                    return 10;
                case "+":
                case "-":
                    return 5;
                default:
                    return 0;
            }
        }

        public static string GetTextFromObject(object sender)
        {
            try
            {
                return (((sender as Button).Content as Viewbox).Child as TextBlock).Text;
            }
            catch
            {
                try
                {
                    return (((sender as ToggleButton).Content as Viewbox).Child as TextBlock).Text;
                }
                catch { }
            }
            return string.Empty;
        }

        public static double convertToRadians(double deg)
        {
            return deg * (Math.PI / 180);
        }

        public static double convertToDegrees(double rad)
        {
            return rad * (180 / Math.PI);
        }

        #endregion

        #region Conversion Functions

        // Long converstion
        public const string EXCEPTION_STR = "exception";

        public static string toDec(Calc_MainPage.CalcStateValues type, string exp, Programmer.ProgrammerCalcBits currentBits)
        {
            if (type == Calc_MainPage.CalcStateValues.Bin)
                return bin2Dec(exp, currentBits);
            else if (type == Calc_MainPage.CalcStateValues.Hex)
                return hex2Dec(exp, currentBits);
            else if (type == Calc_MainPage.CalcStateValues.Oct)
                return oct2Dec(exp, currentBits);

            return exp;
        }

        #region Dec To...

        public static string dec2Hex(string data, Programmer.ProgrammerCalcBits currentBits)
        {
            try
            {
                if (currentBits == Programmer.ProgrammerCalcBits.Qword)
                    return dec2Long(data, 16);
                else if (currentBits == Programmer.ProgrammerCalcBits.Dword)
                    return dec2Int(data, 16);
                else if (currentBits == Programmer.ProgrammerCalcBits.Word)
                    return dec2Short(data, 16);
                else if (currentBits == Programmer.ProgrammerCalcBits.Byte)
                    return dec2Byte(data, 16);
            }
            catch { }

            return string.Empty;
        }

        public static string dec2Oct(string data, Programmer.ProgrammerCalcBits currentBits)
        {
            try
            {
                if (currentBits == Programmer.ProgrammerCalcBits.Qword)
                    return dec2Long(data, 8);
                else if (currentBits == Programmer.ProgrammerCalcBits.Dword)
                    return dec2Int(data, 8);
                else if (currentBits == Programmer.ProgrammerCalcBits.Word)
                    return dec2Short(data, 8);
                else if (currentBits == Programmer.ProgrammerCalcBits.Byte)
                    return dec2Byte(data, 8);
            }
            catch { }

            return string.Empty;
        }

        public static string dec2Bin(string data, Programmer.ProgrammerCalcBits currentBits)
        {
            try
            {
                if (currentBits == Programmer.ProgrammerCalcBits.Qword)
                    return dec2Long(data, 2);
                else if (currentBits == Programmer.ProgrammerCalcBits.Dword)
                    return dec2Int(data, 2);
                else if (currentBits == Programmer.ProgrammerCalcBits.Word)
                    return dec2Short(data, 2);
                else if (currentBits == Programmer.ProgrammerCalcBits.Byte)
                    return dec2Byte(data, 2);
            }
            catch { }

            return string.Empty;
        }

        private static string dec2Byte(string data, int numBase)
        {
            try
            {
                // since convert doesn't support going smaller than word we'll 
                // initially convert to binary then clip off the top 8 bits


                if (numBase == 2)
                {
                    string binVal = Convert.ToString(sbyte.Parse(data), 2).PadLeft(16, '0').Substring(8);
                    return binVal;
                }
                else if (numBase == 16)
                    return sbyte.Parse(data).ToString("x", CultureInfo.InvariantCulture);
                else if (numBase == 10)
                {
                    int val = int.Parse(data);
                    if (val <= sbyte.MaxValue || val >= sbyte.MinValue)
                        return data;
                }
                else if (numBase == 8)
                {
                    int val = int.Parse(data);
                    return int.Parse(string.Format(@"{0}{1}{2}",
                            ((val >> 6) & 3),
                            ((val >> 3) & 7),
                            (val & 7)
                        )).ToString();
                }
            }
            catch { }

            return string.Empty;
        }

        private static string dec2Short(string data, int numBase)
        {
            try { return Convert.ToString(short.Parse(data), numBase); }
            catch { }

            return string.Empty;
        }

        private static string dec2Int(string data, int numBase)
        {
            try { return Convert.ToString(int.Parse(data), numBase); }
            catch { }

            return string.Empty;
        }

        public static string dec2Long(string data, int numBase)
        {
            try { return Convert.ToString(long.Parse(data), numBase); }
            catch (OverflowException) { return EXCEPTION_STR; }
            catch { }

            return string.Empty;
        }

        #endregion

        #region Hex To...

        public static string hex2Dec(string data, Programmer.ProgrammerCalcBits currentBits)
        {
            if (currentBits == Programmer.ProgrammerCalcBits.Qword)
                return hex2Long(data);
            else if (currentBits == Programmer.ProgrammerCalcBits.Dword)
                return hex2Int(data);
            else if (currentBits == Programmer.ProgrammerCalcBits.Word)
                return hex2Short(data);
            else if (currentBits == Programmer.ProgrammerCalcBits.Byte)
                return hex2Byte(data);

            return string.Empty;
        }

        private static string hex2Oct(string data, Programmer.ProgrammerCalcBits currentBits)
        {
            try { return hex2Dec(dec2Oct(data, currentBits), currentBits); }
            catch { }

            return string.Empty;
        }

        private static string hex2Bin(string data, Programmer.ProgrammerCalcBits currentBits)
        {
            try { return hex2Dec(dec2Bin(data, currentBits), currentBits); }
            catch { }

            return string.Empty;
        }


        private static string hex2Byte(string data)
        {
            try { return sbyte.Parse(data, System.Globalization.NumberStyles.HexNumber).ToString(); }
            catch { }

            return string.Empty;
        }

        private static string hex2Short(string data)
        {
            try { return short.Parse(data, System.Globalization.NumberStyles.HexNumber).ToString(); }
            catch { }

            return string.Empty;
        }

        private static string hex2Int(string data)
        {
            try { return int.Parse(data, System.Globalization.NumberStyles.HexNumber).ToString(); }
            catch { }

            return string.Empty;
        }

        public static string hex2Long(string data)
        {
            try { return long.Parse(data, System.Globalization.NumberStyles.HexNumber).ToString(); }
            catch (OverflowException) { return EXCEPTION_STR; }
            catch { }

            return string.Empty;
        }

        #endregion

        #region Oct To...

        public static string oct2Dec(string data, Programmer.ProgrammerCalcBits currentBits)
        {
            if (currentBits == Programmer.ProgrammerCalcBits.Qword)
                return oct2Long(data);
            else if (currentBits == Programmer.ProgrammerCalcBits.Dword)
                return oct2Int(data);
            else if (currentBits == Programmer.ProgrammerCalcBits.Word)
                return oct2Short(data);
            else if (currentBits == Programmer.ProgrammerCalcBits.Byte)
                return oct2Byte(data);

            return string.Empty;
        }

        private static string oct2Hex(string data, Programmer.ProgrammerCalcBits currentBits)
        {
            try { return dec2Hex(oct2Dec(data, currentBits), currentBits); }
            catch { }

            return string.Empty;
        }

        private static string oct2Bin(string data, Programmer.ProgrammerCalcBits currentBits)
        {
            try { return dec2Bin(oct2Dec(data, currentBits), currentBits); }
            catch { }

            return string.Empty;
        }

        private static string oct2Byte(string data)
        {
            try { return Convert.ToSByte(data, 8).ToString(); }
            catch { }

            return string.Empty;
        }

        private static string oct2Short(string data)
        {
            try { return Convert.ToInt16(data, 8).ToString(); }
            catch { }

            return string.Empty;
        }

        private static string oct2Int(string data)
        {
            try { return Convert.ToInt32(data, 8).ToString(); }
            catch { }

            return string.Empty;
        }

        public static string oct2Long(string data)
        {
            try { return Convert.ToInt64(data, 8).ToString(); }
            catch (OverflowException) { return EXCEPTION_STR; }
            catch { }

            return string.Empty;
        }

        #endregion

        #region Bin To...

        public static string bin2Dec(string data, Programmer.ProgrammerCalcBits currentBits)
        {
            if (currentBits == Programmer.ProgrammerCalcBits.Qword)
                return bin2Long(data);
            else if (currentBits == Programmer.ProgrammerCalcBits.Dword)
                return bin2Int(data);
            else if (currentBits == Programmer.ProgrammerCalcBits.Word)
                return bin2Short(data);
            else if (currentBits == Programmer.ProgrammerCalcBits.Byte)
                return bin2Byte(data);

            return string.Empty;
        }

        private static string bin2Hex(string data, Programmer.ProgrammerCalcBits currentBits)
        {
            try { return dec2Hex(bin2Dec(data, currentBits), currentBits); }
            catch { }

            return string.Empty;
        }

        private static string bin2Oct(string data, Programmer.ProgrammerCalcBits currentBits)
        {
            try { return dec2Oct(bin2Dec(data, currentBits), currentBits); }
            catch { }

            return string.Empty;
        }

        private static string bin2Byte(string data)
        {
            try { return Convert.ToSByte(data, 2).ToString(); }
            catch { }

            return string.Empty;
        }

        private static string bin2Short(string data)
        {
            try { return Convert.ToInt16(data, 2).ToString(); }
            catch { }

            return string.Empty;
        }

        private static string bin2Int(string data)
        {
            try { return Convert.ToInt32(data, 2).ToString(); }
            catch { }

            return string.Empty;
        }

        public static string bin2Long(string data)
        {
            try { return Convert.ToInt64(data, 2).ToString(); }
            catch (OverflowException) { return EXCEPTION_STR; }
            catch { }

            return string.Empty;
        }

        #endregion

        #endregion
    }
}
