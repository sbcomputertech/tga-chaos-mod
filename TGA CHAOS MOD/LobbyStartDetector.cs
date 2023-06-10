using System;
using HarmonyLib;

namespace ChaosMod;

[HarmonyPatch(typeof(Lobby_Trigger_StartBot), nameof(Lobby_Trigger_StartBot.OnTriggerEnter))]
public static class LobbyStartDetector
{
    public static void Postfix()
    {
        OnStart();
    }

    public static Action OnStart;
}