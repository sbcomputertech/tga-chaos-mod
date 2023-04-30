﻿using UnityEngine;

namespace ChaosMod.Events.Global;

public class ConstantBoop : MonoBehaviour
{
    private bool play;
    private AudioClip clip;
    private Transform player;
    
    public void E_Enable()
    {
        clip = Plugin.modAssets.LoadAsset<AudioClip>("boop");
        play = true;
        player = Camera.main!.transform;
    }

    public void E_Disable()
    {
        play = false;
    }

    private void Update()
    {
        if(!play) return;
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }

    public override string ToString() => "Boooooooooop";
}