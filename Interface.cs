using ReMod.Core.Managers;
using ClockUI.Internals;
using UnityEngine;
using TMPro;
using MelonLoader;
using ReMod.Core.VRChat;
using ReMod.Core.UI.QuickMenu;
using UnityEngine.UI;

namespace ClockUI;

internal static class Interface
{
    private static UiManager _ui;

    public static TextMeshProUGUI text;
    public static ReMenuLabel FontSizeDisplay;

    internal static ReCategoryPage configpage;
    internal static ReCategoryPage stylepage;
    internal static Transform qmHTransform;
    internal static GameObject clockObject = new();

    internal static void LoadPage_General()
    {
        ReMenuCategory genpage = configpage.AddCategory("General", false);

        genpage.AddToggle("12hr/24hr Format", "Switch between time formats", (e) => Clock.SwitchTimeFormats());
        genpage.AddToggle("Long Date", "Convert the date into word form", (e) => Clock.dateFormat = Clock.dateFormat == "d" ? "D" : "d");
        genpage.AddToggle("Show Seconds", "Show the amount of seconds", (e) => Clock.ToggleSecondsDisplay());

        genpage.AddSpacer();

        genpage.AddButton("Date and time", "Display both today's date and the current time", () => Clock.DisplayMode = 0);
        genpage.AddButton("Date only", "Display only today's date", () => Clock.DisplayMode = 1);
        genpage.AddButton("Time only", "Display only the current time", () => Clock.DisplayMode = 2);

        genpage.AddSpacer();

        genpage.AddButton("Size -", "Decrease the size of the text", () => 
        {
            Clock.ChangeSize(-5);
            Clock.Refresh();
        });
        FontSizeDisplay = genpage.AddLabel("46", "Current font size", 28);
        genpage.AddButton("Size +", "Increase the size of the text", () =>
        {
            Clock.ChangeSize(5);
            Clock.Refresh();
        });

        genpage.AddSpacer();

        genpage.AddButton("Refresh (debug)", "Refresh clock text", Clock.Refresh);
        genpage.AddButton("Save Settings", "Save config to file", Config.SaveAll);
    }

    internal static void LoadPage_Position()
    {
        ReMenuSliderCategory pospage = configpage.AddSliderCategory("Position", false);

        pospage.AddSlider("m_marginHorizontal", "test", (e) => text.m_marginWidth = e, true, 25, 1, 1000);
        pospage.AddSlider("Clock Position X", "The horizontal local position of the clock", (e) => clockObject.transform.localPosition = new Vector2(e, clockObject.transform.localPosition.y), true, -100f, -1500f, 1500f);
        pospage.AddSlider("Clock Position Y", "The vertical local position of the clock", (e) => clockObject.transform.localPosition = new Vector2(clockObject.transform.localPosition.x, e), true, 700f, -1500f, 1500f);
    }

    internal static void LoadPage_Style()
    {
        ReMenuCategory cat = stylepage.AddCategory("Text Styles");
        cat.AddToggle("Monospace Font", "Change the clock font to monospace", Clock.Monospace);
        ReMenuSliderCategory slcat = stylepage.AddSliderCategory("Monospace");
        slcat.AddSlider("Monospace Character Spacing", "Change the spacing of the monospace font", (e) =>
        {
            Clock.charSpacing = e;
            Clock.Refresh();
        }, false, 1, 50, 1000);

        ReMenuSliderCategory colpage = stylepage.AddSliderCategory("Color", false);

        colpage.AddSlider("Clock Color R", "Red value", (e) => text.color = new Color(e, text.color.g, text.color.b), false, 1, 0, 1);
        colpage.AddSlider("Clock Color G", "Green value", (e) => text.color = new Color(text.color.r, e, text.color.b), false, 1, 0, 1);
        colpage.AddSlider("Clock Color B", "Blue value", (e) => text.color = new Color(text.color.r, text.color.g, e), false, 1, 0, 1);
    }

    public static void UIinit()
    {
        _ui = new UiManager("Clock", Internals.UI.IconMethods.Base64toSprite(Internals.UI.Icons.TabIcon), true, false, false, false);

        qmHTransform = MenuEx.userInterface.transform.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container");

        clockObject.transform.parent = qmHTransform;
        clockObject.transform.localScale = Vector3.one;
        clockObject.transform.localPosition = Vector3.zero;
        clockObject.transform.localRotation = Quaternion.identity;

        text = clockObject.AddComponent<TextMeshProUGUI>();

        configpage = _ui.MainMenu.AddCategoryPage("ClockUI Config", "Settings for the clock", Internals.UI.IconMethods.Base64toSprite(Internals.UI.Icons.ConfigGear));
        stylepage = _ui.MainMenu.AddCategoryPage("Style Settings", "Appearance options", Internals.UI.IconMethods.Base64toSprite(Internals.UI.Icons.StylePalette));
        _ui.MainMenu.AddToggle("Clock Enabled", "Turn the clock on/off", (e) => clockObject.active = e, true);

        LoadPage_General();
        LoadPage_Position();

        LoadPage_Style();

        Canvas canv = new();
        canv.transform.parent = clockObject.transform;
        canv.renderMode = RenderMode.WorldSpace;

        Image bg = new();
        bg.color = Color.black;
        bg.transform.parent = canv.transform;

        MelonLogger.Msg("UI initialized, loading from config");

        Config.Load();
    }
}
