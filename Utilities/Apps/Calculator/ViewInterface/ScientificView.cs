using Calculator.CalcViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public interface ScientificView
    {
        Calc_MainPage.CalcStateValues scientificCalcState { get; set; }
    }
}
