using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using Arp.Databases.Meta.Impl.Stub;
using JetBrains.Util;

namespace Arp.Databases.Meta.Providers.MSSQL2005
{
    public class Database : IDatabase
    {
        private const string TABLES_KEY = "Arp.Databases.Meta.Providers.MSSQL2005.TABLES";
        private const string VIEWS_KEY = "Arp.Databases.Meta.Providers.MSSQL2005.VIEWS";
        
        private readonly Cache cache = HttpRuntime.Cache;
        
        private readonly string connectionString;
        private const int TABLES_LIST_EXPIRATION = 60;


        public Database(string connectionString)
        {
            new HttpRuntime();
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
                    ret.Add(new Table(this)
                                {
                                    ShortName = table.TableName,
                                    QualifiedName = table.TableName,
                                    Schema = table.TableSchema
                                });
                }

                return ret;
            }
        }

        protected ICollection<IView> LoadViews()
        {
            using (var db = new DataContext(connectionString))
            {
                var tablesTable = db.GetTable<Views>();
                var tables = from c in tablesTable select c;
                var ret = new List<IView>();
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
            return GetCachebleResult<ICollection<ITable>>(LoadTables, TABLES_KEY, TABLES_LIST_EXPIRATION);
        }

        protected T GetCachebleResult<T>(Func<T> loader, string cacheKey, int expiration )
        {
            var result = (T)cache.Get(cacheKey);

            if (result == null)
            {
                result = loader();
                cache.Add(cacheKey, result, null, DateTime.Now.AddSeconds(expiration),
                          Cache.NoSlidingExpiration, CacheItemPriority.High, null);
            }

            return result;            
        }

        public ICollection<IView> GetViews()
        {
            return GetCachebleResult<ICollection<IView>>(LoadViews, VIEWS_KEY, TABLES_LIST_EXPIRATION);
        }

        public bool CaseSensitive
        {
            get
            {
                return false ;
            }
        }

        public ICollection<IColumn> GetColmns(Table table)
        {
            return new IColumn[] { new Column { DbType = DbType.Int32, ForeignKeyReferenceTable = null, IsForeignKey = false, IsPrimaryKey = true, Length = 0, Name = "id", Nullable = false, Precission = 0, Scale = 0 } };
        }
    }
}
