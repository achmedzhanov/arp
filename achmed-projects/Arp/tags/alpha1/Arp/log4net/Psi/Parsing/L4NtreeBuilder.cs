using System;
using Arp.log4net.Psi.Tree;
using Arp.log4net.Psi.Tree.Impl;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Parsing;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Psi.Xml.Parsing;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Shell;
using JetBrains.Util;

namespace Arp.log4net.Psi.Parsing
{
    public class L4NTreeBuilder : XmlTreeBuilder
    {
        
        public L4NTreeBuilder(XmlElementFactory elementFactory, CheckForInterrupt checkForInterrupt)
            : base(elementFactory, checkForInterrupt, L4NLanguageService.L4N)
        {
        }

        protected override void AppendNewChild(IXmlAttribute parent, IXmlAttributeValue child)
        {
            base.AppendNewChild(parent, child);
        }
    }
}