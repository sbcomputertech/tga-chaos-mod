using UnityEngine;

namespace ChaosMod.Events.Global;

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
    
    public override string ToString() => "Dummy Event";
}