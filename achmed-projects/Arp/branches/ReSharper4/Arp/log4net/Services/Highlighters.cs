using Arp.log4net.Services;
using JetBrains.ReSharper.TextControl.Markup;

//[assembly:RegisterHighlighter( 
//    L4NHighlightingAttributeIds.XML_BACKGROUND,
//    "{02F2DF90-F1D5-410e-99DF-B32D898FE379}",
//    BackgroundColor = "LightSkyBlue:Cyan",
//    Layer = HighlighterLayer.SELECTION - 1,
//    VSPriority = VSPriority.USAGES,
//    ErrorStripeKind = ErrorStripeKind.NONE,
//    ErrorStripeColorName = "Blue"),
//]


//[assembly: RegisterHighlighter(L4NHighlightingAttributeIds.XML_BACKGROUND, "{02F2DF90-F1D5-410e-99DF-B32D898FE379}", BackgroundColor = "LightSkyBlue:Cyan", Layer = 0x1387, VSPriority = 0x2711, ErrorStripeKind = ErrorStripeKind.NONE, ErrorStripeColorName = "Blue")]
[assembly: RegisterHighlighter(L4NHighlightingAttributeIds.XML_BACKGROUND, "{02F2DF90-F1D5-410e-99DF-B32D898FE379}", BackgroundColor = "LightSkyBlue:Cyan", Layer = 0x1387, VSPriority = 0x2711, ErrorStripeKind = ErrorStripeKind.NONE, ErrorStripeColorName = "Blue")]



namespace Arp.log4net.Services
{
    public static class L4NHighlightingAttributeIds
    {
        public const string XML_BACKGROUND = "Arp Xml Background";
    }
}
