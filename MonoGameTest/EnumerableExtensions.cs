using System.Collections.Generic;
using System.Linq;

namespace MonoGameTest
{
    public static class EnumerableExtensions
    {
        //This implementation is from http://codereview.stackexchange.com/a/120821/7187.
        public static IEnumerable<T> Circle<T>(this IEnumerable<T> list, int startIndex)
        {
            var localList = list.ToList();
            return localList.GetRange(startIndex, localList.Count - startIndex)
                            .Concat(localList.GetRange(0, startIndex));
        }
    }
}