using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.Util;

namespace Arp.log4net.Psi
{
    public class L4NTokenNodeType : TokenNodeType
    {
        public static readonly TokenNodeType IDENTIFIER = new L4NTokenNodeType("IDENTIFIER");
        public static readonly TokenNodeType DOT = new L4NTokenNodeType("DOT");
        public static readonly TokenNodeType TEXT = new L4NTokenNodeType("TEXT");
        public static readonly TokenNodeType QUOTE = new L4NTokenNodeType("QUOTE");
        
        public L4NTokenNodeType(string s) : base(s)
        {
        }

        public override LeafElement Create(IBuffer buffer, int startOffset, int endOffset)
        {
            return new XmlToken(this, buffer, startOffset, endOffset);
        }


        public override bool IsWhitespace
        {
            get { throw new System.NotImplementedException(); }
        }

        public override bool IsComment
        {
            get { return false; }
        }

        public override bool IsStringLiteral
        {
            get { return true; }
        }

        public override PsiLanguageType LanguageType
        {
            get { return L4NLanguageService.L4N; }
        }
    }
}