using System;
using System.Collections.Generic;

namespace Arp.Databases.Meta.Impl
{
    public class BaseTable : ITable
    {
        private readonly string shortName;
        private readonly string owner;
        private readonly BaseDatabase parent;

        public BaseTable(string name, string owner, BaseDatabase parent)
        {
            this.shortName = name;
            this.owner = owner;
            this.parent = parent;
        }

        #region ITable Members

        public string ShortName
        {
            get { return shortName; }
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