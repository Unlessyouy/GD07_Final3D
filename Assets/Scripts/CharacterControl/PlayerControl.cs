using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerControl : BasicControl
{
    public float cameraSpeed;
    public bool canConnect = false;

    Vector3 processedInput = new(0, 0, 0);
    float mouseInputX;
    float mouseInputY;

    public TextMeshProUGUI lightValueUI;

    public CompanionControl companion;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    protected override void Update()
    {
        base.Update();

        lightValueUI.text = "Light: " + (int)lightValue;

        mouseInputX = Input.GetAxisRaw("Mouse X");
        mouseInputY = Input.GetAxisRaw("Mouse Y");

        processedInput = transform.forward * verticalInput + transform.right * horizontalInput;

        if (Input.GetKeyDown(KeyCode.Q) && canConnect)
        {
            connected = !connected;
            companion.connected = !companion.connected;
        }
    }
    void FixedUpdate()
    {
        rb.MovePosition(transform.position + (movingSpeed * Time.deltaTime * processedInput.normalized));

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
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.GetComponent<LightControl>() != null)
        {
            //Debug.Log("enter");
            other.GetComponent<LightControl>().actable = true;
        }
        else if (other.GetComponent<CompanionControl>() != null)
        {
            canConnect = true;
        }
    }
    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        if (other.GetComponent<LightControl>() != null)
        {
            //Debug.Log("exit");
            other.GetComponent<LightControl>().actable = false;
        }
        else if (other.GetComponent<CompanionControl>() != null)
        {
            canConnect = false;
        }
    }
}