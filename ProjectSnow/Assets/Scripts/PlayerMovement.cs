using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float groundDrag;
    public float wallRunSpeed;
    
    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    private bool canJump;


    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    private bool isGrounded;

    [Header("Slope handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    private float horizontalInput;
    private float verticalInput;

    //Player dimensions
    private float playerHalfHeight = 0.5f;
    private float playerHalfHeightOffSet = 0.3f;

    public Transform orientation;

    private Vector3 movementDirection;

    private Rigidbody rb;

    public MovementState state;
    public enum MovementState
    {
        walking,
        sprinting,
        air,
        wallrunning
    }

    public bool wallRunning;

    void Start()
    {
        canJump = true;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * playerHalfHeight + playerHalfHeightOffSet, whatIsGround);

        UpdateInput();
        SpeedControl();
        StateHandler();

        if(isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void UpdateInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //Check when to jump
        if(Input.GetKey(jumpKey) && canJump && isGrounded)
        {
            canJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void StateHandler()
    {
        if(wallRunning)
        {
            state = MovementState.wallrunning;
            moveSpeed = wallRunSpeed;
        }

        //Sprinting 
        if(isGrounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }

        else if(isGrounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }

        else
        {
            state = MovementState.air;
        }
    }

    private void MovePlayer()
    {
        movementDirection = (orientation.forward * verticalInput) + (orientation.right * horizontalInput);

        //Slope
        if(OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20.0f, ForceMode.Force);

            if(rb.velocity.y > 0)//If moving upwards add down velocity to keep on slope
            {
                rb.AddForce(Vector3.down * 80.0f, ForceMode.Force);
            }
        }

        //Grounded
        if(isGrounded)
        {
            rb.AddForce(movementDirection.normalized * moveSpeed * 10.0f, ForceMode.Force);
        }

        //In Air
        else if(!isGrounded)
        {
            rb.AddForce(movementDirection.normalized * moveSpeed * 10.0f * airMultiplier, ForceMode.Force);
        }

        //Turn Gravity off if on a slope
        rb.useGravity = !OnSlope();
    }

    private void SpeedControl()
    {
        //Limit speed on slope

        if(OnSlope() && !exitingSlope)
        {
            if(rb.velocity.magnitude > moveSpeed)
            {
                rb.velocity = rb.velocity.normalized * moveSpeed;
            }
        }

        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            //Limit velocity
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed; //calculate what new max velocity would be
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z); //apply velocity
            }
        }
    }

    private void Jump()
    {
        exitingSlope = true;

        //Reset jump velocity to zero so we only always jump the same height
        rb.velocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);//Impulse since we're only apply force once
    }

    private void ResetJump()
    {
        canJump = true;

        exitingSlope = false;
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * playerHalfHeight + playerHalfHeightOffSet))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(movementDirection, slopeHit.normal).normalized;
    }    

  
}
