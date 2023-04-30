using System;
using System.Collections.Generic;
using ChaosMod.Events.Global;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ChaosMod.Events;

public static class EventSystem
{
    [CanBeNull] private static GameObject _eventManager;

    internal static GameObject EventManager
    {
        get
        {
            if (_eventManager != null) return _eventManager;
            _eventManager = new GameObject("ChaosEventManager");
            Object.DontDestroyOnLoad(_eventManager);
            return _eventManager;
        }
    }

    public static MonoBehaviour MakeEvent(Type comp)
    {
        var mb = EventManager.AddComponent(comp);
        Plugin.Logging.LogInfo("Created event: " + comp.Name);
        return (MonoBehaviour)mb;
    }

    public static readonly Type[] Dummy = { typeof(DummyEvent) };
    public static readonly Type[] Globals = { typeof(NormalizeMovement), typeof(ShortMode), typeof(ConstantBoop), typeof(CoconutMalled) };
    public static readonly Dictionary<Level, Type[]> PerLevels = new()
    {
        { Level.NIGHTMARES, new []{ typeof(DummyEvent) } }
    };
}