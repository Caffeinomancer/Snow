using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float mouseSensitivity;

    private float cameraRotationX;
    private float cameraRotationY;

    public bool lockCursor = true;

    public Transform orientation;

    void Start()
    {
        LockCursor();
    }

    void Update()
    {
        UpdateMouseInput();
    }

    //TODO: CLEAN UP THIS FUNCTION
    private void UpdateMouseInput()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;

        cameraRotationY += mouseX;

        cameraRotationX -= mouseY;
        cameraRotationX = Mathf.Clamp(cameraRotationX, -90f, 90f); //90 is max look up and down rotation

        transform.rotation = Quaternion.Euler(cameraRotationX, cameraRotationY, 0);
        orientation.rotation = Quaternion.Euler(0, cameraRotationY, 0);
    }

    private void LockCursor()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        else
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }
}
