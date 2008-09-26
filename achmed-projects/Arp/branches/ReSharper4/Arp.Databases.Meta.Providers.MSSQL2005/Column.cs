using System.Data;

namespace Arp.Databases.Meta.Providers.MSSQL2005
{
    public class Column : IColumn
    {
        public string Name { get; set; }
        public DbType DbType { get; set; }
        public uint Length { get;  set; }
        public uint Precission { get; set; }
        public uint Scale { get;  set; }
        public bool Nullable { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsForeignKey { get; set; }
        public string ForeignKeyReferenceTable { get; set; }
    }
}