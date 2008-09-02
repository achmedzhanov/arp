using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Resolve;

namespace Arp.Common.Psi.Resolve.Filters
{
    public class PublicOrProtectedFilter : SimpleSymbolFilter
    {
        public static readonly PublicOrProtectedFilter INSTANCE = new PublicOrProtectedFilter();

        private PublicOrProtectedFilter()
        {
        }

        public override bool Accepts(IDeclaredElement declaredElement, ISubstitution substitution)
        {
            IModifiersOwner modifiersOwner = declaredElement as IModifiersOwner;
            if (modifiersOwner == null)
                return false;
            
            return modifiersOwner.GetAccessRights() == AccessRights.PUBLIC || modifiersOwner.GetAccessRights() == AccessRights.PROTECTED;
        }


        public override bool MustRun
        {
            get { return true; }
        }

        public override ResolveErrorType ErrorType
        {
            get
            {
                // TODO resolve erro type extected property or field
                return ResolveErrorType.NOT_RESOLVED;
            }
        }

    }
}