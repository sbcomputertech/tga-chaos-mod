using HarmonyLib;

namespace ChaosMod;

[HarmonyPatch(typeof(LoadingScreen_TipManager), nameof(LoadingScreen_TipManager.ChooseTip))]
public static class LoadingScreenTipPatch
{
    public static void Postfix(ref LoadingScreen_TipManager __instance)
    {
        var t = $"{PluginInfo.PLUGIN_NAME} v{PluginInfo.PLUGIN_VERSION}\n";
        t += "If the mod doesn't activate properly by itself, press M to start it.\n";
        t += "(it will only auto-activate from the start of Lobby as of v1.0.0)\n";
        t += "Make sure to collect any remnant orbs you see in the levels!\n";
        __instance.text.eng = t;
    }
}