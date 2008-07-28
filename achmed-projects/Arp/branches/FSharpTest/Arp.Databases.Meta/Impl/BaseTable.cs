using System;
using System.Collections.Generic;

namespace Arp.Databases.Meta.Impl
{
    public class BaseTable : ITable
    {
        private readonly string name;
        private readonly string owner;
        private readonly BaseDatabase parent;

        public BaseTable(string name, string owner, BaseDatabase parent)
        {
            this.name = name;
            this.owner = owner;
            this.parent = parent;
        }

        #region ITable Members

        public string Name
        {
            get { return name; }
        }


        public string Owner
        {
            get { return owner; }
        }

        #endregion

        #region ITableLike Members

        public ICollection<IColumn> Columns
        {
            get { return parent.GetColumns(this); }
        }

        #endregion
    }
}