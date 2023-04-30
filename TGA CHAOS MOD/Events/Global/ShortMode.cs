using System;
using UnityEngine;

namespace ChaosMod.Events.Global;

public class ShortMode : MonoBehaviour
{
    private const float heightMultiplier = 0.5f;
    private bool active;
    private Player_Move moveRef;

    public void E_Disable()
    {
        active = false;
        moveRef.crawl_obligated = false;
    }

    public void E_Enable()
    {
        active = true;
        moveRef = FindObjectOfType<Player_Move>();
    }

    private void Update()
    {
        if(!active) return;
        moveRef.canCrawl = true;
        moveRef.crawl_obligated = true;
    }

    public override string ToString() => "Short simulator";
}