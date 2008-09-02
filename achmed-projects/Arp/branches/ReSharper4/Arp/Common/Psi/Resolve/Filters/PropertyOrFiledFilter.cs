using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Resolve;

namespace Arp.Common.Psi.Resolve.Filters
{
    public class PropertyOrFiledFilter : SimpleSymbolFilter
    {
        public static readonly PropertyOrFiledFilter INSTANCE = new PropertyOrFiledFilter();
        
        private PropertyOrFiledFilter()
        {
        }

        public override bool Accepts(IDeclaredElement declaredElement, ISubstitution substitution)
        {
            return ((declaredElement is IProperty) || (declaredElement is IField));
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