using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Apps.UnitConverter
{
    class VolumeUnit : ConversionSet
    {
        public VolumeUnit()
            : base(UnitConverter.ConversionTypes.volume, 3)
        {
            Initialize();
        }

        private void Initialize()
        {
            //primary value is cubic meters
            #region units
            string[] volume = 
            {
                "acre foot",
                "acre foot [US survey]",
                "acre inch",
                "barrel [UK, wine]",
                "barrel [UK]",
                "barrel [US, dry]",
                "barrel [US, federal]",
                "barrel [US, liquid]",
                "barrel [US, petroleum]",
                "board foot",
                "bucket [UK]",
                "bucket [US]",
                "bushel [UK]",
                "bushel [US, dry]",
                "centiliter",
                "cord [firewood]",
                "cord foot [timber]",
                "cubic centimeter",
                "cubic cubit [ancient egypt]",
                "cubic decimeter",
                "cubic dekameter",
                "cubic foot",
                "cubic inch",
                "cubic kilometer",
                "cubic meter",
                "cubic mile",
                "cubic micrometer",
                "cubic millimeter",
                "cubic yard",
                "cup [Canada]",
                "cup [metric]",
                "cup [US]",
                "deciliter",
                "dekaliter",
                "dram",
                "drum [US, petroleum]",
                "drum [metric, petroleum]",
                "fifth",
                "gallon [UK]",
                "gallon [US, dry]",
                "gallon [US, liquid]",
                "gill [UK]",
                "gill [US]",
                "hectare meter",
                "hectoliter",
                "hogshead [UK]",
                "hogshead [US]",
                "imperial gallon",
                "jigger",
                "kiloliter",
                "liter",
                "measure [ancient hebrew]",
                "megaliter",
                "microliter",
                "milliliter",
                "minim [UK]",
                "minim [US]",
                "ounce [UK, liquid]",
                "ounce [US, liquid]",
                "peck [UK]",
                "peck [US]",
                "pint [UK]",
                "pint [US, dry]",
                "pint [US, liquid]",
                "pipe [UK]",
                "pipe [US]",
                "pony",
                "quart [Germany]",
                "quart [ancient hebrew]",
                "quart [UK]",
                "quart [US, dry]",
                "quart [US, liquid]",
                "quarter [UK, liquid]",
                "Robie",
                "shot",
                "stere",
                "Tablespoon [metric]",
                "Tablespoon [UK]",
                "Tablespoon [UK]",
                "Teaspoon [metric]",
                "Teaspoon [UK]",
                "Teaspoon [US]",
                "yard"
            };
            this.Units = volume;
            #endregion

            #region conversions
            double[] volumeConversions = 
            {
                0.00081071318212, //acre foot 0
                0.00081070848625, //acre foot [US survey] 1
                0.0097285581853, //acre inch 2
                6.9831507397, //barrel [UK, wine] 3
                6.1102568972, //barrel [UK] 4
                8.6484898096, //barrel [US, dry] 5
                8.5216791086, //barrel [US, federal] 6
                8.3864143603, //barrel [US, liquid] 7
                6.2898107704, //barrel [US, petroleum] 8
                423.77600066, //board foot 9
                54.992312075, //bucket [UK] 10
                52.834410472, //bucket [US] 11
                27.496156037, //bushel [UK] 12
                28.377593071, //bushel [US, dry] 13
                100000, //centiliter 14
                0.27589582979, //cord [firewood] 15
                2.2071666383, //cord foot [timber] 16
                1000000, //cubic centimeter 17
                6.9444444444, //cubic cubit [ancient egypt] 18
                1000, //cubic decimeter 19
                0.001, //cubic dekameter 20
                35.314666721, //cubic foot 21
                61023.744095, //cubic inch 22
                1.0 * Math.Pow(10, -9), //cubic kilometer 23
                1, //cubic meter 24
                2.3991275858 * Math.Pow(10, -10), //cubic mile 25
                1000000000000000000, //cubic micrometer 26
                1000000000, //cubic millimeter 27
                1.3079506193, //cubic yard 28
                4399.384966, //cup [Canada] 29
                4000, //cup [metric] 30
                4226.7528377, //cup [US] 31
                10000, //deciliter 32
                100, //dekaliter 33
                270512.18162, //dram 34
                4.8031282247, //drum [US, petroleum] 35
                5, //drum [metric, petroleum] 36
                1320.8602618, //fifth 37
                219.9692483, //gallon [UK] 38
                227.02074457, //gallon [US, dry] 39
                264.17205236, //gallon [US, liquid] 40
                7039.0159456, //gill [UK] 41
                8453.5056755, //gill [US] 42
                0.0001, //hectare meter 43
                10, //hectoliter 44
                3.4915753698, //hogshead [UK] 45
                4.1932071803, //hogshead [US] 46
                219.9692483, //imperial gallon 47
                22542.681801, //jigger 48
                1, //kiloliter 49
                1000, //liter 50
                129.87012987, //measure [ancient hebrew] 51
                0.001, //megaliter 52
                1000000000, //microliter 53
                1000000, //milliliter 54
                16893638.269, //minim [UK] 55
                16230730.897, //minim [US] 56
                35195.079728, //ounce [UK, liquid] 57
                33814.022701, //ounce [US, liquid] 58
                109.98462415, //peck [UK] 59
                113.51037228, //peck [US] 60
                1759.7539864, //pint [UK] 61
                1816.1659565, //pint [US, dry] 62
                2113.3764189, //pint [US, liquid] 63
                2.0367522991, //pipe [UK] 64
                2.0966035902, //pipe [US] 65
                33814.022701, //pony 66
                873.331936, //quart [Germany] 67
                925.92592593, //quart [ancient hebrew] 68
                879.8769932, //quart [UK] 69
                908.08297826, //quart [US, dry] 70
                1056.6882094, //quart [US, liquid] 71
                3.4370195047, //quarter [UK, liquid] 72
                100000, //Robie 73
                33814.022701, //shot 74
                1, //stere 75
                66666.666667, //Tablespoon [metric] 76
                70390.159456, //Tablespoon [UK] 77
                70390.159456, //Tablespoon [UK] 78
                200000, //Teaspoon [metric] 79
                281560.63782, //Teaspoon [UK] 80
                202884.13621, //Teaspoon [US] 81
                1.3079506193 //yard 82
            };
            this.Conversions = volumeConversions;
            #endregion

            #region filters
            string[] volumeFilters = { };
            this.Filters = volumeFilters;
            #endregion

            #region masks
            int[] volumeCommon = 
            {
                24, 14, 15, 17, 21, 22, 23, 25, 27, 28, 37, 39, 40, 49, 50, 57, 58, 61, 62, 63, 76, 78, 79, 80, 81
            };
            this.Masks.Add(volumeCommon);
            #endregion
        }
    }
}
