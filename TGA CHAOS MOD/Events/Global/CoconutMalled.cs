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
        var canvas = FindObjectOfType<Achievements_CanvasManager>().GetComponent<Canvas>();
        
        var texture = Plugin.modAssets.LoadAsset<Texture2D>("coconut deepfry");
        var rect = new Rect(0, 0, texture.width, texture.height);
        var centre = rect.size / 2;
        var sprite = Sprite.Create(texture, rect, centre);
        
        o = new GameObject("CoconutMallImage");
        o.transform.SetParent(canvas.transform, false);
        var image = o.AddComponent<Image>();
        image.sprite = sprite;
        var imageRect = image.GetRectTransform();
        imageRect.anchorMin = -Vector2.one;
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
        audioSource.Stop();
    }
    
    public override string ToString() => "You just got...";
}