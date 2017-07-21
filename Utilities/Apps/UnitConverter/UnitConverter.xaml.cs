using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.ApplicationSettings;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Utilities.Apps.UnitConverter
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UnitConverter : Page
    {

        #region constants yet to be added
        /* cooking
         * primary unit is liter
         * string[] cooking = 
            {
            "can",
            "can",
            "barrel [UK, wine]",
            "barrel [UK]",
            "barrel [US, dry]",
            "barrel [US, federal]",
            "barrel [US, liquid]",
            "bucket [UK]",
            "bucket [US]",
            "bushel [UK]",
            "bushel [US, dry]",
            "centiliter",
            "coffee spoon",
            "cubic centimeter",
            "cubic decimeter",
            "cubic foot",
            "cubic inch",
            "cubic meter",
            "cubic micrometer",
            "cubic millimeter",
            "cup [Canada]",
            "cup [metric]",
            "cup [US]",
            "dash",
            "deciliter",
            "dekaliter",
            "demi",
            "dram",
            "drop",
            "fifth",
            "gallon [UK]",
            "gallon [US, dry]",
            "gallon [US, liquid]",
            "gill [UK]",
            "gill [US]",
            "hectoliter",
            "hogshead [UK]",
            "hogshead [US]",
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
            "pinch",
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
            "Robie",
            "Robiespoon",
            "Robie Shot",
            "shot",
            "Tablespoon [metric]",
            "Tablespoon [UK]",
            "Tablespoon [US]",
            "Teaspoon [metric]",
            "Teaspoon [UK]",
            "Teaspoon [US]"
            };
            double[] cookingConversions = 
            {
            1.20764366792.5, //can 0
            0.3522294031410, //can 1
            0.0069831507397, //barrel [UK, wine] 2
            0.0061102568972, //barrel [UK] 3
            0.0086484898096, //barrel [US, dry] 4
            0.0085216791086, //barrel [US, federal] 5
            0.0083864143603, //barrel [US, liquid] 6
            0.054992312075, //bucket [UK] 7
            0.052834410472, //bucket [US] 8
            0.027496156037, //bushel [UK] 9
            0.028377593071, //bushel [US, dry] 10
            100, //centiliter 11
            405.76827242, //coffee spoon 12
            1000, //cubic centimeter 13
            1, //cubic decimeter 14
            0.035314666721, //cubic foot 15
            61.023744095, //cubic inch 16
            0.001, //cubic meter 17
            1000000000000000, //cubic micrometer 18
            1000000, //cubic millimeter 19
            4.399384966, //cup [Canada] 20
            4, //cup [metric] 21
            4.2267528377, //cup [US] 22
            1623.0730897, //dash 23
            10, //deciliter 24
            0.1, //dekaliter 25
            4, //demi 26
            270.51218162, //dram 27
            19476.877076, //drop 28
            1.3208602618, //fifth 29
            0.2199692483, //gallon [UK] 30
            0.22702074457, //gallon [US, dry] 31
            0.26417205236, //gallon [US, liquid] 32
            7.0390159456, //gill [UK] 33
            8.4535056755, //gill [US] 34
            0.01, //hectoliter 35
            0.0034915753698, //hogshead [UK] 36
            0.0041932071803, //hogshead [US] 37
            22.542681801, //jigger 38
            0.001, //kiloliter 39
            1, //liter 40
            0.12987012987, //measure [ancient hebrew] 41
            0.000001, //megaliter 42
            1000000, //microliter 43
            1000, //milliliter 44
            16893.638269, //minim [UK] 45
            16230.730897, //minim [US] 46
            35.195079728, //ounce [UK, liquid] 47
            33.814022702, //ounce [US, liquid] 48
            0.10998462415, //peck [UK] 49
            0.11351037228, //peck [US] 50
            3246.1461794, //pinch 51
            1.7597539864, //pint [UK] 52
            1.8161659565, //pint [US, dry] 53
            2.1133764189, //pint [US, liquid] 54
            0.0020367522991, //pipe [UK] 55
            0.0020966035902, //pipe [US] 56
            33.814022702, //pony 57
            0.873331936, //quart [Germany] 58
            0.92592592593, //quart [ancient hebrew] 59
            0.8798769932, //quart [UK] 60
            0.90808297826, //quart [US, dry] 61
            1.0566882094, //quart [US, liquid] 62
            100, //Robie 63
            100, //Robiespoon 64
            20, //Robie Shot 65
            33.814022702, //shot 66
            66.666666667, //Tablespoon [metric] 67
            70.390159456, //Tablespoon [UK] 68
            67.628045405, //Tablespoon [US] 69
            200, //Teaspoon [metric] 70
            281.56063782, //Teaspoon [UK] 71
            202.88413621 //Teaspoon [US] 72
            };

         * 
         * */

        /* density
         * primary value is kilogram/liter
         * string[] density = 
{
"attogram/attoliter",
"attogram/centiliter",
"attogram/cubic attometer",
"attogram/cubic centimeter",
"attogram/cubic decameter",
"attogram/cubic decimeter",
"attogram/cubic dekameter",
"attogram/cubic exameter",
"attogram/cubic femtometer",
"attogram/cubic gigameter",
"attogram/cubic hectometer",
"attogram/cubic kilometer",
"attogram/cubic megameter",
"attogram/cubic meter",
"attogram/cubic micrometer",
"attogram/cubic millimeter",
"attogram/cubic myriameter",
"attogram/cubic nanometer",
"attogram/cubic petameter",
"attogram/cubic picometer",
"attogram/cubic terameter",
"attogram/cubic yoctometer",
"attogram/cubic yottameter",
"attogram/cubic zeptometer",
"attogram/cubic zettameter",
"attogram/decaliter",
"attogram/deciliter",
"attogram/dekaliter",
"attogram/exaliter",
"attogram/femtoliter",
"attogram/gigaliter",
"attogram/hectoliter",
"attogram/kiloliter",
"attogram/liter",
"attogram/litre",
"attogram/megaliter",
"attogram/microliter",
"attogram/milliliter",
"attogram/myrialiter",
"attogram/nanoliter",
"attogram/petaliter",
"attogram/picoliter",
"attogram/teraliter",
"attogram/yoctoliter",
"attogram/yottaliter",
"attogram/zeptoliter",
"attogram/zettaliter",
"centigram/attoliter",
"centigram/centiliter",
"centigram/cubic attometer",
"centigram/cubic centimeter",
"centigram/cubic decameter",
"centigram/cubic decimeter",
"centigram/cubic dekameter",
"centigram/cubic exameter",
"centigram/cubic femtometer",
"centigram/cubic gigameter",
"centigram/cubic hectometer",
"centigram/cubic kilometer",
"centigram/cubic megameter",
"centigram/cubic meter",
"centigram/cubic micrometer",
"centigram/cubic millimeter",
"centigram/cubic myriameter",
"centigram/cubic nanometer",
"centigram/cubic petameter",
"centigram/cubic picometer",
"centigram/cubic terameter",
"centigram/cubic yoctometer",
"centigram/cubic yottameter",
"centigram/cubic zeptometer",
"centigram/cubic zettameter",
"centigram/decaliter",
"centigram/deciliter",
"centigram/dekaliter",
"centigram/exaliter",
"centigram/femtoliter",
"centigram/gigaliter",
"centigram/hectoliter",
"centigram/kiloliter",
"centigram/liter",
"centigram/litre",
"centigram/megaliter",
"centigram/microliter",
"centigram/milliliter",
"centigram/myrialiter",
"centigram/nanoliter",
"centigram/petaliter",
"centigram/picoliter",
"centigram/teraliter",
"centigram/yoctoliter",
"centigram/yottaliter",
"centigram/zeptoliter",
"centigram/zettaliter",
"decagram/attoliter",
"decagram/centiliter",
"decagram/cubic attometer",
"decagram/cubic centimeter",
"decagram/cubic decameter",
"decagram/cubic decimeter",
"decagram/cubic dekameter",
"decagram/cubic exameter",
"decagram/cubic femtometer",
"decagram/cubic gigameter",
"decagram/cubic hectometer",
"decagram/cubic kilometer",
"decagram/cubic megameter",
"decagram/cubic meter",
"decagram/cubic micrometer",
"decagram/cubic millimeter",
"decagram/cubic myriameter",
"decagram/cubic nanometer",
"decagram/cubic petameter",
"decagram/cubic picometer",
"decagram/cubic terameter",
"decagram/cubic yoctometer",
"decagram/cubic yottameter",
"decagram/cubic zeptometer",
"decagram/cubic zettameter",
"decagram/decaliter",
"decagram/deciliter",
"decagram/dekaliter",
"decagram/exaliter",
"decagram/femtoliter",
"decagram/gigaliter",
"decagram/hectoliter",
"decagram/kiloliter",
"decagram/liter",
"decagram/litre",
"decagram/megaliter",
"decagram/microliter",
"decagram/milliliter",
"decagram/myrialiter",
"decagram/nanoliter",
"decagram/petaliter",
"decagram/picoliter",
"decagram/teraliter",
"decagram/yoctoliter",
"decagram/yottaliter",
"decagram/zeptoliter",
"decagram/zettaliter",
"decigram/attoliter",
"decigram/centiliter",
"decigram/cubic attometer",
"decigram/cubic centimeter",
"decigram/cubic decameter",
"decigram/cubic decimeter",
"decigram/cubic dekameter",
"decigram/cubic exameter",
"decigram/cubic femtometer",
"decigram/cubic gigameter",
"decigram/cubic hectometer",
"decigram/cubic kilometer",
"decigram/cubic megameter",
"decigram/cubic meter",
"decigram/cubic micrometer",
"decigram/cubic millimeter",
"decigram/cubic myriameter",
"decigram/cubic nanometer",
"decigram/cubic petameter",
"decigram/cubic picometer",
"decigram/cubic terameter",
"decigram/cubic yoctometer",
"decigram/cubic yottameter",
"decigram/cubic zeptometer",
"decigram/cubic zettameter",
"decigram/decaliter",
"decigram/deciliter",
"decigram/dekaliter",
"decigram/exaliter",
"decigram/femtoliter",
"decigram/gigaliter",
"decigram/hectoliter",
"decigram/kiloliter",
"decigram/liter",
"decigram/litre",
"decigram/megaliter",
"decigram/microliter",
"decigram/milliliter",
"decigram/myrialiter",
"decigram/nanoliter",
"decigram/petaliter",
"decigram/picoliter",
"decigram/teraliter",
"decigram/yoctoliter",
"decigram/yottaliter",
"decigram/zeptoliter",
"decigram/zettaliter",
"dekagram/attoliter",
"dekagram/centiliter",
"dekagram/cubic attometer",
"dekagram/cubic centimeter",
"dekagram/cubic decameter",
"dekagram/cubic decimeter",
"dekagram/cubic dekameter",
"dekagram/cubic exameter",
"dekagram/cubic femtometer",
"dekagram/cubic gigameter",
"dekagram/cubic hectometer",
"dekagram/cubic kilometer",
"dekagram/cubic megameter",
"dekagram/cubic meter",
"dekagram/cubic micrometer",
"dekagram/cubic millimeter",
"dekagram/cubic myriameter",
"dekagram/cubic nanometer",
"dekagram/cubic petameter",
"dekagram/cubic picometer",
"dekagram/cubic terameter",
"dekagram/cubic yoctometer",
"dekagram/cubic yottameter",
"dekagram/cubic zeptometer",
"dekagram/cubic zettameter",
"dekagram/decaliter",
"dekagram/deciliter",
"dekagram/dekaliter",
"dekagram/exaliter",
"dekagram/femtoliter",
"dekagram/gigaliter",
"dekagram/hectoliter",
"dekagram/kiloliter",
"dekagram/liter",
"dekagram/litre",
"dekagram/megaliter",
"dekagram/microliter",
"dekagram/milliliter",
"dekagram/myrialiter",
"dekagram/nanoliter",
"dekagram/petaliter",
"dekagram/picoliter",
"dekagram/teraliter",
"dekagram/yoctoliter",
"dekagram/yottaliter",
"dekagram/zeptoliter",
"dekagram/zettaliter",
"exagram/attoliter",
"exagram/centiliter",
"exagram/cubic attometer",
"exagram/cubic centimeter",
"exagram/cubic decameter",
"exagram/cubic decimeter",
"exagram/cubic dekameter",
"exagram/cubic exameter",
"exagram/cubic femtometer",
"exagram/cubic gigameter",
"exagram/cubic hectometer",
"exagram/cubic kilometer",
"exagram/cubic megameter",
"exagram/cubic meter",
"exagram/cubic micrometer",
"exagram/cubic millimeter",
"exagram/cubic myriameter",
"exagram/cubic nanometer",
"exagram/cubic petameter",
"exagram/cubic picometer",
"exagram/cubic terameter",
"exagram/cubic yoctometer",
"exagram/cubic yottameter",
"exagram/cubic zeptometer",
"exagram/cubic zettameter",
"exagram/decaliter",
"exagram/deciliter",
"exagram/dekaliter",
"exagram/exaliter",
"exagram/femtoliter",
"exagram/gigaliter",
"exagram/hectoliter",
"exagram/kiloliter",
"exagram/liter",
"exagram/litre",
"exagram/megaliter",
"exagram/microliter",
"exagram/milliliter",
"exagram/myrialiter",
"exagram/nanoliter",
"exagram/petaliter",
"exagram/picoliter",
"exagram/teraliter",
"exagram/yoctoliter",
"exagram/yottaliter",
"exagram/zeptoliter",
"exagram/zettaliter",
"femtogram/attoliter",
"femtogram/centiliter",
"femtogram/cubic attometer",
"femtogram/cubic centimeter",
"femtogram/cubic decameter",
"femtogram/cubic decimeter",
"femtogram/cubic dekameter",
"femtogram/cubic exameter",
"femtogram/cubic femtometer",
"femtogram/cubic gigameter",
"femtogram/cubic hectometer",
"femtogram/cubic kilometer",
"femtogram/cubic megameter",
"femtogram/cubic meter",
"femtogram/cubic micrometer",
"femtogram/cubic millimeter",
"femtogram/cubic myriameter",
"femtogram/cubic nanometer",
"femtogram/cubic petameter",
"femtogram/cubic picometer",
"femtogram/cubic terameter",
"femtogram/cubic yoctometer",
"femtogram/cubic yottameter",
"femtogram/cubic zeptometer",
"femtogram/cubic zettameter",
"femtogram/decaliter",
"femtogram/deciliter",
"femtogram/dekaliter",
"femtogram/exaliter",
"femtogram/femtoliter",
"femtogram/gigaliter",
"femtogram/hectoliter",
"femtogram/kiloliter",
"femtogram/liter",
"femtogram/litre",
"femtogram/megaliter",
"femtogram/microliter",
"femtogram/milliliter",
"femtogram/myrialiter",
"femtogram/nanoliter",
"femtogram/petaliter",
"femtogram/picoliter",
"femtogram/teraliter",
"femtogram/yoctoliter",
"femtogram/yottaliter",
"femtogram/zeptoliter",
"femtogram/zettaliter",
"gigagram/attoliter",
"gigagram/centiliter",
"gigagram/cubic attometer",
"gigagram/cubic centimeter",
"gigagram/cubic decameter",
"gigagram/cubic decimeter",
"gigagram/cubic dekameter",
"gigagram/cubic exameter",
"gigagram/cubic femtometer",
"gigagram/cubic gigameter",
"gigagram/cubic hectometer",
"gigagram/cubic kilometer",
"gigagram/cubic megameter",
"gigagram/cubic meter",
"gigagram/cubic micrometer",
"gigagram/cubic millimeter",
"gigagram/cubic myriameter",
"gigagram/cubic nanometer",
"gigagram/cubic petameter",
"gigagram/cubic picometer",
"gigagram/cubic terameter",
"gigagram/cubic yoctometer",
"gigagram/cubic yottameter",
"gigagram/cubic zeptometer",
"gigagram/cubic zettameter",
"gigagram/decaliter",
"gigagram/deciliter",
"gigagram/dekaliter",
"gigagram/exaliter",
"gigagram/femtoliter",
"gigagram/gigaliter",
"gigagram/hectoliter",
"gigagram/kiloliter",
"gigagram/liter",
"gigagram/litre",
"gigagram/megaliter",
"gigagram/microliter",
"gigagram/milliliter",
"gigagram/myrialiter",
"gigagram/nanoliter",
"gigagram/petaliter",
"gigagram/picoliter",
"gigagram/teraliter",
"gigagram/yoctoliter",
"gigagram/yottaliter",
"gigagram/zeptoliter",
"gigagram/zettaliter",
"gigatonne/attoliter",
"gigatonne/centiliter",
"gigatonne/cubic attometer",
"gigatonne/cubic centimeter",
"gigatonne/cubic decameter",
"gigatonne/cubic decimeter",
"gigatonne/cubic dekameter",
"gigatonne/cubic exameter",
"gigatonne/cubic femtometer",
"gigatonne/cubic gigameter",
"gigatonne/cubic hectometer",
"gigatonne/cubic kilometer",
"gigatonne/cubic megameter",
"gigatonne/cubic meter",
"gigatonne/cubic micrometer",
"gigatonne/cubic millimeter",
"gigatonne/cubic myriameter",
"gigatonne/cubic nanometer",
"gigatonne/cubic petameter",
"gigatonne/cubic picometer",
"gigatonne/cubic terameter",
"gigatonne/cubic yoctometer",
"gigatonne/cubic yottameter",
"gigatonne/cubic zeptometer",
"gigatonne/cubic zettameter",
"gigatonne/decaliter",
"gigatonne/deciliter",
"gigatonne/dekaliter",
"gigatonne/exaliter",
"gigatonne/femtoliter",
"gigatonne/gigaliter",
"gigatonne/hectoliter",
"gigatonne/kiloliter",
"gigatonne/liter",
"gigatonne/litre",
"gigatonne/megaliter",
"gigatonne/microliter",
"gigatonne/milliliter",
"gigatonne/myrialiter",
"gigatonne/nanoliter",
"gigatonne/petaliter",
"gigatonne/picoliter",
"gigatonne/teraliter",
"gigatonne/yoctoliter",
"gigatonne/yottaliter",
"gigatonne/zeptoliter",
"gigatonne/zettaliter",
"grain/cubic foot",
"grain/cubic inch",
"grain/cubic mile",
"grain/cubic yard",
"grain/gallon [UK]",
"grain/gallon [US dry]",
"grain/gallon [US]",
"grain/ounce [UK]",
"grain/ounce [US]",
"grain/quart [UK]",
"grain/quart [US dry]",
"grain/quart [US]",
"gram/attoliter",
"gram/centiliter",
"gram/cubic attometer",
"gram/cubic centimeter",
"gram/cubic decameter",
"gram/cubic decimeter",
"gram/cubic dekameter",
"gram/cubic exameter",
"gram/cubic femtometer",
"gram/cubic gigameter",
"gram/cubic hectometer",
"gram/cubic kilometer",
"gram/cubic megameter",
"gram/cubic meter",
"gram/cubic micrometer",
"gram/cubic millimeter",
"gram/cubic myriameter",
"gram/cubic nanometer",
"gram/cubic petameter",
"gram/cubic picometer",
"gram/cubic terameter",
"gram/cubic yoctometer",
"gram/cubic yottameter",
"gram/cubic zeptometer",
"gram/cubic zettameter",
"gram/decaliter",
"gram/deciliter",
"gram/dekaliter",
"gram/exaliter",
"gram/femtoliter",
"gram/gigaliter",
"gram/hectoliter",
"gram/kiloliter",
"gram/liter",
"gram/litre",
"gram/megaliter",
"gram/microliter",
"gram/milliliter",
"gram/myrialiter",
"gram/nanoliter",
"gram/petaliter",
"gram/picoliter",
"gram/teraliter",
"gram/yoctoliter",
"gram/yottaliter",
"gram/zeptoliter",
"gram/zettaliter",
"hectogram/attoliter",
"hectogram/centiliter",
"hectogram/cubic attometer",
"hectogram/cubic centimeter",
"hectogram/cubic decameter",
"hectogram/cubic decimeter",
"hectogram/cubic dekameter",
"hectogram/cubic exameter",
"hectogram/cubic femtometer",
"hectogram/cubic gigameter",
"hectogram/cubic hectometer",
"hectogram/cubic kilometer",
"hectogram/cubic megameter",
"hectogram/cubic meter",
"hectogram/cubic micrometer",
"hectogram/cubic millimeter",
"hectogram/cubic myriameter",
"hectogram/cubic nanometer",
"hectogram/cubic petameter",
"hectogram/cubic picometer",
"hectogram/cubic terameter",
"hectogram/cubic yoctometer",
"hectogram/cubic yottameter",
"hectogram/cubic zeptometer",
"hectogram/cubic zettameter",
"hectogram/decaliter",
"hectogram/deciliter",
"hectogram/dekaliter",
"hectogram/exaliter",
"hectogram/femtoliter",
"hectogram/gigaliter",
"hectogram/hectoliter",
"hectogram/kiloliter",
"hectogram/liter",
"hectogram/litre",
"hectogram/megaliter",
"hectogram/microliter",
"hectogram/milliliter",
"",
"hectogram/nanoliter",
"hectogram/petaliter",
"hectogram/picoliter",
"hectogram/teraliter",
"hectogram/yoctoliter",
"hectogram/yottaliter",
"hectogram/zeptoliter",
"hectogram/zettaliter",
"hundredweight/cubic foot [UK]",
"hundredweight/cubic foot [US]",
"hundredweight/cubic inch [UK]",
"hundredweight/cubic inch [US]",
"hundredweight/cubic mile [UK]",
"hundredweight/cubic mile [US]",
"hundredweight/cubic yard [UK]",
"hundredweight/cubic yard [US]",
"hundredweight/gallon [UK]",
"hundredweight/gallon [US dry]",
"hundredweight/gallon [US]",
"hundredweight/ounce [UK]",
"hundredweight/ounce [US]",
"hundredweight/quart [UK]",
"hundredweight/quart [US dry]",
"hundredweight/quart [US]",
"kilogram/attoliter",
"kilogram/centiliter",
"kilogram/cubic attometer",
"kilogram/cubic centimeter",
"kilogram/cubic decameter",
"kilogram/cubic decimeter",
"kilogram/cubic dekameter",
"kilogram/cubic exameter",
"kilogram/cubic femtometer",
"kilogram/cubic gigameter",
"kilogram/cubic hectometer",
"kilogram/cubic kilometer",
"kilogram/cubic megameter",
"kilogram/cubic meter",
"kilogram/cubic micrometer",
"kilogram/cubic millimeter",
"kilogram/cubic myriameter",
"kilogram/cubic nanometer",
"kilogram/cubic petameter",
"kilogram/cubic picometer",
"kilogram/cubic terameter",
"kilogram/cubic yoctometer",
"kilogram/cubic yottameter",
"kilogram/cubic zeptometer",
"kilogram/cubic zettameter",
"kilogram/decaliter",
"kilogram/deciliter",
"kilogram/dekaliter",
"kilogram/exaliter",
"kilogram/femtoliter",
"kilogram/gigaliter",
"kilogram/hectoliter",
"kilogram/kiloliter",
"kilogram/liter",
"kilogram/litre",
"kilogram/megaliter",
"kilogram/microliter",
"kilogram/milliliter",
"kilogram/myrialiter",
"kilogram/nanoliter",
"kilogram/petaliter",
"kilogram/picoliter",
"kilogram/teraliter",
"kilogram/yoctoliter",
"kilogram/yottaliter",
"kilogram/zeptoliter",
"kilogram/zettaliter",
"kiloton/cubic foot [UK]",
"kiloton/cubic foot [US]",
"kiloton/cubic inch [UK]",
"kiloton/cubic inch [US]",
"kiloton/cubic mile [UK]",
"kiloton/cubic mile [US]",
"kiloton/cubic yard [UK]",
"kiloton/cubic yard [US]",
"kiloton/gallon [UK]",
"kiloton/gallon [US dry]",
"kiloton/gallon [US]",
"kiloton/ounce [UK]",
"kiloton/ounce [US]",
"kiloton/quart [UK]",
"kiloton/quart [US dry]",
"kiloton/quart [US]",
"kilotonne/attoliter",
"kilotonne/centiliter",
"kilotonne/cubic attometer",
"kilotonne/cubic centimeter",
"kilotonne/cubic decameter",
"kilotonne/cubic decimeter",
"kilotonne/cubic dekameter",
"kilotonne/cubic exameter",
"kilotonne/cubic femtometer",
"kilotonne/cubic gigameter",
"kilotonne/cubic hectometer",
"kilotonne/cubic kilometer",
"kilotonne/cubic megameter",
"kilotonne/cubic meter",
"kilotonne/cubic micrometer",
"kilotonne/cubic millimeter",
"kilotonne/cubic myriameter",
"kilotonne/cubic nanometer",
"kilotonne/cubic petameter",
"kilotonne/cubic picometer",
"kilotonne/cubic terameter",
"kilotonne/cubic yoctometer",
"kilotonne/cubic yottameter",
"kilotonne/cubic zeptometer",
"kilotonne/cubic zettameter",
"kilotonne/decaliter",
"kilotonne/deciliter",
"kilotonne/dekaliter",
"kilotonne/exaliter",
"kilotonne/femtoliter",
"kilotonne/gigaliter",
"kilotonne/hectoliter",
"kilotonne/kiloliter",
"kilotonne/liter",
"kilotonne/litre",
"kilotonne/megaliter",
"kilotonne/microliter",
"kilotonne/milliliter",
"kilotonne/myrialiter",
"kilotonne/nanoliter",
"kilotonne/petaliter",
"kilotonne/picoliter",
"kilotonne/teraliter",
"kilotonne/yoctoliter",
"kilotonne/yottaliter",
"kilotonne/zeptoliter",
"kilotonne/zettaliter",
"megagram/attoliter",
"megagram/centiliter",
"megagram/cubic attometer",
"megagram/cubic centimeter",
"megagram/cubic decameter",
"megagram/cubic decimeter",
"megagram/cubic dekameter",
"megagram/cubic exameter",
"megagram/cubic femtometer",
"megagram/cubic gigameter",
"megagram/cubic hectometer",
"megagram/cubic kilometer",
"megagram/cubic megameter",
"megagram/cubic meter",
"megagram/cubic micrometer",
"megagram/cubic millimeter",
"megagram/cubic myriameter",
"megagram/cubic nanometer",
"megagram/cubic petameter",
"megagram/cubic picometer",
"megagram/cubic terameter",
"megagram/cubic yoctometer",
"megagram/cubic yottameter",
"megagram/cubic zeptometer",
"megagram/cubic zettameter",
"megagram/decaliter",
"megagram/deciliter",
"megagram/dekaliter",
"megagram/exaliter",
"megagram/femtoliter",
"megagram/gigaliter",
"megagram/hectoliter",
"megagram/kiloliter",
"megagram/liter",
"megagram/litre",
"megagram/megaliter",
"megagram/microliter",
"megagram/milliliter",
"megagram/myrialiter",
"megagram/nanoliter",
"megagram/petaliter",
"megagram/picoliter",
"megagram/teraliter",
"megagram/yoctoliter",
"megagram/yottaliter",
"megagram/zeptoliter",
"megagram/zettaliter",
"megatonne/attoliter",
"megatonne/centiliter",
"megatonne/cubic attometer",
"megatonne/cubic centimeter",
"megatonne/cubic decameter",
"megatonne/cubic decimeter",
"megatonne/cubic dekameter",
"megatonne/cubic exameter",
"megatonne/cubic femtometer",
"megatonne/cubic gigameter",
"megatonne/cubic hectometer",
"megatonne/cubic kilometer",
"megatonne/cubic megameter",
"megatonne/cubic meter",
"megatonne/cubic micrometer",
"megatonne/cubic millimeter",
"megatonne/cubic myriameter",
"megatonne/cubic nanometer",
"megatonne/cubic petameter",
"megatonne/cubic picometer",
"megatonne/cubic terameter",
"megatonne/cubic yoctometer",
"megatonne/cubic yottameter",
"megatonne/cubic zeptometer",
"megatonne/cubic zettameter",
"megatonne/decaliter",
"megatonne/deciliter",
"megatonne/dekaliter",
"megatonne/exaliter",
"megatonne/femtoliter",
"megatonne/gigaliter",
"megatonne/hectoliter",
"megatonne/kiloliter",
"megatonne/liter",
"megatonne/litre",
"megatonne/megaliter",
"megatonne/microliter",
"megatonne/milliliter",
"megatonne/myrialiter",
"megatonne/nanoliter",
"megatonne/petaliter",
"megatonne/picoliter",
"megatonne/teraliter",
"megatonne/yoctoliter",
"megatonne/yottaliter",
"megatonne/zeptoliter",
"megatonne/zettaliter",
"microgram/attoliter",
"microgram/centiliter",
"microgram/cubic attometer",
"microgram/cubic centimeter",
"microgram/cubic decameter",
"microgram/cubic decimeter",
"microgram/cubic dekameter",
"microgram/cubic exameter",
"microgram/cubic femtometer",
"microgram/cubic gigameter",
"microgram/cubic hectometer",
"microgram/cubic kilometer",
"microgram/cubic megameter",
"microgram/cubic meter",
"microgram/cubic micrometer",
"microgram/cubic millimeter",
"microgram/cubic myriameter",
"microgram/cubic nanometer",
"microgram/cubic petameter",
"microgram/cubic picometer",
"microgram/cubic terameter",
"microgram/cubic yoctometer",
"microgram/cubic yottameter",
"microgram/cubic zeptometer",
"microgram/cubic zettameter",
"microgram/decaliter",
"microgram/deciliter",
"microgram/dekaliter",
"microgram/exaliter",
"microgram/femtoliter",
"microgram/gigaliter",
"microgram/hectoliter",
"microgram/kiloliter",
"microgram/liter",
"microgram/litre",
"microgram/megaliter",
"microgram/microliter",
"microgram/milliliter",
"microgram/myrialiter",
"microgram/nanoliter",
"microgram/petaliter",
"microgram/picoliter",
"microgram/teraliter",
"microgram/yoctoliter",
"microgram/yottaliter",
"microgram/zeptoliter",
"microgram/zettaliter",
"milligram/attoliter",
"milligram/centiliter",
"milligram/cubic attometer",
"milligram/cubic centimeter",
"milligram/cubic decameter",
"milligram/cubic decimeter",
"milligram/cubic dekameter",
"milligram/cubic exameter",
"milligram/cubic femtometer",
"milligram/cubic gigameter",
"milligram/cubic hectometer",
"milligram/cubic kilometer",
"milligram/cubic megameter",
"milligram/cubic meter",
"milligram/cubic micrometer",
"milligram/cubic millimeter",
"milligram/cubic myriameter",
"milligram/cubic nanometer",
"milligram/cubic petameter",
"milligram/cubic picometer",
"milligram/cubic terameter",
"milligram/cubic yoctometer",
"milligram/cubic yottameter",
"milligram/cubic zeptometer",
"milligram/cubic zettameter",
"milligram/decaliter",
"milligram/deciliter",
"milligram/dekaliter",
"milligram/exaliter",
"milligram/femtoliter",
"milligram/gigaliter",
"milligram/hectoliter",
"milligram/kiloliter",
"milligram/liter",
"milligram/litre",
"milligram/megaliter",
"milligram/microliter",
"milligram/milliliter",
"milligram/myrialiter",
"milligram/nanoliter",
"milligram/petaliter",
"milligram/picoliter",
"milligram/teraliter",
"milligram/yoctoliter",
"milligram/yottaliter",
"milligram/zeptoliter",
"milligram/zettaliter",
"myriagram/attoliter",
"myriagram/centiliter",
"myriagram/cubic attometer",
"myriagram/cubic centimeter",
"myriagram/cubic decameter",
"myriagram/cubic decimeter",
"myriagram/cubic dekameter",
"myriagram/cubic exameter",
"myriagram/cubic femtometer",
"myriagram/cubic gigameter",
"myriagram/cubic hectometer",
"myriagram/cubic kilometer",
"myriagram/cubic megameter",
"myriagram/cubic meter",
"myriagram/cubic micrometer",
"myriagram/cubic millimeter",
"myriagram/cubic myriameter",
"myriagram/cubic nanometer",
"myriagram/cubic petameter",
"myriagram/cubic picometer",
"myriagram/cubic terameter",
"myriagram/cubic yoctometer",
"myriagram/cubic yottameter",
"myriagram/cubic zeptometer",
"myriagram/cubic zettameter",
"myriagram/decaliter",
"myriagram/deciliter",
"myriagram/dekaliter",
"myriagram/exaliter",
"myriagram/femtoliter",
"myriagram/gigaliter",
"myriagram/hectoliter",
"myriagram/kiloliter",
"myriagram/liter",
"myriagram/litre",
"myriagram/megaliter",
"myriagram/microliter",
"myriagram/milliliter",
"myriagram/myrialiter",
"myriagram/nanoliter",
"myriagram/petaliter",
"myriagram/picoliter",
"myriagram/teraliter",
"myriagram/yoctoliter",
"myriagram/yottaliter",
"myriagram/zeptoliter",
"myriagram/zettaliter",
"nanogram/attoliter",
"nanogram/centiliter",
"nanogram/cubic attometer",
"nanogram/cubic centimeter",
"nanogram/cubic decameter",
"nanogram/cubic decimeter",
"nanogram/cubic dekameter",
"nanogram/cubic exameter",
"nanogram/cubic femtometer",
"nanogram/cubic gigameter",
"nanogram/cubic hectometer",
"nanogram/cubic kilometer",
"nanogram/cubic megameter",
"nanogram/cubic meter",
"nanogram/cubic micrometer",
"nanogram/cubic millimeter",
"nanogram/cubic myriameter",
"nanogram/cubic nanometer",
"nanogram/cubic petameter",
"nanogram/cubic picometer",
"nanogram/cubic terameter",
"nanogram/cubic yoctometer",
"nanogram/cubic yottameter",
"nanogram/cubic zeptometer",
"nanogram/cubic zettameter",
"nanogram/decaliter",
"nanogram/deciliter",
"nanogram/dekaliter",
"nanogram/exaliter",
"nanogram/femtoliter",
"nanogram/gigaliter",
"nanogram/hectoliter",
"nanogram/kiloliter",
"nanogram/liter",
"nanogram/litre",
"nanogram/megaliter",
"nanogram/microliter",
"nanogram/milliliter",
"nanogram/myrialiter",
"nanogram/nanoliter",
"nanogram/petaliter",
"nanogram/picoliter",
"nanogram/teraliter",
"nanogram/yoctoliter",
"nanogram/yottaliter",
"nanogram/zeptoliter",
"nanogram/zettaliter",
"ounce/cubic foot",
"ounce/cubic inch",
"ounce/cubic mile",
"ounce/cubic yard",
"ounce/gallon [UK]",
"ounce/gallon [US dry]",
"ounce/gallon [US]",
"ounce/ounce [UK]",
"ounce/ounce [US]",
"ounce/quart [UK]",
"ounce/quart [US dry]",
"ounce/quart [US]",
"petagram/attoliter",
"petagram/centiliter",
"petagram/cubic attometer",
"petagram/cubic centimeter",
"petagram/cubic decameter",
"petagram/cubic decimeter",
"petagram/cubic dekameter",
"petagram/cubic exameter",
"petagram/cubic femtometer",
"petagram/cubic gigameter",
"petagram/cubic hectometer",
"petagram/cubic kilometer",
"petagram/cubic megameter",
"petagram/cubic meter",
"petagram/cubic micrometer",
"petagram/cubic millimeter",
"petagram/cubic myriameter",
"petagram/cubic nanometer",
"petagram/cubic petameter",
"petagram/cubic picometer",
"petagram/cubic terameter",
"petagram/cubic yoctometer",
"petagram/cubic yottameter",
"petagram/cubic zeptometer",
"petagram/cubic zettameter",
"petagram/decaliter",
"petagram/deciliter",
"petagram/dekaliter",
"petagram/exaliter",
"petagram/femtoliter",
"petagram/gigaliter",
"petagram/hectoliter",
"petagram/kiloliter",
"petagram/liter",
"petagram/litre",
"petagram/megaliter",
"petagram/microliter",
"petagram/milliliter",
"petagram/myrialiter",
"petagram/nanoliter",
"petagram/petaliter",
"petagram/picoliter",
"petagram/teraliter",
"petagram/yoctoliter",
"petagram/yottaliter",
"petagram/zeptoliter",
"petagram/zettaliter",
"picogram/attoliter",
"picogram/centiliter",
"picogram/cubic attometer",
"picogram/cubic centimeter",
"picogram/cubic decameter",
"picogram/cubic decimeter",
"picogram/cubic dekameter",
"picogram/cubic exameter",
"picogram/cubic femtometer",
"picogram/cubic gigameter",
"picogram/cubic hectometer",
"picogram/cubic kilometer",
"picogram/cubic megameter",
"picogram/cubic meter",
"picogram/cubic micrometer",
"picogram/cubic millimeter",
"picogram/cubic myriameter",
"picogram/cubic nanometer",
"picogram/cubic petameter",
"picogram/cubic picometer",
"picogram/cubic terameter",
"picogram/cubic yoctometer",
"picogram/cubic yottameter",
"picogram/cubic zeptometer",
"picogram/cubic zettameter",
"picogram/decaliter",
"picogram/deciliter",
"picogram/dekaliter",
"picogram/exaliter",
"picogram/femtoliter",
"picogram/gigaliter",
"picogram/hectoliter",
"picogram/kiloliter",
"picogram/liter",
"picogram/litre",
"picogram/megaliter",
"picogram/microliter",
"picogram/milliliter",
"picogram/myrialiter",
"picogram/nanoliter",
"picogram/petaliter",
"picogram/picoliter",
"picogram/teraliter",
"picogram/yoctoliter",
"picogram/yottaliter",
"picogram/zeptoliter",
"picogram/zettaliter",
"pound/cubic foot",
"pound/cubic inch",
"pound/cubic mile",
"pound/cubic yard",
"pound/gallon [UK]",
"pound/gallon [US dry]",
"pound/gallon [US]",
"pound/ounce [UK]",
"pound/ounce [US]",
"pound/quart [UK]",
"pound/quart [US dry]",
"pound/quart [US]",
"teragram/attoliter",
"teragram/centiliter",
"teragram/cubic attometer",
"teragram/cubic centimeter",
"teragram/cubic decameter",
"teragram/cubic decimeter",
"teragram/cubic dekameter",
"teragram/cubic exameter",
"teragram/cubic femtometer",
"teragram/cubic gigameter",
"teragram/cubic hectometer",
"teragram/cubic kilometer",
"teragram/cubic megameter",
"teragram/cubic meter",
"teragram/cubic micrometer",
"teragram/cubic millimeter",
"teragram/cubic myriameter",
"teragram/cubic nanometer",
"teragram/cubic petameter",
"teragram/cubic picometer",
"teragram/cubic terameter",
"teragram/cubic yoctometer",
"teragram/cubic yottameter",
"teragram/cubic zeptometer",
"teragram/cubic zettameter",
"teragram/decaliter",
"teragram/deciliter",
"teragram/dekaliter",
"teragram/exaliter",
"teragram/femtoliter",
"teragram/gigaliter",
"teragram/hectoliter",
"teragram/kiloliter",
"teragram/liter",
"teragram/litre",
"teragram/megaliter",
"teragram/microliter",
"teragram/milliliter",
"teragram/myrialiter",
"teragram/nanoliter",
"teragram/petaliter",
"teragram/picoliter",
"teragram/teraliter",
"teragram/yoctoliter",
"teragram/yottaliter",
"teragram/zeptoliter",
"teragram/zettaliter",
"tonne/attoliter",
"tonne/centiliter",
"tonne/cubic attometer",
"tonne/cubic centimeter",
"tonne/cubic decameter",
"tonne/cubic decimeter",
"tonne/cubic dekameter",
"tonne/cubic exameter",
"tonne/cubic femtometer",
"tonne/cubic gigameter",
"tonne/cubic hectometer",
"tonne/cubic kilometer",
"tonne/cubic megameter",
"tonne/cubic meter",
"tonne/cubic micrometer",
"tonne/cubic millimeter",
"tonne/cubic myriameter",
"tonne/cubic nanometer",
"tonne/cubic petameter",
"tonne/cubic picometer",
"tonne/cubic terameter",
"tonne/cubic yoctometer",
"tonne/cubic yottameter",
"tonne/cubic zeptometer",
"tonne/cubic zettameter",
"tonne/decaliter",
"tonne/deciliter",
"tonne/dekaliter",
"tonne/exaliter",
"tonne/femtoliter",
"tonne/gigaliter",
"tonne/hectoliter",
"tonne/kiloliter",
"tonne/liter",
"tonne/litre",
"tonne/megaliter",
"tonne/microliter",
"tonne/milliliter",
"tonne/myrialiter",
"tonne/nanoliter",
"tonne/petaliter",
"tonne/picoliter",
"tonne/teraliter",
"tonne/yoctoliter",
"tonne/yottaliter",
"tonne/zeptoliter",
"tonne/zettaliter",
"yoctogram/attoliter",
"yoctogram/centiliter",
"yoctogram/cubic attometer",
"yoctogram/cubic centimeter",
"yoctogram/cubic decameter",
"yoctogram/cubic decimeter",
"yoctogram/cubic dekameter",
"yoctogram/cubic exameter",
"yoctogram/cubic femtometer",
"yoctogram/cubic gigameter",
"yoctogram/cubic hectometer",
"yoctogram/cubic kilometer",
"yoctogram/cubic megameter",
"yoctogram/cubic meter",
"yoctogram/cubic micrometer",
"yoctogram/cubic millimeter",
"yoctogram/cubic myriameter",
"yoctogram/cubic nanometer",
"yoctogram/cubic petameter",
"yoctogram/cubic picometer",
"yoctogram/cubic terameter",
"yoctogram/cubic yoctometer",
"yoctogram/cubic yottameter",
"yoctogram/cubic zeptometer",
"yoctogram/cubic zettameter",
"yoctogram/decaliter",
"yoctogram/deciliter",
"yoctogram/dekaliter",
"yoctogram/exaliter",
"yoctogram/femtoliter",
"yoctogram/gigaliter",
"yoctogram/hectoliter",
"yoctogram/kiloliter",
"yoctogram/liter",
"yoctogram/litre",
"yoctogram/megaliter",
"yoctogram/microliter",
"yoctogram/milliliter",
"yoctogram/myrialiter",
"yoctogram/nanoliter",
"yoctogram/petaliter",
"yoctogram/picoliter",
"yoctogram/teraliter",
"yoctogram/yoctoliter",
"yoctogram/yottaliter",
"yoctogram/zeptoliter",
"yoctogram/zettaliter",
"yottagram/attoliter",
"yottagram/centiliter",
"yottagram/cubic attometer",
"yottagram/cubic centimeter",
"yottagram/cubic decameter",
"yottagram/cubic decimeter",
"yottagram/cubic dekameter",
"yottagram/cubic exameter",
"yottagram/cubic femtometer",
"yottagram/cubic gigameter",
"yottagram/cubic hectometer",
"yottagram/cubic kilometer",
"yottagram/cubic megameter",
"yottagram/cubic meter",
"yottagram/cubic micrometer",
"yottagram/cubic millimeter",
"yottagram/cubic myriameter",
"yottagram/cubic nanometer",
"yottagram/cubic petameter",
"yottagram/cubic picometer",
"yottagram/cubic terameter",
"yottagram/cubic yoctometer",
"yottagram/cubic yottameter",
"yottagram/cubic zeptometer",
"yottagram/cubic zettameter",
"yottagram/decaliter",
"yottagram/deciliter",
"yottagram/dekaliter",
"yottagram/exaliter",
"yottagram/femtoliter",
"yottagram/gigaliter",
"yottagram/hectoliter",
"yottagram/kiloliter",
"yottagram/liter",
"yottagram/litre",
"yottagram/megaliter",
"yottagram/microliter",
"yottagram/milliliter",
"yottagram/myrialiter",
"yottagram/nanoliter",
"yottagram/petaliter",
"yottagram/picoliter",
"yottagram/teraliter",
"yottagram/yoctoliter",
"yottagram/yottaliter",
"yottagram/zeptoliter",
"yottagram/zettaliter",
"zeptogram/attoliter",
"zeptogram/centiliter",
"zeptogram/cubic attometer",
"zeptogram/cubic centimeter",
"zeptogram/cubic decameter",
"zeptogram/cubic decimeter",
"zeptogram/cubic dekameter",
"zeptogram/cubic exameter",
"zeptogram/cubic femtometer",
"zeptogram/cubic gigameter",
"zeptogram/cubic hectometer",
"zeptogram/cubic kilometer",
"zeptogram/cubic megameter",
"zeptogram/cubic meter",
"zeptogram/cubic micrometer",
"zeptogram/cubic millimeter",
"zeptogram/cubic myriameter",
"zeptogram/cubic nanometer",
"zeptogram/cubic petameter",
"zeptogram/cubic picometer",
"zeptogram/cubic terameter",
"zeptogram/cubic yoctometer",
"zeptogram/cubic yottameter",
"zeptogram/cubic zeptometer",
"zeptogram/cubic zettameter",
"zeptogram/decaliter",
"zeptogram/deciliter",
"zeptogram/dekaliter",
"zeptogram/exaliter",
"zeptogram/femtoliter",
"zeptogram/gigaliter",
"zeptogram/hectoliter",
"zeptogram/kiloliter",
"zeptogram/liter",
"zeptogram/litre",
"zeptogram/megaliter",
"zeptogram/microliter",
"zeptogram/milliliter",
"zeptogram/myrialiter",
"zeptogram/nanoliter",
"zeptogram/petaliter",
"zeptogram/picoliter",
"zeptogram/teraliter",
"zeptogram/yoctoliter",
"zeptogram/yottaliter",
"zeptogram/zeptoliter",
"zeptogram/zettaliter",
"zettagram/attoliter",
"zettagram/centiliter",
"zettagram/cubic attometer",
"zettagram/cubic centimeter",
"zettagram/cubic decameter",
"zettagram/cubic decimeter",
"zettagram/cubic dekameter",
"zettagram/cubic exameter",
"zettagram/cubic femtometer",
"zettagram/cubic gigameter",
"zettagram/cubic hectometer",
"zettagram/cubic kilometer",
"zettagram/cubic megameter",
"zettagram/cubic meter",
"zettagram/cubic micrometer",
"zettagram/cubic millimeter",
"zettagram/cubic myriameter",
"zettagram/cubic nanometer",
"zettagram/cubic petameter",
"zettagram/cubic picometer",
"zettagram/cubic terameter",
"zettagram/cubic yoctometer",
"zettagram/cubic yottameter",
"zettagram/cubic zeptometer",
"zettagram/cubic zettameter",
"zettagram/decaliter",
"zettagram/deciliter",
"zettagram/dekaliter",
"zettagram/exaliter",
"zettagram/femtoliter",
"zettagram/gigaliter",
"zettagram/hectoliter",
"zettagram/kiloliter",
"zettagram/liter",
"zettagram/litre",
"zettagram/megaliter",
"zettagram/microliter",
"zettagram/milliliter",
"zettagram/myrialiter",
"zettagram/nanoliter",
"zettagram/petaliter",
"zettagram/picoliter",
"zettagram/teraliter",
"zettagram/yoctoliter",
"zettagram/yottaliter",
"zettagram/zeptoliter",
"zettagram/zettaliter",
"zettagram/zettaliter",
"zettagram/zettaliter",
"zettagram/zettaliter",
"zettagram/zettaliter",
"zettagram/zettaliter"
};
double[] densityConversions = 
{
1000, //attogram/attoliter 0
10000000000000000000, //attogram/centiliter 1
1.0 * Math.Pow(10, -30), //attogram/cubic attometer 2
1000000000000000000, //attogram/cubic centimeter 3
1.0 * Math.Pow(10, 27), //attogram/cubic decameter 4
1.0 * Math.Pow(10, 21), //attogram/cubic decimeter 5
1.0 * Math.Pow(10, 27), //attogram/cubic dekameter 6
1.0 * Math.Pow(10, 78), //attogram/cubic exameter 7
1.0 * Math.Pow(10, -21), //attogram/cubic femtometer 8
1.0 * Math.Pow(10, 51), //attogram/cubic gigameter 9
1.0 * Math.Pow(10, 30), //attogram/cubic hectometer 10
1.0 * Math.Pow(10, 33), //attogram/cubic kilometer 11
1.0 * Math.Pow(10, 42), //attogram/cubic megameter 12
1.0 * Math.Pow(10, 24), //attogram/cubic meter 13
1000000, //attogram/cubic micrometer 14
1000000000000000, //attogram/cubic millimeter 15
1.0 * Math.Pow(10, 36), //attogram/cubic myriameter 16
0.001, //attogram/cubic nanometer 17
1.0 * Math.Pow(10, 69), //attogram/cubic petameter 18
1.0 * Math.Pow(10, -12), //attogram/cubic picometer 19
1.0 * Math.Pow(10, 60), //attogram/cubic terameter 20
1.0 * Math.Pow(10, -48), //attogram/cubic yoctometer 21
1.0 * Math.Pow(10, 96), //attogram/cubic yottameter 22
1.0 * Math.Pow(10, -39), //attogram/cubic zeptometer 23
1.0 * Math.Pow(10, 87), //attogram/cubic zettameter 24
1.0 * Math.Pow(10, 22), //attogram/decaliter 25
100000000000000000000, //attogram/deciliter 26
1.0 * Math.Pow(10, 22), //attogram/dekaliter 27
1.0 * Math.Pow(10, 39), //attogram/exaliter 28
1000000, //attogram/femtoliter 29
1.0 * Math.Pow(10, 30), //attogram/gigaliter 30
1.0 * Math.Pow(10, 23), //attogram/hectoliter 31
1.0 * Math.Pow(10, 24), //attogram/kiloliter 32
1.0 * Math.Pow(10, 21), //attogram/liter 33
1.0 * Math.Pow(10, 21), //attogram/litre 34
1.0 * Math.Pow(10, 27), //attogram/megaliter 35
1000000000000000, //attogram/microliter 36
1000000000000000000, //attogram/milliliter 37
1.0 * Math.Pow(10, 25), //attogram/myrialiter 38
1000000000000, //attogram/nanoliter 39
1.0 * Math.Pow(10, 36), //attogram/petaliter 40
1000000000, //attogram/picoliter 41
1.0 * Math.Pow(10, 33), //attogram/teraliter 42
0.001, //attogram/yoctoliter 43
1.0 * Math.Pow(10, 45), //attogram/yottaliter 44
1, //attogram/zeptoliter 45
1.0 * Math.Pow(10, 42), //attogram/zettaliter 46
1.0 * Math.Pow(10, -13), //centigram/attoliter 47
1000, //centigram/centiliter 48
1.0 * Math.Pow(10, -46), //centigram/cubic attometer 49
100, //centigram/cubic centimeter 50
100000000000, //centigram/cubic decameter 51
100000, //centigram/cubic decimeter 52
100000000000, //centigram/cubic dekameter 53
1.0 * Math.Pow(10, 62), //centigram/cubic exameter 54
1.0 * Math.Pow(10, -37), //centigram/cubic femtometer 55
1.0 * Math.Pow(10, 35), //centigram/cubic gigameter 56
100000000000000, //centigram/cubic hectometer 57
100000000000000000, //centigram/cubic kilometer 58
1.0 * Math.Pow(10, 26), //centigram/cubic megameter 59
100000000, //centigram/cubic meter 60
1.0 * Math.Pow(10, -10), //centigram/cubic micrometer 61
0.1, //centigram/cubic millimeter 62
100000000000000000000, //centigram/cubic myriameter 63
1.0 * Math.Pow(10, -19), //centigram/cubic nanometer 64
1.0 * Math.Pow(10, 53), //centigram/cubic petameter 65
1.0 * Math.Pow(10, -28), //centigram/cubic picometer 66
1.0 * Math.Pow(10, 44), //centigram/cubic terameter 67
1.0 * Math.Pow(10, -64), //centigram/cubic yoctometer 68
1.0 * Math.Pow(10, 80), //centigram/cubic yottameter 69
1.0 * Math.Pow(10, -55), //centigram/cubic zeptometer 70
1.0 * Math.Pow(10, 71), //centigram/cubic zettameter 71
1000000, //centigram/decaliter 72
10000, //centigram/deciliter 73
1000000, //centigram/dekaliter 74
1.0 * Math.Pow(10, 23), //centigram/exaliter 75
1.0 * Math.Pow(10, -10), //centigram/femtoliter 76
100000000000000, //centigram/gigaliter 77
10000000, //centigram/hectoliter 78
100000000, //centigram/kiloliter 79
100000, //centigram/liter 80
100000, //centigram/litre 81
100000000000, //centigram/megaliter 82
0.1, //centigram/microliter 83
100, //centigram/milliliter 84
1000000000, //centigram/myrialiter 85
0.0001, //centigram/nanoliter 86
100000000000000000000, //centigram/petaliter 87
1.0 * Math.Pow(10, -7), //centigram/picoliter 88
100000000000000000, //centigram/teraliter 89
1.0 * Math.Pow(10, -19), //centigram/yoctoliter 90
1.0 * Math.Pow(10, 29), //centigram/yottaliter 91
1.0 * Math.Pow(10, -16), //centigram/zeptoliter 92
1.0 * Math.Pow(10, 26), //centigram/zettaliter 93
1.0 * Math.Pow(10, -16), //decagram/attoliter 94
1, //decagram/centiliter 95
1.0 * Math.Pow(10, -49), //decagram/cubic attometer 96
0.1, //decagram/cubic centimeter 97
100000000, //decagram/cubic decameter 98
100, //decagram/cubic decimeter 99
100000000, //decagram/cubic dekameter 100
1.0 * Math.Pow(10, 59), //decagram/cubic exameter 101
1.0 * Math.Pow(10, -40), //decagram/cubic femtometer 102
1.0 * Math.Pow(10, 32), //decagram/cubic gigameter 103
100000000000, //decagram/cubic hectometer 104
100000000000000, //decagram/cubic kilometer 105
1.0 * Math.Pow(10, 23), //decagram/cubic megameter 106
100000, //decagram/cubic meter 107
1.0 * Math.Pow(10, -13), //decagram/cubic micrometer 108
0.0001, //decagram/cubic millimeter 109
100000000000000000, //decagram/cubic myriameter 110
1.0 * Math.Pow(10, -22), //decagram/cubic nanometer 111
1.0 * Math.Pow(10, 50), //decagram/cubic petameter 112
1.0 * Math.Pow(10, -31), //decagram/cubic picometer 113
1.0 * Math.Pow(10, 41), //decagram/cubic terameter 114
1.0 * Math.Pow(10, -67), //decagram/cubic yoctometer 115
1.0 * Math.Pow(10, 77), //decagram/cubic yottameter 116
1.0 * Math.Pow(10, -58), //decagram/cubic zeptometer 117
1.0 * Math.Pow(10, 68), //decagram/cubic zettameter 118
1000, //decagram/decaliter 119
10, //decagram/deciliter 120
1000, //decagram/dekaliter 121
100000000000000000000, //decagram/exaliter 122
1.0 * Math.Pow(10, -13), //decagram/femtoliter 123
100000000000, //decagram/gigaliter 124
10000, //decagram/hectoliter 125
100000, //decagram/kiloliter 126
100, //decagram/liter 127
100, //decagram/litre 128
100000000, //decagram/megaliter 129
0.0001, //decagram/microliter 130
0.1, //decagram/milliliter 131
1000000, //decagram/myrialiter 132
1.0 * Math.Pow(10, -7), //decagram/nanoliter 133
100000000000000000, //decagram/petaliter 134
1.0 * Math.Pow(10, -10), //decagram/picoliter 135
100000000000000, //decagram/teraliter 136
1.0 * Math.Pow(10, -22), //decagram/yoctoliter 137
1.0 * Math.Pow(10, 26), //decagram/yottaliter 138
1.0 * Math.Pow(10, -19), //decagram/zeptoliter 139
1.0 * Math.Pow(10, 23), //decagram/zettaliter 140
1.0 * Math.Pow(10, -14), //decigram/attoliter 141
100, //decigram/centiliter 142
1.0 * Math.Pow(10, -47), //decigram/cubic attometer 143
10, //decigram/cubic centimeter 144
10000000000, //decigram/cubic decameter 145
10000, //decigram/cubic decimeter 146
10000000000, //decigram/cubic dekameter 147
1.0 * Math.Pow(10, 61), //decigram/cubic exameter 148
1.0 * Math.Pow(10, -38), //decigram/cubic femtometer 149
1.0 * Math.Pow(10, 34), //decigram/cubic gigameter 150
10000000000000, //decigram/cubic hectometer 151
10000000000000000, //decigram/cubic kilometer 152
1.0 * Math.Pow(10, 25), //decigram/cubic megameter 153
10000000, //decigram/cubic meter 154
1.0 * Math.Pow(10, -11), //decigram/cubic micrometer 155
0.01, //decigram/cubic millimeter 156
10000000000000000000, //decigram/cubic myriameter 157
1.0 * Math.Pow(10, -20), //decigram/cubic nanometer 158
1.0 * Math.Pow(10, 52), //decigram/cubic petameter 159
1.0 * Math.Pow(10, -29), //decigram/cubic picometer 160
1.0 * Math.Pow(10, 43), //decigram/cubic terameter 161
1.0 * Math.Pow(10, -65), //decigram/cubic yoctometer 162
1.0 * Math.Pow(10, 79), //decigram/cubic yottameter 163
1.0 * Math.Pow(10, -56), //decigram/cubic zeptometer 164
1.0 * Math.Pow(10, 70), //decigram/cubic zettameter 165
100000, //decigram/decaliter 166
1000, //decigram/deciliter 167
100000, //decigram/dekaliter 168
1.0 * Math.Pow(10, 22), //decigram/exaliter 169
1.0 * Math.Pow(10, -11), //decigram/femtoliter 170
10000000000000, //decigram/gigaliter 171
1000000, //decigram/hectoliter 172
10000000, //decigram/kiloliter 173
10000, //decigram/liter 174
10000, //decigram/litre 175
10000000000, //decigram/megaliter 176
0.01, //decigram/microliter 177
10, //decigram/milliliter 178
100000000, //decigram/myrialiter 179
0.00001, //decigram/nanoliter 180
10000000000000000000, //decigram/petaliter 181
1.0 * Math.Pow(10, -8), //decigram/picoliter 182
10000000000000000, //decigram/teraliter 183
1.0 * Math.Pow(10, -20), //decigram/yoctoliter 184
1.0 * Math.Pow(10, 28), //decigram/yottaliter 185
1.0 * Math.Pow(10, -17), //decigram/zeptoliter 186
1.0 * Math.Pow(10, 25), //decigram/zettaliter 187
1.0 * Math.Pow(10, -16), //dekagram/attoliter 188
1, //dekagram/centiliter 189
1.0 * Math.Pow(10, -49), //dekagram/cubic attometer 190
0.1, //dekagram/cubic centimeter 191
100000000, //dekagram/cubic decameter 192
100, //dekagram/cubic decimeter 193
100000000, //dekagram/cubic dekameter 194
1.0 * Math.Pow(10, 59), //dekagram/cubic exameter 195
1.0 * Math.Pow(10, -40), //dekagram/cubic femtometer 196
1.0 * Math.Pow(10, 32), //dekagram/cubic gigameter 197
100000000000, //dekagram/cubic hectometer 198
100000000000000, //dekagram/cubic kilometer 199
1.0 * Math.Pow(10, 23), //dekagram/cubic megameter 200
100000, //dekagram/cubic meter 201
1.0 * Math.Pow(10, -13), //dekagram/cubic micrometer 202
0.0001, //dekagram/cubic millimeter 203
100000000000000000, //dekagram/cubic myriameter 204
1.0 * Math.Pow(10, -22), //dekagram/cubic nanometer 205
1.0 * Math.Pow(10, 50), //dekagram/cubic petameter 206
1.0 * Math.Pow(10, -31), //dekagram/cubic picometer 207
1.0 * Math.Pow(10, 41), //dekagram/cubic terameter 208
1.0 * Math.Pow(10, -67), //dekagram/cubic yoctometer 209
1.0 * Math.Pow(10, 77), //dekagram/cubic yottameter 210
1.0 * Math.Pow(10, -58), //dekagram/cubic zeptometer 211
1.0 * Math.Pow(10, 68), //dekagram/cubic zettameter 212
1000, //dekagram/decaliter 213
10, //dekagram/deciliter 214
1000, //dekagram/dekaliter 215
100000000000000000000, //dekagram/exaliter 216
1.0 * Math.Pow(10, -13), //dekagram/femtoliter 217
100000000000, //dekagram/gigaliter 218
10000, //dekagram/hectoliter 219
100000, //dekagram/kiloliter 220
100, //dekagram/liter 221
100, //dekagram/litre 222
100000000, //dekagram/megaliter 223
0.0001, //dekagram/microliter 224
0.1, //dekagram/milliliter 225
1000000, //dekagram/myrialiter 226
1.0 * Math.Pow(10, -7), //dekagram/nanoliter 227
100000000000000000, //dekagram/petaliter 228
1.0 * Math.Pow(10, -10), //dekagram/picoliter 229
100000000000000, //dekagram/teraliter 230
1.0 * Math.Pow(10, -22), //dekagram/yoctoliter 231
1.0 * Math.Pow(10, 26), //dekagram/yottaliter 232
1.0 * Math.Pow(10, -19), //dekagram/zeptoliter 233
1.0 * Math.Pow(10, 23), //dekagram/zettaliter 234
1.0 * Math.Pow(10, -33), //exagram/attoliter 235
1.0 * Math.Pow(10, -17), //exagram/centiliter 236
1.0 * Math.Pow(10, -66), //exagram/cubic attometer 237
1.0 * Math.Pow(10, -18), //exagram/cubic centimeter 238
1.0 * Math.Pow(10, -9), //exagram/cubic decameter 239
1.0 * Math.Pow(10, -15), //exagram/cubic decimeter 240
1.0 * Math.Pow(10, -9), //exagram/cubic dekameter 241
1.0 * Math.Pow(10, 42), //exagram/cubic exameter 242
1.0 * Math.Pow(10, -57), //exagram/cubic femtometer 243
1000000000000000, //exagram/cubic gigameter 244
0.000001, //exagram/cubic hectometer 245
0.001, //exagram/cubic kilometer 246
1000000, //exagram/cubic megameter 247
1.0 * Math.Pow(10, -12), //exagram/cubic meter 248
1.0 * Math.Pow(10, -30), //exagram/cubic micrometer 249
1.0 * Math.Pow(10, -21), //exagram/cubic millimeter 250
1, //exagram/cubic myriameter 251
1.0 * Math.Pow(10, -39), //exagram/cubic nanometer 252
1.0 * Math.Pow(10, 33), //exagram/cubic petameter 253
1.0 * Math.Pow(10, -48), //exagram/cubic picometer 254
1.0 * Math.Pow(10, 24), //exagram/cubic terameter 255
1.0 * Math.Pow(10, -84), //exagram/cubic yoctometer 256
1.0 * Math.Pow(10, 60), //exagram/cubic yottameter 257
1.0 * Math.Pow(10, -75), //exagram/cubic zeptometer 258
1.0 * Math.Pow(10, 51), //exagram/cubic zettameter 259
1.0 * Math.Pow(10, -14), //exagram/decaliter 260
1.0 * Math.Pow(10, -16), //exagram/deciliter 261
1.0 * Math.Pow(10, -14), //exagram/dekaliter 262
1000, //exagram/exaliter 263
1.0 * Math.Pow(10, -30), //exagram/femtoliter 264
0.000001, //exagram/gigaliter 265
1.0 * Math.Pow(10, -13), //exagram/hectoliter 266
1.0 * Math.Pow(10, -12), //exagram/kiloliter 267
1.0 * Math.Pow(10, -15), //exagram/liter 268
1.0 * Math.Pow(10, -15), //exagram/litre 269
1.0 * Math.Pow(10, -9), //exagram/megaliter 270
1.0 * Math.Pow(10, -21), //exagram/microliter 271
1.0 * Math.Pow(10, -18), //exagram/milliliter 272
1.0 * Math.Pow(10, -11), //exagram/myrialiter 273
1.0 * Math.Pow(10, -24), //exagram/nanoliter 274
1, //exagram/petaliter 275
1.0 * Math.Pow(10, -27), //exagram/picoliter 276
0.001, //exagram/teraliter 277
1.0 * Math.Pow(10, -39), //exagram/yoctoliter 278
1000000000, //exagram/yottaliter 279
1.0 * Math.Pow(10, -36), //exagram/zeptoliter 280
1000000, //exagram/zettaliter 281
1, //femtogram/attoliter 282
10000000000000000, //femtogram/centiliter 283
1.0 * Math.Pow(10, -33), //femtogram/cubic attometer 284
1000000000000000, //femtogram/cubic centimeter 285
1.0 * Math.Pow(10, 24), //femtogram/cubic decameter 286
1000000000000000000, //femtogram/cubic decimeter 287
1.0 * Math.Pow(10, 24), //femtogram/cubic dekameter 288
1.0 * Math.Pow(10, 75), //femtogram/cubic exameter 289
1.0 * Math.Pow(10, -24), //femtogram/cubic femtometer 290
1.0 * Math.Pow(10, 48), //femtogram/cubic gigameter 291
1.0 * Math.Pow(10, 27), //femtogram/cubic hectometer 292
1.0 * Math.Pow(10, 30), //femtogram/cubic kilometer 293
1.0 * Math.Pow(10, 39), //femtogram/cubic megameter 294
1.0 * Math.Pow(10, 21), //femtogram/cubic meter 295
1000, //femtogram/cubic micrometer 296
1000000000000, //femtogram/cubic millimeter 297
1.0 * Math.Pow(10, 33), //femtogram/cubic myriameter 298
0.000001, //femtogram/cubic nanometer 299
1.0 * Math.Pow(10, 66), //femtogram/cubic petameter 300
1.0 * Math.Pow(10, -15), //femtogram/cubic picometer 301
1.0 * Math.Pow(10, 57), //femtogram/cubic terameter 302
1.0 * Math.Pow(10, -51), //femtogram/cubic yoctometer 303
1.0 * Math.Pow(10, 93), //femtogram/cubic yottameter 304
1.0 * Math.Pow(10, -42), //femtogram/cubic zeptometer 305
1.0 * Math.Pow(10, 84), //femtogram/cubic zettameter 306
10000000000000000000, //femtogram/decaliter 307
100000000000000000, //femtogram/deciliter 308
10000000000000000000, //femtogram/dekaliter 309
1.0 * Math.Pow(10, 36), //femtogram/exaliter 310
1000, //femtogram/femtoliter 311
1.0 * Math.Pow(10, 27), //femtogram/gigaliter 312
100000000000000000000, //femtogram/hectoliter 313
1.0 * Math.Pow(10, 21), //femtogram/kiloliter 314
1000000000000000000, //femtogram/liter 315
1000000000000000000, //femtogram/litre 316
1.0 * Math.Pow(10, 24), //femtogram/megaliter 317
1000000000000, //femtogram/microliter 318
1000000000000000, //femtogram/milliliter 319
1.0 * Math.Pow(10, 22), //femtogram/myrialiter 320
1000000000, //femtogram/nanoliter 321
1.0 * Math.Pow(10, 33), //femtogram/petaliter 322
1000000, //femtogram/picoliter 323
1.0 * Math.Pow(10, 30), //femtogram/teraliter 324
0.000001, //femtogram/yoctoliter 325
1.0 * Math.Pow(10, 42), //femtogram/yottaliter 326
0.001, //femtogram/zeptoliter 327
1.0 * Math.Pow(10, 39), //femtogram/zettaliter 328
1.0 * Math.Pow(10, -24), //gigagram/attoliter 329
1.0 * Math.Pow(10, -8), //gigagram/centiliter 330
1.0 * Math.Pow(10, -57), //gigagram/cubic attometer 331
1.0 * Math.Pow(10, -9), //gigagram/cubic centimeter 332
1, //gigagram/cubic decameter 333
0.000001, //gigagram/cubic decimeter 334
1, //gigagram/cubic dekameter 335
1.0 * Math.Pow(10, 51), //gigagram/cubic exameter 336
1.0 * Math.Pow(10, -48), //gigagram/cubic femtometer 337
1.0 * Math.Pow(10, 24), //gigagram/cubic gigameter 338
1000, //gigagram/cubic hectometer 339
1000000, //gigagram/cubic kilometer 340
1000000000000000, //gigagram/cubic megameter 341
0.001, //gigagram/cubic meter 342
1.0 * Math.Pow(10, -21), //gigagram/cubic micrometer 343
1.0 * Math.Pow(10, -12), //gigagram/cubic millimeter 344
1000000000, //gigagram/cubic myriameter 345
1.0 * Math.Pow(10, -30), //gigagram/cubic nanometer 346
1.0 * Math.Pow(10, 42), //gigagram/cubic petameter 347
1.0 * Math.Pow(10, -39), //gigagram/cubic picometer 348
1.0 * Math.Pow(10, 33), //gigagram/cubic terameter 349
1.0 * Math.Pow(10, -75), //gigagram/cubic yoctometer 350
1.0 * Math.Pow(10, 69), //gigagram/cubic yottameter 351
1.0 * Math.Pow(10, -66), //gigagram/cubic zeptometer 352
1.0 * Math.Pow(10, 60), //gigagram/cubic zettameter 353
0.00001, //gigagram/decaliter 354
1.0 * Math.Pow(10, -7), //gigagram/deciliter 355
0.00001, //gigagram/dekaliter 356
1000000000000, //gigagram/exaliter 357
1.0 * Math.Pow(10, -21), //gigagram/femtoliter 358
1000, //gigagram/gigaliter 359
0.0001, //gigagram/hectoliter 360
0.001, //gigagram/kiloliter 361
0.000001, //gigagram/liter 362
0.000001, //gigagram/litre 363
1, //gigagram/megaliter 364
1.0 * Math.Pow(10, -12), //gigagram/microliter 365
1.0 * Math.Pow(10, -9), //gigagram/milliliter 366
0.01, //gigagram/myrialiter 367
1.0 * Math.Pow(10, -15), //gigagram/nanoliter 368
1000000000, //gigagram/petaliter 369
1.0 * Math.Pow(10, -18), //gigagram/picoliter 370
1000000, //gigagram/teraliter 371
1.0 * Math.Pow(10, -30), //gigagram/yoctoliter 372
1000000000000000000, //gigagram/yottaliter 373
1.0 * Math.Pow(10, -27), //gigagram/zeptoliter 374
1000000000000000, //gigagram/zettaliter 375
1.0 * Math.Pow(10, -30), //gigatonne/attoliter 376
1.0 * Math.Pow(10, -14), //gigatonne/centiliter 377
1.0 * Math.Pow(10, -63), //gigatonne/cubic attometer 378
1.0 * Math.Pow(10, -15), //gigatonne/cubic centimeter 379
0.000001, //gigatonne/cubic decameter 380
1.0 * Math.Pow(10, -12), //gigatonne/cubic decimeter 381
0.000001, //gigatonne/cubic dekameter 382
1.0 * Math.Pow(10, 45), //gigatonne/cubic exameter 383
1.0 * Math.Pow(10, -54), //gigatonne/cubic femtometer 384
1000000000000000000, //gigatonne/cubic gigameter 385
0.001, //gigatonne/cubic hectometer 386
1, //gigatonne/cubic kilometer 387
1000000000, //gigatonne/cubic megameter 388
1.0 * Math.Pow(10, -9), //gigatonne/cubic meter 389
1.0 * Math.Pow(10, -27), //gigatonne/cubic micrometer 390
1.0 * Math.Pow(10, -18), //gigatonne/cubic millimeter 391
1000, //gigatonne/cubic myriameter 392
1.0 * Math.Pow(10, -36), //gigatonne/cubic nanometer 393
1.0 * Math.Pow(10, 36), //gigatonne/cubic petameter 394
1.0 * Math.Pow(10, -45), //gigatonne/cubic picometer 395
1.0 * Math.Pow(10, 27), //gigatonne/cubic terameter 396
1.0 * Math.Pow(10, -81), //gigatonne/cubic yoctometer 397
1.0 * Math.Pow(10, 63), //gigatonne/cubic yottameter 398
1.0 * Math.Pow(10, -72), //gigatonne/cubic zeptometer 399
1.0 * Math.Pow(10, 54), //gigatonne/cubic zettameter 400
1.0 * Math.Pow(10, -11), //gigatonne/decaliter 401
1.0 * Math.Pow(10, -13), //gigatonne/deciliter 402
1.0 * Math.Pow(10, -11), //gigatonne/dekaliter 403
1000000, //gigatonne/exaliter 404
1.0 * Math.Pow(10, -27), //gigatonne/femtoliter 405
0.001, //gigatonne/gigaliter 406
1.0 * Math.Pow(10, -10), //gigatonne/hectoliter 407
1.0 * Math.Pow(10, -9), //gigatonne/kiloliter 408
1.0 * Math.Pow(10, -12), //gigatonne/liter 409
1.0 * Math.Pow(10, -12), //gigatonne/litre 410
0.000001, //gigatonne/megaliter 411
1.0 * Math.Pow(10, -18), //gigatonne/microliter 412
1.0 * Math.Pow(10, -15), //gigatonne/milliliter 413
1.0 * Math.Pow(10, -8), //gigatonne/myrialiter 414
1.0 * Math.Pow(10, -21), //gigatonne/nanoliter 415
1000, //gigatonne/petaliter 416
1.0 * Math.Pow(10, -24), //gigatonne/picoliter 417
1, //gigatonne/teraliter 418
1.0 * Math.Pow(10, -36), //gigatonne/yoctoliter 419
1000000000000, //gigatonne/yottaliter 420
1.0 * Math.Pow(10, -33), //gigatonne/zeptoliter 421
1000000000, //gigatonne/zettaliter 422
436995.72588, //grain/cubic foot 423
252.89104507, //grain/cubic inch 424
64324875884000000, //grain/cubic mile 425
11798884.599, //grain/cubic yard 426
70156.889986, //grain/gallon [UK] 427
67977.745306, //grain/gallon [US dry] 428
58417.831411, //grain/gallon [US] 429
438.48056241, //grain/ounce [UK] 430
456.38930791, //grain/ounce [US] 431
17539.222496, //grain/quart [UK] 432
16994.436326, //grain/quart [US dry] 433
14604.457853, //grain/quart [US] 434
1.0 * Math.Pow(10, -15), //gram/attoliter 435
10, //gram/centiliter 436
1.0 * Math.Pow(10, -48), //gram/cubic attometer 437
1, //gram/cubic centimeter 438
1000000000, //gram/cubic decameter 439
1000, //gram/cubic decimeter 440
1000000000, //gram/cubic dekameter 441
1.0 * Math.Pow(10, 60), //gram/cubic exameter 442
1.0 * Math.Pow(10, -39), //gram/cubic femtometer 443
1.0 * Math.Pow(10, 33), //gram/cubic gigameter 444
1000000000000, //gram/cubic hectometer 445
1000000000000000, //gram/cubic kilometer 446
1.0 * Math.Pow(10, 24), //gram/cubic megameter 447
1000000, //gram/cubic meter 448
1.0 * Math.Pow(10, -12), //gram/cubic micrometer 449
0.001, //gram/cubic millimeter 450
1000000000000000000, //gram/cubic myriameter 451
1.0 * Math.Pow(10, -21), //gram/cubic nanometer 452
1.0 * Math.Pow(10, 51), //gram/cubic petameter 453
1.0 * Math.Pow(10, -30), //gram/cubic picometer 454
1.0 * Math.Pow(10, 42), //gram/cubic terameter 455
1.0 * Math.Pow(10, -66), //gram/cubic yoctometer 456
1.0 * Math.Pow(10, 78), //gram/cubic yottameter 457
1.0 * Math.Pow(10, -57), //gram/cubic zeptometer 458
1.0 * Math.Pow(10, 69), //gram/cubic zettameter 459
10000, //gram/decaliter 460
100, //gram/deciliter 461
10000, //gram/dekaliter 462
1.0 * Math.Pow(10, 21), //gram/exaliter 463
1.0 * Math.Pow(10, -12), //gram/femtoliter 464
1000000000000, //gram/gigaliter 465
100000, //gram/hectoliter 466
1000000, //gram/kiloliter 467
1000, //gram/liter 468
1000, //gram/litre 469
1000000000, //gram/megaliter 470
0.001, //gram/microliter 471
1, //gram/milliliter 472
10000000, //gram/myrialiter 473
0.000001, //gram/nanoliter 474
1000000000000000000, //gram/petaliter 475
1.0 * Math.Pow(10, -9), //gram/picoliter 476
1000000000000000, //gram/teraliter 477
1.0 * Math.Pow(10, -21), //gram/yoctoliter 478
1.0 * Math.Pow(10, 27), //gram/yottaliter 479
1.0 * Math.Pow(10, -18), //gram/zeptoliter 480
1.0 * Math.Pow(10, 24), //gram/zettaliter 481
1.0 * Math.Pow(10, -17), //hectogram/attoliter 482
0.1, //hectogram/centiliter 483
1.0 * Math.Pow(10, -50), //hectogram/cubic attometer 484
0.01, //hectogram/cubic centimeter 485
10000000, //hectogram/cubic decameter 486
10, //hectogram/cubic decimeter 487
10000000, //hectogram/cubic dekameter 488
1.0 * Math.Pow(10, 58), //hectogram/cubic exameter 489
1.0 * Math.Pow(10, -41), //hectogram/cubic femtometer 490
1.0 * Math.Pow(10, 31), //hectogram/cubic gigameter 491
10000000000, //hectogram/cubic hectometer 492
10000000000000, //hectogram/cubic kilometer 493
1.0 * Math.Pow(10, 22), //hectogram/cubic megameter 494
10000, //hectogram/cubic meter 495
1.0 * Math.Pow(10, -14), //hectogram/cubic micrometer 496
0.00001, //hectogram/cubic millimeter 497
10000000000000000, //hectogram/cubic myriameter 498
1.0 * Math.Pow(10, -23), //hectogram/cubic nanometer 499
1.0 * Math.Pow(10, 49), //hectogram/cubic petameter 500
1.0 * Math.Pow(10, -32), //hectogram/cubic picometer 501
1.0 * Math.Pow(10, 40), //hectogram/cubic terameter 502
1.0 * Math.Pow(10, -68), //hectogram/cubic yoctometer 503
1.0 * Math.Pow(10, 76), //hectogram/cubic yottameter 504
1.0 * Math.Pow(10, -59), //hectogram/cubic zeptometer 505
1.0 * Math.Pow(10, 67), //hectogram/cubic zettameter 506
100, //hectogram/decaliter 507
1, //hectogram/deciliter 508
100, //hectogram/dekaliter 509
10000000000000000000, //hectogram/exaliter 510
1.0 * Math.Pow(10, -14), //hectogram/femtoliter 511
10000000000, //hectogram/gigaliter 512
1000, //hectogram/hectoliter 513
10000, //hectogram/kiloliter 514
10, //hectogram/liter 515
10, //hectogram/litre 516
10000000, //hectogram/megaliter 517
0.00001, //hectogram/microliter 518
0.01, //hectogram/milliliter 519
, // 520
1.0 * Math.Pow(10, -8), //hectogram/nanoliter 521
10000000000000000, //hectogram/petaliter 522
1.0 * Math.Pow(10, -11), //hectogram/picoliter 523
10000000000000, //hectogram/teraliter 524
1.0 * Math.Pow(10, -23), //hectogram/yoctoliter 525
1.0 * Math.Pow(10, 25), //hectogram/yottaliter 526
1.0 * Math.Pow(10, -20), //hectogram/zeptoliter 527
1.0 * Math.Pow(10, 22), //hectogram/zettaliter 528
0.5573925075, //hundredweight/cubic foot [UK] 529
0.62427960841, //hundredweight/cubic foot [US] 530
0.00032256510851, //hundredweight/cubic inch [UK] 531
0.00036127292153, //hundredweight/cubic inch [US] 532
82047035566, //hundredweight/cubic mile [UK] 533
91892679829, //hundredweight/cubic mile [US] 534
15.049597703, //hundredweight/cubic yard [UK] 535
16.855549427, //hundredweight/cubic yard [US] 536
0.089485829064, //hundredweight/gallon [UK] 537
0.097111064718, //hundredweight/gallon [US dry] 538
0.083454044873, //hundredweight/gallon [US] 539
0.00055928643165, //hundredweight/ounce [UK] 540
0.00065198472556, //hundredweight/ounce [US] 541
0.022371457266, //hundredweight/quart [UK] 542
0.02427776618, //hundredweight/quart [US dry] 543
0.020863511218, //hundredweight/quart [US] 544
1.0 * Math.Pow(10, -18), //kilogram/attoliter 545
0.01, //kilogram/centiliter 546
1.0 * Math.Pow(10, -51), //kilogram/cubic attometer 547
0.001, //kilogram/cubic centimeter 548
1000000, //kilogram/cubic decameter 549
1, //kilogram/cubic decimeter 550
1000000, //kilogram/cubic dekameter 551
1.0 * Math.Pow(10, 57), //kilogram/cubic exameter 552
1.0 * Math.Pow(10, -42), //kilogram/cubic femtometer 553
1.0 * Math.Pow(10, 30), //kilogram/cubic gigameter 554
1000000000, //kilogram/cubic hectometer 555
1000000000000, //kilogram/cubic kilometer 556
1.0 * Math.Pow(10, 21), //kilogram/cubic megameter 557
1000, //kilogram/cubic meter 558
1.0 * Math.Pow(10, -15), //kilogram/cubic micrometer 559
0.000001, //kilogram/cubic millimeter 560
1000000000000000, //kilogram/cubic myriameter 561
1.0 * Math.Pow(10, -24), //kilogram/cubic nanometer 562
1.0 * Math.Pow(10, 48), //kilogram/cubic petameter 563
1.0 * Math.Pow(10, -33), //kilogram/cubic picometer 564
1.0 * Math.Pow(10, 39), //kilogram/cubic terameter 565
1.0 * Math.Pow(10, -69), //kilogram/cubic yoctometer 566
1.0 * Math.Pow(10, 75), //kilogram/cubic yottameter 567
1.0 * Math.Pow(10, -60), //kilogram/cubic zeptometer 568
1.0 * Math.Pow(10, 66), //kilogram/cubic zettameter 569
10, //kilogram/decaliter 570
0.1, //kilogram/deciliter 571
10, //kilogram/dekaliter 572
1000000000000000000, //kilogram/exaliter 573
1.0 * Math.Pow(10, -15), //kilogram/femtoliter 574
1000000000, //kilogram/gigaliter 575
100, //kilogram/hectoliter 576
1000, //kilogram/kiloliter 577
1, //kilogram/liter 578
1, //kilogram/litre 579
1000000, //kilogram/megaliter 580
0.000001, //kilogram/microliter 581
0.001, //kilogram/milliliter 582
10000, //kilogram/myrialiter 583
1.0 * Math.Pow(10, -9), //kilogram/nanoliter 584
1000000000000000, //kilogram/petaliter 585
1.0 * Math.Pow(10, -12), //kilogram/picoliter 586
1000000000000, //kilogram/teraliter 587
1.0 * Math.Pow(10, -24), //kilogram/yoctoliter 588
1.0 * Math.Pow(10, 24), //kilogram/yottaliter 589
1.0 * Math.Pow(10, -21), //kilogram/zeptoliter 590
1.0 * Math.Pow(10, 21), //kilogram/zettaliter 591
0.000027869625375, //kiloton/cubic foot [UK] 592
0.00003121398042, //kiloton/cubic foot [US] 593
1.6128255425 * Math.Pow(10, -8), //kiloton/cubic inch [UK] 594
1.8063646077 * Math.Pow(10, -8), //kiloton/cubic inch [US] 595
4102351.7781, //kiloton/cubic mile [UK] 596
4594633.9917, //kiloton/cubic mile [US] 597
0.00075247988513, //kiloton/cubic yard [UK] 598
0.00084277747134, //kiloton/cubic yard [US] 599
0.000004474291453, //kiloton/gallon [UK] 600
0.0000048555532361, //kiloton/gallon [US dry] 601
0.0000041727022437, //kiloton/gallon [US] 602
2.7964321582 * Math.Pow(10, -8), //kiloton/ounce [UK] 603
3.2599236279 * Math.Pow(10, -8), //kiloton/ounce [US] 604
0.0000011185728633, //kiloton/quart [UK] 605
0.000001213888309, //kiloton/quart [US dry] 606
0.0000010431755609, //kiloton/quart [US] 607
1.0 * Math.Pow(10, -24), //kilotonne/attoliter 608
1.0 * Math.Pow(10, -8), //kilotonne/centiliter 609
1.0 * Math.Pow(10, -57), //kilotonne/cubic attometer 610
1.0 * Math.Pow(10, -9), //kilotonne/cubic centimeter 611
1, //kilotonne/cubic decameter 612
0.000001, //kilotonne/cubic decimeter 613
1, //kilotonne/cubic dekameter 614
1.0 * Math.Pow(10, 51), //kilotonne/cubic exameter 615
1.0 * Math.Pow(10, -48), //kilotonne/cubic femtometer 616
1.0 * Math.Pow(10, 24), //kilotonne/cubic gigameter 617
1000, //kilotonne/cubic hectometer 618
1000000, //kilotonne/cubic kilometer 619
1000000000000000, //kilotonne/cubic megameter 620
0.001, //kilotonne/cubic meter 621
1.0 * Math.Pow(10, -21), //kilotonne/cubic micrometer 622
1.0 * Math.Pow(10, -12), //kilotonne/cubic millimeter 623
1000000000, //kilotonne/cubic myriameter 624
1.0 * Math.Pow(10, -30), //kilotonne/cubic nanometer 625
1.0 * Math.Pow(10, 42), //kilotonne/cubic petameter 626
1.0 * Math.Pow(10, -39), //kilotonne/cubic picometer 627
1.0 * Math.Pow(10, 33), //kilotonne/cubic terameter 628
1.0 * Math.Pow(10, -75), //kilotonne/cubic yoctometer 629
1.0 * Math.Pow(10, 69), //kilotonne/cubic yottameter 630
1.0 * Math.Pow(10, -66), //kilotonne/cubic zeptometer 631
1.0 * Math.Pow(10, 60), //kilotonne/cubic zettameter 632
0.00001, //kilotonne/decaliter 633
1.0 * Math.Pow(10, -7), //kilotonne/deciliter 634
0.00001, //kilotonne/dekaliter 635
1000000000000, //kilotonne/exaliter 636
1.0 * Math.Pow(10, -21), //kilotonne/femtoliter 637
1000, //kilotonne/gigaliter 638
0.0001, //kilotonne/hectoliter 639
0.001, //kilotonne/kiloliter 640
0.000001, //kilotonne/liter 641
0.000001, //kilotonne/litre 642
1, //kilotonne/megaliter 643
1.0 * Math.Pow(10, -12), //kilotonne/microliter 644
1.0 * Math.Pow(10, -9), //kilotonne/milliliter 645
0.01, //kilotonne/myrialiter 646
1.0 * Math.Pow(10, -15), //kilotonne/nanoliter 647
1000000000, //kilotonne/petaliter 648
1.0 * Math.Pow(10, -18), //kilotonne/picoliter 649
1000000, //kilotonne/teraliter 650
1.0 * Math.Pow(10, -30), //kilotonne/yoctoliter 651
1000000000000000000, //kilotonne/yottaliter 652
1.0 * Math.Pow(10, -27), //kilotonne/zeptoliter 653
1000000000000000, //kilotonne/zettaliter 654
1.0 * Math.Pow(10, -21), //megagram/attoliter 655
0.00001, //megagram/centiliter 656
1.0 * Math.Pow(10, -54), //megagram/cubic attometer 657
0.000001, //megagram/cubic centimeter 658
1000, //megagram/cubic decameter 659
0.001, //megagram/cubic decimeter 660
1000, //megagram/cubic dekameter 661
1.0 * Math.Pow(10, 54), //megagram/cubic exameter 662
1.0 * Math.Pow(10, -45), //megagram/cubic femtometer 663
1.0 * Math.Pow(10, 27), //megagram/cubic gigameter 664
1000000, //megagram/cubic hectometer 665
1000000000, //megagram/cubic kilometer 666
1000000000000000000, //megagram/cubic megameter 667
1, //megagram/cubic meter 668
1.0 * Math.Pow(10, -18), //megagram/cubic micrometer 669
1.0 * Math.Pow(10, -9), //megagram/cubic millimeter 670
1000000000000, //megagram/cubic myriameter 671
1.0 * Math.Pow(10, -27), //megagram/cubic nanometer 672
1.0 * Math.Pow(10, 45), //megagram/cubic petameter 673
1.0 * Math.Pow(10, -36), //megagram/cubic picometer 674
1.0 * Math.Pow(10, 36), //megagram/cubic terameter 675
1.0 * Math.Pow(10, -72), //megagram/cubic yoctometer 676
1.0 * Math.Pow(10, 72), //megagram/cubic yottameter 677
1.0 * Math.Pow(10, -63), //megagram/cubic zeptometer 678
1.0 * Math.Pow(10, 63), //megagram/cubic zettameter 679
0.01, //megagram/decaliter 680
0.0001, //megagram/deciliter 681
0.01, //megagram/dekaliter 682
1000000000000000, //megagram/exaliter 683
1.0 * Math.Pow(10, -18), //megagram/femtoliter 684
1000000, //megagram/gigaliter 685
0.1, //megagram/hectoliter 686
1, //megagram/kiloliter 687
0.001, //megagram/liter 688
0.001, //megagram/litre 689
1000, //megagram/megaliter 690
1.0 * Math.Pow(10, -9), //megagram/microliter 691
0.000001, //megagram/milliliter 692
10, //megagram/myrialiter 693
1.0 * Math.Pow(10, -12), //megagram/nanoliter 694
1000000000000, //megagram/petaliter 695
1.0 * Math.Pow(10, -15), //megagram/picoliter 696
1000000000, //megagram/teraliter 697
1.0 * Math.Pow(10, -27), //megagram/yoctoliter 698
1.0 * Math.Pow(10, 21), //megagram/yottaliter 699
1.0 * Math.Pow(10, -24), //megagram/zeptoliter 700
1000000000000000000, //megagram/zettaliter 701
1.0 * Math.Pow(10, -27), //megatonne/attoliter 702
1.0 * Math.Pow(10, -11), //megatonne/centiliter 703
1.0 * Math.Pow(10, -60), //megatonne/cubic attometer 704
1.0 * Math.Pow(10, -12), //megatonne/cubic centimeter 705
0.001, //megatonne/cubic decameter 706
1.0 * Math.Pow(10, -9), //megatonne/cubic decimeter 707
0.001, //megatonne/cubic dekameter 708
1.0 * Math.Pow(10, 48), //megatonne/cubic exameter 709
1.0 * Math.Pow(10, -51), //megatonne/cubic femtometer 710
1.0 * Math.Pow(10, 21), //megatonne/cubic gigameter 711
1, //megatonne/cubic hectometer 712
1000, //megatonne/cubic kilometer 713
1000000000000, //megatonne/cubic megameter 714
0.000001, //megatonne/cubic meter 715
1.0 * Math.Pow(10, -24), //megatonne/cubic micrometer 716
1.0 * Math.Pow(10, -15), //megatonne/cubic millimeter 717
1000000, //megatonne/cubic myriameter 718
1.0 * Math.Pow(10, -33), //megatonne/cubic nanometer 719
1.0 * Math.Pow(10, 39), //megatonne/cubic petameter 720
1.0 * Math.Pow(10, -42), //megatonne/cubic picometer 721
1.0 * Math.Pow(10, 30), //megatonne/cubic terameter 722
1.0 * Math.Pow(10, -78), //megatonne/cubic yoctometer 723
1.0 * Math.Pow(10, 66), //megatonne/cubic yottameter 724
1.0 * Math.Pow(10, -69), //megatonne/cubic zeptometer 725
1.0 * Math.Pow(10, 57), //megatonne/cubic zettameter 726
1.0 * Math.Pow(10, -8), //megatonne/decaliter 727
1.0 * Math.Pow(10, -10), //megatonne/deciliter 728
1.0 * Math.Pow(10, -8), //megatonne/dekaliter 729
1000000000, //megatonne/exaliter 730
1.0 * Math.Pow(10, -24), //megatonne/femtoliter 731
1, //megatonne/gigaliter 732
1.0 * Math.Pow(10, -7), //megatonne/hectoliter 733
0.000001, //megatonne/kiloliter 734
1.0 * Math.Pow(10, -9), //megatonne/liter 735
1.0 * Math.Pow(10, -9), //megatonne/litre 736
0.001, //megatonne/megaliter 737
1.0 * Math.Pow(10, -15), //megatonne/microliter 738
1.0 * Math.Pow(10, -12), //megatonne/milliliter 739
0.00001, //megatonne/myrialiter 740
1.0 * Math.Pow(10, -18), //megatonne/nanoliter 741
1000000, //megatonne/petaliter 742
1.0 * Math.Pow(10, -21), //megatonne/picoliter 743
1000, //megatonne/teraliter 744
1.0 * Math.Pow(10, -33), //megatonne/yoctoliter 745
1000000000000000, //megatonne/yottaliter 746
1.0 * Math.Pow(10, -30), //megatonne/zeptoliter 747
1000000000000, //megatonne/zettaliter 748
1.0 * Math.Pow(10, -9), //microgram/attoliter 749
10000000, //microgram/centiliter 750
1.0 * Math.Pow(10, -42), //microgram/cubic attometer 751
1000000, //microgram/cubic centimeter 752
1000000000000000, //microgram/cubic decameter 753
1000000000, //microgram/cubic decimeter 754
1000000000000000, //microgram/cubic dekameter 755
1.0 * Math.Pow(10, 66), //microgram/cubic exameter 756
1.0 * Math.Pow(10, -33), //microgram/cubic femtometer 757
1.0 * Math.Pow(10, 39), //microgram/cubic gigameter 758
1000000000000000000, //microgram/cubic hectometer 759
1.0 * Math.Pow(10, 21), //microgram/cubic kilometer 760
1.0 * Math.Pow(10, 30), //microgram/cubic megameter 761
1000000000000, //microgram/cubic meter 762
0.000001, //microgram/cubic micrometer 763
1000, //microgram/cubic millimeter 764
1.0 * Math.Pow(10, 24), //microgram/cubic myriameter 765
1.0 * Math.Pow(10, -15), //microgram/cubic nanometer 766
1.0 * Math.Pow(10, 57), //microgram/cubic petameter 767
1.0 * Math.Pow(10, -24), //microgram/cubic picometer 768
1.0 * Math.Pow(10, 48), //microgram/cubic terameter 769
1.0 * Math.Pow(10, -60), //microgram/cubic yoctometer 770
1.0 * Math.Pow(10, 84), //microgram/cubic yottameter 771
1.0 * Math.Pow(10, -51), //microgram/cubic zeptometer 772
1.0 * Math.Pow(10, 75), //microgram/cubic zettameter 773
10000000000, //microgram/decaliter 774
100000000, //microgram/deciliter 775
10000000000, //microgram/dekaliter 776
1.0 * Math.Pow(10, 27), //microgram/exaliter 777
0.000001, //microgram/femtoliter 778
1000000000000000000, //microgram/gigaliter 779
100000000000, //microgram/hectoliter 780
1000000000000, //microgram/kiloliter 781
1000000000, //microgram/liter 782
1000000000, //microgram/litre 783
1000000000000000, //microgram/megaliter 784
1000, //microgram/microliter 785
1000000, //microgram/milliliter 786
10000000000000, //microgram/myrialiter 787
1, //microgram/nanoliter 788
1.0 * Math.Pow(10, 24), //microgram/petaliter 789
0.001, //microgram/picoliter 790
1.0 * Math.Pow(10, 21), //microgram/teraliter 791
1.0 * Math.Pow(10, -15), //microgram/yoctoliter 792
1.0 * Math.Pow(10, 33), //microgram/yottaliter 793
1.0 * Math.Pow(10, -12), //microgram/zeptoliter 794
1.0 * Math.Pow(10, 30), //microgram/zettaliter 795
1.0 * Math.Pow(10, -12), //milligram/attoliter 796
10000, //milligram/centiliter 797
1.0 * Math.Pow(10, -45), //milligram/cubic attometer 798
1000, //milligram/cubic centimeter 799
1000000000000, //milligram/cubic decameter 800
1000000, //milligram/cubic decimeter 801
1000000000000, //milligram/cubic dekameter 802
1.0 * Math.Pow(10, 63), //milligram/cubic exameter 803
1.0 * Math.Pow(10, -36), //milligram/cubic femtometer 804
1.0 * Math.Pow(10, 36), //milligram/cubic gigameter 805
1000000000000000, //milligram/cubic hectometer 806
1000000000000000000, //milligram/cubic kilometer 807
1.0 * Math.Pow(10, 27), //milligram/cubic megameter 808
1000000000, //milligram/cubic meter 809
1.0 * Math.Pow(10, -9), //milligram/cubic micrometer 810
1, //milligram/cubic millimeter 811
1.0 * Math.Pow(10, 21), //milligram/cubic myriameter 812
1.0 * Math.Pow(10, -18), //milligram/cubic nanometer 813
1.0 * Math.Pow(10, 54), //milligram/cubic petameter 814
1.0 * Math.Pow(10, -27), //milligram/cubic picometer 815
1.0 * Math.Pow(10, 45), //milligram/cubic terameter 816
1.0 * Math.Pow(10, -63), //milligram/cubic yoctometer 817
1.0 * Math.Pow(10, 81), //milligram/cubic yottameter 818
1.0 * Math.Pow(10, -54), //milligram/cubic zeptometer 819
1.0 * Math.Pow(10, 72), //milligram/cubic zettameter 820
10000000, //milligram/decaliter 821
100000, //milligram/deciliter 822
10000000, //milligram/dekaliter 823
1.0 * Math.Pow(10, 24), //milligram/exaliter 824
1.0 * Math.Pow(10, -9), //milligram/femtoliter 825
1000000000000000, //milligram/gigaliter 826
100000000, //milligram/hectoliter 827
1000000000, //milligram/kiloliter 828
1000000, //milligram/liter 829
1000000, //milligram/litre 830
1000000000000, //milligram/megaliter 831
1, //milligram/microliter 832
1000, //milligram/milliliter 833
10000000000, //milligram/myrialiter 834
0.001, //milligram/nanoliter 835
1.0 * Math.Pow(10, 21), //milligram/petaliter 836
0.000001, //milligram/picoliter 837
1000000000000000000, //milligram/teraliter 838
1.0 * Math.Pow(10, -18), //milligram/yoctoliter 839
1.0 * Math.Pow(10, 30), //milligram/yottaliter 840
1.0 * Math.Pow(10, -15), //milligram/zeptoliter 841
1.0 * Math.Pow(10, 27), //milligram/zettaliter 842
1.0 * Math.Pow(10, -19), //myriagram/attoliter 843
0.001, //myriagram/centiliter 844
1.0 * Math.Pow(10, -52), //myriagram/cubic attometer 845
0.0001, //myriagram/cubic centimeter 846
100000, //myriagram/cubic decameter 847
0.1, //myriagram/cubic decimeter 848
100000, //myriagram/cubic dekameter 849
1.0 * Math.Pow(10, 56), //myriagram/cubic exameter 850
1.0 * Math.Pow(10, -43), //myriagram/cubic femtometer 851
1.0 * Math.Pow(10, 29), //myriagram/cubic gigameter 852
100000000, //myriagram/cubic hectometer 853
100000000000, //myriagram/cubic kilometer 854
100000000000000000000, //myriagram/cubic megameter 855
100, //myriagram/cubic meter 856
1.0 * Math.Pow(10, -16), //myriagram/cubic micrometer 857
1.0 * Math.Pow(10, -7), //myriagram/cubic millimeter 858
100000000000000, //myriagram/cubic myriameter 859
1.0 * Math.Pow(10, -25), //myriagram/cubic nanometer 860
1.0 * Math.Pow(10, 47), //myriagram/cubic petameter 861
1.0 * Math.Pow(10, -34), //myriagram/cubic picometer 862
1.0 * Math.Pow(10, 38), //myriagram/cubic terameter 863
1.0 * Math.Pow(10, -70), //myriagram/cubic yoctometer 864
1.0 * Math.Pow(10, 74), //myriagram/cubic yottameter 865
1.0 * Math.Pow(10, -61), //myriagram/cubic zeptometer 866
1.0 * Math.Pow(10, 65), //myriagram/cubic zettameter 867
1, //myriagram/decaliter 868
0.01, //myriagram/deciliter 869
1, //myriagram/dekaliter 870
100000000000000000, //myriagram/exaliter 871
1.0 * Math.Pow(10, -16), //myriagram/femtoliter 872
100000000, //myriagram/gigaliter 873
10, //myriagram/hectoliter 874
100, //myriagram/kiloliter 875
0.1, //myriagram/liter 876
0.1, //myriagram/litre 877
100000, //myriagram/megaliter 878
1.0 * Math.Pow(10, -7), //myriagram/microliter 879
0.0001, //myriagram/milliliter 880
1000, //myriagram/myrialiter 881
1.0 * Math.Pow(10, -10), //myriagram/nanoliter 882
100000000000000, //myriagram/petaliter 883
1.0 * Math.Pow(10, -13), //myriagram/picoliter 884
100000000000, //myriagram/teraliter 885
1.0 * Math.Pow(10, -25), //myriagram/yoctoliter 886
1.0 * Math.Pow(10, 23), //myriagram/yottaliter 887
1.0 * Math.Pow(10, -22), //myriagram/zeptoliter 888
100000000000000000000, //myriagram/zettaliter 889
0.000001, //nanogram/attoliter 890
10000000000, //nanogram/centiliter 891
1.0 * Math.Pow(10, -39), //nanogram/cubic attometer 892
1000000000, //nanogram/cubic centimeter 893
1000000000000000000, //nanogram/cubic decameter 894
1000000000000, //nanogram/cubic decimeter 895
1000000000000000000, //nanogram/cubic dekameter 896
1.0 * Math.Pow(10, 69), //nanogram/cubic exameter 897
1.0 * Math.Pow(10, -30), //nanogram/cubic femtometer 898
1.0 * Math.Pow(10, 42), //nanogram/cubic gigameter 899
1.0 * Math.Pow(10, 21), //nanogram/cubic hectometer 900
1.0 * Math.Pow(10, 24), //nanogram/cubic kilometer 901
1.0 * Math.Pow(10, 33), //nanogram/cubic megameter 902
1000000000000000, //nanogram/cubic meter 903
0.001, //nanogram/cubic micrometer 904
1000000, //nanogram/cubic millimeter 905
1.0 * Math.Pow(10, 27), //nanogram/cubic myriameter 906
1.0 * Math.Pow(10, -12), //nanogram/cubic nanometer 907
1.0 * Math.Pow(10, 60), //nanogram/cubic petameter 908
1.0 * Math.Pow(10, -21), //nanogram/cubic picometer 909
1.0 * Math.Pow(10, 51), //nanogram/cubic terameter 910
1.0 * Math.Pow(10, -57), //nanogram/cubic yoctometer 911
1.0 * Math.Pow(10, 87), //nanogram/cubic yottameter 912
1.0 * Math.Pow(10, -48), //nanogram/cubic zeptometer 913
1.0 * Math.Pow(10, 78), //nanogram/cubic zettameter 914
10000000000000, //nanogram/decaliter 915
100000000000, //nanogram/deciliter 916
10000000000000, //nanogram/dekaliter 917
1.0 * Math.Pow(10, 30), //nanogram/exaliter 918
0.001, //nanogram/femtoliter 919
1.0 * Math.Pow(10, 21), //nanogram/gigaliter 920
100000000000000, //nanogram/hectoliter 921
1000000000000000, //nanogram/kiloliter 922
1000000000000, //nanogram/liter 923
1000000000000, //nanogram/litre 924
1000000000000000000, //nanogram/megaliter 925
1000000, //nanogram/microliter 926
1000000000, //nanogram/milliliter 927
10000000000000000, //nanogram/myrialiter 928
1000, //nanogram/nanoliter 929
1.0 * Math.Pow(10, 27), //nanogram/petaliter 930
1, //nanogram/picoliter 931
1.0 * Math.Pow(10, 24), //nanogram/teraliter 932
1.0 * Math.Pow(10, -12), //nanogram/yoctoliter 933
1.0 * Math.Pow(10, 36), //nanogram/yottaliter 934
1.0 * Math.Pow(10, -9), //nanogram/zeptoliter 935
1.0 * Math.Pow(10, 33), //nanogram/zettaliter 936
998.84737348, //ounce/cubic foot 937
0.57803667444, //ounce/cubic inch 938
147028287730000, //ounce/cubic mile 939
26968.879083, //ounce/cubic yard 940
160.35860568, //ounce/gallon [UK] 941
155.37770355, //ounce/gallon [US dry] 942
133.5264718, //ounce/gallon [US] 943
1.0022412855, //ounce/ounce [UK] 944
1.0431755609, //ounce/ounce [US] 945
40.089651419, //ounce/quart [UK] 946
38.844425888, //ounce/quart [US dry] 947
33.381617949, //ounce/quart [US] 948
1.0 * Math.Pow(10, -30), //petagram/attoliter 949
1.0 * Math.Pow(10, -14), //petagram/centiliter 950
1.0 * Math.Pow(10, -63), //petagram/cubic attometer 951
1.0 * Math.Pow(10, -15), //petagram/cubic centimeter 952
0.000001, //petagram/cubic decameter 953
1.0 * Math.Pow(10, -12), //petagram/cubic decimeter 954
0.000001, //petagram/cubic dekameter 955
1.0 * Math.Pow(10, 45), //petagram/cubic exameter 956
1.0 * Math.Pow(10, -54), //petagram/cubic femtometer 957
1000000000000000000, //petagram/cubic gigameter 958
0.001, //petagram/cubic hectometer 959
1, //petagram/cubic kilometer 960
1000000000, //petagram/cubic megameter 961
1.0 * Math.Pow(10, -9), //petagram/cubic meter 962
1.0 * Math.Pow(10, -27), //petagram/cubic micrometer 963
1.0 * Math.Pow(10, -18), //petagram/cubic millimeter 964
1000, //petagram/cubic myriameter 965
1.0 * Math.Pow(10, -36), //petagram/cubic nanometer 966
1.0 * Math.Pow(10, 36), //petagram/cubic petameter 967
1.0 * Math.Pow(10, -45), //petagram/cubic picometer 968
1.0 * Math.Pow(10, 27), //petagram/cubic terameter 969
1.0 * Math.Pow(10, -81), //petagram/cubic yoctometer 970
1.0 * Math.Pow(10, 63), //petagram/cubic yottameter 971
1.0 * Math.Pow(10, -72), //petagram/cubic zeptometer 972
1.0 * Math.Pow(10, 54), //petagram/cubic zettameter 973
1.0 * Math.Pow(10, -11), //petagram/decaliter 974
1.0 * Math.Pow(10, -13), //petagram/deciliter 975
1.0 * Math.Pow(10, -11), //petagram/dekaliter 976
1000000, //petagram/exaliter 977
1.0 * Math.Pow(10, -27), //petagram/femtoliter 978
0.001, //petagram/gigaliter 979
1.0 * Math.Pow(10, -10), //petagram/hectoliter 980
1.0 * Math.Pow(10, -9), //petagram/kiloliter 981
1.0 * Math.Pow(10, -12), //petagram/liter 982
1.0 * Math.Pow(10, -12), //petagram/litre 983
0.000001, //petagram/megaliter 984
1.0 * Math.Pow(10, -18), //petagram/microliter 985
1.0 * Math.Pow(10, -15), //petagram/milliliter 986
1.0 * Math.Pow(10, -8), //petagram/myrialiter 987
1.0 * Math.Pow(10, -21), //petagram/nanoliter 988
1000, //petagram/petaliter 989
1.0 * Math.Pow(10, -24), //petagram/picoliter 990
1, //petagram/teraliter 991
1.0 * Math.Pow(10, -36), //petagram/yoctoliter 992
1000000000000, //petagram/yottaliter 993
1.0 * Math.Pow(10, -33), //petagram/zeptoliter 994
1000000000, //petagram/zettaliter 995
0.001, //picogram/attoliter 996
10000000000000, //picogram/centiliter 997
1.0 * Math.Pow(10, -36), //picogram/cubic attometer 998
1000000000000, //picogram/cubic centimeter 999
1.0 * Math.Pow(10, 21), //picogram/cubic decameter 1000
1000000000000000, //picogram/cubic decimeter 1001
1.0 * Math.Pow(10, 21), //picogram/cubic dekameter 1002
1.0 * Math.Pow(10, 72), //picogram/cubic exameter 1003
1.0 * Math.Pow(10, -27), //picogram/cubic femtometer 1004
1.0 * Math.Pow(10, 45), //picogram/cubic gigameter 1005
1.0 * Math.Pow(10, 24), //picogram/cubic hectometer 1006
1.0 * Math.Pow(10, 27), //picogram/cubic kilometer 1007
1.0 * Math.Pow(10, 36), //picogram/cubic megameter 1008
1000000000000000000, //picogram/cubic meter 1009
1, //picogram/cubic micrometer 1010
1000000000, //picogram/cubic millimeter 1011
1.0 * Math.Pow(10, 30), //picogram/cubic myriameter 1012
1.0 * Math.Pow(10, -9), //picogram/cubic nanometer 1013
1.0 * Math.Pow(10, 63), //picogram/cubic petameter 1014
1.0 * Math.Pow(10, -18), //picogram/cubic picometer 1015
1.0 * Math.Pow(10, 54), //picogram/cubic terameter 1016
1.0 * Math.Pow(10, -54), //picogram/cubic yoctometer 1017
1.0 * Math.Pow(10, 90), //picogram/cubic yottameter 1018
1.0 * Math.Pow(10, -45), //picogram/cubic zeptometer 1019
1.0 * Math.Pow(10, 81), //picogram/cubic zettameter 1020
10000000000000000, //picogram/decaliter 1021
100000000000000, //picogram/deciliter 1022
10000000000000000, //picogram/dekaliter 1023
1.0 * Math.Pow(10, 33), //picogram/exaliter 1024
1, //picogram/femtoliter 1025
1.0 * Math.Pow(10, 24), //picogram/gigaliter 1026
100000000000000000, //picogram/hectoliter 1027
1000000000000000000, //picogram/kiloliter 1028
1000000000000000, //picogram/liter 1029
1000000000000000, //picogram/litre 1030
1.0 * Math.Pow(10, 21), //picogram/megaliter 1031
1000000000, //picogram/microliter 1032
1000000000000, //picogram/milliliter 1033
10000000000000000000, //picogram/myrialiter 1034
1000000, //picogram/nanoliter 1035
1.0 * Math.Pow(10, 30), //picogram/petaliter 1036
1000, //picogram/picoliter 1037
1.0 * Math.Pow(10, 27), //picogram/teraliter 1038
1.0 * Math.Pow(10, -9), //picogram/yoctoliter 1039
1.0 * Math.Pow(10, 39), //picogram/yottaliter 1040
0.000001, //picogram/zeptoliter 1041
1.0 * Math.Pow(10, 36), //picogram/zettaliter 1042
62.427960841, //pound/cubic foot 1043
0.036127292153, //pound/cubic inch 1044
9189267982900, //pound/cubic mile 1045
1685.5549427, //pound/cubic yard 1046
10.022412855, //pound/gallon [UK] 1047
9.7111064718, //pound/gallon [US dry] 1048
8.3454044873, //pound/gallon [US] 1049
0.062640080344, //pound/ounce [UK] 1050
0.065198472556, //pound/ounce [US] 1051
2.5056032138, //pound/quart [UK] 1052
2.427776618, //pound/quart [US dry] 1053
2.0863511218, //pound/quart [US] 1054
1.0 * Math.Pow(10, -27), //teragram/attoliter 1055
1.0 * Math.Pow(10, -11), //teragram/centiliter 1056
1.0 * Math.Pow(10, -60), //teragram/cubic attometer 1057
1.0 * Math.Pow(10, -12), //teragram/cubic centimeter 1058
0.001, //teragram/cubic decameter 1059
1.0 * Math.Pow(10, -9), //teragram/cubic decimeter 1060
0.001, //teragram/cubic dekameter 1061
1.0 * Math.Pow(10, 48), //teragram/cubic exameter 1062
1.0 * Math.Pow(10, -51), //teragram/cubic femtometer 1063
1.0 * Math.Pow(10, 21), //teragram/cubic gigameter 1064
1, //teragram/cubic hectometer 1065
1000, //teragram/cubic kilometer 1066
1000000000000, //teragram/cubic megameter 1067
0.000001, //teragram/cubic meter 1068
1.0 * Math.Pow(10, -24), //teragram/cubic micrometer 1069
1.0 * Math.Pow(10, -15), //teragram/cubic millimeter 1070
1000000, //teragram/cubic myriameter 1071
1.0 * Math.Pow(10, -33), //teragram/cubic nanometer 1072
1.0 * Math.Pow(10, 39), //teragram/cubic petameter 1073
1.0 * Math.Pow(10, -42), //teragram/cubic picometer 1074
1.0 * Math.Pow(10, 30), //teragram/cubic terameter 1075
1.0 * Math.Pow(10, -78), //teragram/cubic yoctometer 1076
1.0 * Math.Pow(10, 66), //teragram/cubic yottameter 1077
1.0 * Math.Pow(10, -69), //teragram/cubic zeptometer 1078
1.0 * Math.Pow(10, 57), //teragram/cubic zettameter 1079
1.0 * Math.Pow(10, -8), //teragram/decaliter 1080
1.0 * Math.Pow(10, -10), //teragram/deciliter 1081
1.0 * Math.Pow(10, -8), //teragram/dekaliter 1082
1000000000, //teragram/exaliter 1083
1.0 * Math.Pow(10, -24), //teragram/femtoliter 1084
1, //teragram/gigaliter 1085
1.0 * Math.Pow(10, -7), //teragram/hectoliter 1086
0.000001, //teragram/kiloliter 1087
1.0 * Math.Pow(10, -9), //teragram/liter 1088
1.0 * Math.Pow(10, -9), //teragram/litre 1089
0.001, //teragram/megaliter 1090
1.0 * Math.Pow(10, -15), //teragram/microliter 1091
1.0 * Math.Pow(10, -12), //teragram/milliliter 1092
0.00001, //teragram/myrialiter 1093
1.0 * Math.Pow(10, -18), //teragram/nanoliter 1094
1000000, //teragram/petaliter 1095
1.0 * Math.Pow(10, -21), //teragram/picoliter 1096
1000, //teragram/teraliter 1097
1.0 * Math.Pow(10, -33), //teragram/yoctoliter 1098
1000000000000000, //teragram/yottaliter 1099
1.0 * Math.Pow(10, -30), //teragram/zeptoliter 1100
1000000000000, //teragram/zettaliter 1101
1.0 * Math.Pow(10, -21), //tonne/attoliter 1102
0.00001, //tonne/centiliter 1103
1.0 * Math.Pow(10, -54), //tonne/cubic attometer 1104
0.000001, //tonne/cubic centimeter 1105
1000, //tonne/cubic decameter 1106
0.001, //tonne/cubic decimeter 1107
1000, //tonne/cubic dekameter 1108
1.0 * Math.Pow(10, 54), //tonne/cubic exameter 1109
1.0 * Math.Pow(10, -45), //tonne/cubic femtometer 1110
1.0 * Math.Pow(10, 27), //tonne/cubic gigameter 1111
1000000, //tonne/cubic hectometer 1112
1000000000, //tonne/cubic kilometer 1113
1000000000000000000, //tonne/cubic megameter 1114
1, //tonne/cubic meter 1115
1.0 * Math.Pow(10, -18), //tonne/cubic micrometer 1116
1.0 * Math.Pow(10, -9), //tonne/cubic millimeter 1117
1000000000000, //tonne/cubic myriameter 1118
1.0 * Math.Pow(10, -27), //tonne/cubic nanometer 1119
1.0 * Math.Pow(10, 45), //tonne/cubic petameter 1120
1.0 * Math.Pow(10, -36), //tonne/cubic picometer 1121
1.0 * Math.Pow(10, 36), //tonne/cubic terameter 1122
1.0 * Math.Pow(10, -72), //tonne/cubic yoctometer 1123
1.0 * Math.Pow(10, 72), //tonne/cubic yottameter 1124
1.0 * Math.Pow(10, -63), //tonne/cubic zeptometer 1125
1.0 * Math.Pow(10, 63), //tonne/cubic zettameter 1126
0.01, //tonne/decaliter 1127
0.0001, //tonne/deciliter 1128
0.01, //tonne/dekaliter 1129
1000000000000000, //tonne/exaliter 1130
1.0 * Math.Pow(10, -18), //tonne/femtoliter 1131
1000000, //tonne/gigaliter 1132
0.1, //tonne/hectoliter 1133
1, //tonne/kiloliter 1134
0.001, //tonne/liter 1135
0.001, //tonne/litre 1136
1000, //tonne/megaliter 1137
1.0 * Math.Pow(10, -9), //tonne/microliter 1138
0.000001, //tonne/milliliter 1139
10, //tonne/myrialiter 1140
1.0 * Math.Pow(10, -12), //tonne/nanoliter 1141
1000000000000, //tonne/petaliter 1142
1.0 * Math.Pow(10, -15), //tonne/picoliter 1143
1000000000, //tonne/teraliter 1144
1.0 * Math.Pow(10, -27), //tonne/yoctoliter 1145
1.0 * Math.Pow(10, 21), //tonne/yottaliter 1146
1.0 * Math.Pow(10, -24), //tonne/zeptoliter 1147
1000000000000000000, //tonne/zettaliter 1148
1000000000, //yoctogram/attoliter 1149
1.0 * Math.Pow(10, 25), //yoctogram/centiliter 1150
1.0 * Math.Pow(10, -24), //yoctogram/cubic attometer 1151
1.0 * Math.Pow(10, 24), //yoctogram/cubic centimeter 1152
1.0 * Math.Pow(10, 33), //yoctogram/cubic decameter 1153
1.0 * Math.Pow(10, 27), //yoctogram/cubic decimeter 1154
1.0 * Math.Pow(10, 33), //yoctogram/cubic dekameter 1155
1.0 * Math.Pow(10, 84), //yoctogram/cubic exameter 1156
1.0 * Math.Pow(10, -15), //yoctogram/cubic femtometer 1157
1.0 * Math.Pow(10, 57), //yoctogram/cubic gigameter 1158
1.0 * Math.Pow(10, 36), //yoctogram/cubic hectometer 1159
1.0 * Math.Pow(10, 39), //yoctogram/cubic kilometer 1160
1.0 * Math.Pow(10, 48), //yoctogram/cubic megameter 1161
1.0 * Math.Pow(10, 30), //yoctogram/cubic meter 1162
1000000000000, //yoctogram/cubic micrometer 1163
1.0 * Math.Pow(10, 21), //yoctogram/cubic millimeter 1164
1.0 * Math.Pow(10, 42), //yoctogram/cubic myriameter 1165
1000, //yoctogram/cubic nanometer 1166
1.0 * Math.Pow(10, 75), //yoctogram/cubic petameter 1167
0.000001, //yoctogram/cubic picometer 1168
1.0 * Math.Pow(10, 66), //yoctogram/cubic terameter 1169
1.0 * Math.Pow(10, -42), //yoctogram/cubic yoctometer 1170
1.0 * Math.Pow(10, 102), //yoctogram/cubic yottameter 1171
1.0 * Math.Pow(10, -33), //yoctogram/cubic zeptometer 1172
1.0 * Math.Pow(10, 93), //yoctogram/cubic zettameter 1173
1.0 * Math.Pow(10, 28), //yoctogram/decaliter 1174
1.0 * Math.Pow(10, 26), //yoctogram/deciliter 1175
1.0 * Math.Pow(10, 28), //yoctogram/dekaliter 1176
1.0 * Math.Pow(10, 45), //yoctogram/exaliter 1177
1000000000000, //yoctogram/femtoliter 1178
1.0 * Math.Pow(10, 36), //yoctogram/gigaliter 1179
1.0 * Math.Pow(10, 29), //yoctogram/hectoliter 1180
1.0 * Math.Pow(10, 30), //yoctogram/kiloliter 1181
1.0 * Math.Pow(10, 27), //yoctogram/liter 1182
1.0 * Math.Pow(10, 27), //yoctogram/litre 1183
1.0 * Math.Pow(10, 33), //yoctogram/megaliter 1184
1.0 * Math.Pow(10, 21), //yoctogram/microliter 1185
1.0 * Math.Pow(10, 24), //yoctogram/milliliter 1186
1.0 * Math.Pow(10, 31), //yoctogram/myrialiter 1187
1000000000000000000, //yoctogram/nanoliter 1188
1.0 * Math.Pow(10, 42), //yoctogram/petaliter 1189
1000000000000000, //yoctogram/picoliter 1190
1.0 * Math.Pow(10, 39), //yoctogram/teraliter 1191
1000, //yoctogram/yoctoliter 1192
1.0 * Math.Pow(10, 51), //yoctogram/yottaliter 1193
1000000, //yoctogram/zeptoliter 1194
1.0 * Math.Pow(10, 48), //yoctogram/zettaliter 1195
1.0 * Math.Pow(10, -39), //yottagram/attoliter 1196
1.0 * Math.Pow(10, -23), //yottagram/centiliter 1197
1.0 * Math.Pow(10, -72), //yottagram/cubic attometer 1198
1.0 * Math.Pow(10, -24), //yottagram/cubic centimeter 1199
1.0 * Math.Pow(10, -15), //yottagram/cubic decameter 1200
1.0 * Math.Pow(10, -21), //yottagram/cubic decimeter 1201
1.0 * Math.Pow(10, -15), //yottagram/cubic dekameter 1202
1.0 * Math.Pow(10, 36), //yottagram/cubic exameter 1203
1.0 * Math.Pow(10, -63), //yottagram/cubic femtometer 1204
1000000000, //yottagram/cubic gigameter 1205
1.0 * Math.Pow(10, -12), //yottagram/cubic hectometer 1206
1.0 * Math.Pow(10, -9), //yottagram/cubic kilometer 1207
1, //yottagram/cubic megameter 1208
1.0 * Math.Pow(10, -18), //yottagram/cubic meter 1209
1.0 * Math.Pow(10, -36), //yottagram/cubic micrometer 1210
1.0 * Math.Pow(10, -27), //yottagram/cubic millimeter 1211
0.000001, //yottagram/cubic myriameter 1212
1.0 * Math.Pow(10, -45), //yottagram/cubic nanometer 1213
1.0 * Math.Pow(10, 27), //yottagram/cubic petameter 1214
1.0 * Math.Pow(10, -54), //yottagram/cubic picometer 1215
1000000000000000000, //yottagram/cubic terameter 1216
1.0 * Math.Pow(10, -90), //yottagram/cubic yoctometer 1217
1.0 * Math.Pow(10, 54), //yottagram/cubic yottameter 1218
1.0 * Math.Pow(10, -81), //yottagram/cubic zeptometer 1219
1.0 * Math.Pow(10, 45), //yottagram/cubic zettameter 1220
1.0 * Math.Pow(10, -20), //yottagram/decaliter 1221
1.0 * Math.Pow(10, -22), //yottagram/deciliter 1222
1.0 * Math.Pow(10, -20), //yottagram/dekaliter 1223
0.001, //yottagram/exaliter 1224
1.0 * Math.Pow(10, -36), //yottagram/femtoliter 1225
1.0 * Math.Pow(10, -12), //yottagram/gigaliter 1226
1.0 * Math.Pow(10, -19), //yottagram/hectoliter 1227
1.0 * Math.Pow(10, -18), //yottagram/kiloliter 1228
1.0 * Math.Pow(10, -21), //yottagram/liter 1229
1.0 * Math.Pow(10, -21), //yottagram/litre 1230
1.0 * Math.Pow(10, -15), //yottagram/megaliter 1231
1.0 * Math.Pow(10, -27), //yottagram/microliter 1232
1.0 * Math.Pow(10, -24), //yottagram/milliliter 1233
1.0 * Math.Pow(10, -17), //yottagram/myrialiter 1234
1.0 * Math.Pow(10, -30), //yottagram/nanoliter 1235
0.000001, //yottagram/petaliter 1236
1.0 * Math.Pow(10, -33), //yottagram/picoliter 1237
1.0 * Math.Pow(10, -9), //yottagram/teraliter 1238
1.0 * Math.Pow(10, -45), //yottagram/yoctoliter 1239
1000, //yottagram/yottaliter 1240
1.0 * Math.Pow(10, -42), //yottagram/zeptoliter 1241
1, //yottagram/zettaliter 1242
1000000, //zeptogram/attoliter 1243
1.0 * Math.Pow(10, 22), //zeptogram/centiliter 1244
1.0 * Math.Pow(10, -27), //zeptogram/cubic attometer 1245
1.0 * Math.Pow(10, 21), //zeptogram/cubic centimeter 1246
1.0 * Math.Pow(10, 30), //zeptogram/cubic decameter 1247
1.0 * Math.Pow(10, 24), //zeptogram/cubic decimeter 1248
1.0 * Math.Pow(10, 30), //zeptogram/cubic dekameter 1249
1.0 * Math.Pow(10, 81), //zeptogram/cubic exameter 1250
1.0 * Math.Pow(10, -18), //zeptogram/cubic femtometer 1251
1.0 * Math.Pow(10, 54), //zeptogram/cubic gigameter 1252
1.0 * Math.Pow(10, 33), //zeptogram/cubic hectometer 1253
1.0 * Math.Pow(10, 36), //zeptogram/cubic kilometer 1254
1.0 * Math.Pow(10, 45), //zeptogram/cubic megameter 1255
1.0 * Math.Pow(10, 27), //zeptogram/cubic meter 1256
1000000000, //zeptogram/cubic micrometer 1257
1000000000000000000, //zeptogram/cubic millimeter 1258
1.0 * Math.Pow(10, 39), //zeptogram/cubic myriameter 1259
1, //zeptogram/cubic nanometer 1260
1.0 * Math.Pow(10, 72), //zeptogram/cubic petameter 1261
1.0 * Math.Pow(10, -9), //zeptogram/cubic picometer 1262
1.0 * Math.Pow(10, 63), //zeptogram/cubic terameter 1263
1.0 * Math.Pow(10, -45), //zeptogram/cubic yoctometer 1264
1.0 * Math.Pow(10, 99), //zeptogram/cubic yottameter 1265
1.0 * Math.Pow(10, -36), //zeptogram/cubic zeptometer 1266
1.0 * Math.Pow(10, 90), //zeptogram/cubic zettameter 1267
1.0 * Math.Pow(10, 25), //zeptogram/decaliter 1268
1.0 * Math.Pow(10, 23), //zeptogram/deciliter 1269
1.0 * Math.Pow(10, 25), //zeptogram/dekaliter 1270
1.0 * Math.Pow(10, 42), //zeptogram/exaliter 1271
1000000000, //zeptogram/femtoliter 1272
1.0 * Math.Pow(10, 33), //zeptogram/gigaliter 1273
1.0 * Math.Pow(10, 26), //zeptogram/hectoliter 1274
1.0 * Math.Pow(10, 27), //zeptogram/kiloliter 1275
1.0 * Math.Pow(10, 24), //zeptogram/liter 1276
1.0 * Math.Pow(10, 24), //zeptogram/litre 1277
1.0 * Math.Pow(10, 30), //zeptogram/megaliter 1278
1000000000000000000, //zeptogram/microliter 1279
1.0 * Math.Pow(10, 21), //zeptogram/milliliter 1280
1.0 * Math.Pow(10, 28), //zeptogram/myrialiter 1281
1000000000000000, //zeptogram/nanoliter 1282
1.0 * Math.Pow(10, 39), //zeptogram/petaliter 1283
1000000000000, //zeptogram/picoliter 1284
1.0 * Math.Pow(10, 36), //zeptogram/teraliter 1285
1, //zeptogram/yoctoliter 1286
1.0 * Math.Pow(10, 48), //zeptogram/yottaliter 1287
1000, //zeptogram/zeptoliter 1288
1.0 * Math.Pow(10, 45), //zeptogram/zettaliter 1289
1.0 * Math.Pow(10, -36), //zettagram/attoliter 1290
1.0 * Math.Pow(10, -20), //zettagram/centiliter 1291
1.0 * Math.Pow(10, -69), //zettagram/cubic attometer 1292
1.0 * Math.Pow(10, -21), //zettagram/cubic centimeter 1293
1.0 * Math.Pow(10, -12), //zettagram/cubic decameter 1294
1.0 * Math.Pow(10, -18), //zettagram/cubic decimeter 1295
1.0 * Math.Pow(10, -12), //zettagram/cubic dekameter 1296
1.0 * Math.Pow(10, 39), //zettagram/cubic exameter 1297
1.0 * Math.Pow(10, -60), //zettagram/cubic femtometer 1298
1000000000000, //zettagram/cubic gigameter 1299
1.0 * Math.Pow(10, -9), //zettagram/cubic hectometer 1300
0.000001, //zettagram/cubic kilometer 1301
1000, //zettagram/cubic megameter 1302
1.0 * Math.Pow(10, -15), //zettagram/cubic meter 1303
1.0 * Math.Pow(10, -33), //zettagram/cubic micrometer 1304
1.0 * Math.Pow(10, -24), //zettagram/cubic millimeter 1305
0.001, //zettagram/cubic myriameter 1306
1.0 * Math.Pow(10, -42), //zettagram/cubic nanometer 1307
1.0 * Math.Pow(10, 30), //zettagram/cubic petameter 1308
1.0 * Math.Pow(10, -51), //zettagram/cubic picometer 1309
1.0 * Math.Pow(10, 21), //zettagram/cubic terameter 1310
1.0 * Math.Pow(10, -87), //zettagram/cubic yoctometer 1311
1.0 * Math.Pow(10, 57), //zettagram/cubic yottameter 1312
1.0 * Math.Pow(10, -78), //zettagram/cubic zeptometer 1313
1.0 * Math.Pow(10, 48), //zettagram/cubic zettameter 1314
1.0 * Math.Pow(10, -17), //zettagram/decaliter 1315
1.0 * Math.Pow(10, -19), //zettagram/deciliter 1316
1.0 * Math.Pow(10, -17), //zettagram/dekaliter 1317
1, //zettagram/exaliter 1318
1.0 * Math.Pow(10, -33), //zettagram/femtoliter 1319
1.0 * Math.Pow(10, -9), //zettagram/gigaliter 1320
1.0 * Math.Pow(10, -16), //zettagram/hectoliter 1321
1.0 * Math.Pow(10, -15), //zettagram/kiloliter 1322
1.0 * Math.Pow(10, -18), //zettagram/liter 1323
1.0 * Math.Pow(10, -18), //zettagram/litre 1324
1.0 * Math.Pow(10, -12), //zettagram/megaliter 1325
1.0 * Math.Pow(10, -24), //zettagram/microliter 1326
1.0 * Math.Pow(10, -21), //zettagram/milliliter 1327
1.0 * Math.Pow(10, -14), //zettagram/myrialiter 1328
1.0 * Math.Pow(10, -27), //zettagram/nanoliter 1329
0.001, //zettagram/petaliter 1330
1.0 * Math.Pow(10, -30), //zettagram/picoliter 1331
0.000001, //zettagram/teraliter 1332
1.0 * Math.Pow(10, -42), //zettagram/yoctoliter 1333
1000000, //zettagram/yottaliter 1334
1.0 * Math.Pow(10, -39), //zettagram/zeptoliter 1335
1000, //zettagram/zettaliter 1336
};

         * 
         * */

        /* energy
         * primary value is joule
         * 
         * string[] energy = 
            {
            "attojoule",
            "Board of Trade unit",
            "Btu",
            "Btu [thermochemical]",
            "calorie [I.T.]",
            "calorie [15° C]",
            "Calorie [nutritional]",
            "calorie [thermochemical]",
            "celsius heat unit",
            "centijoule",
            "cheval vapeur heure",
            "decijoule",
            "dekajoule",
            "dekawatt hour",
            "dekatherm",
            "electronvolt",
            "erg",
            "exajoule",
            "exawatt hour",
            "femtojoule",
            "foot pound",
            "foot poundal",
            "gallon [UK] of automotive gasoline",
            "gallon [U.S.] of automotive gasoline",
            "gallon [UK] of aviation gasoline",
            "gallon [U.S.] of aviation gasoline",
            "gallon [UK] of diesel oil",
            "gallon [U.S.] of diesel oil",
            "gallon [UK] of distilate #2 fuel oil",
            "gallon [U.S.] of distilate #2 fuel oil",
            "gallon [UK] of kerosene type jet fuel",
            "gallon [U.S.] of kerosene type jet fuel",
            "gallon [UK] of LPG",
            "gallon [U.S.] of LPG",
            "gallon [UK] of naphtha type jet fuel",
            "gallon [U.S.] of naphtha type jet fuel",
            "gallon [UK] of kerosene",
            "gallon [U.S.] of kerosene",
            "gallon [UK] of residual fuel oil",
            "gallon [U.S.] of residual fuel oil",
            "gigacalorie [I.T.]",
            "gigacalorie [15° C]",
            "gigaelectronvolt",
            "gigajoule",
            "gigawatt hour",
            "gram calorie",
            "hartree",
            "hectojoule",
            "hectowatt hour",
            "horsepower hour",
            "hundred cubic foot of natural gas",
            "inch ounce",
            "inch pound",
            "joule",
            "kilocalorie [15° C]",
            "kilocalorie [I.T.]",
            "kilocalorie [thermochemical]",
            "kiloelectronvolt",
            "kilogram calorie",
            "kilogram-force meter",
            "kilojoule",
            "kilopond meter",
            "kiloton [explosive]",
            "kilowatt hour",
            "liter atmosphere",
            "megaelectronvolt",
            "megacalorie [I.T.]",
            "megacalorie [15° C]",
            "megajoule",
            "megalerg",
            "megaton [explosive]",
            "megawatthour",
            "meter kilogram-force",
            "microjoule",
            "millijoule",
            "myriawatt hour",
            "nanojoule",
            "newton meter",
            "petajoule",
            "petawatthour",
            "pferdestärkenstunde",
            "picojoule",
            "Q unit",
            "quad",
            "teraelectronvolt",
            "terajoule",
            "terawatthour",
            "therm [Europe]",
            "therm [U.S. (uncommon)]",
            "thermie",
            "ton [explosive]",
            "tonne of coal equivalent",
            "tonne of oil equivalent",
            "watthour",
            "wattsecond",
            "yoctojoule",
            "yottajoule",
            "yottawatthour",
            "zeptojoule",
            "zettajoule",
            "zettawatthour"
            };
            double[] energyConversions = 
            {
            1000000000000000000, //attojoule 0
            2.7777777778 * Math.Pow(10, -7), //Board of Trade unit 1
            0.00094781707775, //Btu 2
            0.00094845138281, //Btu [thermochemical] 3
            0.23884589663, //calorie [I.T.] 4
            0.23890295762, //calorie [15° C] 5
            0.00023884589663, //Calorie [nutritional] 6
            0.23900573614, //calorie [thermochemical] 7
            0.00052656507647, //celsius heat unit 8
            100, //centijoule 9
            3.7767267147 * Math.Pow(10, -7), //cheval vapeur heure 10
            10, //decijoule 11
            0.1, //dekajoule 12
            0.000027777777778, //dekawatt hour 13
            9.4781608956 * Math.Pow(10, -10), //dekatherm 14
            6241506480000000000, //electronvolt 15
            10000000, //erg 16
            1.0 * Math.Pow(10, -18), //exajoule 17
            2.7777777778 * Math.Pow(10, -22), //exawatt hour 18
            1000000000000000, //femtojoule 19
            0.73756217557, //foot pound 20
            23.730360457, //foot poundal 21
            6.3196276031 * Math.Pow(10, -9), //gallon [UK] of automotive gasoline 22
            7.5895567699 * Math.Pow(10, -9), //gallon [U.S.] of automotive gasoline 23
            6.3196276031 * Math.Pow(10, -9), //gallon [UK] of aviation gasoline 24
            7.5895567699 * Math.Pow(10, -9), //gallon [U.S.] of aviation gasoline 25
            5.6830066406 * Math.Pow(10, -9), //gallon [UK] of diesel oil 26
            6.825006825 * Math.Pow(10, -9), //gallon [U.S.] of diesel oil 27
            5.6830066406 * Math.Pow(10, -9), //gallon [UK] of distilate #2 fuel oil 28
            6.825006825 * Math.Pow(10, -9), //gallon [U.S.] of distilate #2 fuel oil 29
            5.8556549436 * Math.Pow(10, -9), //gallon [UK] of kerosene type jet fuel 30
            7.0323488045 * Math.Pow(10, -9), //gallon [U.S.] of kerosene type jet fuel 31
            8.2641127059 * Math.Pow(10, -9), //gallon [UK] of LPG 32
            9.9247861544 * Math.Pow(10, -9), //gallon [U.S.] of LPG 33
            6.2176981256 * Math.Pow(10, -9), //gallon [UK] of naphtha type jet fuel 34
            7.4671445639 * Math.Pow(10, -9), //gallon [U.S.] of naphtha type jet fuel 35
            5.8556549436 * Math.Pow(10, -9), //gallon [UK] of kerosene 36
            7.0323488045 * Math.Pow(10, -9), //gallon [U.S.] of kerosene 37
            5.2687555871 * Math.Pow(10, -9), //gallon [UK] of residual fuel oil 38
            6.3275120223 * Math.Pow(10, -9), //gallon [U.S.] of residual fuel oil 39
            2.3884589663 * Math.Pow(10, -10), //gigacalorie [I.T.] 40
            2.3890295762 * Math.Pow(10, -10), //gigacalorie [15° C] 41
            6241506480, //gigaelectronvolt 42
            1.0 * Math.Pow(10, -9), //gigajoule 43
            2.7777777778 * Math.Pow(10, -13), //gigawatt hour 44
            0.23890295762, //gram calorie 45
            229371044870000000, //hartree 46
            0.01, //hectojoule 47
            0.0000027777777778, //hectowatt hour 48
            3.7250614123 * Math.Pow(10, -7), //horsepower hour 49
            9.1979396615 * Math.Pow(10, -9), //hundred cubic foot of natural gas 50
            141.61193295, //inch ounce 51
            8.8507461068, //inch pound 52
            1, //joule 53
            0.00023890295762, //kilocalorie [15° C] 54
            0.00023884589663, //kilocalorie [I.T.] 55
            0.00023900573614, //kilocalorie [thermochemical] 56
            6241506480000000, //kiloelectronvolt 57
            0.00023890295762, //kilogram calorie 58
            0.1019716213, //kilogram-force meter 59
            0.001, //kilojoule 60
            0.1019716213, //kilopond meter 61
            2.3900573614 * Math.Pow(10, -13), //kiloton [explosive] 62
            2.7777777778 * Math.Pow(10, -7), //kilowatt hour 63
            0.0098692326672, //liter atmosphere 64
            6241506480000, //megaelectronvolt 65
            2.3884589663 * Math.Pow(10, -7), //megacalorie [I.T.] 66
            2.3890295762 * Math.Pow(10, -7), //megacalorie [15° C] 67
            0.000001, //megajoule 68
            10, //megalerg 69
            2.3900573614 * Math.Pow(10, -16), //megaton [explosive] 70
            2.7777777778 * Math.Pow(10, -10), //megawatthour 71
            0.1019716213, //meter kilogram-force 72
            1000000, //microjoule 73
            1000, //millijoule 74
            2.7777777778 * Math.Pow(10, -8), //myriawatt hour 75
            1000000000, //nanojoule 76
            1, //newton meter 77
            1.0 * Math.Pow(10, -15), //petajoule 78
            2.7777777778 * Math.Pow(10, -19), //petawatthour 79
            3.7767267147 * Math.Pow(10, -7), //pferdestärkenstunde 80
            1000000000000, //picojoule 81
            9.4781707775 * Math.Pow(10, -22), //Q unit 82
            9.4781707775 * Math.Pow(10, -19), //quad 83
            6241506.48, //teraelectronvolt 84
            1.0 * Math.Pow(10, -12), //terajoule 85
            2.7777777778 * Math.Pow(10, -16), //terawatthour 86
            9.4781707775 * Math.Pow(10, -9), //therm [Europe] 87
            9.4804342797 * Math.Pow(10, -9), //therm [U.S. (uncommon)] 88
            2.3890295762 * Math.Pow(10, -7), //thermie 89
            2.3900573614 * Math.Pow(10, -10), //ton [explosive] 90
            3.4120842375 * Math.Pow(10, -11), //tonne of coal equivalent 91
            2.3884589663 * Math.Pow(10, -11), //tonne of oil equivalent 92
            0.00027777777778, //watthour 93
            1, //wattsecond 94
            1.0 * Math.Pow(10, 24), //yoctojoule 95
            1.0 * Math.Pow(10, -24), //yottajoule 96
            2.7777777778 * Math.Pow(10, -28), //yottawatthour 97
            1.0 * Math.Pow(10, 21), //zeptojoule 98
            1.0 * Math.Pow(10, -21), //zettajoule 99
            2.7777777778 * Math.Pow(10, -25) //zettawatthour 100
            };

         */

        /*force
         * primary unit is newton
         * 
         * string[] force = 
        {
        "attonewton",
        "centinewton",
        "decigram-force",
        "decinewton",
        "dekagram-force",
        "dekanewton",
        "dyne",
        "exanewton",
        "femtonewton",
        "giganewton",
        "gram-force",
        "hectonewton",
        "joule/meter",
        "kilogram-force",
        "kilonewton",
        "kilopond",
        "kip",
        "meganewton",
        "megapond",
        "micronewton",
        "millinewton",
        "nanonewton",
        "newton",
        "ounce-force",
        "petanewton",
        "piconewton",
        "pond",
        "pound-force",
        "poundal",
        "sthene",
        "teranewton",
        "ton-force [long]",
        "ton-force [metric]",
        "ton-force [short]",
        "yoctonewton",
        "yottanewton",
        "zeptonewton",
        "zettanewton",
        "zettanewton"
        };
        double[] forceConversions = 
        {
        1000000000000000000, //attonewton 0
        100, //centinewton 1
        1019.716213, //decigram-force 2
        10, //decinewton 3
        10.19716213, //dekagram-force 4
        0.1, //dekanewton 5
        100000, //dyne 6
        1.0 * Math.Pow(10, -18), //exanewton 7
        1000000000000000, //femtonewton 8
        1.0 * Math.Pow(10, -9), //giganewton 9
        101.9716213, //gram-force 10
        0.01, //hectonewton 11
        1, //joule/meter 12
        0.1019716213, //kilogram-force 13
        0.001, //kilonewton 14
        0.1019716213, //kilopond 15
        0.00022480894387, //kip 16
        0.000001, //meganewton 17
        0.0001019716213, //megapond 18
        1000000, //micronewton 19
        1000, //millinewton 20
        1000000000, //nanonewton 21
        1, //newton 22
        3.5969431019, //ounce-force 23
        1.0 * Math.Pow(10, -15), //petanewton 24
        1000000000000, //piconewton 25
        101.9716213, //pond 26
        0.22480894387, //pound-force 27
        7.2330140801, //poundal 28
        0.001, //sthene 29
        1.0 * Math.Pow(10, -12), //teranewton 30
        0.00010036113566, //ton-force [long] 31
        0.0001019716213, //ton-force [metric] 32
        0.00011240447194, //ton-force [short] 33
        1.0 * Math.Pow(10, 24), //yoctonewton 34
        1.0 * Math.Pow(10, -24), //yottanewton 35
        1.0 * Math.Pow(10, 21), //zeptonewton 36
        1.0 * Math.Pow(10, -21), //zettanewton 37
        1.0 * Math.Pow(10, -21) //zettanewton 38
        };

         * 
         */

        /* frequency
         * primary unit is hertz
         * string[] frequency = 
            {
            "second",
            "cycle/second",
            "degree/hour",
            "degree/minute",
            "degree/second",
            "gigahertz",
            "hertz",
            "kilohertz",
            "megahertz",
            "millihertz",
            "radian/hour",
            "radian/minute",
            "radian/second",
            "revolution/hour",
            "revolution/minute",
            "revolution/second",
            "RPM",
            "terrahertz"
            };
            double[] frequencyConversions = 
            {
            11, //second 0
            1, //cycle/second 1
            1296000, //degree/hour 2
            21600, //degree/minute 3
            360, //degree/second 4
            1.0 * Math.Pow(10, -9), //gigahertz 5
            1, //hertz 6
            0.001, //kilohertz 7
            0.000001, //megahertz 8
            1000, //millihertz 9
            22619.467, //radian/hour 10
            376.99112, //radian/minute 11
            6.2831853001, //radian/second 12
            3600, //revolution/hour 13
            60, //revolution/minute 14
            1, //revolution/second 15
            60, //RPM 16
            1.0 * Math.Pow(10, -12) //terrahertz 17
            };

         */

        /* capacitance
         * primary unit is farad [international]
         * 
         * string[] capacitance = 
            {
            "abfarad",
            "centifarad",
            "coulomb/volt",
            "decifarad",
            "dekafarad",
            "electromagnetic unit",
            "electrostatic unit",
            "farad [SI standard]",
            "farad [international]",
            "gaussian",
            "gigafarad",
            "hectofarad",
            "jar",
            "kilofarad",
            "megafarad",
            "microfarad",
            "millifarad",
            "nanofarad",
            "picofarad",
            "puff",
            "second/ohm",
            "statfarad",
            "terafarad"
            };
            double[] capacitanceConversions = 
            {
            9.9951 * Math.Pow(10, -10), //abfarad 0
            99.951, //centifarad 1
            0.99951, //coulomb/volt 2
            9.9951, //decifarad 3
            0.099951, //dekafarad 4
            9.9951 * Math.Pow(10, -10), //electromagnetic unit 5
            898314833950, //electrostatic unit 6
            0.99951, //farad [SI standard] 7
            1, //farad [international] 8
            898314833950, //gaussian 9
            9.9951 * Math.Pow(10, -10), //gigafarad 10
            0.0099951, //hectofarad 11
            899559000.01, //jar 12
            0.00099951, //kilofarad 13
            9.9951 * Math.Pow(10, -7), //megafarad 14
            999510, //microfarad 15
            999.51, //millifarad 16
            999510000, //nanofarad 17
            999510000000, //picofarad 18
            999510000000, //puff 19
            0.99951, //second/ohm 20
            898314833950, //statfarad 21
            9.9951 * Math.Pow(10, -13) //terafarad 22
            };

         */
        #endregion

        public enum ConversionTypes
        {
            distance = 0, mass, volume, speed, area, acceleration, angles, current
        };

        string[] baseFilters =
        {
            "All", "Common"
        };

        bool readyForDisplay;//, unitSelectionMode;
        private UnitConverterData data;
        private int filterIndex, unitIndex;
        private string selectedUnit;
        private List<UnitConverterItem> filteredUnits;
        private List<string> filterStrings;
        private DispatcherTimer ConvertTimer;
        
        //This is the current ConversionSet, values are derived from this
        ConversionSet currentConversion;

        public UnitConverter()
        {
            readyForDisplay = false;
            //unitSelectionMode = false;
            this.InitializeComponent();

            //Window.Current.SizeChanged += Current_SizeChanged;
            this.SizeChanged += Current_SizeChangedUAP;

            filteredUnits = new List<UnitConverterItem>();
            filterStrings = new List<string>();
            data = new UnitConverterData();
            filterIndex = 0; //changes when we set the common filter
            unitIndex = 0; //changes when we grab the first item from the common filter
            selectedUnit = ""; //changes when we grab the first item from the common filter
            ConvertTimer = new DispatcherTimer();
            ConvertTimer.Interval = TimeSpan.FromMilliseconds(750);
            ConvertTimer.Tick += ConvertTimer_Tick;
        }

        void ConvertTimer_Tick(object sender, object e)
        {
            Convert();
            ConvertTimer.Stop();
        }


        private void Current_SizeChangedUAP(object sender, SizeChangedEventArgs e)
        {
            App.UpdateViewModelFromSize(e.NewSize);
            if (App.CurrentView == App.PreviousView)
                return;

            Current_SizeChanged();
        }

        //It is impossible to switch the scroll mode to vertical for a GridView. Need to see if this is fixed in RC and if not then bug file.
        private void Current_SizeChanged()
        {
            ChangeViewState();
        }

        private void ChangeViewState()
        {
            this.title.ChangeViewState();

            if(App.isLandscape() || App.isPortrait())
            {
                VisualStateManager.GoToState(this, "Full", false);

                if (this.ItemGridView.Visibility != Windows.UI.Xaml.Visibility.Visible)
                {
                    //Only refresh the GridView if something has changed
                    this.ItemGridView.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    this.ItemListView.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    RefreshGridView();
                    App.ScheduleOnNextRender(delegate
                    {
                        Convert();
                    });
                }
                this.imgLine.Visibility = Windows.UI.Xaml.Visibility.Visible;
                this.imgLine2.Visibility = Windows.UI.Xaml.Visibility.Visible;
                SetFilterButtonStyles();
                this.txtFilter.Visibility = Windows.UI.Xaml.Visibility.Visible;

                this.controlColumn.Width = new GridLength(1, GridUnitType.Auto);

                App.ScheduleOnNextRender(delegate
                {
                    this.imgLine.Height = this.LineRow.ActualHeight * 0.75;
                    this.imgLine2.Height = this.LineRow.ActualHeight * 0.75;
                });
                this.txtUnitAmount.Text = this.txtUnitAmount.Text; //stupid bug
            }
            else if (App.isSnapped())
            {
                VisualStateManager.GoToState(this, "Snapped", false);
                //this.ItemGridView.ItemsPanel = (ItemsPanelTemplate)Resources["GridItemsPanelTemplateSnapped"];

                if (this.ItemListView.Visibility != Windows.UI.Xaml.Visibility.Visible)
                {
                    //Only refresh the GridView if something has changed
                    this.ItemListView.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    this.ItemGridView.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    RefreshGridView();

                    App.ScheduleOnNextRender(delegate
                    {
                        Convert();
                    });

                }

                SetFilterButtonStyles();
                this.txtFilter.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

                this.controlColumn.Width = new GridLength(1, GridUnitType.Star);

                this.imgLine.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                this.imgLine2.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }

        public void Init(ConversionTypes type, string title, App.Apps convertType)
        {
            App.ScheduleOnNextRender(delegate
            {
                List<Breadcrumb> breadcrumbs = new List<Breadcrumb>();
                breadcrumbs.Add(new Breadcrumb(App.Apps.MainPage));
                breadcrumbs.Add(new Breadcrumb(App.Apps.UnitConverter));
                breadcrumbs.Add(new Breadcrumb(convertType, true));
                //breadcrumbs.Add(new Breadcrumb("Current", App.Apps.Current, true));
                this.title.SetupBreadcrumbs(breadcrumbs);
            });

            ChangeUnits(type);
            readyForDisplay = true;
            Convert();
        }

        private void ChangeUnits(ConversionTypes type)
        {
            if (currentConversion != null && currentConversion.Type == type)
                return;
            data.Collection.Clear();
            this.currentConversion = ConversionSet.GetSetByType(type);
            
            int idx = currentConversion.Masks[0][0];
            SetUnitIndex(idx);           

            RetrieveFilters();
            RefreshGridView();
        }

        //Go through the list of filters and add them as buttons to the stackFilterControls stack panel
        private void RetrieveFilters()
        {
            stackFilterControls.Children.Clear();
            for (int i = 0; i < baseFilters.Length; i++)
            {
                //For now, add buttons
                TextBlock a = new TextBlock();
                a.Text = baseFilters[i];
                this.filterStrings.Add(baseFilters[i]);
                if (a.Text == "Common")
                    this.filterIndex = i;
            
                a.Tag = i;
                a.PointerReleased += ChangeFilterIndex;
                stackFilterControls.Children.Add(a);
            }

            //Add filters specific to the current type
            for (int i = 0; i < currentConversion.Filters.Length; i++)
            {
                TextBlock a = new TextBlock();
                a.Text = currentConversion.Filters[i];
                this.filterStrings.Add(currentConversion.Filters[i]);
                a.Tag = baseFilters.Length + i;
                a.PointerReleased += ChangeFilterIndex;
                stackFilterControls.Children.Add(a);        
        
            }

            SetFilterButtonStyles();
            
        }

        private void SetUnitIndex(int idx)
        {
            this.unitIndex = idx;
            this.selectedUnit = currentConversion.Units[idx];
            this.txtSelectedUnit.Text = selectedUnit;
            this.txtSelectedUnit.SelectionStart = this.txtSelectedUnit.Text.Length;
            this.txtSelectedUnit.Foreground = new SolidColorBrush(Color.FromArgb(255, 128, 128, 128));
        }

        private void ChangeFilterIndex(object sender, PointerRoutedEventArgs e)
        {
            
            TextBlock snd = sender as TextBlock;

            InternalChangeFilter((int)snd.Tag);
        }

        private void InternalChangeFilter(int index)
        {
            this.filterIndex = index;
            SetFilterButtonStyles();

            this.txtSelectedFilter.Text = GetFilterName();
            this.txtSelectedFilter.SelectionStart = this.txtSelectedFilter.Text.Length;
            this.txtSelectedFilter.SelectionLength = 0;
            txtSelectedFilter.Foreground = new SolidColorBrush(ColorHelper.FromArgb(255, 128, 128, 128));

            Convert();
        }

        private string GetFilterName()
        {
            //if (filterIndex <= baseFilters.Length - 1)
            //    return baseFilters[filterIndex];
            //else
            //    return currentConversion.Filters[filterIndex - baseFilters.Length];
            return filterStrings[filterIndex];
        }

        private void SetFilterButtonStyles()
        {
            var isSnapped = ItemGridView.Visibility == Windows.UI.Xaml.Visibility.Visible ? false : true;

            for (int i = 0; i < stackFilterControls.Children.Count; i++)
            {
                if (i != this.filterIndex)
                {
                    if (isSnapped)
                        (stackFilterControls.Children[i] as TextBlock).Style = this.Resources["UnselectedFilterStyleSnapped"] as Style;
                    else
                        (stackFilterControls.Children[i] as TextBlock).Style = this.Resources["UnselectedFilterStyle"] as Style;
                }
                else
                {
                    if (isSnapped)
                        (stackFilterControls.Children[i] as TextBlock).Style = this.Resources["SelectedFilterStyleSnapped"] as Style;
                    else
                        (stackFilterControls.Children[i] as TextBlock).Style = this.Resources["SelectedFilterStyle"] as Style;
                }
            }
        }

        private void RefreshGridView()
        {
            //Clear both views
            ItemGridView.ItemsSource = null;
            ItemListView.ItemsSource = null;

            if (ItemGridView.Visibility != Windows.UI.Xaml.Visibility.Visible)
            {
                //Get the proper view ready for population
                ItemListView.ItemsSource = data.Collection;
                ItemListView.Visibility = Windows.UI.Xaml.Visibility.Visible;
                ItemListView.UpdateLayout();
            }

            else if (ItemListView.Visibility != Windows.UI.Xaml.Visibility.Visible)
            {
                this.ItemGridView.ItemsSource = data.Collection;
                this.ItemGridView.Visibility = Windows.UI.Xaml.Visibility.Visible;
                ItemGridView.UpdateLayout();
            }
        }

        //TBD:
        //public void Settings_CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        //{
        //    SettingsCommand about = new SettingsCommand("About", "About", (x) =>
        //    {
        //        MainPage.ShowPopup(new Utilities.Settings.About(), this.ActualHeight, "About");
        //    });

        //    SettingsCommand feedback = new SettingsCommand("Feedback", "Report a bug", (x) =>
        //    {
        //        (Application.Current as App).Feedback();
        //    });

        //    args.Request.ApplicationCommands.Add(about);
        //    args.Request.ApplicationCommands.Add(feedback);

        //}

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
            ChangeViewState();

            //TBD:SettingsPane.GetForCurrentView().CommandsRequested += Settings_CommandsRequested;

            // show help
            Utilities.Common.ShowHelp.ShowHelpMessage(App.Apps.UnitConverter);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            //TBD:SettingsPane.GetForCurrentView().CommandsRequested -= Settings_CommandsRequested;
        }

        private void Convert()
        {
            if (this.txtUnitAmount.Text == "value" || this.txtUnitAmount.Text == "")
                this.txtUnitAmount.Text = "1";
            
            if (!readyForDisplay || this.txtUnitAmount.Text == "value")
                return;
            //This function will take the current value and convert it into all known types with zippering.
            //It will then create an array of UnitConverterItem objects, and pass it to the InitializeGridView method
            var units = currentConversion.Units;
            var conversions = currentConversion.Conversions;
            var masks = currentConversion.Masks;

            if (units == null || conversions == null || masks == null || units.Length != conversions.Length)
            {
                //If this is hit, then it is a programming error
                UpdateStatus("Unknown Error");
                return;
            }
            else if (this.txtUnitAmount.Text.Length <= 0)
            {
                UpdateStatus("Insert a value, a unit and click \"Convert\".");
                return;
            }

            double value = 0;

            if (!double.TryParse(this.txtUnitAmount.Text, out value))
            {
                UpdateStatus("There was an error trying to perform the conversion, please check your input and try again.");
                return;
            }
            //No errors, continue
            UpdateStatus("");

            //Set the mode
            //unitSelectionMode = false;

            //Show the conversion controls
            this.txtUnitAmount.Visibility = Windows.UI.Xaml.Visibility.Visible;
            //only show this if we are not snapped
            if (ItemGridView.Visibility == Windows.UI.Xaml.Visibility.Visible)
                this.imgLine2.Visibility = Windows.UI.Xaml.Visibility.Visible;
            this.txtSelectedFilter.Visibility = Windows.UI.Xaml.Visibility.Visible;

            this.txtSelectedUnit.Text = selectedUnit;
            this.txtSelectedUnit.SelectionStart = this.txtSelectedUnit.Text.Length;
            this.txtUnitAmount.SelectionStart = this.txtUnitAmount.Text.Length;            

            //Hide the helper text
            this.txtSelectAUnit.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            //The selected index on the combo box matches the index of the conversion
            //Divide by the conversion to get the base value in the base unit
            
            double baseValue = value / conversions[unitIndex];

            //Iterate through each of the conversions and create an array of UnitConverterItems
            List<UnitConverterItem> items = new List<UnitConverterItem>();

            for (int i = 0; i < conversions.Length; i++)
            {
                double result = baseValue * conversions[i];
                UnitConverterItem item = new UnitConverterItem(units[i], result.ToString(), PickColor(masks, i));
                item.Index = i;
                items.Add(item);
            }

            //Now that we have all of the conversions, filter them appropriately
            List<UnitConverterItem> prefilter = new List<UnitConverterItem>();

            if (filterIndex == -1 || masks.Count == 0)
            {
                RefreshGridView();
                return;
            }
            else if (filterIndex == 0)
            {
                //this is "All", show everything
                prefilter = items;
            }
            else if (filterIndex > 0 && filterIndex <= masks.Count)
            {
                //This is a specific mask
                int[] mask = masks[filterIndex - 1];
                //A mask contains integers which map to the indices in the items array that we want to display
                for (int i = 0; i < mask.Length; i++)
                    prefilter.Add(items[mask[i]]);                
            }

            List<UnitConverterItem> filteredItems = new List<UnitConverterItem>();

            bool isFilterSelected = false;
            string compare = this.txtSelectedFilter.Text;
            if (compare == "")
                isFilterSelected = true;
            if (!isFilterSelected)
            {
                for (int i = 0; i < filterStrings.Count; i++)
                {
                    if (compare == filterStrings[i])
                    {
                        isFilterSelected = true;
                        break;
                    }
                }
            }

            //Now we know that if isFilterSelected, one of the base filters' names is typed into the search box.
            //In this case, we don't want to search the text, the filter will be applied as part of typing that in
            //If !isFilterSelected, we want to filter on the search results

            if (isFilterSelected)
                filteredItems = prefilter;
            else
            {
                for (int i = 0; i < prefilter.Count; i++)
                {
                    if (prefilter[i].Title.ToLower().Contains(compare.ToLower()))
                        filteredItems.Add(prefilter[i]);
                }
            }

            if (filteredItems.Count == 0)
                data.InitializeGridView(prefilter);
            else
                data.InitializeGridView(filteredItems);
            RefreshGridView();
        }

        private void SelectUnit(string unitStart = "")
        {
            var units = currentConversion.Units;
            var masks = currentConversion.Masks;
            
            if (units == null)
            {
                UpdateStatus("Unknown Error");
                return;
            }
            UpdateStatus("");
            
            //Set the mode
            //unitSelectionMode = true;

            //Hide the conversion controls
            this.txtUnitAmount.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            this.imgLine2.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            this.txtSelectedFilter.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            //Show the helper text
            this.txtSelectAUnit.Visibility = Windows.UI.Xaml.Visibility.Visible;

            List<UnitConverterItem> items = new List<UnitConverterItem>();

            //Add all of the units to the list
            for (int i = 0; i < units.Length; i++)
            {
                UnitConverterItem item = new UnitConverterItem(units[i], "", PickColor(masks, i));
                item.Index = i;
                items.Add(item);
            }

            //Then filter the items 
            List<UnitConverterItem> prefilter = new List<UnitConverterItem>();
            if (filterIndex == -1 || masks.Count == 0)
            {
                RefreshGridView();
                return;
            }
            else if (filterIndex == 0)
            {
                //this is "All", show everything
                prefilter = items;
            }
            else if (filterIndex > 0 && filterIndex <= masks.Count)
            {
                //This is a specific mask
                int[] mask = masks[filterIndex - 1];
                //A mask contains integers which map to the indices in the items array that we want to display
                for (int i = 0; i < mask.Length; i++)
                    prefilter.Add(items[mask[i]]);
            }
            
            //and store in the "filteredUnits" list
            filteredUnits.Clear();

            //this is "All", show everything
            if (unitStart == "")
                filteredUnits = prefilter;
            else
            {
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].Title.ToLower().Contains(unitStart.ToLower()))
                        filteredUnits.Add(items[i]);
                }
            }

            if (filteredUnits.Count == 0)
            {
                UnitConverterItem i = new UnitConverterItem("No conversions found", "", new SolidColorBrush(ColorHelper.FromArgb(255, 61, 216, 216)));
                i.Index = -1;
                filteredUnits.Add(i);
            }
            data.InitializeGridView(filteredUnits);
            RefreshGridView();
        }

        private void UpdateStatus(string text)
        {
            var isSnapped = ItemGridView.Visibility == Windows.UI.Xaml.Visibility.Visible ? false : true;


            if (!isSnapped || text == "")
                this.txtStatus.Text = text;
            else
                this.txtStatus.Text = "Please check your input";            
        }

        //Colorize by unit type
        private SolidColorBrush PickColor(List<int[]> filters, int index)
        {
            SolidColorBrush retVal = new SolidColorBrush(ColorHelper.FromArgb(255, 255, 255, 255));
            if (filters.Count > 4)
                return retVal;

            SolidColorBrush metroGreen = Helpers.GetBrush(Helpers.Colors.PastelGreen); //new SolidColorBrush(ColorHelper.FromArgb(255, 45, 197, 18));
            SolidColorBrush metroPurple = Helpers.GetBrush(Helpers.Colors.Purple);//new SolidColorBrush(ColorHelper.FromArgb(255, 218, 45, 232));
            SolidColorBrush metroBlue = Helpers.GetBrush(Helpers.Colors.PastelBlue); //new SolidColorBrush(ColorHelper.FromArgb(255, 61, 216, 216));
            SolidColorBrush metroRed = Helpers.GetBrush(Helpers.Colors.PastelRed); //new SolidColorBrush(ColorHelper.FromArgb(255, 255, 26, 49));            

            List<SolidColorBrush> brushes = new List<SolidColorBrush>();
            brushes.Add(metroGreen);
            brushes.Add(metroPurple);
            brushes.Add(metroRed);            
            brushes.Add(metroBlue);

            //Prefer the higher order filter
            bool found = false;

            for (int i = 0; i < filters.Count; i++)
            {
                if (filters[i].Contains(index))
                {
                    retVal = brushes[i];
                    found = true;
                    break;
                }
            }

            if (found)
                return retVal;
            else            
                return metroBlue;
        }

        #region ComboBox Highlight Code
        //private void cmbUnitKey(object sender, KeyRoutedEventArgs e)
        //{
        //    VirtualKey[] keys =
        //    {
        //        VirtualKey.A, VirtualKey.B, VirtualKey.C, VirtualKey.D, VirtualKey.E, VirtualKey.F, VirtualKey.G, VirtualKey.H, VirtualKey.I, VirtualKey.J, VirtualKey.K, VirtualKey.L,
        //         VirtualKey.M, VirtualKey.N, VirtualKey.O, VirtualKey.P, VirtualKey.Q, VirtualKey.R, VirtualKey.S, VirtualKey.T, VirtualKey.U, VirtualKey.V, VirtualKey.W, VirtualKey.X,
        //          VirtualKey.Y, VirtualKey.Z
        //    };
        //    if (keys.Contains(e.Key))
        //        HighlightProperIndex(cmbUnits, e, this.unitIndices);
        //}

        //private void cmbFilterKey(object sender, KeyRoutedEventArgs e)
        //{
        //    VirtualKey[] keys =
        //    {
        //        VirtualKey.A, VirtualKey.B, VirtualKey.C, VirtualKey.D, VirtualKey.E, VirtualKey.F, VirtualKey.G, VirtualKey.H, VirtualKey.I, VirtualKey.J, VirtualKey.K, VirtualKey.L,
        //         VirtualKey.M, VirtualKey.N, VirtualKey.O, VirtualKey.P, VirtualKey.Q, VirtualKey.R, VirtualKey.S, VirtualKey.T, VirtualKey.U, VirtualKey.V, VirtualKey.W, VirtualKey.X,
        //          VirtualKey.Y, VirtualKey.Z
        //    };
        //    if (keys.Contains(e.Key))
        //        HighlightProperIndex(cmbFilter, e, this.filterIndices);
        //}

        //private void HighlightProperIndex(ComboBox cmbNavigate, KeyRoutedEventArgs e, List<ComboUnit> indices)
        //{
        //    //Now we have ComboUnit array. Each one has a character and an index
        //    //Map the key pressed to one of these characters, then select that index

        //    for (int i = 0; i < indices.Count; i++)
        //    {
        //        if (e.Key.ToString().ToLower() == indices[i].Letter.ToString())
        //        {
        //            cmbNavigate.SelectedIndex = indices[i].Index;
        //            break;
        //        }
        //    }
        //}

        //private List<ComboUnit> GenerateComboIndices(ComboBox box)
        //{
        //    string abc = "abcdefghijklmnopqrstuvwxyz";
        //    List<ComboUnit> idx = new List<ComboUnit>();
        //    char[] letters = abc.ToCharArray();
        //    int lastIndex = 0;

        //    for (int i = 0; i < letters.Length; i++)
        //    {
        //        for (int j = lastIndex; j < box.Items.Count; j++)
        //        {
        //            string content = (box.Items[j] as ComboBoxItem).Content.ToString();
        //            if (content[0] > letters[i])
        //                break;
        //            if (content.StartsWith(letters[i].ToString()))
        //            {
        //                idx.Add(new ComboUnit(j, letters[i]));
        //                lastIndex = j;
        //                break;
        //            }
        //        }
        //    }
        //    return idx;
        //}
        #endregion

        private void txtUnitAmount_KeyUp_1(object sender, KeyRoutedEventArgs e)
        {
            txtUnitAmount.Foreground = new SolidColorBrush(ColorHelper.FromArgb(255, 0, 0, 0));
            if (this.txtUnitAmount.Text != "")
            {
                ConvertTimer.Stop();
                ConvertTimer.Start();
            }
        }

        private void btnCopy_Click_1(object sender, RoutedEventArgs e)
        {
            var g = ItemGridView.Visibility == Windows.UI.Xaml.Visibility.Visible ? ItemGridView as ListViewBase: ItemListView as ListViewBase;
            DataPackage dp = new DataPackage();
            dp.RequestedOperation = DataPackageOperation.Copy;
            dp.SetText((g.SelectedItem as UnitConverterItem).Result);
            Clipboard.SetContent(dp);
            g.SelectedIndex = -1;
        }

        private void txtUnitAmount_GotFocus_1(object sender, RoutedEventArgs e)
        {
            double d;
            if (!double.TryParse(txtUnitAmount.Text, out d))
            {
                txtUnitAmount.Text = "";
                txtUnitAmount.Foreground = new SolidColorBrush(ColorHelper.FromArgb(255, 0, 0, 0));
            }
            this.txtUnitAmount.SelectionStart = this.txtUnitAmount.Text.Length;
            this.txtUnitAmount.SelectionLength = 0;
        }

        private void txtUnitAmount_LostFocus_1(object sender, RoutedEventArgs e)
        {
            if (txtUnitAmount.Text == "")
            {
                this.txtUnitAmount.Text = "1";
                txtUnitAmount.Foreground = new SolidColorBrush(ColorHelper.FromArgb(255, 128, 128, 128));
            }
            this.txtUnitAmount.SelectionStart = this.txtUnitAmount.Text.Length;
            this.txtUnitAmount.SelectionLength = 0;
        }

        private void ItemGridView_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            var index = (e.ClickedItem as UnitConverterItem).Index;

            if (index != -1)
            {
                this.SetUnitIndex(index);
                Convert();

            }
            else
            {
                Convert();
            }
        }

        private void txtSelectedUnit_KeyUp_1(object sender, KeyRoutedEventArgs e)
        {
            this.txtSelectedUnit.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            if (e.Key == VirtualKey.Enter)
            {
                var item = filteredUnits[0] as UnitConverterItem;
                if (item.Index != -1)
                {
                    //We have a result
                    this.SetUnitIndex(item.Index);
                    Convert();
                    this.txtSelectedUnit.SelectionStart = this.txtSelectedUnit.Text.Length;
                }
                else
                {
                    //There are no results
                    Convert();
                }
                
                e.Handled = true;
            }
            else
                SelectUnit(this.txtSelectedUnit.Text);            
        }

        private void txtSelectedUnit_GotFocus_1(object sender, RoutedEventArgs e)
        {
            this.txtSelectedUnit.SelectionStart = this.txtSelectedUnit.Text.Length;       
            this.txtSelectedUnit.SelectionLength = 0;                 

            SelectUnit();
        }

        private void ItemGridView_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListViewBase).SelectedIndex == -1)
                this.appbar.IsOpen = false;
            else
                this.appbar.IsOpen = true;
        }

        private void txtSelectedFilter_GotFocus_1(object sender, RoutedEventArgs e)
        {
            this.txtSelectedFilter.SelectionStart = this.txtSelectedFilter.Text.Length;
            this.txtSelectedFilter.SelectionLength = 0;
        }

        private void txtSelectedFilter_KeyUp_1(object sender, KeyRoutedEventArgs e)
        {
            this.txtSelectedFilter.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));

            //Do custom search logic here
            string compare = txtSelectedFilter.Text;
            for (int i = 0; i < filterStrings.Count; i++)
            {
                if (compare.ToLower() == filterStrings[i].ToLower())
                {
                    InternalChangeFilter(i);
                    return;
                }
            }

            ConvertTimer.Stop();
            ConvertTimer.Start();
        }

        private void appbar_Opened(object sender, object e)
        {
            var g = ItemGridView.Visibility == Windows.UI.Xaml.Visibility.Visible ? ItemGridView as ListViewBase : ItemListView as ListViewBase;
            if (g.SelectedIndex == -1)
                this.appbar.IsOpen = false;

        }


    }
}
