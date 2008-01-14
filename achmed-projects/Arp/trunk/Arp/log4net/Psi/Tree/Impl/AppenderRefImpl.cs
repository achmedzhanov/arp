using System;
using System.Xml;
using JetBrains.ReSharper.Editor;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Resolve;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace Arp.log4net.Psi.Tree.Impl
{
    public class AppenderRefImpl : BaseL4NTag, IAppenderRef, IScope
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


        #region IScope

        public IScope GetParentScope()
        {
            return SharedImplUtil.GetParentScopeOnTree(this);
        }

        public void AddGlobalDeclarations(IWritableSymbolTable table, int level)
        {
            // do nothing
        }

        public void AddDeclarationsBeforeElement(IWritableSymbolTable table, IElement element, int level)
        {
            IReference[] references = this.GetReferences();

            if (references == null || references.Length == 0)
                return;

            foreach (IReference reference in references)
            {
                IElement elementFromReference = reference.GetElement();
                IDeclaredElement declaredElement = elementFromReference as IDeclaredElement;

                if (declaredElement == null)
                    continue;

                table.AddSymbol(declaredElement, EmptySubstitution.INSTANCE, this, LevelDelta);
            }
           
        }

        public void AddDeclarationsAfterElement(IWritableSymbolTable table, IElement element, int level)
        {
            // do nothing
        }

        public int LevelDelta
        {
            get { return 1; }
        }

        #endregion

        protected override ITreeReference[] CreateReferences()
        {
            return new ITreeReference[] { new AppenderReference(this) };
//            return base.CreateReferences();
        }

        public override ITreeReference[] GetInternalReferences()
        {
            ITreeReference[] baseRet = base.GetInternalReferences();
            return baseRet;
        }


        public void SetRef(string name)
        {
            XMLPSIUtils.SetAttributeValiue(GetAttribute(L4NConstants.REF), name);
        }

//        public override ITreeReference[] GetInternalReferences()
//        {
//            //return base.GetInternalReferences();
//        }

    }

}