using System;
using System.Collections.Generic;
using Arp.Databases.Meta;
using Arp.Databases.Meta.Impl.Stub;
using Arp.Databases.Meta.Providers.MSSQL2005;
using JetBrains.ComponentModel;
using JetBrains.ProjectModel;
using JetBrains.Util;

namespace Arp.Databases
{
    [SolutionComponentImplementation(ProgramConfigurations.ALL), SolutionComponentInterface(ProgramConfigurations.ALL)]
    public class DatabaseManager : ISolutionComponent
    {
        private Database database;

        public static DatabaseManager GetInstance(ISolution solution)
        {
            return solution.GetComponent<DatabaseManager>();
        }

        private string connectionString = string.Empty;

        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }

        public bool Enabled
        {
            get; set;
        }

        public IDatabase GetDatabaseForSolution()
        {
            if (!Enabled)
                return null;

            // TODO implement ITypeLoadHandler to collect metadata providers
            
            if(database == null)
            {
                database = new Database(ConnectionString);
            }

            return database;
        }

        public void Dispose()
        {
            
        }

        public void Init()
        {
            
        }

        public void AfterSolutionOpened()
        {
            
        }

        public void BeforeSolutionClosed()
        {
            
        }

        public void Refresh()
        {
            database = null;
        }
    }
}