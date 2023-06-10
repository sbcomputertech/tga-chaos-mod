using UnityEngine;

namespace ChaosMod.Events.Global;

public class NoInteraction : MonoBehaviour
{
    private Player_Raycast raycast;
    private bool block;
    
    public void E_Enable()
    {
        raycast = FindObjectOfType<Player_Raycast>();
        block = true;
    }

    public void E_Disable()
    {
        block = false;
        raycast.canInteract = true;
    }

    private void Update()
    {
        if(!block) return;
        raycast.canInteract = false;
    }

    public override string ToString() => "Not very handy";
}