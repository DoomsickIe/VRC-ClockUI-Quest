using ReMod.Core;

namespace ClockUI.Internals;
// this file actually hurt me to write
internal static class Config
{
    public static class Settings
    {
        public static ConfigValue<string> clock_timeformat = new("clock_timeformat", "hh:mm");
        public static ConfigValue<string> clock_dateformat = new("clock_dateformat", "d");
        public static ConfigValue<int> clock_display = new("clock_display", 0);
    }
    public static class Position
    {
        public static ConfigValue<float> clockpos_x = new("clockpos_x", -100);
        public static ConfigValue<float> clockpos_y = new("clockpos_y", 700);
    }
    public static class Style
    {
        public static ConfigValue<float> clockcolor_r = new("clockcolor_r", 255);
        public static ConfigValue<float> clockcolor_g = new("clockcolor_g", 255);
        public static ConfigValue<float> clockcolor_b = new("clockcolor_b", 255);
        public static ConfigValue<bool> clock_monospace = new("clock_monospace", true);
        public static ConfigValue<float> clock_fontsize = new("clock_fontsize", 50);
    }

    public static void Load()
    {
        Clock.dateFormat = Settings.clock_dateformat.Value;
        Clock.timeFormat = Settings.clock_timeformat.Value;
        Clock.DisplayMode = Settings.clock_display.Value;
        Clock.Monospace(Style.clock_monospace.Value);
        Interface.clockObject.transform.localPosition = new UnityEngine.Vector3(Position.clockpos_x.Value, Position.clockpos_y.Value);
        Interface.text.color = new UnityEngine.Color(Style.clockcolor_r.Value, Style.clockcolor_g.Value, Style.clockcolor_b.Value);
        Interface.text.m_fontSize = Style.clock_fontsize.Value;
    }

    public static void SaveAll()
    {
        Settings.clock_timeformat.SetValue(Clock.timeFormat);
        Settings.clock_dateformat.SetValue(Clock.dateFormat);
        Settings.clock_display.SetValue(Clock.DisplayMode);
        Position.clockpos_x.SetValue(Interface.clockObject.transform.localPosition.x);
        Position.clockpos_y.SetValue(Interface.clockObject.transform.localPosition.y);
        Style.clockcolor_r.SetValue(Interface.text.color.r);
        Style.clockcolor_g.SetValue(Interface.text.color.g);
        Style.clockcolor_b.SetValue(Interface.text.color.b);
        Style.clock_monospace.SetValue(Clock.monoBack == "</mspace>");
        Style.clock_fontsize.SetValue(Interface.text.m_fontSize);
    }
}
