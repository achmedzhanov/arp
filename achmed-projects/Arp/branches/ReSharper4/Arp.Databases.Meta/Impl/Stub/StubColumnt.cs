using System.Data;

namespace Arp.Databases.Meta.Impl.Stub
{
    public class StubColumnt : IColumn
    {

        public string Name { get; private set; }
        public DbType DbType { get; private set; }
        public uint Length { get; private set; }
        public uint Precission { get; private set; }
        public uint Scale { get; private set; }
        public bool Nullable { get; private set; }
        public bool IsPrimaryKey { get; private set; }
        public bool IsForeignKey { get; private set; }
        public string ForeignKeyReferenceTable { get; private set; }
    }
}