using ReMod.Core;
using ConfigManager = ReMod.Core.Managers.ConfigManager;

namespace ClockUI.Internals;
// this file actually hurt me to write
public static class Config
{
    public static ConfigManager cfg = new("clockui_config");
 
    public static void init() => Load();

    public static ConfigValue<bool> clock_enabled = new("clock_enabled", true);
    public static ConfigValue<bool> clock_timeformat = new("clock_timeformat", false);
    public static ConfigValue<bool> clock_dateformat = new("clock_dateformat", false);
    public static ConfigValue<bool> clock_seconds = new("clock_seconds", false);
    public static ConfigValue<Clock.displayMode> clock_display = new("clock_display", 0);
    public static ConfigValue<float> clockpos_x = new("clockpos_x", -380);
    public static ConfigValue<float> clockpos_y = new("clockpos_y", 590);
    public static ConfigValue<float> clockcolor_r = new("clockcolor_r", 255);
    public static ConfigValue<float> clockcolor_g = new("clockcolor_g", 255);
    public static ConfigValue<float> clockcolor_b = new("clockcolor_b", 255);
    public static ConfigValue<bool> clock_monospace = new("clock_monospace", false);
    public static ConfigValue<bool> clock_bold = new("clock_bold", true);
    public static ConfigValue<bool> clock_italic = new("clock_italic", false);
    public static ConfigValue<bool> clock_multiline = new("clock_multiline", false);
    public static ConfigValue<string> clock_align = new("clock_align", "<align=\"left\">");
    public static ConfigValue<float> clock_fontsize = new("clock_fontsize", 36);

    public static void Load()
    {
        Interface.clockObject.SetActive(clock_enabled.Value);
        Clock.SwitchDateFormats(clock_dateformat.Value);
        Clock.SwitchDateFormats(clock_timeformat.Value);
        Clock.ToggleSeconds(clock_seconds.Value);
        Clock.ChangeDisplay(clock_display.Value);
        UI.Style.Monospace(clock_monospace.Value);
        UI.Style.Bold(clock_bold.Value);
        UI.Style.Italic(clock_italic.Value);
        UI.Style.Multiline(clock_multiline.Value);
        UI.Style.align = clock_align.Value;
        Interface.clockObject.transform.localPosition = new UnityEngine.Vector3(clockpos_x.Value, clockpos_y.Value);
        Interface.text.color = new UnityEngine.Color(clockcolor_r.Value, clockcolor_g.Value, clockcolor_b.Value);
        Interface.text.m_fontSize = clock_fontsize.Value;
        Clock.Refresh();
    }

    public static void SaveAll()
    {
        clock_enabled.SetValue(Interface.clockObject.active);
        clock_timeformat.SetValue(Clock.hourFormat == "HH");
        clock_dateformat.SetValue(Clock.dateFormat == "D");
        clock_display.SetValue((Clock.displayMode)Clock.dm);
        clockpos_x.SetValue(Interface.clockObject.transform.localPosition.x);
        clockpos_y.SetValue(Interface.clockObject.transform.localPosition.y);
        clockcolor_r.SetValue(Interface.text.color.r);
        clockcolor_g.SetValue(Interface.text.color.g);
        clockcolor_b.SetValue(Interface.text.color.b);
        clock_monospace.SetValue(UI.Style.monoTag.Contains("<mspace"));
        clock_bold.SetValue(UI.Style.boldTag == "<b>");
        clock_italic.SetValue(UI.Style.italicTag == "<i>");
        clock_multiline.SetValue(UI.Style.linebreak == "<br>");
        clock_align.SetValue(UI.Style.align);
        clock_fontsize.SetValue(Interface.text.m_fontSize);
    }
}
