using System.Collections.Generic;

namespace Arp.Databases.Meta
{
    public interface ITable : ITableLike
    {
        string Name { get;}
        string Owner { get;}
    }
}