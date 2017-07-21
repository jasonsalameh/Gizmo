using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Apps.UnitConverter
{
    class ComboUnit
    {
        private int idx;
        private char letter;
        public ComboUnit(int idx, char letter)
        {
            this.Index = idx;
            this.Letter = letter;
        }

        public int Index
        {
            get
            {
                return idx;
            }
            set
            {
                this.idx = value;
            }
        }

        public char Letter
        {
            get
            {
                return letter;
            }
            set
            {
                this.letter = value;
            }
        }
    }
}
