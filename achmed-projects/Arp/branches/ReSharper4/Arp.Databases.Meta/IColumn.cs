using System.Data;

namespace Arp.Databases.Meta
{
    public interface IColumn
    {
        string Name {get;}
        DbType DbType { get;}
        uint Length { get;}
        uint Precission { get;}
        uint Scale { get;}
        bool Nullable { get;}
        bool IsPrimaryKey { get; }
        bool IsForeignKey { get; }
        string ForeignKeyReferenceTable { get; }
    }
}