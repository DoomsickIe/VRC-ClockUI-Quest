using ReMod.Core.Managers;
using System;
using UnityEngine;
using TMPro;
using MelonLoader;
using ReMod.Core.VRChat;
using UnityEngine.UI;

namespace ClockUI;

static internal class Interface
{
    private static UiManager _ui;
    public static TextMeshProUGUI text;

    private static int mode = 0;
    private static bool format = true;

    static string icon = "iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAYAAABXAvmHAAAFI0lEQVRoQ92aWchVVRTHHTIthwctSxSHxBR9EDU1R1DEkF4qCy0VXyynskCN3gslnOcJIcqs9CExyCEloZxyihC0skQhjEofrJzF/r/LPXLcrn3O2eeeo+aCPx/fHtZeaw9rOrdunWLocbEZKvQVughPCC2EJlX2/+jvWeFX4biwT/ha+L3W5evWwAABxwjjhKdy8jmgeR8J66sKBrPJo0AbrTJDeFV4OHhFe8K/al4jzBV+C+EZokADMZ4ivBe7GiFrZRl7QYPmCLOFy1kmZFWgs5h9JnTPwrSAMd+LxyjhpzReWRQYKSYflLjrPhn/Vsd44fMkJdIUmKTJS4X6aTuh/hPCNmG3gKU5JSAE1ExoK2ChBgrPCB0z8LyuMVzb1b6xSQog/IqURa6pn6u1TNibQaD4kP76Z6rAVUnboIk+JXwKcG0QLInxVvVPE34OFNwdzvtaLAxP4MNJvChscsdYCsDwoBA5IXcOlgLB19YouDsds7xIeMjDl+vYy90wV4EHNeA7wWdt/lDfCOFwwcJH7HCIXwqPevgfUTve/mrU7yrwjjqwwRYh/GDhx5KEj9hyA75JUOJt9eErKhRXoHVVuMaGgFybQQE7f8OjZJrVi58ESjQy+BBXPSmccRVYqIY3PQtPUHvIna9VAcTA8qz0yDNf7dPjChCYnRas2AZrw70PoSIUYL2vhGHGwsRO7YSz0ZG+oX8wZS5h57sKoaayKAVwfEcFy5wj89JIAcJaKyT+WO1jQ7a+OrYoBWD3iTDakAFr2RcFWgmEsNYD66d2ko9QKlKBAVr8W0MA1miF0C8LJBQuEdt0CpW8hBNAxl+EDoYso+nE++FZXVquBmKVPFTkCbA+1gir5NJCFCCCtOKQV9TO/ctDRStA6rrOEGRrdDwk4S71UAOJRR7yKeDjlebgemriIWPyCSZSLWhudD5S7bsXFCA2IpRxqeIHyD0J4lxqqIYreaTXnKJPAFkuGbJcvi8U+D9fob84Aey9lZ/ycIi/81DRV4hEhiTLpcojJlgjyXYJ02U5uDwK1TqHcIYKnktbynJktQrszk90ZL5QAvdNKBF6HYoWnk2mKNzeYDyKzscEshvLmVD6CC2XFK0AdSSyM5duBnN0EJr2NgbxBngLd5M+1eLUjlwiSu6XltBQj+kmlJ3I+zaIZOoHwUpoXlf7skgBUkpKgVZCv13tlpW6E6eyU4vw4cSl21JKBiwQ3vJI9Zraqd/fSZqsxQjpLZqnRr5RZC6rXNRYakKWMylDKYpXuwSrrEKFjrJK5fOUa3koGr3vkehPtVMbKvs9kMhjdYiGLWLnOYEKuQrwFWa/QC5gEUo8K1AEKIPY+S8EX2mRkubTgre0iFAcD1elqUdCwlreyqqCNeDOU7Cyrg1LnReIiYjdbpIvE3peIzYKSeX1HeqnNsPHjFoIU7lEsKxNxBdz/oKw2V0oKZXD8qTtMow3CHzg2CNkDTtYl3IJtvwloV7CDsCThN60gmm5KEpgytK+oLD+SQGfwQM8JuBX4p+YKAXyQDEE+JX2CULHd56r5TXhaQrA6DnhQ8H3JjLIkWsId56P6Lddmzi3LAownqiUT04+65RLwoRJWBvin1serDU+qwLMfUCg0PVuiacRfeiepTUyFRRCFIg2gFrqTIH3YcVOeU6D2AaDwZeXoB+A5FEgEpAAkGSIdK+PEMoL64LTpOJGBfBcHs1DF/Wt0VIdQwS8JJamg4A3jf/cBi+OpYr/3MYqVgXp8R9U3/YUl+1QxwAAAABJRU5ErkJggg==";

