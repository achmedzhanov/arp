using System.Collections.Generic;
using JetBrains.ReSharper.Editor;
using JetBrains.ReSharper.Psi;
using JetBrains.Util;

namespace Arp.log4net.Psi.Tree.Impl
{
    public class ParamPropertyReference : BaseDeclaredElementReference<ParamImpl>
    {
        public ParamPropertyReference(ParamImpl owner) : base(owner)
        {
        }

        protected override ICollection<IDeclaredElement> GetDeclaredElements()
        {
            IDeclaredElement element = GetDeclaredElement();
            if (element == null)
                return EmptyArray<IDeclaredElement>.Instance;
            else
            {
                List<IDeclaredElement> ret = new List<IDeclaredElement>(1);
                ret.Add(element);
                return ret;
            }
        }

        public override string GetName()
        {
            IDeclaredElement element = GetDeclaredElement();
            if (element != null)
                return element.ShortName;
            else
                return myOwner.Name;
        }

        private IDeclaredElement GetDeclaredElement()
        {
            return myOwner.IsAvailable ? myOwner.ParameterDescriptor.DecraedElement : null;
        }

        public override TextRange GetTreeTextRange()
        {
            return myOwner.NameDocumentRange.TextRange;
        }

        public override DocumentRange GetDocumentRange()
        {
            return myOwner.NameDocumentRange;
        }
    }
}