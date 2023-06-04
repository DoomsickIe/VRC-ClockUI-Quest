using System;
using ClockUI.Internals.UI;

namespace ClockUI.Internals;

public static class Clock
{
    public enum displayMode
    {
        All,
        Date,
        Time
    }
    public static int dm = 0;

    public static string dateFormat = "d";
    public static string hourFormat = "hh";
    public static string seconds = "";

    public static string ampm = "";

    public static void ChangeDisplay(displayMode mode) => dm = (int)mode;
    
    /// <summary>
    /// Updates the clock's text with the current time. This shouldn't be called anywhere else except OnLateUpdate().
    /// </summary>
    public static void ClockUpdate()
    {
        if (Interface.text == null)
        {
            return;
        }
        if(hourFormat == "hh")
        {
            ampm = DateTime.Now.Hour / 12 >= 1 ? "PM" : "AM";
        }
        else
        {
            ampm = "";
        }
        switch (dm)
        {
            case 0:
                Interface.text.text = $"{Style.monoTag}{Style.boldTag}{Style.italicTag}{Style.align}{DateTime.Now.ToString(dateFormat)}{Style.linebreak}{DateTime.Now.ToString($"{hourFormat}:mm{seconds}")} {ampm}";
                break;
            case 1:
                Interface.text.text = $"{Style.monoTag}{Style.boldTag}{Style.italicTag}{Style.align}{DateTime.Now.Date.ToString(dateFormat)}";
                break;
            case 2:
                Interface.text.text = $"{Style.monoTag}{Style.boldTag}{Style.italicTag}{Style.align}{DateTime.Now.ToString($"{hourFormat}:mm{seconds}")} {ampm}";
                break;
        }
    }

    /// <summary>
    /// Toggles the display of seconds on the clock.
    /// </summary>
    public static void ToggleSeconds(bool val)
    {
        seconds = val == true ? ":ss" : "";
        Clock.Refresh();
    }

    /// <summary>
    /// Cycles between 12hr and 24hr time formats.
    /// </summary>
    public static void SwitchTimeFormats(bool val)
    {
        hourFormat = val == true ? "HH" : "hh";
        Clock.Refresh();
    }

    public static void SwitchDateFormats(bool val)
    {
        dateFormat = val == true ? "D" : "d";
        Clock.Refresh();
    }

    ///<summary>
    ///Refreshes the text on the clock, and applies any changes such as margin or size adjustments.
    ///</summary>
    public static void Refresh()
    {
        string tmp = Interface.text.text;
        Interface.text.text = string.Empty;
        Interface.text.text = tmp;
        return;
    }
}
