using System;
using System.Collections.Generic;
using System.Linq;
namespace WebPerformancer
{
    public static class ListAssembler
    {
        public static List<string> UniqueItemsInOriginal(List<string> original, List<string> second)
        {
            var cloneOriginal = new List<string>(original);

            var intersection = original.Intersect(second);
            cloneOriginal.RemoveAll(i => intersection.Contains(i));
            return cloneOriginal;
        }

        public static List<string> Merge(List<string> first, List<string> second)
        {
            var clone = new List<string>(first);
            clone.AddRange(second);
            clone = clone.Distinct().ToList();
            return clone;
        }
    }
}