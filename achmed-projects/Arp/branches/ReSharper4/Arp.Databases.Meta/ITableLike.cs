using System.Collections.Generic;

namespace Arp.Databases.Meta
{
    public interface ITableLike
    {
        ICollection<IColumn> Columns { get;}        
    }
}