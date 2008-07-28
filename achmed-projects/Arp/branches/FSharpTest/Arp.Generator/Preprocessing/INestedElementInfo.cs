using System.Collections.Generic;

namespace Arp.Generator.Preprocessing
{
    public interface INestedElementInfo
    {
        bool IsCollection { get;}
        IElementInfo Element { get;}
        ICollection<IElementInfo> Elements { get;}
    }
}