using ReMod.Core.Managers;
using ClockUI.Internals;
using ClockUI.Internals.UI;
using UnityEngine;
using TMPro;
using MelonLoader;
using ReMod.Core.VRChat;
using ReMod.Core.UI.QuickMenu;

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
        ReMenuCategory genpage = configpage.AddCategory("General");

        genpage.AddToggle("12hr/24hr Format", "Switch between time formats", Clock.SwitchTimeFormats, false);
        genpage.AddToggle("Long Date", "Convert the date into word form", Clock.SwitchDateFormats, Config.clock_dateformat.Value);
        genpage.AddToggle("Show Seconds", "Show the amount of seconds", Clock.ToggleSeconds, Config.clock_seconds.Value);
            
        genpage.AddSpacer();
        
        genpage.AddButton("Date and time", "Display both today's date and the current time", () => Clock.ChangeDisplay(Clock.displayMode.All), IconMethods.Base64toSprite(Icons.DisplayBoth));
        genpage.AddButton("Date only", "Display only today's date", () => Clock.ChangeDisplay(Clock.displayMode.Date), IconMethods.Base64toSprite(Icons.DisplayCalendar));
        genpage.AddButton("Time only", "Display only the current time", () => Clock.ChangeDisplay(Clock.displayMode.Time), IconMethods.Base64toSprite(Icons.TabIcon));

        genpage.AddSpacer();

        genpage.AddButton("Size -", "Decrease the size of the text", () => 
        {
            Style.ChangeSize(-5);
            Clock.Refresh();
        });
        FontSizeDisplay = genpage.AddLabel("36", "Current font size", 32);
        genpage.AddButton("Size +", "Increase the size of the text", () =>
        {
            Style.ChangeSize(5);
            Clock.Refresh();
        });

        genpage.AddSpacer();
    }

    internal static void LoadPage_Position()
    {
        ReMenuSliderCategory pospage = configpage.AddSliderCategory("Position");

        pospage.AddSlider("Clock Position X", "The horizontal local position of the clock", (e) => clockObject.transform.localPosition = new Vector2(e, clockObject.transform.localPosition.y), true, -100f, -1500f, 1500f);
        pospage.AddSlider("Clock Position Y", "The vertical local position of the clock", (e) => clockObject.transform.localPosition = new Vector2(clockObject.transform.localPosition.x, e), true, 700f, -1500f, 1500f);
    }

    internal static void LoadPage_Style()
    {
        ReMenuCategory cat = stylepage.AddCategory("Text Styles");
        cat.AddToggle("Monospace", "Make the clock font be monospace", Style.Monospace, Config.clock_monospace.Value);
        cat.AddToggle("Bold", "Make the clock font be boldface", Style.Bold, Config.clock_bold.Value);
        cat.AddToggle("Italic", "Italicize the font", Style.Italic, Config.clock_italic.Value);
        cat.AddToggle("Multi-line", "Put the time and date on separate lines", Style.Multiline, Config.clock_multiline.Value);

        ReMenuCategory alcat = stylepage.AddCategory("Alignment");
        alcat.AddButton("Left", "Left text align", () => Style.Align(Style.AlignMode.left), IconMethods.Base64toSprite(Icons.Alignment.left));
        alcat.AddButton("Center", "Center text align", () => Style.Align(Style.AlignMode.center), IconMethods.Base64toSprite(Icons.Alignment.center));
        alcat.AddButton("Right", "Right text align", () => Style.Align(Style.AlignMode.right), IconMethods.Base64toSprite(Icons.Alignment.right));
        alcat.AddButton("Justify", "Justified text align", () => Style.Align(Style.AlignMode.justify), IconMethods.Base64toSprite(Icons.Alignment.justify));

        ReMenuSliderCategory slcat = stylepage.AddSliderCategory("Monospace");
        slcat.AddSlider("Monospace Character Spacing", "Change the spacing of monospace", (e) =>
        {
            bool tmp = Style.monoTag.Contains("<mspace");
            Style.charSpacing = e;
            Style.Monospace(false);
            if(tmp) Style.Monospace(true);
        }, false, 50, 25, 150);

        ReMenuSliderCategory colpage = stylepage.AddSliderCategory("Color");

        colpage.AddSlider("Clock Color R", "Red value", (e) => text.color = new Color(e, text.color.g, text.color.b), false, Config.clockcolor_r.Value, 0, 1);
        colpage.AddSlider("Clock Color G", "Green value", (e) => text.color = new Color(text.color.r, e, text.color.b), false, Config.clockcolor_g.Value, 0, 1);
        colpage.AddSlider("Clock Color B", "Blue value", (e) => text.color = new Color(text.color.r, text.color.g, e), false, Config.clockcolor_b.Value, 0, 1);
    }

    public static void UIinit()
    {
        _ui = new UiManager("Clock", IconMethods.Base64toSprite(Icons.TabIcon), true, false, false, false);

        qmHTransform = MenuEx.userInterface.transform.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container");

        clockObject.transform.parent = qmHTransform;
        clockObject.transform.localScale = Vector3.one;
        clockObject.transform.localPosition = Vector3.zero;
        clockObject.transform.localRotation = Quaternion.identity;

        text = clockObject.AddComponent<TextMeshProUGUI>();

        configpage = _ui.MainMenu.AddCategoryPage("ClockUI Config", "Settings for the clock", IconMethods.Base64toSprite(Icons.ConfigGear));
        stylepage = _ui.MainMenu.AddCategoryPage("Style Settings", "Appearance options", IconMethods.Base64toSprite(Icons.StylePalette));
        _ui.MainMenu.AddToggle("Clock Enabled", "Turn the clock on/off", (e) => clockObject.active = e, true);
        _ui.MainMenu.AddButton("Save Config", "Save config to file", Config.SaveAll);
        LoadPage_General();
        LoadPage_Position();

        LoadPage_Style();

        MelonLogger.Msg("UI initialized, loading from config");

        Config.init();
    }
}
