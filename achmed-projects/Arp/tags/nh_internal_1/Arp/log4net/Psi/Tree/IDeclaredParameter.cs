using JetBrains.ReSharper.Editor;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;

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