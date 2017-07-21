using Calculator.CalcViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public interface ProgrammerView
    {
        Calc_MainPage.CalcStateValues calcState { get; set; }
        Programmer.ProgrammerCalcBits currentBits { get; set; }
        void Redraw(List<int> binArray);
        void ProgrammerSettings_Click(Calc_MainPage.CalcStateValues mode);
    }
}
