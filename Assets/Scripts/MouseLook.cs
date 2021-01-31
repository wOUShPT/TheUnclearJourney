using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 200f;
    public Transform playerBody;
    float xRotation = 0f;

    private bool canMove;

    private void Awake()
    {
        canMove = true;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY; // mover vertical
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX); // mover horizontal
        }
    }
    
    public void ActivateDeactivate()
    {
        canMove = !canMove;
    }

    public void ResetXRotation()
    {
        xRotation = 0;
    }
}
