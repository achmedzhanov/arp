using System;
using Arp.Generator.Preprocessing.Impl;

namespace Arp.Generator.Preprocessing.Impl
{
    public class ElementGenerationInfoRefRegistry : IElementGenerationInfoRef
    {
        private readonly IRegistry registry;
        private readonly string id;


        public ElementGenerationInfoRefRegistry(IRegistry registry, string id)
        {
            if (registry == null) throw new ArgumentNullException("registry");
            if (id == null) throw new ArgumentNullException("id");
            this.registry = registry;
            this.id = id;
        }

        public ElementGenerationInfo Get()
        {
            return registry.GetElementInfo(id);
        }
    }
}