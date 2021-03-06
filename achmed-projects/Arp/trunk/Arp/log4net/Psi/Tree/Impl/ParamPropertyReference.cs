using System.Collections.Generic;
using JetBrains.ReSharper.Editor;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.Util;

namespace Arp.log4net.Psi.Tree.Impl
{
    public class ParamPropertyReference : BaseDeclaredElementReference<PropertyParamImpl>
    {
        public ParamPropertyReference(PropertyParamImpl owner) : base(owner)
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


        public override IReference BindTo(IDeclaredElement element)
        {
            throw new System.NotImplementedException();
        }

        public override IReference BindTo(IDeclaredElement element, ISubstitution substitution)
        {
            throw new System.NotImplementedException();
        }
    }
}