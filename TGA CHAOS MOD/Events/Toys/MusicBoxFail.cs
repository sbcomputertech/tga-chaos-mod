using UnityEngine;

namespace ChaosMod.Events.Toys;

public class MusicBoxFail : MonoBehaviour
{
    public void E_Enable()
    {
        // MUSIC BOX WORKINGS:
        // inits to Random(100, 105)
        // if less than 50 you die
        // if greater than 115 you die
        // increased by 2.5 per second you crank it
        // deacresed bu 0.5 per second you don't crank it
        
        var freddyAi = FindObjectOfType<WitheredFreddy_AI1>();
        var minus50Percent = freddyAi.musicBox - freddyAi.musicBox * 0.3f; // take away 30 percent
        freddyAi.musicBox = Mathf.Clamp(minus50Percent, 55f, 110f); // just so you don't insta-lose, you get a few seconds of time
    }

    public void E_Disable() { }
    
    public override string ToString() => "Spring(lock) failiure";
}