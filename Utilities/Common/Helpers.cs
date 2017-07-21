using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Utilities
{
    public class Helpers
    {
        public enum Colors
        {
            None, Red, Green, Blue, Black, Gray, Orange, White, Purple, LightGray, MetroGreen,
            MetroBlue, DarkGray, SnapBlue, SnapGreen, SnapRed, SnapLightGreen, SnapTurquoise, SnapOrange, SnapPurple, SnapLightRed,
            SnapTeal, SnapLightBlue, SnapPink, PastelGreen, PastelBlue, PastelRed, DarkBlue, DarkGreen, DarkOrange, DarkPurple,
            DarkRed, GreenBlue, LightRed, OrangeRed, OrangeYellow, Pink, PrimeRed, SkyBlue, Violet
        }

        #region All Colors

        public static SolidColorBrush GetBrush(Colors color)
        {
            Color c = new Color();
            c.A = 255;

            switch (color)
            {
                case Colors.Red:
                    c.R = 203;
                    c.G = 51;
                    c.B = 51;
                    break;

                case Colors.MetroGreen:
                    c.R = 202;
                    c.G = 253;
                    c.B = 206;
                    break;

                case Colors.MetroBlue:
                    c.R = 202;
                    c.G = 253;
                    c.B = 255;
                    break;

                case Colors.Green:
                    c.R = 19;
                    c.G = 170;
                    c.B = 15;
                    break;

                case Colors.Blue:
                    c.R = 56;
                    c.G = 102;
                    c.B = 219;
                    break;

                case Colors.Black:
                    c.R = 32;
                    c.G = 32;
                    c.B = 32;
                    break;

                case Colors.Gray:
                    c.R = 95;
                    c.G = 95;
                    c.B = 95;
                    break;

                case Colors.DarkGray:
                    c.R = 48;
                    c.G = 48;
                    c.B = 48;
                    break;

                case Colors.LightGray:
                    c.R = 240;
                    c.G = 240;
                    c.B = 240;
                    break;

                case Colors.Orange:
                    c.R = 231;
                    c.G = 109;
                    c.B = 0;
                    break;

                case Colors.White:
                    c.R = 255;
                    c.G = 255;
                    c.B = 255;
                    break;

                case Colors.Purple:
                    c.R = 202;
                    c.G = 158;
                    c.B = 241;
                    break;

                case Colors.DarkBlue:
                    c.R = 16;
                    c.G = 21;
                    c.B = 205;
                    break;

                case Colors.DarkGreen:
                    c.R = 60;
                    c.G = 147;
                    c.B = 37;
                    break;

                case Colors.DarkOrange:
                    c.R = 220;
                    c.G = 110;
                    c.B = 22;
                    break;

                case Colors.DarkPurple:
                    c.R = 138;
                    c.G = 24;
                    c.B = 226;
                    break;

                case Colors.DarkRed:
                    c.R = 179;
                    c.G = 15;
                    c.B = 15;
                    break;

                case Colors.GreenBlue:
                    c.R = 49;
                    c.G = 192;
                    c.B = 89;
                    break;

                case Colors.LightRed:
                    c.R = 230;
                    c.G = 93;
                    c.B = 93;
                    break;

                case Colors.OrangeRed:
                    c.R = 217;
                    c.G = 40;
                    c.B = 16;
                    break;

                case Colors.OrangeYellow:
                    c.R = 216;
                    c.G = 160;
                    c.B = 25;
                    break;

                case Colors.Pink:
                    c.R = 215;
                    c.G = 81;
                    c.B = 179;
                    break;

                case Colors.PrimeRed:
                    c.R = 214;
                    c.G = 18;
                    c.B = 18;
                    break;

                case Colors.SkyBlue:
                    c.R = 124;
                    c.G = 141;
                    c.B = 243;
                    break;

                case Colors.Violet:
                    c.R = 161;
                    c.G = 31;
                    c.B = 199;
                    break;

                //Colors for snap

                case Colors.SnapBlue:
                    c.R = 51;
                    c.G = 46;
                    c.B = 231;
                    break;

                case Colors.SnapGreen:
                    c.R = 59;
                    c.G = 181;
                    c.B = 47;
                    break;

                case Colors.SnapRed:
                    c.R = 216;
                    c.G = 26;
                    c.B = 26;
                    break;

                case Colors.SnapTurquoise:
                    c.R = 98;
                    c.G = 185;
                    c.B = 75;
                    break;

                case Colors.SnapLightGreen:
                    c.R = 48;
                    c.G = 178;
                    c.B = 103;
                    break;

                case Colors.SnapOrange:
                    c.R = 255;
                    c.G = 149;
                    c.B = 64;
                    break;

                case Colors.SnapPurple:
                    c.R = 204;
                    c.G = 71;
                    c.B = 243;
                    break;

                case Colors.SnapLightRed:
                    c.R = 227;
                    c.G = 48;
                    c.B = 48;
                    break;

                case Colors.SnapTeal:
                    c.R = 89;
                    c.G = 215;
                    c.B = 208;
                    break;

                case Colors.SnapLightBlue:
                    c.R = 62;
                    c.G = 153;
                    c.B = 247;
                    break;

                case Colors.SnapPink:
                    c.R = 237;
                    c.G = 97;
                    c.B = 97;
                    break;

                case Colors.PastelGreen:
                    c.R = 76;
                    c.G = 183;
                    c.B = 165;
                    break;

                case Colors.PastelRed:
                    c.R = 231;
                    c.G = 116;
                    c.B = 113;
                    break;

                case Colors.PastelBlue:
                    c.R = 181;
                    c.G = 208;
                    c.B = 223;
                    break;

                case Colors.None:
                default:
                    c.R = 255;
                    c.G = 255;
                    c.B = 255;
                    break;
            }

            return new SolidColorBrush(c);
        }

        #endregion

        #region Getting App Colors

        public static SolidColorBrush GetSnappedBrush(App.Apps app)
        {
            switch (app)
            {
                case App.Apps.MainPage:
                    return GetBrush(Colors.Gray);

                // notepad
                case App.Apps.Notepad:
                    return GetBrush(Colors.SnapLightBlue);


                // notepad
                case App.Apps.ToDo:
                    return GetBrush(Colors.Violet);

                // unit converter
                case App.Apps.UnitConverter:
                    return GetBrush(Colors.SnapTurquoise);
                case App.Apps.Area:
                    return GetBrush(Colors.SnapLightGreen);
                case App.Apps.Distance:
                    return GetBrush(Colors.SnapPurple);
                case App.Apps.Mass:
                    return GetBrush(Colors.SnapOrange);
                case App.Apps.Speed:
                    return GetBrush(Colors.SnapLightRed);
                case App.Apps.Volume:
                    return GetBrush(Colors.SnapTeal);
                case App.Apps.Acceleration:
                    return GetBrush(Colors.Blue);
                case App.Apps.Angles:
                    return GetBrush(Colors.DarkBlue);
                case App.Apps.Current:
                    return GetBrush(Colors.DarkGreen);
                case App.Apps.Cooking:
                    return GetBrush(Colors.DarkOrange);
                case App.Apps.Density:
                    return GetBrush(Colors.DarkPurple);
                case App.Apps.Energy:
                    return GetBrush(Colors.DarkRed);
                case App.Apps.Flow:
                    return GetBrush(Colors.GreenBlue);
                case App.Apps.Force:
                    return GetBrush(Colors.LightRed);
                case App.Apps.Frequency:
                    return GetBrush(Colors.OrangeRed);
                case App.Apps.Light:
                    return GetBrush(Colors.OrangeYellow);
                case App.Apps.Power:
                    return GetBrush(Colors.Pink);
                case App.Apps.Pressure:
                    return GetBrush(Colors.PrimeRed);
                case App.Apps.Torque:
                    return GetBrush(Colors.SkyBlue);
                case App.Apps.Viscosity:
                    return GetBrush(Colors.Violet);

                // calculator
                case App.Apps.Calculator:
                    return GetBrush(Colors.Orange);
                case App.Apps.Scientific:
                    return GetBrush(Colors.SnapBlue);
                case App.Apps.Programmer:
                    return GetBrush(Colors.SnapGreen);
                case App.Apps.Statistics:
                    return GetBrush(Colors.SnapRed);

                // PCal
                case App.Apps.PCalendar:
                    return GetBrush(Colors.SnapPink);

                default:
                    return GetBrush(Colors.None);
            }
        }

        #endregion

        #region Getting App Images

        public static BitmapImage GetMainImage(string ImageFileName)
        {
            return new BitmapImage(GetMainURI(ImageFileName));
        }

        public static Uri GetMainURI(string ImageFileName)
        {
            return new Uri(new Uri("ms-appx:///"), "Assets/GridViewTiles/" + ImageFileName);
        }

        public static BitmapImage GetSnappedImage(string ImageFileName)
        {
            return new BitmapImage(GetSnappedURI(ImageFileName));
        }

        public static Uri GetSnappedURI(string ImageFileName)
        {
            return new Uri(new Uri("ms-appx:///"), "Assets/GridViewTiles/Snapped/" + ImageFileName);
        }


        public static string selectImage(App.Apps app, bool SmallTileRequested = false)
        {
            //All apps should be added here to appear on the main page.
            string suffix = ".png";

            if (SmallTileRequested)
                suffix = ".png";

            switch (app)
            {
                // Notepad
                case App.Apps.Notepad:
                    return "Notepad" + suffix;

                // Calc
                case App.Apps.Calculator:
                    return "Calculator" + suffix;
                case App.Apps.Scientific:
                    return "Scientific.png";
                case App.Apps.Programmer:
                    return "Programmer.png";
                case App.Apps.Statistics:
                    return "Statistics.png";

                // Todo
                case App.Apps.ToDo:
                    return "ToDo.png";

                // misc
                case App.Apps.Stopwatch:
                    return "StopWatch.png";
                case App.Apps.Timer:
                    return "Timer.png";

                // Pcal
                case App.Apps.PCalendar:
                    return "PCalendar" + suffix;

                // Unit converter
                case App.Apps.UnitConverter:
                    return "UnitConverter" + suffix;
                case App.Apps.Area:
                    return "Area.png";
                case App.Apps.Distance:
                    return "Distance.png";
                case App.Apps.Mass:
                    return "Mass.png";
                case App.Apps.Speed:
                    return "Speed.png";
                case App.Apps.Volume:
                    return "Volume.png";
                case App.Apps.Acceleration:
                    return "Acceleration.png";
                case App.Apps.Angles:
                    return "Angles.png";
                case App.Apps.Cooking:
                    return "Cooking.png";
                case App.Apps.Density:
                    return "Density.png";
                case App.Apps.Current:
                    return "Current.png";
                case App.Apps.Energy:
                    return "Energy.png";
                case App.Apps.Flow:
                    return "Flow.png";
                case App.Apps.Force:
                    return "Force.png";
                case App.Apps.Frequency:
                    return "Frequency.png";
                case App.Apps.Light:
                    return "Light.png";
                case App.Apps.Power:
                    return "Power.png";
                case App.Apps.Pressure:
                    return "Pressure.png";
                case App.Apps.Torque:
                    return "Torque.png";
                case App.Apps.Viscosity:
                    return "Viscosity.png";

                // Main Page
                case App.Apps.MainPage:
                    return "Gizmo" + suffix;

                // this should never get called
                default:
                    return string.Empty;

            }
        }

        #endregion

        #region Get App Object

        public static App.Apps GetAppObject(string appName)
        {
            // Notepad
            if (appName == App.Apps.Notepad.ToString())
                return App.Apps.Notepad;

            // Calculator
            else if (appName == App.Apps.Calculator.ToString())
                return App.Apps.Calculator;
            else if (appName == App.Apps.Scientific.ToString())
                return App.Apps.Scientific;
            else if (appName == App.Apps.Programmer.ToString())
                return App.Apps.Programmer;
            else if (appName == App.Apps.Statistics.ToString())
                return App.Apps.Statistics;

            // ToDo
            else if (appName == App.Apps.ToDo.ToString())
                return App.Apps.ToDo;

            // PCal
            else if (appName == App.Apps.PCalendar.ToString())
                return App.Apps.PCalendar;

            // Unit Converter
            else if (appName == App.Apps.UnitConverter.ToString())
                return App.Apps.UnitConverter;
            else if (appName == App.Apps.Area.ToString())
                return App.Apps.Area;
            else if (appName == App.Apps.Distance.ToString())
                return App.Apps.Distance;
            else if (appName == App.Apps.Mass.ToString())
                return App.Apps.Mass;
            else if (appName == App.Apps.Speed.ToString())
                return App.Apps.Speed;
            else if (appName == App.Apps.Volume.ToString())
                return App.Apps.Volume;
            else if (appName == App.Apps.Acceleration.ToString())
                return App.Apps.Acceleration;
            else if (appName == App.Apps.Angles.ToString())
                return App.Apps.Angles;
            else if (appName == App.Apps.Current.ToString())
                return App.Apps.Current;
            else if (appName == App.Apps.Cooking.ToString())
                return App.Apps.Cooking;
            else if (appName == App.Apps.Density.ToString())
                return App.Apps.Density;
            else if (appName == App.Apps.Energy.ToString())
                return App.Apps.Energy;
            else if (appName == App.Apps.Flow.ToString())
                return App.Apps.Flow;
            else if (appName == App.Apps.Force.ToString())
                return App.Apps.Force;
            else if (appName == App.Apps.Frequency.ToString())
                return App.Apps.Frequency;
            else if (appName == App.Apps.Light.ToString())
                return App.Apps.Light;
            else if (appName == App.Apps.Power.ToString())
                return App.Apps.Power;
            else if (appName == App.Apps.Pressure.ToString())
                return App.Apps.Pressure;
            else if (appName == App.Apps.Torque.ToString())
                return App.Apps.Torque;
            else if (appName == App.Apps.Viscosity.ToString())
                return App.Apps.Viscosity;

            // Stopwatch
            else if (appName == App.Apps.Stopwatch.ToString())
                return App.Apps.Stopwatch;

            // Timer
            else if (appName == App.Apps.Timer.ToString())
                return App.Apps.Timer;


            // TODO add more apps

            return App.Apps.MainPage;
        }

        #endregion

        #region Get App Name

        public static string GetAppName(App.Apps app)
        {
            switch (app)
            {
                // Notepad
                case App.Apps.Notepad:
                    return "Notepad";

                // Calc
                case App.Apps.Calculator:
                    return "Calculator";
                case App.Apps.Scientific:
                    return "Scientific";
                case App.Apps.Programmer:
                    return "Programmer";
                case App.Apps.Statistics:
                    return "Statistics";

                // Todo
                case App.Apps.ToDo:
                    return "To Do";

                // misc
                case App.Apps.Stopwatch:
                    return "StopWatch";
                case App.Apps.Timer:
                    return "Timer";

                // Pcal
                case App.Apps.PCalendar:
                    return "P.Calendar";

                // Unit converter
                case App.Apps.UnitConverter:
                    return "Converter";
                case App.Apps.Area:
                    return "Area";
                case App.Apps.Distance:
                    return "Distance";
                case App.Apps.Mass:
                    return "Mass";
                case App.Apps.Speed:
                    return "Speed";
                case App.Apps.Volume:
                    return "Volume";
                case App.Apps.Acceleration:
                    return "Acceleration";
                case App.Apps.Angles:
                    return "Angles";
                case App.Apps.Cooking:
                    return "Cooking";
                case App.Apps.Density:
                    return "Density";
                case App.Apps.Current:
                    return "Current";
                case App.Apps.Energy:
                    return "Energy";
                case App.Apps.Flow:
                    return "Flow";
                case App.Apps.Force:
                    return "Force";
                case App.Apps.Frequency:
                    return "Frequency";
                case App.Apps.Light:
                    return "Light";
                case App.Apps.Power:
                    return "Power";
                case App.Apps.Pressure:
                    return "Pressure";
                case App.Apps.Torque:
                    return "Torque";
                case App.Apps.Viscosity:
                    return "Viscosity";

                // Navigation States


                // Main Page
                case App.Apps.MainPage:
                    return App.AppName;

                // this should never get called
                default:
                    return string.Empty;

            }
        }

        #endregion
    }
}
