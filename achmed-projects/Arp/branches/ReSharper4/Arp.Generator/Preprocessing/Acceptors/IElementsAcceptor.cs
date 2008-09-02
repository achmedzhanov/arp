using Arp.Generator.Preprocessing;
using Arp.Generator.Preprocessing.Impl;

namespace Arp.Generator.Acceptors
{
    public interface IElementsAcceptor
    {
        void Accept(IElementInfo elementGenerationInfo);
    }
}