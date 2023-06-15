using UnityEngine;

namespace ChaosMod.Events.Nightmares;

public class ScramblePuzzle : MonoBehaviour
{
    public void E_Enable()
    {
        var check = FindObjectOfType<PuzzlePictures_CheckComplete>(); // only will work before completion
        foreach (var block in check.PuzzlePictures_RotateBlock)       // have to settle for just changing the code for now
        {
            block.stade = Random.Range(0, 4);
        }

        var passw = FindObjectOfType<Make_Password>();
        passw.Start(); // re-init password system which chooses random nums
    }
    
    public void E_Disable() { }

    public override string ToString() => "Scramble scramble";
}