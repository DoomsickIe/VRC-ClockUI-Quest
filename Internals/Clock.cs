using System;

namespace ClockUI.Internals;

internal static class Clock
{
    public static int DisplayMode = 0;
    public static string dateFormat = "d";
    public static string timeFormat = "hh:mm";

    public static string monoFront = $"<mspace={charSpacing}em>", monoBack = "</mspace>";
    public static float charSpacing = 50;

    /// <summary>
    /// Updates the clock's text with the current time. This shouldn't be called anywhere else except OnLateUpdate().
    /// </summary>
    public static void ClockUpdate()
    {
        if (Interface.text == null)
        {
            return;
        }
        switch (DisplayMode)
        {
            case 0:
                Interface.text.text = $"{monoFront}{DateTime.Now.ToString(dateFormat)} {DateTime.Now.ToString(timeFormat)}{monoBack}";
                break;
            case 1:
                Interface.text.text = $"{monoFront}{DateTime.Now.Date.ToString(dateFormat)}{monoBack}";
                break;
            case 2:
                Interface.text.text = $"{monoFront}{DateTime.Now.ToString(timeFormat)}{monoBack}";
                break;
        }
    }

    public static void ChangeSize(int increment)
    {
        Interface.text.m_fontSize += increment;
        Interface.FontSizeDisplay.SetText(Interface.text.m_fontSize.ToString(), 28);
    }

    /// <summary>
    /// Toggles the display of seconds on the clock.
    /// </summary>
    public static void ToggleSecondsDisplay()
    {
        switch (timeFormat)
        {
            case "hh:mm":
                timeFormat = "hh:mm:ss";
                break;
            case "hh:mm:ss":
                timeFormat = "hh:mm";
                break;
            case "HH:mm:ss":
                timeFormat = "HH:mm";
                break;
            case "HH:mm":
                timeFormat = "HH:mm:ss";
                break;
        }
    }

    /// <summary>
    /// Cycles between 12hr and 24hr time formats.
    /// </summary>
    public static void SwitchTimeFormats()
    {
        switch (timeFormat)
        {
            case "HH:mm:ss":
                timeFormat = "hh:mm:ss";
                break;
            case "HH:mm":
                timeFormat = "hh:mm";
                break;
            case "hh:mm:ss":
                timeFormat = "HH:mm:ss";
                break;
            case "hh:mm":
                timeFormat = "HH:mm";
                break;
        }
    }

    public static void Monospace(bool val)
    {
        if(val == true)
        {
            monoFront = $"<mspace={charSpacing}>";
            monoBack = "</mspace>";
        }
        else
        {
            monoFront = "";
            monoBack = monoFront;
        }
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
