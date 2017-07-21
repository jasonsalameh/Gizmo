using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Apps.UnitConverter
{
    class MassUnit : ConversionSet
    {
        public MassUnit()
            : base(UnitConverter.ConversionTypes.mass, 1)
        {
            Initialize();
        }

        private void Initialize()
        {
            //primary value is kilograms
            #region units
            string[] mass = 
            {
                "arratel, artel [Arab]",
                "arroba [Portugal]",
                "arroba [Spain]",
                "as, ass [Northern Europe]",
                "atomic mass unit [1960]",
                "atomic mass unit [1973]",
                "atomic mass unit [1986]",
                "atomic mass unit [1998]",
                "avogram",
                "bag [portland cement]",
                "baht [Thailand]",
                "bale [UK]",
                "bale [US]",
                "bismar pound [Denmark]",
                "candy [India]",
                "carat [international]",
                "carat [metric]",
                "carat [UK]",
                "carat [pre-1913 US]",
                "carga [Mexico]",
                "catti [China]",
                "catti [Japan]",
                "catty [China]",
                "catty [Japan, Thailand]",
                "cental",
                "centigram",
                "centner [Germany]",
                "centner [Russia]",
                "chalder, chaldron",
                "chin [China]",
                "chin [Japan]",
                "clove",
                "crith",
                "dalton",
                "dan [China]",
                "dan [Japan]",
                "decigram",
                "decitonne",
                "dekagram",
                "dekatonne",
                "denaro [Italy]",
                "denier [France]",
                "drachme",
                "dram",
                "dram [apothecaries]",
                "dyne",
                "electron",
                "electronvolt",
                "etto [Italy]",
                "exagram",
                "femtogram",
                "firkin [butter, soap]",
                "flask",
                "fother [lead]",
                "fotmal [lead]",
                "funt, funte [Russia]",
                "gamma",
                "gigaelectronvolt",
                "gigagram",
                "gigatonne",
                "gin [China]",
                "gin [Japan]",
                "grain",
                "gram",
                "gran [Germany]",
                "grano, grani [Italy]",
                "gros",
                "hectogram",
                "hundredweight [long, UK]",
                "hundredweight [short, US]",
                "hyl",
                "jin [China]",
                "jupiter",
                "kati [China]",
                "kati [Japan]",
                "keel [coal]",
                "keg [nails]",
                "kilodalton",
                "kilogram",
                "kilogram-force",
                "kiloton [long, UK]",
                "kiloton [short, US]",
                "kilotonne",
                "kin [Japan]",
                "kip",
                "koyan [Malaysia]",
                "kwan [Japan]",
                "last [Germany]",
                "last [US]",
                "last [US, wool]",
                "lb,lbs",
                "liang [China]",
                "libra [Italy]",
                "libra [Portugal, Spain]",
                "libra [ancient Rome]",
                "libra [metric]",
                "livre [France]",
                "long ton",
                "lot [Germany]",
                "mace [China]",
                "mahnd [Arab]",
                "marc [France]",
                "marco [Spanish]",
                "mark [English]",
                "mark [German]",
                "maund [India]",
                "maund [Pakistan]",
                "megadalton",
                "megagram",
                "megatonne",
                "mercantile pound",
                "metric ton",
                "mic",
                "microgram",
                "millidalton",
                "millier",
                "milligram",
                "millimass unit",
                "mina [Hebrew]",
                "momme [Japan]",
                "myriagram",
                "nanogram",
                "Newton",
                "obol, obolos, obolus [Greece]",
                "obolos [Ancient Greece]",
                "obolus [Ancient Rome]",
                "okka [Turkey]",
                "onça [Portuguese]",
                "once [France]",
                "oncia [Italy]",
                "onza [Spanish]",
                "ons [Dutch]",
                "ounce",
                "ounce-force",
                "ounce [troy]",
                "packen [Russia]",
                "pennyweight [troy]",
                "petagram",
                "pfund [Denmark, Germany]",
                "picogram",
                "point",
                "pond [Dutch]",
                "pound",
                "pound-force",
                "pound [metric]",
                "pound [troy]",
                "pud, pood [Russia]",
                "pund [Scandinavia]",
                "qian [China]",
                "qintar [Arab]",
                "quarter [UK]",
                "quarter [US]",
                "quarter (ton) [US]",
                "quartern",
                "quartern-loaf",
                "quintal [French]",
                "quintal [metric]",
                "quintal [Portugal]",
                "quintal [Spanish]",
                "rebah",
                "Robie",
                "rotl, rotel, rottle, ratel [Arab]",
                "sack [UK, wool]",
                "scruple [troy]",
                "seer [India]",
                "seer [Pakistan]",
                "shekel [Hebrew]",
                "short ton",
                "slinch",
                "slug",
                "stone",
                "tael, tahil [Japan]",
                "tahil [China]",
                "talent [Hebrew]",
                "tan [China]",
                "technische mass einheit (TME)",
                "teragram",
                "tetradrachm [Hebrew]",
                "tical [Asia]",
                "tod",
                "tola [India]",
                "tola [Pakistan]",
                "ton [long, UK]",
                "ton [metric]",
                "ton [short, US]",
                "tonelada [Portugal]",
                "tonelada [Spain]",
                "tonne",
                "tonneau [France]",
                "tovar [Bulgaria]",
                "troy ounce",
                "troy pound",
                "truss",
                "uncia [Rome]",
                "unze [Germany]",
                "vagon [Yugoslavia]",
                "yoctogram",
                "yottagram",
                "zentner [Germany]",
                "zeptogram",
                "zettagram"
            };
            this.Units = mass;
            
            #endregion

            #region conversions
            double[] massConversions = 
            {
                2, //arratel, artel [Arab] 0
                0.068073519401, //arroba [Portugal] 1
                0.086941401495, //arroba [Spain] 2
                19230.769231, //as, ass [Northern Europe] 3
                6.0229552895 * Math.Pow(10, 26), //atomic mass unit [1960] 4
                6.0220448998 * Math.Pow(10, 26), //atomic mass unit [1973] 5
                6.0221366517 * Math.Pow(10, 26), //atomic mass unit [1986] 6
                6.0221419828 * Math.Pow(10, 26), //atomic mass unit [1998] 7
                6.0221366517 * Math.Pow(10, 26), //avogram 8
                0.023453432147, //bag [portland cement] 9
                66.666666667, //baht [Thailand] 10
                0.0030619758637, //bale [UK] 11
                0.0045929637955, //bale [US] 12
                0.16686133823, //bismar pound [Denmark] 13
                0.003937007874, //candy [India] 14
                5000, //carat [international] 15
                5000, //carat [metric] 16
                3858.0895882, //carat [UK] 17
                4870.920604, //carat [pre-1913 US] 18
                0.0071428571429, //carga [Mexico] 19
                1.6532341393, //catti [China] 20
                1.6835016835, //catti [Japan] 21
                2, //catty [China] 22
                1.6666666667, //catty [Japan, Thailand] 23
                0.022046226218, //cental 24
                100000, //centigram 25
                0.02, //centner [Germany] 26
                0.01, //centner [Russia] 27
                0.00037139928394, //chalder, chaldron 28
                2, //chin [China] 29
                1.6666666667, //chin [Japan] 30
                0.31496062992, //clove 31
                11125.326806, //crith 32
                6.0221366517 * Math.Pow(10, 26), //dalton 33
                0.02, //dan [China] 34
                0.016666666667, //dan [Japan] 35
                10000, //decigram 36
                0.01, //decitonne 37
                100, //dekagram 38
                0.0001, //dekatonne 39
                909.09090909, //denaro [Italy] 40
                784.31372549, //denier [France] 41
                263.15789474, //drachme 42
                564.3833912, //dram 43
                257.20597255, //dram [apothecaries] 44
                980665.00286, //dyne 45
                1.0977693108 * Math.Pow(10, 30), //electron 46
                5.6095883572 * Math.Pow(10, 35), //electronvolt 47
                10, //etto [Italy] 48
                1.0 * Math.Pow(10, -15), //exagram 49
                1000000000000000000, //femtogram 50
                0.039368261104, //firkin [butter, soap] 51
                0.028818443804, //flask 52
                0.0010206586212, //fother [lead] 53
                0.030619758637, //fotmal [lead] 54
                2.442002442, //funt, funte [Russia] 55
                1000000000, //gamma 56
                5.6095883572 * Math.Pow(10, 26), //gigaelectronvolt 57
                0.000001, //gigagram 58
                1.0 * Math.Pow(10, -12), //gigatonne 59
                1.6666666667, //gin [China] 60
                1.6835016835, //gin [Japan] 61
                15432.358353, //grain 62
                1000, //gram 63
                1219.5121951, //gran [Germany] 64
                20387.359837, //grano, grani [Italy] 65
                261.50627615, //gros 66
                10, //hectogram 67
                0.019684130552, //hundredweight [long, UK] 68
                0.022046226218, //hundredweight [short, US] 69
                0.1019716213, //hyl 70
                2, //jin [China] 71
                5.2659294365 * Math.Pow(10, -28), //jupiter 72
                2, //kati [China] 73
                1.6666666667, //kati [Japan] 74
                0.000046424836207, //keel [coal] 75
                0.022046226218, //keg [nails] 76
                6.0221366517 * Math.Pow(10, 23), //kilodalton 77
                1, //kilogram 78
                1, //kilogram-force 79
                9.8420652761 * Math.Pow(10, -7), //kiloton [long, UK] 80
                0.0000011023113109, //kiloton [short, US] 81
                0.000001, //kilotonne 82
                1.6666666667, //kin [Japan] 83
                0.0022046226218, //kip 84
                0.00041339396445, //koyan [Malaysia] 85
                0.26666666667, //kwan [Japan] 86
                0.0005, //last [Germany] 87
                0.00055115565546, //last [US] 88
                0.0005047212962, //last [US, wool] 89
                2.2046226218, //lb,lbs 90
                20, //liang [China] 91
                2.9498525074, //libra [Italy] 92
                2.1786492375, //libra [Portugal, Spain] 93
                3.0959752322, //libra [ancient Rome] 94
                1, //libra [metric] 95
                2.0429009193, //livre [France] 96
                0.00098420652761, //long ton 97
                66.666666667, //lot [Germany] 98
                264.69031233, //mace [China] 99
                1.0806973637, //mahnd [Arab] 100
                4.0858018386, //marc [France] 101
                4.347826087, //marco [Spanish] 102
                4.4091710758, //mark [English] 103
                3.5650623886, //mark [German] 104
                0.026792268823, //maund [India] 105
                0.025, //maund [Pakistan] 106
                6.0221366517 * Math.Pow(10, 20), //megadalton 107
                0.001, //megagram 108
                1.0 * Math.Pow(10, -9), //megatonne 109
                2.1433929911, //mercantile pound 110
                0.001, //metric ton 111
                1000000000, //mic 112
                1000000000, //microgram 113
                6.0221366517 * Math.Pow(10, 29), //millidalton 114
                0.001, //millier 115
                1000000, //milligram 116
                6.0221366517 * Math.Pow(10, 29), //millimass unit 117
                2.004008016, //mina [Hebrew] 118
                266.66666667, //momme [Japan] 119
                0.1, //myriagram 120
                1000000000000, //nanogram 121
                9.8066500286, //Newton 122
                10000, //obol, obolos, obolus [Greece] 123
                2000, //obolos [Ancient Greece] 124
                1754.3859649, //obolus [Ancient Rome] 125
                0.78125, //okka [Turkey] 126
                34.855350296, //onça [Portuguese] 127
                32.690421706, //once [France] 128
                36.63003663, //oncia [Italy] 129
                34.855350296, //onza [Spanish] 130
                10, //ons [Dutch] 131
                35.27396195, //ounce 132
                35.27396195, //ounce-force 133
                32.150746569, //ounce [troy] 134
                0.002037531327, //packen [Russia] 135
                643.01493137, //pennyweight [troy] 136
                1.0 * Math.Pow(10, -12), //petagram 137
                2, //pfund [Denmark, Germany] 138
                1000000000000000, //picogram 139
                500000, //point 140
                2, //pond [Dutch] 141
                2.2046226218, //pound 142
                2.2046226218, //pound-force 143
                2, //pound [metric] 144
                2.6792288807, //pound [troy] 145
                0.061349693252, //pud, pood [Russia] 146
                2, //pund [Scandinavia] 147
                200, //qian [China] 148
                0.02, //qintar [Arab] 149
                0.078736522209, //quarter [UK] 150
                0.088184904874, //quarter [US] 151
                0.0044092452437, //quarter (ton) [US] 152
                0.62989217767, //quartern 153
                0.55115565546, //quartern-loaf 154
                0.020429009193, //quintal [French] 155
                0.01, //quintal [metric] 156
                0.017020697168, //quintal [Portugal] 157
                0.021786492375, //quintal [Spanish] 158
                350.26269702, //rebah 159
                100, //Robie 160
                2, //rotl, rotel, rottle, ratel [Arab] 161
                0.0060566555545, //sack [UK, wool] 162
                771.61791765, //scruple [troy] 163
                1.0716907529, //seer [India] 164
                1, //seer [Pakistan] 165
                87.565674256, //shekel [Hebrew] 166
                0.0011023113109, //short ton 167
                0.0057101471302, //slinch 168
                0.068521765562, //slug 169
                0.15747304442, //stone 170
                26.659557451, //tael, tahil [Japan] 171
                20, //tahil [China] 172
                0.033333333333, //talent [Hebrew] 173
                0.02, //tan [China] 174
                0.1019716213, //technische mass einheit (TME) 175
                1.0 * Math.Pow(10, -9), //teragram 176
                71.428571429, //tetradrachm [Hebrew] 177
                60.975609756, //tical [Asia] 178
                0.078736522209, //tod 179
                85.735260233, //tola [India] 180
                80, //tola [Pakistan] 181
                0.00098420652761, //ton [long, UK] 182
                0.001, //ton [metric] 183
                0.0011023113109, //ton [short, US] 184
                0.001260795562, //tonelada [Portugal] 185
                0.001087074682, //tonelada [Spain] 186
                0.001, //tonne 187
                0.0010214504597, //tonneau [France] 188
                0.0077639751553, //tovar [Bulgaria] 189
                32.150746569, //troy ounce 190
                2.6792288807, //troy pound 191
                0.039368261104, //truss 192
                36.646816308, //uncia [Rome] 193
                32, //unze [Germany] 194
                0.0001, //vagon [Yugoslavia] 195
                1.0 * Math.Pow(10, 27), //yoctogram 196
                1.0 * Math.Pow(10, -21), //yottagram 197
                0.02, //zentner [Germany] 198
                1.0 * Math.Pow(10, 24), //zeptogram 199
                1.0 * Math.Pow(10, -18) //zettagram 200
            };
            this.Conversions = massConversions;

            #endregion

            #region filters
            string[] massFilters = 
            {
                "Metric"
            };
            this.Filters = massFilters;
            #endregion

            #region masks
            int[] massCommon = 
            {
                78, 38, 43, 62, 63, 90, 111, 113, 116, 132, 142, 182, 183, 184, 187
            };

            int[] massMetric = 
            {
                25, 36, 38, 43, 62, 63, 67, 68, 69, 78, 90, 97, 108, 111, 113, 132, 142, 167, 182, 183, 184,
                187
            };
            this.Masks.Add(massCommon);
            this.Masks.Add(massMetric);
            #endregion
        }

    }
}
