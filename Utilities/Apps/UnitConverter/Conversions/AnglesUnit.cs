using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Apps.UnitConverter
{
    class AnglesUnit : ConversionSet
    {
        public AnglesUnit() 
            : base(UnitConverter.ConversionTypes.angles, 6)
        {
            Initialize();   
        }

        private void Initialize()
        {
            #region units
            //primary value: radians
            string[] angles = 
            {
                "radian",
                "mil",
                "grad",
                "degree",
                "minute",
                "second",
                "point",
                "1/16 circle",
                "1/10 circle",
                "1/8 circle",
                "1/6 circle",
                "1/4 circle",
                "1/2 circle",
                "full circle "
            };
            this.Units = angles;
            #endregion

            #region conversions
            double[] anglesConversions = 
            {
                1, //radian 0
                1018.5916358, //mil 1
                63.661977237, //grad 2
                57.295779513, //degree 3
                3437.7467707, //minute 4
                206264.80625, //second 5
                5.0929581789, //point 6
                2.54647908951, //1/16 circle 7
                1.59154943091, //1/10 circle 8
                1.27323954471, //1/8 circle 9
                0.954929658551, //1/6 circle 10
                0.636619772371, //1/4 circle 11
                0.318309886182, //1/2 circle 12
                0.15915494309 //full circle  13
            };
            this.Conversions = anglesConversions;
            #endregion

            #region filters
            string[] angleFilters = { };
            this.Filters = angleFilters;
            #endregion

            #region masks
            int[] angleCommon = 
            { 
                3, 0, 1, 4, 5, 6
            };
            Masks.Add(angleCommon);
            #endregion
        }
    }
}
