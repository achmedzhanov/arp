using System;
using System.Xml;
using JetBrains.ReSharper.Editor;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Resolve;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace Arp.log4net.Psi.Tree.Impl
{
    public class AppenderRefImpl : BaseL4NTag, IAppenderRef
    {
        public AppenderRefImpl() : base(L4NElementType.APPENDER_REF_ELEMENT)
        {
        }

        public string Ref
        {
            get { return GetAttributeValue(L4NConstants.REF, string.Empty); }
        }

        public IXmlAttributeValue RefValue
        {
            get { return GetAttributeValueElement(L4NConstants.REF); }
        }

        public new ILogger Parent
        {
            get { return (ILogger)base.Parent; }
        }


        protected override ITreeReference[] CreateReferences()
        {
            return new ITreeReference[] { new AppenderReference(this) };
//            return base.CreateReferences();
        }

//        public override ITreeReference[] GetInternalReferences()
//        {
//            //return base.GetInternalReferences();
//        }

    }

}