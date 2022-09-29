using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

    private float horizontalInput;
    private float verticalInput;

    public Transform orientation;

    Vector3 movementDirection;

    Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        UpdateKeyboardInput();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void UpdateKeyboardInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        movementDirection = (orientation.forward * verticalInput) + (orientation.right * horizontalInput);

        rb.AddForce(movementDirection.normalized * moveSpeed * 10.0f, ForceMode.Force);
    }
}
