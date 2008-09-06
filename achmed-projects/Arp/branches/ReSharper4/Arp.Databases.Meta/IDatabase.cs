using System.Collections.Generic;

namespace Arp.Databases.Meta
{
    public interface IDatabase
    {
        ITable GetTable(string name);
        ITable GetView(string name);
        ICollection<ITable> GetTables();
        ICollection<IView> GetViews();

        bool CaseSensitive { get; }

    }
}