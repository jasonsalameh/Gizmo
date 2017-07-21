using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Apps.UnitConverter
{
    class AreaUnit : ConversionSet
    {
        public AreaUnit()
            : base(UnitConverter.ConversionTypes.area, 4)
        {
            Initialize();
        }

        private void Initialize()
        {
            //primary value is square meter
            #region units
            string[] area = 
            {
                "acre",
                "acre [suburbs]",
                "acre [survey]",
                "acre [Ireland]",
                "are",
                "arpent [Canada]",
                "barn",
                "bovate",
                "bunder",
                "caballeria [Spain/Peru]",
                "caballeria [Central America]",
                "caballeria [Cuba]",
                "carreau",
                "carucate",
                "cawney",
                "centiare",
                "cong",
                "cover",
                "cuerda",
                "dekare",
                "dessiatina",
                "dhur",
                "dunum, dunham",
                "fall [Scots]",
                "fall [English]",
                "fanega",
                "farthingdale",
                "hacienda",
                "hectare",
                "hide",
                "homestead",
                "hundred",
                "jerib",
                "jitro, joch, jutro",
                "jo [Japan]",
                "kappland",
                "kattha [Nepal]",
                "labor",
                "legua",
                "manzana [Costa Rican]",
                "manzana [Argentina]",
                "manzana [Nicaragua]",
                "morgen [Germany]",
                "morgen [South Africa]",
                "mu",
                "ngarn",
                "nook",
                "oxgang",
                "perch",
                "perche [Canada]",
                "ping",
                "pyong",
                "rai",
                "rood",
                "section",
                "shed",
                "sitio",
                "square",
                "square angstrom",
                "square astronomical unit",
                "square attometer",
                "square bicron",
                "square centimeter",
                "square chain [Gunter, survey]",
                "square chain [Ramden, Engineer]",
                "square city block [East U.S.]",
                "square city block [Midwest U.S.]",
                "square city block [South, West U.S.]",
                "square cubit",
                "square decimeter",
                "square dekameter",
                "square exameter",
                "square fathom",
                "square femtometer",
                "square fermi",
                "square foot",
                "square foot [survey]",
                "square furlong",
                "square gigameter",
                "square hectometer",
                "square inch",
                "square inch [survey]",
                "square kilometer",
                "square league [nautical]",
                "square league [U.S. statute]",
                "square light year",
                "square link [Gunter, survey]",
                "square link [Ramden, Engineer]",
                "square megameter",
                "square meter",
                "square microinch",
                "square micrometer",
                "square micromicron",
                "square micron",
                "square mil",
                "square mile",
                "square mile [nautical]",
                "square mile [survey, U.S. statute]",
                "square millimeter",
                "square millimicron",
                "square myriameter",
                "square nanometer",
                "square Paris foot",
                "square parsec",
                "square perch",
                "square perche",
                "square petameter",
                "square picometer",
                "square rod",
                "square tenthmeter",
                "square terameter",
                "square thou",
                "square vara [California]",
                "square vara [Texas]",
                "square yard",
                "square yard [survey]",
                "square yoctometer",
                "square yottameter",
                "stang",
                "stremma",
                "tarea",
                "tatami [Japan]",
                "tønde land",
                "township",
                "tsubo",
                "tunnland",
                "yard",
                "virgate"
            };
            this.Units = area;
            #endregion

            #region conversions
            double[] areaConversions = 
            {
                0.00024710538147, //acre 0
                0.00029899751158, //acre [suburbs] 1
                0.00024710439202, //acre [survey] 2
                0.0001525553013, //acre [Ireland] 3
                0.01, //are 4
                0.00029249259263, //arpent [Canada] 5
                1.0 * Math.Pow(10, 28), //barn 6
                0.000016666666667, //bovate 7
                0.0001, //bunder 8
                0.0000025, //caballeria [Spain/Peru] 9
                0.0000022222222222, //caballeria [Central America] 10
                0.0000074515648286, //caballeria [Cuba] 11
                0.000077519379845, //carreau 12
                0.0000020576131687, //carucate 13
                0.0001853290361, //cawney 14
                1, //centiare 15
                0.001, //cong 16
                0.00037064492216, //cover 17
                0.00025445292621, //cuerda 18
                0.001, //dekare 19
                0.000091533180778, //dessiatina 20
                0.059070234509, //dhur 21
                0.001, //dunum, dunham 22
                0.031104199067, //fall [Scots] 23
                0.021263023602, //fall [English] 24
                0.00015552099533, //fanega 25
                0.00098814229249, //farthingdale 26
                1.1160714286 * Math.Pow(10, -8), //hacienda 27
                0.0001, //hectare 28
                0.0000020576131687, //hide 29
                0.0000015444015444, //homestead 30
                2.0 * Math.Pow(10, -8), //hundred 31
                0.0005, //jerib 32
                0.00017376194613, //jitro, joch, jutro 33
                0.61728395062, //jo [Japan] 34
                0.0064825619085, //kappland 35
                0.0029585798817, //kattha [Nepal] 36
                0.0000013949919788, //labor 37
                5.5803571429 * Math.Pow(10, -8), //legua 38
                0.00014308280488, //manzana [Costa Rican] 39
                0.0001, //manzana [Argentina] 40
                0.00014196479273, //manzana [Nicaragua] 41
                0.0004, //morgen [Germany] 42
                0.0001167269756, //morgen [South Africa] 43
                0.0015, //mu 44
                0.0025, //ngarn 45
                0.000012355269142, //nook 46
                0.000016666666667, //oxgang 47
                0.039536861035, //perch 48
                0.029248318222, //perche [Canada] 49
                0.30257186082, //ping 50
                0.30248033878, //pyong 51
                0.000625, //rai 52
                0.00098842153134, //rood 53
                3.8610060971 * Math.Pow(10, -7), //section 54
                1.0 * Math.Pow(10, 51), //shed 55
                5.5555555556 * Math.Pow(10, -8), //sitio 56
                0.10763910417, //square 57
                10 * Math.Pow(10, 19), //square angstrom 58
                4.4683704831 * Math.Pow(10, -23), //square astronomical unit 59
                1.0 * Math.Pow(10, 36), //square attometer 60
                1.0 * Math.Pow(10, 24), //square bicron 61
                10000, //square centimeter 62
                0.0024710439365, //square chain [Gunter, survey] 63
                0.0010763867316, //square chain [Ramden, Engineer] 64
                0.00015444086342, //square city block [East U.S.] 65
                0.000098842152587, //square city block [Midwest U.S.] 66
                0.000038610215855, //square city block [South, West U.S.] 67
                4.7839601852, //square cubit 68
                100, //square decimeter 69
                0.01, //square dekameter 70
                1.0 * Math.Pow(10, -36), //square exameter 71
                0.2989963172, //square fathom 72
                1.0 * Math.Pow(10, 30), //square femtometer 73
                1.0 * Math.Pow(10, 30), //square fermi 74
                10.763910417, //square foot 75
                10.763867316, //square foot [survey] 76
                0.000024710439365, //square furlong 77
                1.0 * Math.Pow(10, -18), //square gigameter 78
                0.0001, //square hectometer 79
                1550.0031, //square inch 80
                1549.9968936, //square inch [survey] 81
                0.000001, //square kilometer 82
                3.2394816622 * Math.Pow(10, -8), //square league [nautical] 83
                4.2900068666 * Math.Pow(10, -8), //square league [U.S. statute] 84
                1.1172508764 * Math.Pow(10, -32), //square light year 85
                24.710439365, //square link [Gunter, survey] 86
                10.763867316, //square link [Ramden, Engineer] 87
                1.0 * Math.Pow(10, -12), //square megameter 88
                1, //square meter 89
                1550003100000000, //square microinch 90
                1000000000000, //square micrometer 91
                1.0 * Math.Pow(10, 24), //square micromicron 92
                1000000000000, //square micron 93
                1550003100, //square mil 94
                3.8610215855 * Math.Pow(10, -7), //square mile 95
                2.915533496 * Math.Pow(10, -7), //square mile [nautical] 96
                3.8610060971 * Math.Pow(10, -7), //square mile [survey, U.S. statute] 97
                1000000, //square millimeter 98
                1000000000000000000, //square millimicron 99
                1.0 * Math.Pow(10, -8), //square myriameter 100
                1000000000000000000, //square nanometer 101
                9.4786729858, //square Paris foot 102
                1.0502647576 * Math.Pow(10, -33), //square parsec 103
                0.039536702593, //square perch 104
                0.019580200501, //square perche 105
                1.0 * Math.Pow(10, -30), //square petameter 106
                1.0 * Math.Pow(10, 24), //square picometer 107
                0.039536702723, //square rod 108
                10 * Math.Pow(10, 19), //square tenthmeter 109
                1.0 * Math.Pow(10, -24), //square terameter 110
                1550003100, //square thou 111
                1.4233213046, //square vara [California] 112
                1.3949972136, //square vara [Texas] 113
                1.1959900463, //square yard 114
                1.1959852574, //square yard [survey] 115
                1.0 * Math.Pow(10, 48), //square yoctometer 116
                1.0 * Math.Pow(10, -48), //square yottameter 117
                0.00036913990402, //stang 118
                0.001, //stremma 119
                0.0015903307888, //tarea 120
                0.61728395062, //tatami [Japan] 121
                0.00018129079043, //tønde land 122
                1.0725017015 * Math.Pow(10, -8), //township 123
                0.30249863876, //tsubo 124
                0.0002025767766, //tunnland 125
                1.1959900463, //yard 126
                0.0000083333333333 //virgate 127
            };
            this.Conversions = areaConversions;
            #endregion

            #region filters
            string[] areaFilters = {};
            this.Filters = areaFilters;
            #endregion

            #region masks
            int[] areaCommon = 
            {
                89, 0, 28, 62, 75, 80, 82, 95, 98, 114
            };

            this.Masks.Add(areaCommon);
            #endregion
        }
    }
}
