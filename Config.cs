using System.Collections;
using MelonLoader;
using UnityEngine;

namespace ClockUI;
public static class Config
{
    public static MelonPreferences_Entry<float> _clockposx;
    public static MelonPreferences_Entry<float> _clockposy;

    public static MelonPreferences_Entry<int> _clocksize;

    public static MelonPreferences_Entry<int> _clockdisplay;

    public static MelonPreferences_Entry<bool> _clockformat;

    public static MelonPreferences_Entry<float> _clockcolor_r;
    public static MelonPreferences_Entry<float> _clockcolor_g;
    public static MelonPreferences_Entry<float> _clockcolor_b;

    public static void ChangeVal<T>(string cat, string iden, T value)
    {
        MelonPreferences.SetEntryValue<T>(cat, iden, value);
        TMPConfigUpdate();
    }

    public static void Init()
    {
        var cat = MelonPreferences.CreateCategory("ClockUI");

        _clockposx = cat.CreateEntry<float>("clock_localpos_x", 0.2f);
        _clockposy = cat.CreateEntry<float>("clock_localpos_y", 3f);
        _clocksize = cat.CreateEntry<int>("clock_fontsize", 1);
        _clockdisplay = cat.CreateEntry<int>("clock_mode", 0);
        _clockformat = cat.CreateEntry<bool>("clock_format", true);
        _clockcolor_r = cat.CreateEntry<float>("clock_color_r", 255f);
        _clockcolor_g = cat.CreateEntry<float>("clock_color_g", 255f);
        _clockcolor_b = cat.CreateEntry<float>("clock_color_b", 255f);

        MelonPreferences.Save();

        MelonLogger.Msg("ClockUI config initialized successfully");
        TMPConfigUpdate();
    }

    static IEnumerator TMPConfigUpdate()
    {
        while (Interface.text == null)
        {
            yield return null;
        }
        Interface.text.color = new(_clockcolor_r.Value, _clockcolor_g.Value, _clockcolor_b.Value);
        Interface.text.fontSize = _clocksize.Value;
        Interface.text.rectTransform.localPosition = new Vector3(_clockposx.Value, _clockposy.Value, 0f);
    }
}
