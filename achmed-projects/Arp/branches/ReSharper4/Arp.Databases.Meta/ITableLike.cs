using System.Collections.Generic;

namespace Arp.Databases.Meta
{
    public interface ITableLike
    {
        ICollection<IColumn> Columns { get;}
        string ShortName { get; }
        string Schema { get; }
        string QualifiedName { get; }
    }
}