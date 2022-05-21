using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicControl : MonoBehaviour
{
    Rigidbody rb;
    public float movingSpeed;
    public float cameraSpeed;

    protected Vector3 processedInput = new Vector3(0, 0, 0);
    protected float mouseInputX;
    protected float mouseInputY;
    protected float horizontalInput;
    protected float verticalInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");//×óÓÒ£¬×ó-1£¬ÓÒ1
        verticalInput = Input.GetAxisRaw("Vertical");//Ç°ºó

        mouseInputX = Input.GetAxisRaw("Mouse X");
        mouseInputY = Input.GetAxisRaw("Mouse Y");

        processedInput = (transform.forward * verticalInput + transform.right * horizontalInput);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + (processedInput.normalized * movingSpeed * Time.deltaTime));

        transform.Rotate(new Vector3(0, mouseInputX, 0) * cameraSpeed, Space.Self);
        transform.GetChild(0).transform.Rotate(new Vector3(-mouseInputY, 0, 0) * cameraSpeed, Space.Self);

        if (CheckAngle(transform.GetChild(0).transform.localEulerAngles.x) < -60)
        {
            transform.GetChild(0).transform.localEulerAngles = new Vector3(-60, 0, 0);
        }
        if (CheckAngle(transform.GetChild(0).transform.localEulerAngles.x) > 30)
        {
            transform.GetChild(0).transform.localEulerAngles = new Vector3(30, 0, 0);
        }
    }
    public float CheckAngle(float Value)
    {
        float Angle = Value - 180;
        if (Angle > 0)
        {
            return Angle - 180;
        }
        return Angle + 180;
    }
}