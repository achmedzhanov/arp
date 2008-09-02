using System.Collections.Generic;

namespace Arp.Generator.Preprocessing
{
    public interface ITypeInfo
    {
        string BaseName
        { 
            get;
        }

        ICollection<IAttributeInfo> AttributesInfo
        {
            get;
        }

        ICollection<INestedElementInfo> NestedElementsInfo
        {
            get;
        }

    }
}