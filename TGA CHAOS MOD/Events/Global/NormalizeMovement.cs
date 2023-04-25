using HarmonyLib;
using UnityEngine;

namespace ChaosMod.Events.Global;

public class NormalizeMovement : MonoBehaviour
{
    public void E_Disable()
    {
        PatchPlayerMove.doPatch = false;
    }

    public void E_Enable()
    {
        PatchPlayerMove.doPatch = true;
    }

    public override string ToString() => "No strafing for you";

    [HarmonyPatch(typeof(Player_Move), "Walk")]
    public static class PatchPlayerMove
    {
        public static bool doPatch;
        public static void Postfix(ref Player_Move __instance)
        {
            if(doPatch) __instance.move = __instance.move.normalized;
        }
    }
}