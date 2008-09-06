using System.Collections.Generic;
using Arp.Databases.Meta.Providers.MSSQL2005;
using JetBrains.Util;
using NUnit.Framework;

namespace Arp.Databases.Meta.Tests
{
    [TestFixture]
    public class MSSQLTests
    {
        [Test]
        public void TablesList()
        {
            var database = new Database("User ID=report_test_user; Password=report_test_user; Server=localhost;initial catalog=ReportServer;timeout=1000");
            var tables = database.GetTables();
                    Assert.AreEqual(27,tables.Count);
            Assert.IsTrue(CollectionUtil.FindAll(tables, table => table.ShortName == "Event").Count > 0);
        }
    }
}