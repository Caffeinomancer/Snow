using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    public Camera cameraRef;

    private bool canRotate = true;

    public Sprite smile;
    public Sprite smilewtf;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //TODO: Make subroutines
    void Update()
    {
        LookAtCamera();

        if (Input.GetKey(KeyCode.Alpha1))
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = smile;
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = smilewtf;
        }


    }

    private void LookAtCamera()
    {
        if(canRotate)
        {
            if(cameraRef != null)
            {
                transform.LookAt(cameraRef.transform.position, -Vector3.up);
                transform.forward = new Vector3(cameraRef.transform.forward.x, transform.forward.y, cameraRef.transform.forward.z);
            }
        }
    }    
}
