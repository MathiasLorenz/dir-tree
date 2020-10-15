using System;
using System.Collections.Generic;
using System.Linq;

namespace DirTree
{
    internal static class FolderItemSorter
    {
        public static IEnumerable<FolderItem> Sort(IEnumerable<FolderItem> items, TreeRunnerOrdering ordering)
        {
            return ordering switch
            {
                TreeRunnerOrdering.Alphabetically => items.OrderBy(x => x.Name),
                TreeRunnerOrdering.LastModification => items.OrderBy(x => x.LastModification),
                TreeRunnerOrdering.FileSize => items.OrderByDescending(x => x.FileSize)
                    .ThenBy(x => x.Name),
                _ => throw new Exception("Unknown ordering chosen.")
            };
        }
    }
}
