using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Apps.UnitConverter
{
    class CurrentUnit : ConversionSet
    {
        public CurrentUnit() 
            : base(UnitConverter.ConversionTypes.current, 7)
        {
            Initialize();   
        }

        private void Initialize()
        {
            #region units
            //primary unit is amp
            string[] electricCurrent = 
            {
                "abampere",
                "ampere",
                "amp",
                "biot",
                "centiampere",
                "coulomb/second",
                "deciampere",
                "dekaampere",
                "electromagnetic unit of current",
                "electrostatic unit of current",
                "franklin/second",
                "gaussian electric current",
                "gigaampere",
                "gilbert",
                "hectoampere",
                "kiloampere",
                "megaampere",
                "microampere",
                "milliampere",
                "milliamp",
                "nanoampere",
                "picoampere",
                "siemens volt",
                "statampere",
                "teraampere",
                "volt/ohm",
                "watt/volt",
                "weber/henry"
            };
            this.Units = electricCurrent;
            #endregion

            #region conversions
            double[] electricCurrentConversions = 
            {
                0.1, //abampere 0
                1, //ampere 1
                1, //amp 2
                0.1, //biot 3
                100, //centiampere 4
                1, //coulomb/second 5
                10, //deciampere 6
                0.1, //dekaampere 7
                0.1, //electromagnetic unit of current 8
                2997924536.8, //electrostatic unit of current 9
                2997924536.8, //franklin/second 10
                2997924536.8, //gaussian electric current 11
                1.0 * Math.Pow(10, -9), //gigaampere 12
                1.2566370543, //gilbert 13
                0.01, //hectoampere 14
                0.001, //kiloampere 15
                0.000001, //megaampere 16
                1000000, //microampere 17
                1000, //milliampere 18
                1000, //milliamp 19
                1000000000, //nanoampere 20
                1000000000000, //picoampere 21
                1, //siemens volt 22
                2997924536.8, //statampere 23
                1.0 * Math.Pow(10, -12), //teraampere 24
                1, //volt/ohm 25
                1, //watt/volt 26
                1 //weber/henry 27
            };
            this.Conversions = electricCurrentConversions;
            #endregion

            #region filters
            string[] currentFilters = 
            {
                "Metric"
            };
            this.Filters = currentFilters;

            #endregion

            #region masks
            int[] currentCommon = 
            {
                2, 1, 5, 8, 9, 11, 15, 16, 17, 19, 25, 26
            };
            int[] currentMetric = 
            {
                1, 2, 4, 6, 7, 12, 14, 15, 16, 17, 18, 19, 20, 21, 23, 24
            };
            Masks.Add(currentCommon);
            Masks.Add(currentMetric);
            #endregion
        }
    }
}
