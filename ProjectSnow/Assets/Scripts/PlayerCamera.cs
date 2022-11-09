using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float mouseSensitivity;

    private float cameraRotationX;
    private float cameraRotationY;

    public Transform orientation;

    private bool trackMouse = false;

    void Start()
    {
        //LockCursor();
    }

    void Update()
    {
        if (trackMouse)
        {
            UpdateMouseInput();
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if(focus)
        {
            trackMouse = true;
            LockCursor(true);
        }
        else
        {
            trackMouse = false;
            LockCursor(false);
        }
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

    private void LockCursor(bool lockCursor)
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
