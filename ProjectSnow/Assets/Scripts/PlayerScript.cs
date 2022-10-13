using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float interactDistance = 100.0f;

    Ray rayOrigin;
    RaycastHit hitInfo;

    private bool canInteract = true;

    void Start()
    {

    }

    void Update()
    {
        CheckInput();
    }

    private void FixedUpdate()
    {
    }

    private void CheckInput()
    {
        if(Input.GetMouseButtonUp(0))
        {
            canInteract = true;
        }

        if(Input.GetMouseButton(0)) // LEFT CLICK
        {
            CheckInteraction();
        }


    }

    private void CheckInteraction()
    {
        if (canInteract)
        {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, interactDistance))
            {
                Debug.Log(hitInfo.transform.tag);
            }
        }

        canInteract = false;
    }
}
