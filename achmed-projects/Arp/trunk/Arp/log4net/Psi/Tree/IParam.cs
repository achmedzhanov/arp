using JetBrains.ReSharper.Editor;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;

namespace Arp.log4net.Psi.Tree
{
    public interface IParam : IElement, IElementParametersOwner, IL4NElement
    {
        string Name { get;}

        DocumentRange NameDocumentRange { get;}

        string StringValue { get;}

        IElementParametersOwner Owner { get;}

        IElementParameterInfoProvider ElementParameterInfoProvider {get;}

    }
}