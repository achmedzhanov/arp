using System.Collections.Generic;

namespace Arp.Databases.Meta.Impl.Stub
{
    public class StubTable : ITable
    {
        public ICollection<IColumn> Columns { get; set; }
        public string ShortName { get; set; }
        public string Schema { get; set; }
        public string QualifiedName { get; set; }
    }
}