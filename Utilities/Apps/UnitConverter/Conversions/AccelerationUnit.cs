using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Apps.UnitConverter
{
    class AccelerationUnit : ConversionSet
    {

        public AccelerationUnit() : base(UnitConverter.ConversionTypes.acceleration, 5)
        {
            Initialize();
        }

        private void Initialize()
        {
            #region units
            //primary unit: kilometers/square second
            string[] acceleration = 
            {
                "centigal",
                "centimeter/square second",
                "decigal",
                "decimeter/square second",
                "dekameter/square second",
                "foot/square second",
                "g-unit (G)",
                "gal",
                "galileo",
                "gn",
                "grav",
                "hectometer/square second",
                "inch/square second",
                "kilometer/hour second",
                "kilometer/square second",
                "meter/square second",
                "mile/hour minute",
                "mile/hour second",
                "mile/square second",
                "milligal",
                "millimeter/square second"
            };
            this.Units = acceleration;
            #endregion

            #region conversions
            double[] accelerationConversions = 
            {
                10000, //centigal 0
                100, //centimeter/square second 1
                1000, //decigal 2
                10, //decimeter/square second 3
                0.1, //dekameter/square second 4
                3.280839895, //foot/square second 5
                0.1019716213, //g-unit (G) 6
                100, //gal 7
                100, //galileo 8
                0.1019716213, //gn 9
                0.1019716213, //grav 10
                0.01, //hectometer/square second 11
                39.37007874, //inch/square second 12
                3.6, //kilometer/hour second 13
                0.001, //kilometer/square second 14
                1, //meter/square second 15
                134.21617752, //mile/hour minute 16
                2.2369362921, //mile/hour second 17
                0.00062137119224, //mile/square second 18
                100000, //milligal 19
                1000 //millimeter/square second 20
            };
            this.Conversions = accelerationConversions;
            #endregion

            #region filters
            string[] accelerationFilters = 
            {
                "Metric", "Imperial"
            };
            this.Filters = accelerationFilters;
            #endregion

            #region masks
            int[] accelerationCommon = 
            {
                15, 1, 5, 12, 13, 14, 16, 17, 18, 20
            };
            int[] accelerationMetric = 
            {
                0, 1, 2, 3, 4, 11, 13, 14, 15, 19, 20
            };
            int[] accelerationImperial = 
            {
                5, 12, 16, 17, 18
            };
            Masks.Add(accelerationCommon);
            Masks.Add(accelerationMetric);
            Masks.Add(accelerationImperial);
            #endregion

        }
    }
}
