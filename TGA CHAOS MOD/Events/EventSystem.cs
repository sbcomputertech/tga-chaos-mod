using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

using ChaosMod.Events.Global;
using ChaosMod.Events.Nightmares;

namespace ChaosMod.Events;

public static class EventSystem
{
    [CanBeNull] private static GameObject _eventManager;

    private static GameObject EventManager
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
    
    public static readonly Type[] Globals = { 
        typeof(NormalizeMovement), typeof(ShortMode), typeof(ConstantBoop), typeof(MrHippo),
        typeof(CoconutMalled), typeof(SlowPlayer), typeof(HelpfulTip), typeof(NoInteraction)
    };
    
    public static readonly Dictionary<Level, Type[]> PerLevels = new()
    {
        { Level.NIGHTMARES, new []{ typeof(NoTorch) } }
    };
}