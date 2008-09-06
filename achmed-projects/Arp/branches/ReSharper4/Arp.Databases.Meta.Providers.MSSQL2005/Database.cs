using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.Caching;
using Arp.Databases.Meta.Impl.Stub;
using JetBrains.Util;

namespace Arp.Databases.Meta.Providers.MSSQL2005
{
    public class Database : IDatabase
    {
        private const string TABLES_KEY = "TABLES";
        
        private readonly Cache cache = new Cache();
        
        private readonly string connectionString;
        private const int TABLES_LIST_EXPIRATION = 60;

        public Database(string connectionString)
        {
            this.connectionString = connectionString;
        }


        public ITable GetTable(string name)
        {
            throw new System.NotImplementedException();
        }

        protected ICollection<ITable> LoadTables()
        {
            using(var db = new DataContext(connectionString))
            {
                var tablesTable = db.GetTable<Tables>();
                var tables = from c in tablesTable select c;
                var ret = new List<ITable>();
                foreach (var table in tables)
                {
                    ret.Add(new StubTable
                                {
                                    ShortName = table.TableName,
                                    QualifiedName = table.TableName,
                                    Schema = table.TableSchema
                                });
                }

                return ret;
            }
        }

        public ITable GetView(string name)
        {
            throw new System.NotImplementedException();
        }

        public ICollection<ITable> GetTables()
        {
            // TODO user reader writer lock ?
            var tables = (ICollection<ITable>)cache.Get(TABLES_KEY);
            if(tables == null)
            {
                tables = LoadTables();
                cache.Add(TABLES_KEY, tables, null, DateTime.Now.AddSeconds(TABLES_LIST_EXPIRATION),
                          Cache.NoSlidingExpiration, CacheItemPriority.High, null);
            }

            return tables;
        }

        public ICollection<IView> GetViews()
        {
            return EmptyArray<IView>.Instance;
        }

        public bool CaseSensitive
        {
            get
            {
                return false ;
            }
        }
    }
}
