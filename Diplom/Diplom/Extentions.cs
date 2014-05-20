using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Diplom
{
    public static class Extentions
    {
        public static Vector3 Average<TSource>(this IEnumerable<TSource> source, Func<TSource, Vector3> selector)
        {
            Vector3 result = new Vector3();
            foreach (var item in source)
            {
                result += selector(item) / source.Count();
            }
            return result;
        }

        public static int IndexOf(this Vector3[] source, Vector3 selector)
        {
            for (int i = 0; i < source.Length; i++)
            {
                if (selector == source[i])
                    return i;
            }
            return -1;
        }
    }
}
