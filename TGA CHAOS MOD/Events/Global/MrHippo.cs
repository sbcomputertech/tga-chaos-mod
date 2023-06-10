using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine.UI;

// unhandled case in switch
#pragma warning disable CS8509

namespace ChaosMod.Events.Global;

using UnityEngine;

public class MrHippo : MonoBehaviour
{
    
    private AudioSource audio;
    private float length = 99999;
    private AudioClip[] clips;
    private readonly List<GameObject> images = new();
    private float time;
    private int whatImage;
    
    public void E_Enable()
    {
        // look i can't be bothered to type out an array of clips/strings/whatever
        clips = Enumerable.Range(1, 4).Select(n => "Hippo0" + n).Select(c => Plugin.modAssets.LoadAsset<AudioClip>(c)).ToArray();

        var obj = new GameObject("HippoAudio");
        obj.transform.SetParent(Camera.main!.transform);
        audio = obj.AddComponent<AudioSource>();
        audio.clip = Utils.RandomFrom(clips);
        length = audio.clip.length;
        audio.Play();
        StartCoroutine(CheckAudioOver());
        ShowImage(++whatImage);
    }

    private IEnumerator CheckAudioOver()
    {
        while (audio.isPlaying) yield return null;
        // over
        foreach (var img in images)
        {
            Destroy(img, 1f);
        }
        Destroy(audio.gameObject);
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time >= length / 3) // divide length by no of images
        {
            ShowImage(++whatImage);
            time = 0;
        }
    }

    private void ShowImage(int num)
    {
        if(num is < 1 or > 3) return;
        Plugin.Logging.LogInfo("Hippo: " + num);
        
        var obj = new GameObject(num.ToString());
        obj.transform.SetParent(Plugin.ui);
        var img = obj.AddComponent<Image>();
        img.sprite = Utils.SpriteFromModAssets("hippo_" + num);
        
        var rect = img.GetRectTransform();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        
        rect.localPosition = num switch
        {
            1 => new Vector3(-500, 45, 0),
            2 => new Vector3(500, -130, 0),
            3 => new Vector3(300, -250, 0),
        };
        rect.localScale = Vector3.one * num switch
        {
            1 => 1f,
            2 => .7f,
            3 => .5f
        };
        rect.DOLocalMove(num switch
        {
            1 => new Vector3(500, -50, 0),
            2 => new Vector3(-550, 150, 0),
            3 => new Vector3(-300, 0, 0)
        }, length / num).SetEase(Ease.OutQuart); // https://howto.smooch.computer/i/qz54r.png
        
        images.Add(obj);
    }

    public void E_Disable()
    {
        // don't clear before next event, i want them to feel pain
    }
    
    public override string ToString() => "I have a story...";
}