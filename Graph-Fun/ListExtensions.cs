using System.Collections.Generic;

namespace Graph_Fun
{
    public static class ListExtensions
    {
        /// <summary>
        /// Returns the index of the next item.
        /// </summary>
        /// <typeparam name="T">Type.</typeparam>
        /// <param name="l">Instance of list.</param>
        /// <param name="currentItem">Current item.</param>
        /// <returns>Integer corresponding to the next index.</returns>
        public static int NextIndex<T>(this List<T> l, T currentItem)
        {
            var nextIndex = l.IndexOf(currentItem) + 1;
            return nextIndex < l.Count ? nextIndex : -1;
        }

        /// <summary>
        /// Returns the index of the previous item.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="l">Instance of list.</param>
        /// <param name="currentItem">Current item.</param>
        /// <returns>Integer corresponding to the previous index.</returns>
        public static int PrevIndex<T>(this List<T> l, T currentItem)
        {
            return l.IndexOf(currentItem) - 1;
        }
    }
}
