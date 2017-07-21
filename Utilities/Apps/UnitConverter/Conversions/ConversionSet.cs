using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Apps.UnitConverter
{
    //A conversion set contains a list of units (strings), conversions (doubles), masks (ints), filters(strings), a character for the app bar
    //and a combo box selection index (int)
    public class ConversionSet
    {
        string[] units, filters;
        double[] conversions;
        List<int[]> masks;
        UnitConverter.ConversionTypes type;
        int index;

        //This constructor must be called from all of the inheritors, then they need to initialize the values for each
        public ConversionSet(UnitConverter.ConversionTypes type, int index)
        {
            units = null;
            filters = null;
            conversions = null;
            masks = new List<int[]>();
            this.type = type;
            this.index = index;
        }

        #region Getters/Setters
        public int Index
        {
            get
            {
                return this.index;
            }
            private set
            {
                this.index = value;
            }
        }
        public UnitConverter.ConversionTypes Type
        {
            get
            {
                return this.type;
            }
            private set
            {
                this.type = value;
            }
        }
        public string[] Units
        {
            get
            {
                return this.units;
            }
            set
            {
                this.units = value;
            }
        }

        public string[] Filters 
        {
            get
            {
                return this.filters;
            }
            set
            {
                this.filters = value;
            }
        }

        public double[] Conversions
        {
            get
            {
                return this.conversions;
            }
            set
            {
                this.conversions = value;
            }
        }

        public List<int[]> Masks
        {
            get
            {
                return this.masks;
            }
            set
            {
                this.masks = value;
            }
        }
        #endregion

        public static ConversionSet GetSetByType(UnitConverter.ConversionTypes type)
        {
            switch (type)
            {
                case UnitConverter.ConversionTypes.distance:
                    return new LengthUnit();
                case UnitConverter.ConversionTypes.mass:
                    return new MassUnit();
                case UnitConverter.ConversionTypes.speed:
                    return new SpeedUnit();
                case UnitConverter.ConversionTypes.volume:
                    return new VolumeUnit();
                case UnitConverter.ConversionTypes.area:
                    return new AreaUnit();
                case UnitConverter.ConversionTypes.acceleration:
                    return new AccelerationUnit();
                case UnitConverter.ConversionTypes.angles:
                    return new AnglesUnit();
                case UnitConverter.ConversionTypes.current:
                    return new CurrentUnit();

                default:
                    return new LengthUnit();
            }
        }

        public static UnitConverter.ConversionTypes GetTypeByIndex(int index)
        {
            switch (index)
            {
                case 0:
                    return UnitConverter.ConversionTypes.distance;
                case 1:
                    return UnitConverter.ConversionTypes.mass;
                case 2:
                    return UnitConverter.ConversionTypes.speed;
                case 3:
                    return UnitConverter.ConversionTypes.volume;
                case 4:
                    return UnitConverter.ConversionTypes.area;
                case 5:
                    return UnitConverter.ConversionTypes.acceleration;
                case 6:
                    return UnitConverter.ConversionTypes.angles;
                case 7:
                    return UnitConverter.ConversionTypes.current;

                default:
                    return UnitConverter.ConversionTypes.distance;
            }
        }
    }
}
