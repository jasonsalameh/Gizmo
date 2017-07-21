using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Common
{
    public static class ShowHelp
    {
        public static async void ShowHelpMessage(App.Apps app, bool ForceMessage = false)
        {
            // check if this app has already showed help
            if (StateManager.RoamingStateExists(app, StateManager.HELP_TEXT) && !ForceMessage)
                return;

            // if not show the message
            await new Windows.UI.Popups.MessageDialog(GetContent(app), "Welcome to " + GetAppName(app) + ", let's get you started!").ShowAsync();

            // finally add it's state so it doesn't get to show again
            if(!StateManager.RoamingStateExists(app, StateManager.HELP_TEXT))
                StateManager.SaveRoamingState(app, StateManager.HELP_TEXT, string.Empty);
        }

        private static string GetAppName(App.Apps app)
        {
            switch (app)
            {
                case App.Apps.Notepad:
                    return "Notepad";
                case App.Apps.Calculator:
                    return "Calculator";
                case App.Apps.UnitConverter:
                    return "Converter";
                case App.Apps.PCalendar:
                    return "P.Calendar";
                case App.Apps.MainPage:
                    return "Gizmo";
                case App.Apps.ToDo:
                    return "ToDo List";

                default:
                    return string.Empty;
            }
        }

        private static string GetContent(App.Apps app)
        {
            switch (app)
            {
                case App.Apps.Notepad:
                    return @"1. Once you've made some changes, your most recently edited files will be shown
2. You can navigate between your recent files just by clicking on them, give it a try";
                case App.Apps.Calculator:
                    return @"1. Calculator has 3 different views (Scientific, Programmer & Statistics)
2. You can change the 'mode' (e.g. Deg/Rad or Hex/Dec/Oct/Bin) by tapping on the value
3. Swipe from the bottom or right click to see values saved in memory. These are saved across views and your PCs";
                case App.Apps.UnitConverter:
                    return @"1. Type in any value to convert the selected unit to all target units
2. You can change the selected unit by clicking on it and either typing or selecting a new one
3. You can change the target units by adjusting the Filter at the bottom of the screen, or by typing in a specific unit, give it a try!
4. The results are color-coded by their set. For example, you can quickly see which results are metric
5. Many more conversions are on the way!";
                case App.Apps.PCalendar:
                    return @"1. Swipe from the bottom or right click to add or edit periods
2. Swipe or click the arrows to move between months
3. All of your period information is saved across all of your PCs";

                case App.Apps.ToDo:
                    return @"1. Add new tasks and categories using the Add Task button.
2. Click any task to edit it. Your changes will be automaticlly saved.
3. Once you complete a task, check it off the list using the box to the left.

Don't forget, you can switch views at any time by using the Date and Category buttons in the upper right.";

                case App.Apps.MainPage:
                    return @"These help dialogs will appear once for each Gizmo and give you tips on how to best use them!

1. You can pin any Gizmo to your start screen by right clicking on it
2. Every Gizmo functions while snapped, give it a try
3. Like the app? Give us a review!
4. Found a problem or want to give feedback? Use the Settings charm to report a bug or send mail to 'threemonkeys@live.com'

More gizmos are on the way!

The Gizmo Team";

                default:
                    return string.Empty;
            }

        }
    }
}
