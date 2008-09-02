using JetBrains.DocumentModel;

namespace Arp.log4net.Psi.Tree
{
    public interface IDeclaredParameter /*: IElement, IDeclaredParametersOwner, IL4NElement, IParameterDescriptorProvider*/ : IL4NElement
    {
        string Name { get;}

        DocumentRange NameDocumentRange { get;}

        string StringValue { get;}

        IParameterDescriptorProvider ParameterDescriptorProvider {get;}

    }
}