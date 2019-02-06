using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorPad : MonoBehaviour {

    public void FloorPadPressed()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetTrigger("FloorPadPressed");

    }
}
