using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerControl : BasicControl
{
    public float cameraSpeed;

    Vector3 lineVector;

    Vector3 processedInput = new(0, 0, 0);
    float mouseInputX;
    float mouseInputY;

    public TextMeshProUGUI lightValueUI;

    public CompanionControl companion;

    LineRenderer lr;

    protected override void Start()
    {
        base.Start();
        Cursor.lockState = CursorLockMode.Locked;
        lr = GetComponentInChildren<LineRenderer>();
    }
    protected override void Update()
    {
        base.Update();

        connected = (connected && companion.connected);

        if (companion.alive)
        {
            lineVector = companion.transform.position - transform.position;
            Debug.DrawRay(transform.position, lineVector, Color.red, 0);
        }
        else
        {
            lineVector = Vector3.zero;
            Debug.DrawRay(transform.position, lineVector, Color.red, 0);
        }

        lr.SetPosition(0, transform.position);

        if (connected)
        {
            lr.SetPosition(1, companion.transform.position);
        }
        else
        {
            lr.SetPosition(1, transform.position);
        }

        #region UI
        lightValueUI.text = "Light: " + (int)lightValue;
        #endregion

        #region Input
        horizontalInput = Input.GetAxisRaw("Horizontal");//×óÓÒ£¬×ó-1£¬ÓÒ1
        verticalInput = Input.GetAxisRaw("Vertical");//Ç°ºó
        mouseInputX = Input.GetAxisRaw("Mouse X");
        mouseInputY = Input.GetAxisRaw("Mouse Y");

        processedInput = transform.forward * verticalInput + transform.right * horizontalInput;

        if (Input.GetKeyDown(KeyCode.Q) && companion.alive)
        {
            companion.following = !companion.following;

            if (companion.following)
            {
                companion.GetComponent<MeshRenderer>().material.color = Color.green;
            }
            else
            {
                companion.GetComponent<MeshRenderer>().material.color = Color.blue;
            }
        }
        #endregion
    }
    void FixedUpdate()
    {
        float angleBetweenLineAndInput = Vector3.Angle(processedInput, lineVector);

        if (alive)
        {
            if (angleBetweenLineAndInput <= 15 && lineVector != Vector3.zero)
            {
                rb.MovePosition(transform.position + (movingSpeed * 2 * Time.deltaTime * processedInput.normalized));
                rb.useGravity = false;
            }
            else
            {
                rb.MovePosition(transform.position + (movingSpeed * Time.deltaTime * processedInput.normalized));
                rb.useGravity = true;
            }
        }

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
    }
    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        if (other.GetComponent<LightControl>() != null)
        {
            //Debug.Log("exit");
            other.GetComponent<LightControl>().actable = false;
        }
    }
}