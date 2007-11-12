using System;
using System.Collections.Generic;
using JetBrains.ProjectModel;

namespace Arp.UnusedReferences
{
    public class UnusedReferencesSearchResult
    {
        readonly Dictionary<IProject, ICollection<IModule>> results = new Dictionary<IProject, ICollection<IModule>>();


        public ICollection<IProject> Projects
        {
            get
            {
                return results.Keys;
            }
        }

        public ICollection<IModule> this[IProject project]
        {
            get
            {
                return results[project];
            }
        }

        public void Add(IProject key, List<IModule> list)
        {
            results.Add(key, list);
        }
    }
}