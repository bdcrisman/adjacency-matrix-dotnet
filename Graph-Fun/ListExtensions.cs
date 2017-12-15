using System.Collections.Generic;

namespace Graph_Fun
{
    public static class ListExtensions
    {
        public static int NextIndex<T>(this List<T> l, T currentItem)
        {
            var nextIndex = l.IndexOf(currentItem) + 1;
            return nextIndex < l.Count ? nextIndex : -1;
        }

        public static int PrevIndex<T>(this List<T> l, T currentItem)
        {
            return l.IndexOf(currentItem) - 1;
        }
    }
}
