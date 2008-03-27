using System;
using System.Collections.Generic;
using JetBrains.ProjectModel;

namespace Arp.UnusedReferences
{
    public class UnusedReferencesSearchResult
    {
        readonly Dictionary<IProject, ICollection<IModuleReference>> results = new Dictionary<IProject, ICollection<IModuleReference>>();

        public ICollection<IProject> Projects
        {
            get
            {
                return results.Keys;
            }
        }

        public ICollection<IModuleReference> this[IProject project]
        {
            get
            {
                return results[project];
            }
        }

        public void Add(IProject key, List<IModuleReference> list)
        {
            results.Add(key, list);
        }
    }
}