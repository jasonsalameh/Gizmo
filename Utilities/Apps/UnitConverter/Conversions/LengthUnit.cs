using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Apps.UnitConverter
{
    class LengthUnit : ConversionSet
    {
        public LengthUnit() 
            : base(UnitConverter.ConversionTypes.distance, 0)
        {
            Initialize();   
        }

        //Initialize all of the member variables here
        private void Initialize()
        {
            //primary value is meters
            #region units
            string[] lengthUnits =  
            {
                "agate [Adobe]",
                "alen [Danish]",
                "alen [Scandinavia]",
                "alen [Swedish]",
                "angstrom",
                "arms-length",
                "arpent [Canada]",
                "arpent [France]",
                "arsheen [Russia]",
                "arshin [iran]",
                "arshin [iraq]",
                "astronomical unit",
                "attometer",
                "bamboo",
                "barleycorn",
                "bee space",
                "bicron",
                "block [East U.S.]",
                "block [Midwest U.S.]",
                "block [South, West U.S.]",
                "bohr",
                "braccio [Italy]",
                "braza [Argentina]",
                "braza [Spain]",
                "braza [Texas]",
                "button measure",
                "cable [U.S.]",
                "cable [British]",
                "caliber",
                "cana (canna, canne)",
                "cape foot",
                "cape inch",
                "cape rood",
                "centimeter",
                "chain [Gunter, survey]",
                "chain [Ramsden, engineer]",
                "ch'ih [China]",
                "chinese foot",
                "chinese inch",
                "chinese mile",
                "chinese yard",
                "city block [East U.S.]",
                "city block [Midwest U.S.]",
                "city block [South, West U.S.]",
                "click [U.S. military]",
                "cuadra",
                "cuadra [Argentina]",
                "cubit [Egyptian]",
                "cubit [Royal Egyptian]",
                "cubit [English]",
                "cubit [Roman]",
                "cuerda",
                "decimeter",
                "dekameter",
                "didot point",
                "digit",
                "diraa [Egypt]",
                "dong [Vietnam]",
                "douzième [watchmaking]",
                "douzième [print]",
                "dra [Iraq]",
                "dra [Russia]",
                "el [Dutch]",
                "ell [English]",
                "ell [Scotland]",
                "elle [Germany]",
                "elle [Vienna]",
                "em",
                "estadio [Portugal]",
                "estadio [Spain]",
                "exameter",
                "faden [Austria]",
                "faden [Switzerland]",
                "fall [English]",
                "fall [Scotland]",
                "fathom",
                "fathom [ancient]",
                "faust [Hungary]",
                "feet [pre-1963 Canada]",
                "feet [Egypt]",
                "feet [France]",
                "feet [international, U.S.]",
                "feet [iraq]",
                "feet [Netherlands]",
                "feet [Rome]",
                "feet [survey]",
                "femtometer",
                "fermi",
                "finger",
                "fingerbreadth",
                "fist",
                "fod",
                "foot [Egypt]",
                "foot [France]",
                "foot [international, U.S.]",
                "foot [iraq]",
                "foot [Netherlands]",
                "foot [Rome]",
                "foot [survey]",
                "football field [Canada]",
                "football field [U.S.]",
                "football field [U.S., complete]",
                "furlong [international]",
                "furlong [survey]",
                "fuss [German]",
                "gigameter",
                "gigaparsec",
                "gnat's eye",
                "goad",
                "gry",
                "hair's breadth",
                "hand [non-equine]",
                "handbreadth",
                "hat [Cambodia]",
                "hectometer",
                "heer",
                "hiro [Japan]",
                "hubble",
                "hvat [Croatia]",
                "inch [international, U.S.]",
                "iron",
                "ken [Japan]",
                "kerat",
                "kilofoot",
                "kilometer",
                "kiloparsec",
                "kiloyard",
                "kind",
                "klafter [Austria]",
                "klafter [Switzerland]",
                "klick",
                "kyu",
                "lap [old]",
                "lap [competition]",
                "lap [olympic pool]",
                "league [ancient Celtic]",
                "league [nautical]",
                "league [UK nautical]",
                "league [UK]",
                "league [US statute]",
                "leap",
                "legoa",
                "legua",
                "legua [Texas]",
                "legua [Spanish, pre-1568]",
                "legua [Spanish, post-1568]",
                "li [ancient China]",
                "li [imperial China]",
                "li [modern China]",
                "lieue [France]",
                "lieue [France, metric]",
                "lieue [France, nautical]",
                "light second",
                "light minute",
                "light hour",
                "light day",
                "light year [Julian]",
                "light year [tropical]",
                "light year [traditional]",
                "ligne [France]",
                "ligne [Swiss]",
                "line",
                "line [small]",
                "link [Gunter, survey]",
                "link [Ramden, engineer]",
                "lug",
                "lug [great]",
                "marathon",
                "mark twain",
                "megameter",
                "megaparsec",
                "meile [Austria]",
                "meile [geographische]",
                "meile [North Germany]",
                "meter",
                "metre",
                "metric mile",
                "metric mile [high school]",
                "microinch",
                "micrometer",
                "micromicron",
                "micron",
                "miglio",
                "miil (mijl) [Danish]",
                "miil (mijl) [Denmark]",
                "miil (mijl) [Sweden, ancient]",
                "mil",
                "mil [Sweden]",
                "mile [Britain, ancient]",
                "mile [Irish]",
                "mile [international]",
                "mile [nautical, international]",
                "mile [nautical, UK]",
                "mile [nautical, US]",
                "mile [Roman, ancient]",
                "mile [Scottish]",
                "mile [statute]",
                "mile [survey, US]",
                "milha [Portuguese]",
                "military pace",
                "military pace [double time]",
                "milla [Spanish]",
                "mille [French]",
                "milliare [Rome]",
                "millimeter",
                "millimicron",
                "mkono [Africa]",
                "moot [India]",
                "myriameter",
                "nail",
                "nanometer",
                "nanon",
                "pace [great]",
                "pace [Roman]",
                "palm [Dutch]",
                "palm [Britain, Roman minor]",
                "palm [US, Roman major]",
                "palmo [Portuguese]",
                "palmo [Spanish]",
                "palmo [Texas]",
                "parasang",
                "Paris foot",
                "parsec",
                "pe [Portuguese]",
                "pearl",
                "perch",
                "perch [Ireland]",
                "pertica",
                "pes",
                "petameter",
                "pica",
                "picometer",
                "pie [Argentina]",
                "pie [Italian]",
                "pie [Spanish]",
                "pie [Texas]",
                "pied de roi",
                "pik",
                "pike [Greece]",
                "point [Adobe]",
                "point [Britain, US]",
                "point [Didot]",
                "point [TeX]",
                "pole",
                "polegada [Portuguese]",
                "pouce",
                "pu [China]",
                "pulgada",
                "pygme [Greece]",
                "Q",
                "quadrant",
                "quarter",
                "quarter [cloth]",
                "quarter [print]",
                "range",
                "reed [Israel]",
                "ri [Japan]",
                "ridge",
                "river [Egypt]",
                "Robie",
                "rod [international]",
                "rod [survey]",
                "roede",
                "rood",
                "rope",
                "royal foot",
                "rute [Germany]",
                "sadzhen",
                "sagene",
                "Scots foot",
                "Scots mile",
                "seemeile",
                "shackle",
                "shaftment",
                "shaftment [ancient]",
                "shaku [Japan]",
                "siriometer",
                "smoot",
                "span",
                "spat",
                "stadium",
                "step",
                "stick",
                "story",
                "stride [great]",
                "stride [Roman]",
                "tenthmeter",
                "terameter",
                "thou",
                "toise",
                "township",
                "t'sun",
                "tu",
                "twain",
                "twip",
                "U",
                "vara [California]",
                "vara [Mexico]",
                "vara [Portuguese]",
                "vara [South America]",
                "vara [Spanish]",
                "vara [Texas]",
                "verge",
                "vershok",
                "verst",
                "wah [Thailand]",
                "werst",
                "X unit",
                "yard",
                "yoctometer",
                "yottameter",
                "zeptometer",
                "zettameter",
                "zoll [Germany]",
                "zoll [Switzerland]"
            };
            Units = lengthUnits;
            #endregion

            #region conversions
            double[] lengthConversions = 
            {
                2834.6456693, //agate [Adobe] 0
                1.5931177314, //alen [Danish] 1
                1.6666666667, //alen [Scandinavia] 2
                1.68406871, //alen [Swedish] 3
                10000000000, //angstrom 4
                1.4285714286, //arms-length 5
                0.017102787754, //arpent [Canada] 6
                0.017102405166, //arpent [France] 7
                1.4060742407, //arsheen [Russia] 8
                0.96153846154, //arshin [iran] 9
                0.013422818792, //arshin [iraq] 10
                6.6845871227 * Math.Pow(10, -12), //astronomical unit 11
                1000000000000000000, //attometer 12
                0.3125, //bamboo 13
                117.64705882, //barleycorn 14
                153.84615385, //bee space 15
                1000000000000, //bicron 16
                0.012427423845, //block [East U.S.] 17
                0.0099419390758, //block [Midwest U.S.] 18
                0.0062137119224, //block [South, West U.S.] 19
                18897161646, //bohr 20
                1.4285714286, //braccio [Italy] 21
                0.57703404501, //braza [Argentina] 22
                0.59880239521, //braza [Spain] 23
                0.59066745422, //braza [Texas] 24
                1574.8031496, //button measure 25
                0.0045567220764, //cable [U.S.] 26
                0.0053961182484, //cable [British] 27
                39.37007874, //caliber 28
                0.5, //cana (canna, canne) 29
                3.176034911, //cape foot 30
                38.112418931, //cape inch 31
                0.26466957591, //cape rood 32
                100, //centimeter 33
                0.049709595959, //chain [Gunter, survey] 34
                0.03280839895, //chain [Ramsden, engineer] 35
                2.792204166, //ch'ih [China] 36
                2.6919711959, //chinese foot 37
                26.919711959, //chinese inch 38
                0.0017946555159, //chinese mile 39
                1.121654665, //chinese yard 40
                0.012427423845, //city block [East U.S.] 41
                0.0099419390758, //city block [Midwest U.S.] 42
                0.0062137119224, //city block [South, West U.S.] 43
                0.001, //click [U.S. military] 44
                0.011904761905, //cuadra 45
                0.0076923076923, //cuadra [Argentina] 46
                2.2222222222, //cubit [Egyptian] 47
                1.9102196753, //cubit [Royal Egyptian] 48
                2.1872265967, //cubit [English] 49
                2.2522522523, //cubit [Roman] 50
                0.047619047619, //cuerda 51
                10, //decimeter 52
                0.1, //dekameter 53
                2652.5198939, //didot point 54
                52.631578947, //digit 55
                1.724137931, //diraa [Egypt] 56
                42.857142858, //dong [Vietnam] 57
                5319.1489362, //douzième [watchmaking] 58
                5669.2913385, //douzième [print] 59
                1.3422818792, //dra [Iraq] 60
                1.4060742407, //dra [Russia] 61
                1.4492753623, //el [Dutch] 62
                0.87489063867, //ell [English] 63
                1.0582010582, //ell [Scotland] 64
                1.6666666667, //elle [Germany] 65
                1.2832028744, //elle [Vienna] 66
                237.10630158, //em 67
                0.0038314176245, //estadio [Portugal] 68
                0.0057471264368, //estadio [Spain] 69
                1.0 * Math.Pow(10, -18), //exameter 70
                0.52728710783, //faden [Austria] 71
                0.55555555556, //faden [Switzerland] 72
                0.14581510645, //fall [English] 73
                0.17636684303, //fall [Scotland] 74
                0.54680664917, //fathom 75
                0.54674685621, //fathom [ancient] 76
                9.4912680334, //faust [Hungary] 77
                3.0769230769, //feet [pre-1963 Canada] 78
                2.7777777778, //feet [Egypt] 79
                3.0784329299, //feet [France] 80
                3.280839895, //feet [international, U.S.] 81
                3.164556962, //feet [iraq] 82
                3.5319464557, //feet [Netherlands] 83
                3.3783783784, //feet [Rome] 84
                3.2808333333, //feet [survey] 85
                1000000000000000, //femtometer 86
                1000000000000000, //fermi 87
                8.7489063867, //finger 88
                52.49343832, //fingerbreadth 89
                10, //fist 90
                3.1836994588, //fod 91
                2.7777777778, //foot [Egypt] 92
                3.0784329299, //foot [France] 93
                3.280839895, //foot [international, U.S.] 94
                3.164556962, //foot [iraq] 95
                3.5319464557, //foot [Netherlands] 96
                3.3783783784, //foot [Rome] 97
                3.2808333333, //foot [survey] 98
                0.0099419390758, //football field [Canada] 99
                0.010936132983, //football field [U.S.] 100
                0.0091134441528, //football field [U.S., complete] 101
                0.0049709695379, //furlong [international] 102
                0.0049709595959, //furlong [survey] 103
                3.1637560111, //fuss [German] 104
                1.0 * Math.Pow(10, -9), //gigameter 105
                3.2407788499 * Math.Pow(10, -26), //gigaparsec 106
                8000, //gnat's eye 107
                0.72907553223, //goad 108
                4724.4020088, //gry 109
                10000, //hair's breadth 110
                9.842519685, //hand [non-equine] 111
                12.5, //handbreadth 112
                2, //hat [Cambodia] 113
                0.01, //hectometer 114
                0.013670166229, //heer 115
                0.5500550055, //hiro [Japan] 116
                1.0570265842 * Math.Pow(10, -25), //hubble 117
                0.52728710783, //hvat [Croatia] 118
                39.37007874, //inch [international, U.S.] 119
                1889.7637795, //iron 120
                0.5500550055, //ken [Japan] 121
                34.965034965, //kerat 122
                0.003280839895, //kilofoot 123
                0.001, //kilometer 124
                3.24077927 * Math.Pow(10, -20), //kiloparsec 125
                0.0010936132983, //kiloyard 126
                2, //kind 127
                0.52728710783, //klafter [Austria] 128
                0.55555555556, //klafter [Switzerland] 129
                0.001, //klick 130
                4000, //kyu 131
                0.0024854847689, //lap [old] 132
                0.0025, //lap [competition] 133
                0.01, //lap [olympic pool] 134
                0.00043956043956, //league [ancient Celtic] 135
                0.00017998560115, //league [nautical] 136
                0.00017987060828, //league [UK nautical] 137
                0.00020712510356, //league [UK] 138
                0.00020712331461, //league [US statute] 139
                0.48605035482, //leap 140
                0.00016196692635, //legoa 141
                0.0002380952381, //legua 142
                0.00023621675249, //legua [Texas] 143
                0.00023926879456, //legua [Spanish, pre-1568] 144
                0.0001497005988, //legua [Spanish, post-1568] 145
                0.002, //li [ancient China] 146
                0.0015512293493, //li [imperial China] 147
                0.002, //li [modern China] 148
                0.00025654181632, //lieue [France] 149
                0.00025, //lieue [France, metric] 150
                0.00017998560115, //lieue [France, nautical] 151
                3.335640952 * Math.Pow(10, -9), //light second 152
                5.5594015866 * Math.Pow(10, -11), //light minute 153
                9.2656693111 * Math.Pow(10, -13), //light hour 154
                3.8606955463 * Math.Pow(10, -14), //light day 155
                1.057000834 * Math.Pow(10, -16), //light year [Julian] 156
                1.0570234105 * Math.Pow(10, -16), //light year [tropical] 157
                1.0577248072 * Math.Pow(10, -16), //light year [traditional] 158
                472.43350498, //ligne [France] 159
                443.26241135, //ligne [Swiss] 160
                472.43350498, //line 161
                1574.8031496, //line [small] 162
                4.9709595959, //link [Gunter, survey] 163
                3.280839895, //link [Ramden, engineer] 164
                0.19883878152, //lug 165
                0.15623047119, //lug [great] 166
                0.000023699497201, //marathon 167
                0.27340277144, //mark twain 168
                0.000001, //megameter 169
                3.24077927 * Math.Pow(10, -23), //megaparsec 170
                0.00013182177696, //meile [Austria] 171
                0.00013490361137, //meile [geographische] 172
                0.00013275804846, //meile [North Germany] 173
                1, //meter 174
                1, //metre 175
                0.00066666666667, //metric mile 176
                0.000625, //metric mile [high school] 177
                39370078.74, //microinch 178
                1000000, //micrometer 179
                1000000000000, //micromicron 180
                1000000, //micron 181
                0.00067177213489, //miglio 182
                0.00013333333333, //miil (mijl) [Danish] 183
                0.00013275804846, //miil (mijl) [Denmark] 184
                0.000093571629082, //miil (mijl) [Sweden, ancient] 185
                39370.07874, //mil 186
                0.0001, //mil [Sweden] 187
                0.00062150403978, //mile [Britain, ancient] 188
                0.00048828125, //mile [Irish] 189
                0.00062137119224, //mile [international] 190
                0.00053995680346, //mile [nautical, international] 191
                0.00053961182484, //mile [nautical, UK] 192
                0.00053995680346, //mile [nautical, US] 193
                0.00065789473684, //mile [Roman, ancient] 194
                0.00055126791621, //mile [Scottish] 195
                0.00062137119224, //mile [statute] 196
                0.00062136994949, //mile [survey, US] 197
                0.0004790878168, //milha [Portuguese] 198
                1.312335958, //military pace 199
                1.0936132983, //military pace [double time] 200
                0.0007183908046, //milla [Spanish] 201
                0.00051308363263, //mille [French] 202
                676.58998647, //milliare [Rome] 203
                1000, //millimeter 204
                1000000000, //millimicron 205
                2.1872265967, //mkono [Africa] 206
                13.12335958, //moot [India] 207
                0.0001, //myriameter 208
                17.497812773, //nail 209
                1000000000, //nanometer 210
                1000000000, //nanon 211
                0.656167979, //pace [great] 212
                0.67567567568, //pace [Roman] 213
                10, //palm [Dutch] 214
                13.333333333, //palm [Britain, Roman minor] 215
                4.3744531934, //palm [US, Roman major] 216
                4.5454545455, //palmo [Portuguese] 217
                5, //palmo [Spanish] 218
                4.7236655645, //palmo [Texas] 219
                0.00016666666667, //parasang 220
                3.0784329299, //Paris foot 221
                3.2407793877 * Math.Pow(10, -17), //parsec 222
                3.0008402353, //pe [Portuguese] 223
                569.0551238, //pearl 224
                0.19883878152, //perch 225
                0.15623047119, //perch [Ireland] 226
                0.33783783784, //pertica 227
                3.3704078193, //pes 228
                1.0 * Math.Pow(10, -15), //petameter 229
                237.10630158, //pica 230
                1000000000000, //picometer 231
                3.4614053306, //pie [Argentina] 232
                3.355704698, //pie [Italian] 233
                3.5893754487, //pie [Spanish] 234
                3.5435861091, //pie [Texas] 235
                3.0784329299, //pied de roi 236
                1.4084507042, //pik 237
                1.4084507042, //pike [Greece] 238
                2834.6456693, //point [Adobe] 239
                2857.1428571, //point [Britain, US] 240
                2652.5198939, //point [Didot] 241
                2845.2755907, //point [TeX] 242
                0.19883878152, //pole 243
                36.010082823, //polegada [Portuguese] 244
                36.941263391, //pouce 245
                0.55844083319, //pu [China] 246
                42.283298097, //pulgada 247
                2.8901734104, //pygme [Greece] 248
                4000, //Q 249
                9.998700169 * Math.Pow(10, -8), //quadrant 250
                0.0024854847689, //quarter 251
                4.3744531934, //quarter [cloth] 252
                4000, //quarter [print] 253
                0.00010356165825, //range 254
                0.37327360956, //reed [Israel] 255
                0.00025464731347, //ri [Japan] 256
                0.16201678494, //ridge 257
                0.0005, //river [Egypt] 258
                40, //Robie 259
                0.19883878152, //rod [international] 260
                0.19883838384, //rod [survey] 261
                0.1, //roede 262
                0.26466929572, //rood 263
                0.26466929572, //rope 264
                3.0784329299, //royal foot 265
                0.26666666667, //rute [Germany] 266
                0.46869141357, //sadzhen 267
                0.46869141357, //sagene 268
                3.2631750693, //Scots foot 269
                0.00055120714364, //Scots mile 270
                0.00053995680346, //seemeile 271
                0.036453776611, //shackle 272
                6.6120074054, //shaftment 273
                6.0606060606, //shaftment [ancient] 274
                3.300330033, //shaku [Japan] 275
                6.6845871535 * Math.Pow(10, -18), //siriometer 276
                0.58761311552, //smoot 277
                4.3744531934, //span 278
                1.0 * Math.Pow(10, -12), //spat 279
                0.0054054054054, //stadium 280
                1.312335958, //step 281
                0.3280839895, //stick 282
                0.30303030303, //story 283
                0.656167979, //stride [great] 284
                0.67567567568, //stride [Roman] 285
                10000000000, //tenthmeter 286
                1.0 * Math.Pow(10, -12), //terameter 287
                39370.07874, //thou 288
                0.51308363263, //toise 289
                0.00010356165825, //township 290
                27.932960894, //t'sun 291
                0.0000062061689319, //tu 292
                0.27340277144, //twain 293
                56692.556267, //twip 294
                22.497187852, //U 295
                1.1930302979, //vara [California] 296
                1.193288943, //vara [Mexico] 297
                0.90909090909, //vara [Portuguese] 298
                1.1574074074, //vara [South America] 299
                1.1963582854, //vara [Spanish] 300
                1.1811, //vara [Texas] 301
                1.0936132983, //verge 302
                22.497187852, //vershok 303
                0.00093738282715, //verst 304
                0.5, //wah [Thailand] 305
                0.00093738282715, //werst 306
                9979320851300, //X unit 307
                1.0936132983, //yard 308
                1.0 * Math.Pow(10, 24), //yoctometer 309
                1.0 * Math.Pow(10, -24), //yottameter 310
                1.0 * Math.Pow(10, 21), //zeptometer 311
                1.0 * Math.Pow(10, -21), //zettameter 312
                37.965072134, //zoll [Germany] 313
                33.333333333 //zoll [Switzerland] 314
            };
            Conversions = lengthConversions;
            #endregion

            #region filters
            string[] lengthFilters = 
            {
                "Metric", "Imperial"
            };
            this.Filters = lengthFilters;
            #endregion

            #region masks

            int[] lengthCommon = 
            {
                174, 33, 81, 119, 124, 190, 204, 308
            };
            int[] lengthMetric = 
            {
                4, 12, 33, 52, 53, 70, 86, 105, 114, 124, 169, 174, 179, 180, 181, 204, 205, 208, 210, 229, 231, 286, 287, 309, 310, 311, 312
            };
            int[] lengthImperial = 
            {
                26, 81, 119, 139, 190, 193, 308
            };
            Masks.Add(lengthCommon);
            Masks.Add(lengthMetric);
            Masks.Add(lengthImperial);

            #endregion
        }

        //For creating new ones:
        /*
         *             
         *  #region units

            #endregion

            #region conversions

            #endregion

            #region filters

            #endregion

            #region masks

            #endregion
         * */

    }
}
