namespace Arp.Generator.Preprocessing.Impl
{
    public class RegistryInfoRef<T> : IInfoRef<T>
    {
        private readonly IRegistry<T> registry;
        private readonly string name;

        public RegistryInfoRef(IRegistry<T> registry, string name)
        {
            this.registry = registry;
            this.name = name;
        }

        #region IInfoRef<T> Members

        public T Get()
        {
            return registry.Get(name);
        }

        #endregion
    }
}