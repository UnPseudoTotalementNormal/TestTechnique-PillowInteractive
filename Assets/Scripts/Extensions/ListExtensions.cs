using System;
using System.Collections.Generic;

namespace Extensions
{
    public static class ListExtensions
    {
            /// <summary>
            /// Picks a random element from the list using UnityEngine.Random
            /// </summary>
            public static T PickRandom<T>(this IList<T> _list)
            {
                if (_list == null || _list.Count == 0)
                    throw new InvalidOperationException("List is null or empty.");
                return _list[UnityEngine.Random.Range(0, _list.Count)];
            }
    }
}