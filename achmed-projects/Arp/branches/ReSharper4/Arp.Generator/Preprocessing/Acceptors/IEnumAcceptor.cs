using Arp.Generator.Preprocessing.Impl;

namespace Arp.Generator.Acceptors
{
    public interface IEnumAcceptor
    {
        void Accept(EnumGenerationInfo enumGenerationInfo);
    }
}