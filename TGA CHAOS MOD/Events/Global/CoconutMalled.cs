using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace ChaosMod.Events.Global;

public class CoconutMalled : MonoBehaviour
{
    private GameObject o;
    private AudioSource audioSource;
    public void E_Enable()
    {
        o = new GameObject("CoconutMallImage");
        o.transform.SetParent(Plugin.ui, false);
        var image = o.AddComponent<Image>();
        image.sprite = Utils.SpriteFromModAssets("coconut deepfry");
        var imageRect = image.GetRectTransform();
        imageRect.anchorMin = Vector2.zero;
        imageRect.anchorMax = Vector2.one;

        var audioClip = Plugin.modAssets.LoadAsset<AudioClip>("coconut mall");
        var obj = Camera.main!.gameObject;
        audioSource = obj.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void E_Disable()
    {
        Destroy(o);
        Destroy(this);
        if (Plugin.DEBUG) audioSource.Stop(); // I don't want this pain while testing
    }
    
    public override string ToString() => "You just got...";
}