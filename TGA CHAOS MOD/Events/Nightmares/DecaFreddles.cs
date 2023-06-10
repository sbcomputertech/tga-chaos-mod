using UnityEngine;

namespace ChaosMod.Events.Nightmares;

public class DecaFreddles : MonoBehaviour
{
    private static readonly Vector3[] positions = {
        new(0.684000015f,0.0037f,2.90899992f),
        new(1.60899997f,0.00079999998f,-3.51999998f),
        new(-0.497000009f,0.786000013f,-0.509000003f),
        new(-3.31200004f,0.0037f,2.96600008f),
        new(-4.78800011f,1.29999995f,-3.1730001f),
        new(-2.4059999f,0.0036000018f,-3.95300007f),
        new(0.495000005f,2.16820002f,-4.046f),
        new(-4.87900019f,1.95700002f,-0.32100001f),
        new(-4.37599993f,2.30200005f,2.35700011f),
        new(-1.046f,0.0839999989f,-0.61500001f)
    };

    public void E_Enable()
    {
        // var aiBase = FindObjectOfType<Freddles_AI1>();
        // var obj = aiBase.freddle;
        // foreach (var pos in positions)
        // {
        //     var o = new GameObject("CFreddle");
        //     o.transform.position = pos;
        //     o.transform.rotation = Quaternion.identity;
        //     var ai = o.AddComponent<CustomfreddleAi>();
        //     ai.freddle = Instantiate(obj, Vector3.zero, Quaternion.identity, o.transform);
        //     ai.freddle.SetActive(true);
        //     ai.freddle.transform.LookAt(aiBase.Arms.transform);
        //     ai.Player_Insanity = aiBase.Player_Insanity;
        //     ai.Arms = aiBase.Arms;
        //     ai.Player_InfinityRaycast = aiBase.Player_InfinityRaycast;
        //     ai.enableAI = true;
        //
        // }
    }
    
    public void E_Disable() { }
    
    public override string ToString() => "Fred-deca-les";
}