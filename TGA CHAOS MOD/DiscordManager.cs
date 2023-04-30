using DiscordRPC;
using DiscordRPC.Logging;
using UnityEngine;

namespace ChaosMod;

public class DiscordManager : MonoBehaviour
{
    private DiscordRpcClient client;
    private const string DiscordId = "1102253904764354641";

    private void Start()
    {
        client = new DiscordRpcClient(DiscordId);
        client.Logger = new DiscordBepinexLogger(LogLevel.Info);
        client.Initialize();
    }

    private void Update()
    {
        var level = Level.GetCurrentLevel();
        client.SetPresence(new RichPresence
        {
            Details = $"{PluginInfo.PLUGIN_NAME} v{PluginInfo.PLUGIN_VERSION}",
            State = "Playing " + level.name,
            Assets = new Assets
            {
                LargeImageKey = "level" + level.spawnVar,
                LargeImageText = level.name,
                SmallImageKey = "remnant",
                SmallImageText = Plugin.remnantAmount + " remnant"
            }
        });
    }

    private void OnDestroy()
    {
        client.Dispose();
    }
}