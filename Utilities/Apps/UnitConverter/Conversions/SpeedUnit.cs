using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Apps.UnitConverter
{
    class SpeedUnit : ConversionSet
    {
        public SpeedUnit()
            : base(UnitConverter.ConversionTypes.speed, 2)
        {
            Initialize();
        }

        private void Initialize()
        {
            //primary value is meters/sec
            #region units
            string[] speed = 
            {
                "benz",
                "centimeter/day",
                "centimeter/hour",
                "centimeter/minute",
                "centimeter/second",
                "dekameter/day",
                "dekameter/hour",
                "dekameter/minute",
                "dekameter/second",
                "foot/day",
                "foot/hour",
                "foot/minute",
                "foot/second",
                "furlong/day [survey]",
                "furlong/fortnight [survey]",
                "furlong/hour [survey]",
                "furlong/minute [survey]",
                "furlong/second [survey]",
                "hectometer/day",
                "hectometer/hour",
                "hectometer/minute",
                "hectometer/second",
                "inch/day",
                "inch/hour",
                "inch/minute",
                "inch/second",
                "kilometer/day",
                "kilometer/hour",
                "kilometer/minute",
                "kilometer/second",
                "knot",
                "league/day [statute]",
                "league/hour [statute]",
                "league/minute [statute]",
                "league/second [statute]",
                "mach",
                "megameter/day",
                "megameter/hour",
                "megameter/minute",
                "megameter/second",
                "meter/day",
                "meter/hour",
                "meter/minute",
                "meter/second",
                "mile/day",
                "mile/hour",
                "mile/minute",
                "mile/second",
                "millimeter/day",
                "millimeter/hour",
                "millimeter/minute",
                "millimeter/second",
                "millimeter/microsecond",
                "millimeter/100 microsecond",
                "nautical mile/day",
                "nautical mile/hour",
                "nautical mile/minute",
                "nautical mile/second",
                "speed of light [air]",
                "speed of light [glass]",
                "speed of light [ice]",
                "speed of light [vacuum]",
                "speed of light [water]",
                "speed of sound [air]",
                "speed of sound [metal]",
                "speed of sound [water]",
                "yard/day",
                "yard/hour",
                "yard/minute",
                "yard/second"
            };

            this.Units = speed;
            #endregion

            #region conversions
            double[] speedConversions = 
            {
                1, //benz 0
                8640000.0001, //centimeter/day 1
                360000, //centimeter/hour 2
                5999.9999999, //centimeter/minute 3
                100, //centimeter/second 4
                8640.0000001, //dekameter/day 5
                360, //dekameter/hour 6
                5.9999999999, //dekameter/minute 7
                0.1, //dekameter/second 8
                283464.56693, //foot/day 9
                11811.023622, //foot/hour 10
                196.8503937, //foot/minute 11
                3.280839895, //foot/second 12
                429.49091407, //furlong/day [survey] 13
                6012.872797, //furlong/fortnight [survey] 14
                17.895454753, //furlong/hour [survey] 15
                0.29825757922, //furlong/minute [survey] 16
                0.0049709596537, //furlong/second [survey] 17
                864.00000001, //hectometer/day 18
                36, //hectometer/hour 19
                0.59999999999, //hectometer/minute 20
                0.01, //hectometer/second 21
                3401574.8032, //inch/day 22
                141732.28346, //inch/hour 23
                2362.2047244, //inch/minute 24
                39.37007874, //inch/second 25
                86.400000001, //kilometer/day 26
                3.6, //kilometer/hour 27
                0.059999999999, //kilometer/minute 28
                0.001, //kilometer/second 29
                1.9438444925, //knot 30
                17.895454383, //league/day [statute] 31
                0.74564393264, //league/hour [statute] 32
                0.012427398877, //league/minute [statute] 33
                0.00020712331461, //league/second [statute] 34
                0.002938669958, //mach 35
                0.086400000001, //megameter/day 36
                0.0036, //megameter/hour 37
                0.000059999999999, //megameter/minute 38
                0.000001, //megameter/second 39
                86400.000001, //meter/day 40
                3600, //meter/hour 41
                60, //meter/minute 42
                1, //meter/second 43
                53.686471008, //mile/day 44
                2.2369362921, //mile/hour 45
                0.037282271534, //mile/minute 46
                0.00062137119224, //mile/second 47
                86400000.001, //millimeter/day 48
                3600000, //millimeter/hour 49
                59999.999999, //millimeter/minute 50
                1000, //millimeter/second 51
                0.001, //millimeter/microsecond 52
                0.1, //millimeter/100 microsecond 53
                46.652267819, //nautical mile/day 54
                1.9438444925, //nautical mile/hour 55
                0.032397408207, //nautical mile/minute 56
                0.00053995680346, //nautical mile/second 57
                3.3366416469 * Math.Pow(10, -9), //speed of light [air] 58
                5.0034614447 * Math.Pow(10, -9), //speed of light [glass] 59
                4.3696896582 * Math.Pow(10, -9), //speed of light [ice] 60
                3.335640952 * Math.Pow(10, -9), //speed of light [vacuum] 61
                4.4364024692 * Math.Pow(10, -9), //speed of light [water] 62
                0.002938669958, //speed of sound [air] 63
                0.0002, //speed of sound [metal] 64
                0.00066666666667, //speed of sound [water] 65
                94488.188979, //yard/day 66
                3937.007874, //yard/hour 67
                65.6167979, //yard/minute 68
                1.0936132983 //yard/second 69
            };
            this.Conversions = speedConversions;
            #endregion

            #region filters
            string[] speedFilters = { };
            this.Filters = speedFilters;
            #endregion

            #region masks
            int[] speedCommon = 
            {
                43, 4, 12, 25, 27, 29, 30, 35, 45, 47, 51, 61, 63
            };
            this.Masks.Add(speedCommon);
            #endregion
        }
    }
}
