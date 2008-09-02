using System.Collections.Generic;

namespace Arp.Databases.Meta
{
    public interface IDatabase
    {
        ICollection<ITable> Tables {get;}
        ICollection<IView> Views {get;}
    }
}