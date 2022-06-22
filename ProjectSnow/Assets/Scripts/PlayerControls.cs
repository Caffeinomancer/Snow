using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public GameObject playerSphere;
    public GameObject cameraRef;
    public Rigidbody rb;

    public float speed = 1;

    private Vector3 cameraPosDif;


    private bool canJump = true;

    public PlayerFeet playerFeet;

    // Start is called before the first frame update
    void Start()
    {

        cameraPosDif = -playerSphere.transform.forward;
        cameraPosDif.y = 0.6f;

    }

    // Update is called once per frame
    void Update()
    {
        UpdateInput();
        UpdateCamera();
        UpdatePosition();

    }

    void UpdateCamera()
    {
        cameraRef.transform.position = playerSphere.transform.position - cameraPosDif * -2f;
        cameraRef.transform.forward = playerSphere.transform.position - cameraRef.transform.position;
        cameraRef.transform.eulerAngles = new Vector3(5.0f, cameraRef.transform.rotation.y, cameraRef.transform.rotation.z);
    }

    void UpdatePosition()
    {
        //Debug.Log("Player Pos: " + transform.position);
        //Debug.Log("Sphere Pos: " + playerSphere.transform.position);
        Vector3 lastLoc = playerSphere.transform.position;
        transform.position = playerSphere.transform.position;
        playerSphere.transform.position = lastLoc;
    }

    void UpdateInput()
    {
        if(Input.GetKey(KeyCode.W))
        {
            rb.AddForce(Vector3.forward * speed);
        }

        if(Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector3.left * speed);
        }

        if(Input.GetKey(KeyCode.S))
        {
            rb.AddForce(Vector3.back * speed);
        }

        if(Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector3.right * speed);
        }

        if(Input.GetKey(KeyCode.Space))
        {
            if(canJump)
            {
                rb.AddForce(new Vector3(0, 1000, 0));
                canJump = false;
            }
        }
    }

    public void TouchedGround()
    {
        canJump = true;
    }
}
