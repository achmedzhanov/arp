using System;
using System.Collections.Generic;
using System.Data;
using Arp.Databases.Meta.Impl;

namespace Arp.Databases.Meta.Providers.Oracle
{
    /// <summary>
    /// this code based on OracleNativeSchemaProvider
    /// </summary>
    public class Database : BaseDatabase
    {
        private IDbConnection connection;


        public Database(IDbConnection connection)
        {
            this.connection = connection;
        }

        protected override ICollection<ITable> QueryTables()
        {
            return InvokeStatement<ICollection<ITable>>(
                delegate(IDbCommand cmd)
                    {
                        const int POS_OWNER = 0;
                        const int POS_NAME = 1;

                        string sql = string.Format(@"
						SELECT		owner, object_name, created 
						FROM		all_objects
						WHERE		{0}
									AND object_type = 'TABLE'      
						ORDER BY	owner, 
									object_name",
                            GetSchemaOwnershipFilter());                        
                        
                        cmd.CommandText = sql;

                        using (IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            List<ITable> ret = new List<ITable>();
                            while (reader.Read())
                            {
                                BaseTable table = new BaseTable(reader.GetString(POS_NAME), reader.GetString(POS_OWNER), this);
                                ret.Add(table);
                            }

                            return ret;
                        }                        
                    });
        }


        protected override ICollection<IColumn> QueryColumns(BaseTable table)
        {
            return InvokeStatement<ICollection<IColumn>>(
    delegate(IDbCommand cmd)
    {
        const int POS_COLUMN_NAME = 0;
        const int POS_DATA_TYPE = 1;
        //const int POS_data_length	= 2;
        const int POS_DATA_PRECISION = 3;
        const int POS_DATA_SCALE = 4;
        const int POS_NULLABLE = 5;
        //				const int POS_COMMENTS		= 6;

        string sql = string.Format(
            @"select cols.column_name, 
							 cols.data_type, 
							 cols.data_length,
							 cols.data_precision, 
							 cols.data_scale,
							 cols.nullable,        
							 cmts.comments
					  from  all_tab_columns cols, 
							all_col_comments cmts 
					  where 
							cols.owner = '{0}'
						and cols.table_name = '{1}'
						and cols.owner = cmts.owner 
						and cols.table_name = cmts.table_name 
						and cols.column_name = cmts.column_name
						order by column_id",
            table.Owner,
            table.ShortName);

        cmd.CommandText = sql;


        using (IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
        {
            List<IColumn> ret= new List<IColumn>();
            while (reader.Read())
            {
                ColumnSchema cs = new ColumnSchema(
                    table,
                    reader.GetString(POS_COLUMN_NAME),
                    GetDbType(reader),
                    reader.GetString(POS_DATA_TYPE),
                    GetDBTypeLength(reader),
                    reader.IsDBNull(POS_DATA_PRECISION) ? (byte)0 : Convert.ToByte(reader[POS_DATA_PRECISION]),
                    reader.IsDBNull(POS_DATA_SCALE) ? 0 : Convert.ToInt32(reader[POS_DATA_SCALE]),
                    reader.GetString(POS_NULLABLE) == "Y",
                    properties);

                columnList.Add(cs);
            }

            return ret;
        }



    });
        }

        private delegate T InvokeStatementDelegate<T>(IDbCommand cmd);

        private T InvokeStatement<T>(InvokeStatementDelegate<T> invoke)
        {
            using (IDbConnection conn = CreateConnection())
            {
                conn.Open();

                using (IDbCommand cmd = connection.CreateCommand())
                {
                    cmd.Connection = conn;
                    return invoke(cmd);
                }
            }            
        }

        private string GetSchemaOwnershipFilter()
        {
            return "";

        }

        private IDbConnection CreateConnection()
        {
            throw new NotImplementedException();
        }

        private IDbCommand CreateCommand(IDbConnection connection)
        {
            return connection.CreateCommand();
        }

        protected override ICollection<IView> QueryViews()
        {
            throw new System.NotImplementedException();
        }
    }
}