using System.Collections.Generic;
using Arp.Assertions;
using JetBrains.ReSharper.Editor;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Resolve;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.Util;

namespace Arp.log4net.Psi.Tree.Impl
{
    //TODO use CheckedReferenceBase or TreeReferenceBase?
    public class AppenderReference : BaseDeclaredElementReference<AppenderRefImpl>
    {
        public AppenderReference(AppenderRefImpl owner)
            : base(owner)
        {
        }

        public override string GetName()
        {
            return myOwner.Ref;
        }

        protected override ICollection<IDeclaredElement> GetDeclaredElements()
        {
            ICollection<IAppender> appenders = myOwner.Parent.Parent.GetAppenders();

            IList<IAppender> found = CollectionUtil.FindAll(appenders, delegate(IAppender obj) { return (obj.Name == myOwner.Ref); });

            ICollection<IDeclaredElement> converted = CollectionUtil.Convert<IDeclaredElement, IAppender>(found, delegate(IAppender input)
                                                                                       {
                                                                                           return (IDeclaredElement)input;
                                                                                       });

            return converted;
        }


        public override TextRange GetTreeTextRange()
        {
            return myOwner.RefValue.ToTreeNode().GetTreeTextRange();
        }

        public override DocumentRange GetDocumentRange()
        {
            return myOwner.RefValue.GetDocumentRange();
        }


        public override IReference BindTo(IDeclaredElement element)
        {
            myOwner.SetRef(element.ShortName);
            // TODO check ref type
            IReference reference = myOwner.GetReferences()[0];
            Assert.Check(reference.GetName() == element.ShortName, "reference.GetElement() == element");
            return reference;
        }

        public override IReference BindTo(IDeclaredElement element, ISubstitution substitution)
        {
            return BindTo(element);
        }


        public override IAccessContext GetAccessContext()
        {
            return null;
            //return new ElementAccessContext(this.myOwner);
        }
    }
}
