using JetBrains.ReSharper.Psi.Resolve;

namespace Arp.NH.Psi.Tree
{
    public enum QualifierType
    {
        Type,
        Table
    } ;

    public interface IQualifierOwner
    {
        IQualifier GetQualifier(QualifierType qualifierType);
    }
}