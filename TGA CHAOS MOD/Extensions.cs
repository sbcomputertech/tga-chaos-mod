using UnityEngine;

public static class Extensions
{
    public static RectTransform GetRectTransform(this GameObject o)
    {
        return o.GetComponent<RectTransform>();
    }

    public static void EventSetActive(this MonoBehaviour mb, bool active)
    {
        mb.Invoke(active ? "E_Enable" : "E_Disable", 0f);
    }
}