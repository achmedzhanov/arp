using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Arp.Databases.Meta.Tests
{
    [TestFixture]
    public abstract class BaseDatabaseTest
    {
        protected abstract IDatabase GetDatabase();

        [Test]
        public void TablesTest()
        {
            // TODO get tables list compary with expected

            throw new NotImplementedException();
        }
        
        [Test]
        public void ColumnsTest()
        {
            // TODO get columns list compary with expected

            throw new NotImplementedException();
        }
    }
}
