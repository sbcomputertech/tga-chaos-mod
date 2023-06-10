using UnityEngine;

namespace ChaosMod;

public class Remnant : MonoBehaviour
{
    public static Remnant Create(Vector3 pos)
    {
        var o = new GameObject("RemnantItem")
        {
            tag = "Interactable" // hope this works
        };
        var rem = o.AddComponent<Remnant>();
        return rem;
    }

    private void Pickup()
    {
        Plugin.remnantAmount++;
        //TODO: Particle system
        Destroy(gameObject);
    }
}