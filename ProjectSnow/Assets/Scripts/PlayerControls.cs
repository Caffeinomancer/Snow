using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public GameObject playerSphere;
    public GameObject cameraRef;
    public Rigidbody rb;

    public float moveSpeed = 1;
    public float cameraSpeed = 5.0f;

    public PlayerFeet playerFeet;

    private Vector3 cameraPosDif;


    private bool f = false;
    private bool canJump = true;

    private float camOffsetX;
    private float camOffsetY;

    // Start is called before the first frame update
    void Start()
    {

        cameraPosDif = -playerSphere.transform.forward;
        cameraPosDif.y = 0.6f;
        rb = playerSphere.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        UpdateKeyboardInput();
        UpdateMouseInput();
        //UpdateCamera();
        UpdatePosition();

        //TODO: CLEAN THIS UP
        if(f)
        {
            playerSphere.transform.forward = cameraRef.transform.forward;
            f = false;
            Debug.Log("test");

        }
    }

    //Legacy code delete later????
    /*private void UpdateCamera()
    {
        cameraRef.transform.position = playerSphere.transform.position - cameraPosDif * -2f;
        cameraRef.transform.forward = playerSphere.transform.position - cameraRef.transform.position;
        cameraRef.transform.eulerAngles = new Vector3(5.0f, cameraRef.transform.rotation.y, cameraRef.transform.rotation.z);
    }*/

    private void UpdatePosition()
    {
        //Debug.Log("Player Pos: " + transform.position);
        //Debug.Log("Sphere Pos: " + playerSphere.transform.position);
        Vector3 lastLoc = playerSphere.transform.position;
        transform.position = playerSphere.transform.position;
        playerSphere.transform.position = lastLoc;
    }

    //TODO: CLEAN UP THIS FUNCTION
    private void UpdateMouseInput()
    {
        float x, y, z;
        x = cameraRef.transform.rotation.x;
        y = cameraRef.transform.rotation.x;
        z = cameraRef.transform.rotation.x;

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
        if (Input.GetAxis("Mouse X") < 0)
        {
            cameraRef.transform.rotation = new Quaternion(0, cameraRef.transform.rotation.y, cameraRef.transform.rotation.z, cameraRef.transform.rotation.w);

            cameraRef.transform.RotateAround(transform.position, Vector3.down, 1);

            f = true;
        }

        //Mouse Right
        if (Input.GetAxis("Mouse X") > 0)
        {
            cameraRef.transform.rotation = new Quaternion(0, cameraRef.transform.rotation.y, cameraRef.transform.rotation.z, cameraRef.transform.rotation.w);

            cameraRef.transform.RotateAround(transform.position, Vector3.up, 1);

            f = true;
        }
    }

    private void UpdateKeyboardInput()
    {
        if(Input.GetKey(KeyCode.W))
        {

            rb.AddRelativeForce(Vector3.forward * moveSpeed);
        }

        if(Input.GetKey(KeyCode.A))
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

        if (Input.GetKey(KeyCode.Space))
        {
            if(canJump)
            {
                rb.AddForce(new Vector3(0, 250, 0));
                canJump = false;
            }
        }
    }

    public void TouchedGround()
    {
        canJump = true;
    }
}
