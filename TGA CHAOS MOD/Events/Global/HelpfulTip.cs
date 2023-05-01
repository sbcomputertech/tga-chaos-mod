using UnityEngine;

namespace ChaosMod.Events.Global;

public class HelpfulTip : MonoBehaviour
{
    private GameObject obj;
    private PanelTips_animations anim;
    public void E_Enable()
    {
        var tipManagerPrefab = GlobalObjectReferences.tipManager;
        var player = FindObjectOfType<Player_Move>().transform;
        obj = Instantiate(tipManagerPrefab, player.position + (player.forward * 3) + (Vector3.up * 5.5f), Quaternion.identity);
        anim = obj.GetComponent<PanelTips_animations>();
    }

    private void Update()
    {
        if(anim != null) anim.open = true;
    }

    public void E_Disable()
    {
        Destroy(obj, 20f);
    }
    
    public override string ToString() => "A quick helpful tip";
}