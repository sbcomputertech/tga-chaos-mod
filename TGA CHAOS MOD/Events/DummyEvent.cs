using UnityEngine;

namespace ChaosMod.Events;

public class DummyEvent : MonoBehaviour
{
    public void E_Enable()
    {
        Debug.Log("Dummy event enabled!");
    }

    public void E_Disable()
    {
        Debug.Log("Dummy event disabled!");
    }
    
    public override string ToString() => Plugin.DEBUG ? "Dummy Event" : "THIS IS A BUG";
}