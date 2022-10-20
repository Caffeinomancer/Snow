using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform cameraPos;

    // Update is called once per frame
    void Update()
    {
        if(cameraPos != null)
        {
            transform.position = cameraPos.position;
        }
        else
        {
            Debug.LogError("Camera tranform not set in MoveCamera script");
        }
    }
}
