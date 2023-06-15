using System;
using System.Collections.Generic;
using System.IO;
using BepInEx;
using BepInEx.Logging;
using ChaosMod.Events;
using HarmonyLib;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace ChaosMod;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    internal static ManualLogSource Logging;
    private TextMeshProUGUI eventText;
    private float eventTimer;
    private MonoBehaviour currentEvent;
    private MonoBehaviour nextEvent;
    private bool doNewEvent;
    private bool modEnabled;
    private bool hudCreated;
    internal static float remnantAmount;
    internal static AssetBundle modAssets;
    public static Transform ui;
    public const bool DEBUG = true;
    private float pingTimer = 180; // 3 minutes to first ping
    private static bool isModPaused;

    private static string modAssetBundlePath =>
        Path.Join(Paths.PluginPath, "chaos-modassets");

    private void Awake()
    {
        var harmony = new Harmony(PluginInfo.PLUGIN_GUID);
        harmony.PatchAll();
        
        Logging = Logger;
        modEnabled = false;
        modAssets = AssetBundle.LoadFromFile(modAssetBundlePath);
        
        LobbyStartDetector.OnStart += OnLobbyStart;
    }

    private void Start()
    {
        SetupDiscordRpc();
        Logging.LogInfo($"{PluginInfo.PLUGIN_NAME} has initialized! Developed by {PluginInfo.PLUGIN_DEV}");
    }

    private static MonoBehaviour GetNewEvent()
    {
        if (isModPaused)
        {
            return EventSystem.MakeEvent(typeof(DummyEvent));
        }
        
        var level = Level.GetCurrentLevel();
        var levelEvents = EventSystem.PerLevels.GetValueOrDefault(level, Array.Empty<Type>());
        var global = Utils.WeightedRandomBool(EventSystem.Globals.Length, levelEvents.Length);
        var type = Utils.RandomFrom(global ? EventSystem.Globals : levelEvents);
        return EventSystem.MakeEvent(type);
    }

    private void OnLobbyStart()
    {
        modEnabled = true;
    }

    private void Update()
    {
        // enable
        if (Input.GetKeyDown(KeyCode.M) && !modEnabled)
        {
            modEnabled = true;
        }
        if(!modEnabled) return;
        
        // check if should pause
        if (Level.IsInMenus)
        {
            if (!isModPaused)
            {
                isModPaused = true;
            }
        }
        else
        {
            if (isModPaused)
            {
                isModPaused = false;
            }
        }
        
        // hud
        if(!hudCreated)
        {
            CreateHud();
        }
        else
        {
            if (isModPaused)
            {
                eventText.text = $"Mod paused | You have {remnantAmount} remnant";
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
            currentEvent = nextEvent;
            currentEvent.EventSetActive(true);
            nextEvent = GetNewEvent();
            doNewEvent = false;
        }
        
        //  ping
        if (Input.GetKeyDown(KeyCode.P)) pingTimer = 1; // for debugging lol
        if (pingTimer <= 0)
        {
            pingTimer = Random.Range(200, 400); // average of 300 seconds / 5 minutes
            Ping();
        }
        pingTimer -= Time.deltaTime;
    }

    private static void Ping()
    {
        var cam = Camera.main!.transform;
        var clip = modAssets.LoadAsset<AudioClip>("ping");
        AudioSource.PlayClipAtPoint(clip, cam.position);
        Logging.LogInfo("Ping!");
    }

    private static void SetupDiscordRpc()
    {
        var obj = new GameObject("ChaosModDiscordRpcManager");
        DontDestroyOnLoad(obj);
        obj.AddComponent<DiscordManager>();
    }

    private void CreateHud()
    {
        var obj = FindObjectOfType<Achievements_CanvasManager>();
        ui = obj.transform;
        Logging.LogInfo("Using achievement manager's canvas for event hud!");

        var panelObj = new GameObject("ChaosModHudPanel");
        panelObj.transform.SetParent(ui, false);
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