namespace Arp.Generator.Generation
{
    public interface IRegistry
    {
        EnumGenerationInfo GetEnumInfo(string id);
        ElementGenerationInfo GetElementInfo(string id);
        TypeGenerationInfo GetTypeGenerationInfo(string xmlName);
    }
}