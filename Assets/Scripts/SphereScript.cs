using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereScript : MonoBehaviour
{
    private CubeScript parent;

    void Start() {
        parent = GetComponentInParent<CubeScript>();
    }

    public void AnimationEnded() {
        //tell GameManager that the player didn't hit the button
        parent.isActive = false;
        GameManager.instance.MissedNote();
    }
}
