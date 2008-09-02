namespace Arp.Generator.Names
{
    public interface INameConverter
    {
        string ConvertElementName(string name);
        string ConvertTypeName(string name);
        string ConvertAttributeName(string name);
        string ConvertSimpleTypeName(string name);
        string ConvertSimpleTypeRestrictionName(string name);
        string ConvertEnumerationName(string name);
        string CreateComplexName(string [] names);
        string ConvertFacetName(string value);
    }
}