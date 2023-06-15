using HarmonyLib;

namespace ChaosMod;

[HarmonyPatch(typeof(MainMenu_VersionTextManager), nameof(MainMenu_VersionTextManager.Start))]
public static class VersionTextPatch
{
    public static void Postfix(ref MainMenu_VersionTextManager __instance)
    {
        __instance.txt.text += $"\n{PluginInfo.PLUGIN_NAME} v{PluginInfo.PLUGIN_VERSION} by {PluginInfo.PLUGIN_DEV}";
    }
}