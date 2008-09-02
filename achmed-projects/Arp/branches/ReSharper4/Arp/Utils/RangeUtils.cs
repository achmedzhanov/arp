using JetBrains.ReSharper.Editor;
using JetBrains.Util;

namespace Arp.Utils
{
    public static class RangeUtils
    {
        public static TextRange Narrow(TextRange textRange, int value)
        {
            return new TextRange(textRange.StartOffset + value, textRange.EndOffset - value);
        }
 
//        public static DocumentRange Narrow(DocumentRange textRange, int value)
//        {
//            return new TextRange(textRange. StartOffset + value, textRange.EndOffset - value);
//        }
    }
}