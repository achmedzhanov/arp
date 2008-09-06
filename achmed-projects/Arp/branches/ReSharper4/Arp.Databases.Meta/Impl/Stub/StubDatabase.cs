using System.Collections.Generic;

namespace Arp.Databases.Meta.Impl.Stub
{
    public class StubDatabase : IDatabase
    {

        public ICollection<ITable> Tables { get; set; }
        public ICollection<IView> Views { get; set; }
        
        public ITable GetTable(string name)
        {
            throw new System.NotImplementedException();
        }

        public ITable GetView(string name)
        {
            throw new System.NotImplementedException();
        }

        public ICollection<ITable> GetTables()
        {
            return Tables;
        }

        public ICollection<IView> GetViews()
        {
            return Views;
        }

        public bool CaseSensitive {get;set;}
    }
}