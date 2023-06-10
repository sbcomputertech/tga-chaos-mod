using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;
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

    public static Sprite SpriteFromModAssets(string name)
    {
        var texture = Plugin.modAssets.LoadAsset<Texture2D>(name);
        var rect = new Rect(0, 0, texture.width, texture.height);
        var centre = rect.size / 2;
        var sprite = Sprite.Create(texture, rect, centre);
        return sprite;
    }

    [CanBeNull]
    public static GameObject FindGameObjectOrNull<T>() where T : Component
    {
        return Object.FindObjectOfType<T>().gameObject ? Object.FindObjectOfType<T>().gameObject : null;
    }
}