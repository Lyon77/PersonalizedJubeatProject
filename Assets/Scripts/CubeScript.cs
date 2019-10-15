using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour
{
    public bool isActive;
    private Animator sphereAnimation;

    // Start is called before the first frame update
    void Start()
    {
        sphereAnimation = GetComponentInChildren<Animator>();
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown() {
        if (isActive) {
            //when the button is active, do something
            Debug.Log("Clicked while Active");
            sphereAnimation.SetTrigger("clicked");
            GameManager.instance.HitNote();
            isActive = false;
        } else {
            //when the button is inactive
            Debug.Log("Clicked while Inactive");
        }
    }

    public void ActivateAnimation() {
        //activate the animation whenever GameManager requests
        isActive = true;
        sphereAnimation.SetTrigger("noteRequested");
    }
}
