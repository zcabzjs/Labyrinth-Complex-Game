using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorPad : MonoBehaviour {

    MeshRenderer meshRenderer;
    Light[] lights;
    AudioSource audiosource;
    public void FloorPadPressed()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetTrigger("FloorPadPressed");

        // Set animations for floorpad here i guess...
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.EnableKeyword("_EMISSION");
        lights = GetComponentsInChildren<Light>();
        for(int i = 0; i < lights.Length; i++)
        {
            lights[i].enabled = true;
        }
        audiosource = GetComponent<AudioSource>();
        audiosource.Play();
    }
}
