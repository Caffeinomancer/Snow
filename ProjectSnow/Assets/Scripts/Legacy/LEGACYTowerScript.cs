using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LEGACYTowerScript : MonoBehaviour
{
    public Camera cameraRef;

    private bool canRotate = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //TODO: Make subroutines
    void Update()
    {
        LookAtCamera();

       


    }

    private void LookAtCamera()
    {
        if(canRotate)
        {
            if(cameraRef != null)
            {

                transform.LookAt(cameraRef.transform.position, -Vector3.up);
                transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
                transform.forward = new Vector3(cameraRef.transform.forward.x, transform.forward.y, cameraRef.transform.forward.z);
            }
        }
    }    
}
