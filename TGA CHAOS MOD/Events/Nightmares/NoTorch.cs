using UnityEngine;

namespace ChaosMod.Events.Nightmares;

public class NoTorch : MonoBehaviour
{
    private Arms arms;
    private bool @do;
    
    public void E_Enable()
    {
        arms = FindObjectOfType<Arms>();
        @do = true;
        arms.takingOut = false;
    }

    public void E_Disable()
    {
        @do = false;
        arms.canTakeOutFlashlight = true;
    }

    private void Update()
    {
        if (@do) arms.canTakeOutFlashlight = false;
    }

    public override string ToString() => "It's  D A R K";
}