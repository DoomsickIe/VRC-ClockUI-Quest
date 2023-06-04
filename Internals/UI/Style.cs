namespace ClockUI.Internals.UI;

internal class Style
{
    public static string monoTag = $"<mspace={charSpacing}em>";
    public static string boldTag = "<b>";
    public static string italicTag = "";
    public static string linebreak = " ";

    public enum AlignMode
    {
        left,
        right,
        center,
        justify,
        flush
    }

    public static string align = $"<align=\"left\">";

    public static float charSpacing = 50;

    public static void Monospace(bool val)
    {
        monoTag = val ? $"<mspace={charSpacing}>" : "";
        Clock.Refresh();
    }

    public static void Bold(bool val)
    {
        boldTag = val ? "<b>" : "";
        Clock.Refresh();
    }

    public static void Italic(bool val)
    {
        italicTag = val ? "<i>" : "";
        Clock.Refresh();
    }

    public static void Multiline(bool val)
    {
        linebreak = val ? "<br>" : " ";
        Clock.Refresh();
    }

    public static void Align(AlignMode mode)
    {
        switch(mode)
        {
            case AlignMode.left:
                align = "<align=\"left\">"; break;
            case AlignMode.right:
                align = "<align=\"right\">"; break;
            case AlignMode.center:
                align = "<align=\"center\">"; break;
            case AlignMode.justify:
                align = "<align=\"justify\">"; break;
        }
        Clock.Refresh();
    }

    public static void ChangeSize(int increment)
    {
        Interface.text.m_fontSize += increment;
        Interface.FontSizeDisplay.SetText(Interface.text.m_fontSize.ToString(), 28);
        Clock.Refresh();
    }

}
