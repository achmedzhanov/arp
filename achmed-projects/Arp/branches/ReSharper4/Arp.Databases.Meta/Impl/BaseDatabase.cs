using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;

namespace Arp.Databases.Meta.Impl
{
    public abstract class BaseDatabase : IDatabase
    {
        private ICollection<ITable> tables;
        private ICollection<IView> views;
        private readonly Dictionary<BaseTable, ICollection<IColumn>> tableColumns = new Dictionary<BaseTable, ICollection<IColumn>>();

        #region IDatabase Members

        public virtual ICollection<ITable> Tables
        {
            get
            {
                return InvokeDBMethodAspect<ITable>(delegate
                                        {
                                            if (tables == null)

                                                tables = QueryTables();
                                            return tables;
                                        });
            }
        }

        public virtual ICollection<IView> Views
        {
            get
            {

                    return InvokeDBMethodAspect<IView>(delegate
                                                            {
                                                                                if (views == null)
                
                                                                views =  QueryViews();
                                                                                    return views;                
                                                            });
            }
        }

        #endregion

        public virtual ICollection<IColumn> GetColumns(BaseTable table)
        {
            return InvokeDBMethodAspect<IColumn>(delegate
                                    {
                                        if (!tableColumns.ContainsKey(table))
                                        {
                                            tableColumns[table] = QueryColumns(table);
                                        }

                                        return tableColumns[table];
                                    });            
        }

        protected abstract ICollection<IColumn> QueryColumns(BaseTable table);

        protected abstract ICollection<IView> QueryViews();

        protected abstract ICollection<ITable> QueryTables();

        protected delegate ICollection<T> InvokeDBMethodDelegate<T>();

        protected ICollection<T> InvokeDBMethodAspect<T>(InvokeDBMethodDelegate<T> invoke)
        {
            try
            {
                return invoke();
            }
            catch(DbException ex)
            {
                // TODO empty array
                return new List<T>();
            }
        }

    }
}