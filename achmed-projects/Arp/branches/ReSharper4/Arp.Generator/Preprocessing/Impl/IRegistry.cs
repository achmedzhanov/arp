namespace Arp.Generator.Preprocessing.Impl
{
    public interface IRegistry<T>
    {
        T Get(string name);
    }
}