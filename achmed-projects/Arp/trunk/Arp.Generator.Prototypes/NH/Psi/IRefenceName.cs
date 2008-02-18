using JetBrains.ReSharper.Psi.ExtensionsAPI.Resolve;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Tree;

namespace Arp.Generator.Prototypes.NH.Psi
{
    public interface IRefenceName : IElement, ICompleteableReference, IQualifier, IQualifiableReference, IAccessContext, ICheckedReference
    {
        
    }
}