namespace Arp.Generator.Names
{
    public interface INameProvider
    {
        string GetElelentTypeNameForUsage(string xmlElementName);

        string GetTypeNameForGeneration(string xmlElementName);
        
    }
}