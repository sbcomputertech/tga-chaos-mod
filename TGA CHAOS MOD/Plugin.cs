using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using ChaosMod.Events;
using HarmonyLib;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChaosMod;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    internal static ManualLogSource Logging;
    private DiscordManager discord;
    private TextMeshProUGUI eventText;
    private float eventTimer;
    private MonoBehaviour currentEvent;
    private MonoBehaviour nextEvent;
    private bool doNewEvent;
    private bool modEnabled;
    private bool hudCreated;
    internal static float remnantAmount;
    internal static AssetBundle modAssets;

    private static string modAssetBundlePath =>
        Path.Join(Paths.PluginPath, "chaos-modassets");

    private void Awake()
    {
        var harmony = new Harmony(PluginInfo.PLUGIN_GUID);
        harmony.PatchAll();
        Logging = Logger;
        modEnabled = false;
        modAssets = AssetBundle.LoadFromFile(modAssetBundlePath);
    }

    private void Start()
    {
        SetupDiscordRpc();
        Logging.LogInfo($"Plugin {PluginInfo.PLUGIN_NAME} is loaded! Made by {PluginInfo.PLUGIN_DEV}");
    }

    private static MonoBehaviour GetNewEvent()
    {
        var level = Level.GetCurrentLevel();
        var levelEvents = EventSystem.PerLevels.GetValueOrDefault(level, EventSystem.Dummy);
        var global = Utils.WeightedRandomBool(EventSystem.Globals.Length, levelEvents.Length);
        var type = Utils.RandomFrom(global ? EventSystem.Globals : levelEvents);
        return EventSystem.MakeEvent(type);
    }

    private void Update()
    {
        GetGlobalObjectReferences();
        
        // enable
        if (Input.GetKeyDown(KeyCode.M) && !modEnabled)
        {
            modEnabled = true;
        }
        if(!modEnabled) return;
        
        // hud
        if(!hudCreated)
        {
            CreateHud();
        }
        else
        {
            eventText.text = "Current event: ";
            if(currentEvent == null) { eventText.text += "None"; }
            else { eventText.text += currentEvent; }
            
            var eventStr = eventTimer < 5 ? nextEvent.ToString() : "???";
            eventText.text += $"  |  Time until next event: {Math.Round(eventTimer, 1)}s - {eventStr}";

            eventText.text += $"  |  Remnant: {remnantAmount}";
        }

        // timer
        eventTimer -= Time.deltaTime;
        if (eventTimer < 0)
        {
            doNewEvent = true;
            eventTimer = 10;
        }
        
        // event debug key
        if (Input.GetKeyDown(KeyCode.N))
        {
            doNewEvent = true;
        }
        
        // new event
        if (doNewEvent)
        {
            currentEvent.EventSetActive(false);
            Destroy(currentEvent, 0.2f);
            currentEvent = nextEvent;
            currentEvent.EventSetActive(true);
            nextEvent = GetNewEvent();
            doNewEvent = false;
        }
    }

    private void GetGlobalObjectReferences()
    {
        GlobalObjectReferences.tipManager ??= FindObjectOfType<PanelTips_animations>().gameObject;
    }

    private void SetupDiscordRpc()
    {
        var obj = new GameObject("ChaosModDiscordRpcManager");
        DontDestroyOnLoad(obj);
        discord = obj.AddComponent<DiscordManager>();
    }

    private void CreateHud()
    {
        var obj = FindObjectOfType<Achievements_CanvasManager>();
        Logging.LogInfo("Using achievement manager's canvas for event hud!");

        var panelObj = new GameObject("Panel");
        panelObj.transform.SetParent(obj.transform, false);
        panelObj.layer = 5;
        var panelImage = panelObj.AddComponent<Image>();
        panelImage.color = Color.gray; 
        var panelRect = panelObj.GetRectTransform();
        panelRect.anchorMin = new Vector2(-1, -1);
        panelRect.anchorMax = new Vector2(1, -1);
        panelObj.transform.localPosition = new Vector3(0, 500, 0);

        var textObj = new GameObject("Text");
        textObj.transform.SetParent(panelRect, false);
        textObj.layer = 5;
        var tmp = textObj.AddComponent<TextMeshProUGUI>();
        tmp.SetText("Waiting for level...");
        tmp.color = Color.black;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.enableWordWrapping = false;
        tmp.autoSizeTextContainer = true;
        var textRect = textObj.GetRectTransform();
        textRect.anchorMin = textRect.anchorMax = new Vector2(-1, 0);
        textRect.localPosition = new Vector3(0, 0, 0);
        
        eventText = tmp;
        hudCreated = true;
        Logging.LogInfo("Created EventHud!");
    }
}