    //ty Cyril-Xd for the original base64 to sprite code

    internal static Texture2D CreateTexture(byte[] data)
    {
        Texture2D texture2D = new Texture2D(2, 2);
        texture2D.LoadRawTextureData(data);
        texture2D.hideFlags |= HideFlags.DontUnloadUnusedAsset;
        return texture2D;
    }

    internal static Sprite Base64toSprite(string data)
    {
        Texture2D texture2D = CreateTexture(Convert.FromBase64String(data));
        Rect rect = new Rect(0f, 0f, texture2D.width, texture2D.height);
        Vector2 pivot = new Vector2(0.5f, 0.5f);
        Vector4 border = Vector4.zero;
        Sprite sprite = Sprite.CreateSprite_Injected(texture2D, ref rect, ref pivot, 100f, 0u, SpriteMeshType.Tight, ref border, generateFallbackPhysicsShape: false);
        sprite.hideFlags |= HideFlags.DontUnloadUnusedAsset;
        return sprite;
    } 
    
    public static void UIinit()
    {
        Sprite hudIcon = Base64toSprite(icon);

        _ui = new UiManager("Clock", hudIcon, true, false, false, false);

        Transform qmHTransform = MenuEx.userInterface.transform.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent");

        GameObject clockObject = new();
        clockObject.transform.parent = qmHTransform;
        clockObject.transform.localScale = Vector3.one;
        clockObject.transform.localPosition = Vector3.zero;
        clockObject.transform.localRotation = Quaternion.identity;

        var configpage = _ui.MainMenu.AddCategoryPage("ClockUI Config", "Settings for the clock");

        _ui.MainMenu.AddToggle("Clock Enabled", "Turn the clock on/off", (e) => Config.ChangeVal<bool>("ClockUI", "_clockenabled", e));

        var genpage = configpage.AddCategory("General", false);
        var pospage = configpage.AddSliderCategory("Position", false);
        var colpage = configpage.AddSliderCategory("Color", false);

        //here comes the boilerplate stuff :pensive:

        genpage.AddToggle("12hr/24hr Format", "Switch between time formats", (e) => format = e);
        genpage.AddSpacer();
        genpage.AddSpacer();
        genpage.AddSpacer();
        genpage.AddButton("Date and time", "Display both today's date and the current time", () => mode = 0);
        genpage.AddButton("Date only", "Display only today's date", () => mode = 1);
        genpage.AddButton("Time only", "Display only the current time", () => mode = 2);

        pospage.AddSlider("Clock Position X", "The horizontal local position of the clock", (e) => clockObject.transform.localPosition = new Vector2(e, clockObject.transform.localPosition.y), true, -10f, -1500f, 1500f);
        pospage.AddSlider("Clock Position Y", "The vertical local position of the clock", (e) => clockObject.transform.localPosition = new Vector2(clockObject.transform.localPosition.x, e), true, 1000f, -1500f, 1500f);

        text = clockObject.AddComponent<TextMeshProUGUI>();
        MelonLogger.Msg("ClockUI interface loaded");
    }

    public static void ClockUpdate()
    {
        if(text != null)
        {
            switch(mode)
            {
                case 0:
                    text.text = DateTime.Now.ToString();
                    break;
                case 1:
                    text.text = DateTime.Now.Date.ToString().Split(' ')[0];
                    break;
                case 2:
                    text.text = format == true
                        ? $"{DateTime.Now.Hour:D2}:{DateTime.Now.Minute:D2}{DateTime.Now.Second:D2}"
                        : $"{(DateTime.Now.Hour % 13) % 24:D2}:{DateTime.Now.Minute:D2}{DateTime.Now.Second:D2}";
                    break;
            }
        }
    }
}
