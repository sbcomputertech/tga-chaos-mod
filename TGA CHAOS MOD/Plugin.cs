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
    private TextMeshProUGUI eventText;
    private RectTransform uiPanelRect;
    private float eventTimer;
    private MonoBehaviour currentEvent;
    private MonoBehaviour nextEvent;
    private bool doNewEvent;

    private void Awake()
    {
        var harmony = new Harmony(PluginInfo.PLUGIN_GUID);
        harmony.PatchAll();
        Logging = Logger;
    }

    private void Start()
    {
        CreateHud();
        Logging.LogInfo($"Plugin {PluginInfo.PLUGIN_NAME} is loaded! Made by {PluginInfo.PLUGIN_DEV}");
    }

    private static MonoBehaviour GetNewEvent()
    {
        var level = Level.GetCurrentLevel();
        var levelEvents = EventSystem.PerLevels[level];
        var global = Utils.WeightedRandomBool(EventSystem.Globals.Length, levelEvents.Length);
        var type = Utils.RandomFrom(global ? EventSystem.Globals : levelEvents);
        return EventSystem.MakeEvent(type);
    }

    private void Update()
    {
        // ui size
        var scale = uiPanelRect.localScale;
        scale.x = eventText.textBounds.size.x;
        // uiPanelRect.localScale = scale; // this breaks the whole gui and crashes the game lmao
        
        // timer
        eventTimer -= Time.deltaTime;
        if (eventTimer < 0)
        {
            doNewEvent = true;
            eventTimer = 10;
        }
        
        // new event
        if (doNewEvent)
        {
            currentEvent.EventSetActive(false);
            currentEvent = nextEvent;
            currentEvent.EventSetActive(true);
            nextEvent = GetNewEvent();
        }
        
        // text
        var eventStr = eventTimer < 5 ? nextEvent.ToString() : "???";
        eventText.text = $"Time until next event: {eventTimer} - {eventStr}";
    }

    private void CreateHud()
    {
        var obj = new GameObject("ChaosModEventHud");
        DontDestroyOnLoad(obj);
        
        var canvas = obj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = Camera.main;

        var panelSize = new Vector2(600, 200);

        var panelObj = new GameObject("Panel");
        panelObj.transform.SetParent(obj.transform, false);
        var panelImage = panelObj.AddComponent<Image>();
        panelImage.color = Color.gray; 
        var panelRect = panelObj.GetRectTransform();
        panelRect.anchorMin = new Vector2(0, 1);
        panelRect.anchorMax = new Vector2(0, 1);
        panelRect.sizeDelta = panelSize;

        var textObj = new GameObject("Text");
        textObj.transform.SetParent(panelRect, false);
        var tmp = textObj.AddComponent<TextMeshProUGUI>();
        tmp.SetText("Waiting for level...");
        tmp.color = Color.black;
        tmp.alignment = TextAlignmentOptions.Left;
        tmp.enableWordWrapping = false;
        var textRect = textObj.GetRectTransform();
        textRect.anchorMin = textRect.anchorMax = new Vector2(1, 1);
        textRect.localPosition = new Vector3(100, -50, 0);
        
        eventText = tmp;
        uiPanelRect = panelRect;

        Logging.LogInfo("Created EventHud!");
    }
}