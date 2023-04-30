using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace ChaosMod;

public static class Utils
{
    public static bool WeightedRandomBool(int c1, int c2)
    {
        var num = Random.Range(0f, c1 + c2);
        return num < c1;
    }

    public static T RandomFrom<T>(IEnumerable<T> src)
    {
        var enumerable = src.ToArray();
        var num = Random.Range(0, enumerable.Length);
        return enumerable.ElementAt(num);
    }
}