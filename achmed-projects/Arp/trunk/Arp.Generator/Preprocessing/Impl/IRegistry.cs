using Arp.Generator.Acceptors;
using Arp.Generator.Preprocessing.Impl;

namespace Arp.Generator.Preprocessing.Impl
{
    public interface IRegistry
    {
        EnumGenerationInfo GetEnumInfo(string id);
        ElementGenerationInfo GetElementInfo(string id);
        TypeGenerationInfo GetTypeGenerationInfo(string xmlName);
    }
}