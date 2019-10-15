using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public bool isActive;
    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown() {
        if (isActive)
            Debug.Log("Clicked while Active");
        else
            Debug.Log("Clicked while Inactive");
    }
}
