using System.Collections.Generic;

namespace Arp.Databases.Meta.Providers.MSSQL2005
{
    public class Table : ITable, IView
    {
        private Database database;

        public Table(Database database)
        {
            this.database = database;
        }

        public ICollection<IColumn> Columns 
        { 
            get
            {
                return database.GetColmns(this);
            }

        }
        public string ShortName { get; set; }
        public string Schema { get; set; }
        public string QualifiedName { get; set; }
    }
}