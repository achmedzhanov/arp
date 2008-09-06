using System;
using System.Collections.Generic;
using Arp.Databases.Meta;
using Arp.Databases.Meta.Impl.Stub;
using JetBrains.ProjectModel;

namespace Arp.Databases
{
    public class DatabaseManager
    {

        #region Singletone implementation

        private static DatabaseManager _instance;

        public static DatabaseManager Instance()
        {
            if (_instance == null)
            {
                lock (typeof (DatabaseManager))
                {
                    if (_instance == null)
                    {
                        _instance = new DatabaseManager();
                    }
                }
            }

            return _instance;
        }

        #endregion


        public IDatabase GetDatabaseForSolution(ISolution solution)
        {
            StubDatabase database = new StubDatabase();
//            database.Tables = new List<ITable> { new StubTable { ShortName = "roles", QualifiedName = "dbo.roles" }, new StubTable { ShortName = "users", QualifiedName = "dbo.users" } };
            return database;
        }
    }
}