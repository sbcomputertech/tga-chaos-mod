using HarmonyLib;
using UnityEngine;

namespace ChaosMod.Events.Global;

public class SlowPlayer : MonoBehaviour
{
    private static bool doSlow;
    
    public void E_Enable()
    {
        doSlow = true;
    }

    public void E_Disable()
    {
        doSlow = false;
    }
    
    public override string ToString() => "Tired Legs";

    [HarmonyPatch(typeof(Player_Move), nameof(Player_Move.Crawl))]
    public static class PreCrawlPatch
    {
        public static void Prefix(ref Player_Move __instance)
        {
            if (doSlow)
            {
                __instance.speed_actual = 0.1f;
            }
        }
    }
}