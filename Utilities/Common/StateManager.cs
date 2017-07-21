using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator;
using System.Collections.ObjectModel;

namespace Utilities
{
    public static class StateManager
    {
        public enum CalcState { Text, Value, Type}
        public enum PCalState { StartYear, StartMonth, StartDay, Duration, CycleLength }

        public const string CALC_MEMORY_KEY = "MEMORY";
        public const string PCAL_PERIOD_KEY = "PERIOD";
        public const string TITLE_ROW_TIME = "TIME";
        public const string APP_VERSION = "VERSION";
        public const string HELP_TEXT = "HELP";


        /// <summary>
        /// Saves data into the Windows.Storage.ApplicaitonDataContainer object with the App.Apps enum
        /// as the key. Note: this will fail if the state to be saved is larger than 8k
        /// </summary>
        /// <param name="app">The calling app's identity</param>
        /// <param name="key">A unique key for the type of memory being saved, handy if the app saves more than one type of state</param>
        /// <param name="value">The object which represents the state to be serialized and saved into roaming state</param>
        /// <returns>True if the save was successfull, false otherwise</returns>
        public static bool SaveRoamingState(App.Apps app, string key, object value)
        {
            // Because there are many "calc" views but we want the memory to be preserved in between
            // all of the views
            if (Calc_MainPage.IsCalculator(app))
                app = App.Apps.Calculator;

            try
            {
                Windows.Storage.ApplicationDataContainer roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

                string serialized = string.Empty;
                List<object> memoryItemsObj = new List<object>();

                // Calc specific
                if (value is ObservableCollection<Calculator.MemoryItem>)
                {
                    foreach (Calculator.MemoryItem memItem in value as ObservableCollection<Calculator.MemoryItem>)
                        memoryItemsObj.Add(memItem as object);
                }

                else if (value is List<PCalender.Period>)
                {
                    foreach (PCalender.Period period in value as List<PCalender.Period>)
                        memoryItemsObj.Add(period as object);
                }

                // TODO other apps


                serialized = JSONCode.JSON.JsonEncode(memoryItemsObj);

                // add the value into the roaming state array
                if (roamingSettings.Values.ContainsKey(app.ToString() + key))
                    roamingSettings.Values[app.ToString() + key] = serialized;
                else
                    roamingSettings.Values.Add(app.ToString() + key, serialized);
            }
            catch { return false; }

            return true;
        }


        /// <summary>
        /// Saves data into the Windows.Storage.ApplicaitonDataContainer object with the App.Apps enum
        /// as the key. Note: this will fail if the state to be saved is larger than 8k
        /// </summary>
        /// <param name="app">The calling app's identity</param>
        /// <param name="state">The object which represents the state to be serialized and saved into roaming state</param>
        /// <returns>True if the save was successfull, false otherwise</returns>
        public static bool SaveRoamingState(App.Apps app, string key, string value)
        {
            try
            {
                Windows.Storage.ApplicationDataContainer roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

                // add the value into the roaming state array
                if (roamingSettings.Values.ContainsKey(app.ToString() + key))
                    roamingSettings.Values[app.ToString() + key] = value;
                else
                    roamingSettings.Values.Add(app.ToString() + key, value);
            }
            catch { return false; }

            return true;
        }

        /// <summary>
        /// Clears the roaming state of a given application
        /// </summary>
        /// <param name="app">The calling app's identity</param>
        /// <returns>True if the clear was successfull, false otherwise</returns>
        public static bool ClearRoamingState(App.Apps app, string key)
        {
            try
            {
                Windows.Storage.ApplicationDataContainer roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

                if (roamingSettings.Values.ContainsKey(app.ToString() + key))
                    roamingSettings.Values.Remove(app.ToString() + key);
            }
            catch { return false; }

            return true;
        }

        /// <summary>
        /// Determines if the roaming state manager has the selected value saved
        /// </summary>
        /// <param name="app">The calling app's identity</param>
        /// <param name="key">The key for the settings</param>
        /// <returns></returns>
        public static bool RoamingStateExists(App.Apps app, string key)
        {
            Windows.Storage.ApplicationDataContainer roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

            return roamingSettings.Values.ContainsKey(app.ToString() + key);
        }


        /// <summary>
        /// Deserializes the input string into a c# generic object
        /// </summary>
        /// <param name="app">The calling app's identity</param>
        /// <param name="success">True if the clear was successfull, false otherwise</param>
        /// <returns>The deserialized generic object. If the method fails the return is null</returns>
        public static object LoadRoamingState(App.Apps app, string key, ref bool success, Calc_MainPage page = null)
        {
            success = false;
            try
            {
                Windows.Storage.ApplicationDataContainer roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

                string serialized = roamingSettings.Values[app.ToString() + key] as string;

                if (serialized.StartsWith("{") || serialized.StartsWith("["))
                {
                    object obj = JSONCode.JSON.JsonDecode(serialized, ref success);

                    // Calculator specific
                    if (app == App.Apps.Calculator)
                        return ConvertToCalc(obj, ref success, page);
                    
                    // PCalendar specific
                    else if (app == App.Apps.PCalendar)
                        return ConvertToPCal(obj, ref success);

                    // TODO other apps
                }
                else
                    return serialized;

            }
            catch { return null; }

            return null;
        }

        private static object ConvertToPCal(object obj, ref bool success)
        {
            success = false;

            List<PCalender.Period> returnList = new List<PCalender.Period>();

            try
            {
                List<object> firstList = obj as List<object>;

                foreach (object val in firstList)
                {
                    Dictionary<string, object> dict = val as Dictionary<string, object>;

                    // get the datetime object
                    int year = int.Parse(dict[PCalState.StartYear.ToString()].ToString());
                    int month = int.Parse(dict[PCalState.StartMonth.ToString()].ToString());
                    int day = int.Parse(dict[PCalState.StartDay.ToString()].ToString());

                    DateTime startDate = new DateTime(year, month, day);

                    // get the duration and cycle length
                    int duration = int.Parse(dict[PCalState.Duration.ToString()].ToString());
                    int cycleLength = int.Parse(dict[PCalState.CycleLength.ToString()].ToString());

                    PCalender.Period period = new PCalender.Period(startDate, duration, cycleLength);

                    returnList.Add(period);
                }

                success = true;
            }
            catch { return null; }

            return returnList;
        }

        /// <summary>
        /// Converts the object into a List<MemoryItem>. 
        /// </summary>
        /// <param name="obj">Expects this object to be the following List<object> where each object
        /// is a List<object> where each sub-object is Key,Value pair of Text, Value, Type</param>
        /// <param name="success"></param>
        /// <returns></returns>
        private static object ConvertToCalc(object obj, ref bool success, Calc_MainPage page )
        {
            success = false;

            // if there's no valid page to link to
            if (page == null)
                return null;

            List<Calculator.MemoryItem> returnList = new List<MemoryItem>();
            
            try
            {
                List<object> firstList = obj as List<object>;

                foreach (object val in firstList)
                {
                    Dictionary<string, object> dict = val as Dictionary<string, object>;

                    // create a new item
                    MemoryItem mi = new MemoryItem();
                    mi.InitializeWindow(page);
                    mi.Text = dict[CalcState.Text.ToString()] as string;

                    // the first part is the value
                    mi.Value = (dict[CalcState.Value.ToString()] as string);
                    mi.Type = (Calc_MainPage.CalcStateValues)int.Parse(dict[CalcState.Type.ToString()].ToString());

                    returnList.Add(mi);
                }

                success = true;
            }
            catch { return null; }

            return returnList;
        }

    }
}
