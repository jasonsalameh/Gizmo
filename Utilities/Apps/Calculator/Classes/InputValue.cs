using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class InputValue
    {
        public InputValue inputValue { get; set; }
        public TextWindow.InputType inputType { get; set; }
        public string inputData { get; set; }
        public bool hasParan { get; set; }

        public InputValue(TextWindow.InputType inputType, string inputData, bool hasParan)
        {
            this.inputData = inputData;
            this.inputType = inputType;
            this.hasParan = hasParan;

            inputValue = null;
        }

        //public InputValue(TextWindow.InputType inputType, InputValue inputValue)
        //{
        //    this.inputData = string.Empty;
        //    this.inputType = inputType;
        //    this.hasParan = false;

        //    inputValue = inputValue;
        //}

    }
}
