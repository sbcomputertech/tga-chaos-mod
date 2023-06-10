using UnityEngine;

namespace ChaosMod.Events.Global;

public class HelpfulTip : MonoBehaviour
{
    private GameObject obj;
    private PanelTips_animations anim;
    public void E_Enable()
    {
        var tipManagerPrefab = FindObjectOfType<PanelTips_animations>().gameObject;
        var player = FindObjectOfType<Player_Move>().transform;
        obj = Instantiate(tipManagerPrefab, player.position + (player.forward * 3) + (Vector3.up * 4f), Quaternion.identity);
        anim = obj.GetComponent<PanelTips_animations>();
    }

    private void Update()
    {
        if(anim != null) anim.open = true; // you cant close it lmao
    }

    public void E_Disable()
    {
        // 20s + 10s from the event manager = stays for 30 seconds
        Destroy(obj, 20f);
        Destroy(this, 20f);
    }
    
    public override string ToString() => "A quick helpful tip";
}