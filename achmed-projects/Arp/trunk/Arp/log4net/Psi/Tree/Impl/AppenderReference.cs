using System.Collections.Generic;
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
    }
}