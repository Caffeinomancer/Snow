using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public float mouseSensitivity = 1.0f;

    public Camera cameraRef;

    public Rigidbody rb;

    public GameObject r1;
    public GameObject r2;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    //TODO: Subroutine updates
    void Update()
    {
        UpdateKeyboardInput();
        UpdateMouseInput();
    }

    //TODO: CLEAN UP THIS FUNCTION
    private void UpdateMouseInput()
    {
        //float x, y, z;
        //x = transform.rotation.x;
        //y = transform.rotation.x;
        //z = transform.rotation.x;

        //float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        //float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //xRotation -= mouseY;
        //xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //playerBody.Rotate(Vector3.up * mouseX);

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        float xRotation = 0;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraRef.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        if (Input.GetKey(KeyCode.Q))
        {



            //cameraRef.transform.rotation = new Quaternion(x, cameraRef.transform.rotation.y, cameraRef.transform.rotation.z, cameraRef.transform.rotation.w);

            /* cameraRef.transform.rotation = new Quaternion(
                     x,//cameraRef.transform.rotation.x,//x,
                     y,//cameraRef.transform.rotation.y,//y,
                     z,//cameraRef.transform.rotation.z,//z,
                     cameraRef.transform.rotation.w);*/
        }


        //Mouse Left
        /*if (Input.GetAxis("Mouse X") < 0)
        {
           // transform.rotation = new Quaternion(0, transform.rotation.y, transform.rotation.z, transform.rotation.w);

            cameraRef.transform.RotateAround(transform.position, Vector3.down, 0.1f);

            transform.rotation = new Quaternion(0, cameraRef.transform.rotation.y, cameraRef.transform.rotation.z, cameraRef.transform.rotation.w);
        }

        //Mouse Right
        if (Input.GetAxis("Mouse X") > 0)
        {
            //transform.rotation = new Quaternion(0, transform.rotation.y, transform.rotation.z, transform.rotation.w);

            cameraRef.transform.RotateAround(transform.position, Vector3.up, 0.1f);

            transform.rotation = new Quaternion(0, cameraRef.transform.rotation.y, cameraRef.transform.rotation.z, cameraRef.transform.rotation.w);

        }

        if (Input.GetAxis("Mouse Y") < 0)
        {
            //cameraRef.transform.RotateAround(transform.position, Vector3.right, 1);

        }

        if (Input.GetAxis("Mouse Y") > 0)
        {
            //cameraRef.transform.RotateAround(transform.position, Vector3.left, 1);

        }*/
    }

    private void UpdateKeyboardInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddRelativeForce(Vector3.forward * moveSpeed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddRelativeForce(Vector3.left * moveSpeed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.AddRelativeForce(Vector3.back * moveSpeed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.AddRelativeForce(Vector3.right * moveSpeed);
        }

        if (Input.GetKey(KeyCode.Alpha3))
        {
            if(r1.GetComponent<Renderer>().enabled)
            {
                r1.GetComponent<Renderer>().enabled = false;

            }
            else
            {
                r1.GetComponent<Renderer>().enabled = true;
            }
        }

        if (Input.GetKey(KeyCode.Alpha4))
        {
            if (r2.GetComponent<Renderer>().enabled)
            {
                r2.GetComponent<Renderer>().enabled = false;

            }
            else
            {
                r2.GetComponent<Renderer>().enabled = true;
            }
        }

      

        if (Input.GetKey(KeyCode.Space))
        {
            /*if (canJump)
            {
                rb.AddForce(new Vector3(0, 250, 0));
                canJump = false;
            }*/
        }
    }
}
