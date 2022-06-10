using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerControl : BasicControl
{
    [Header("同步控制的开关")]
    public bool synchronous;

    [Header("主角UI")]
    public TextMeshProUGUI lightValueUI;

    LineRenderer lr;

    CompanionControl companion;

    protected override void Start()
    {
        base.Start();
        Cursor.lockState = CursorLockMode.Locked;
        lr = GetComponentInChildren<LineRenderer>();
        controlled = true;
        companion = otherOne.GetComponent<CompanionControl>();
        GetComponent<MeshRenderer>().material.color = Color.green;
    }
    protected override void Update()
    {
        base.Update();

        #region Other One
        if (companion != null)
        {
            connected = (connected && otherOne.GetComponent<CompanionControl>().connected);

            if (companion.alive)
            {
                lineVector = companion.transform.position - transform.position;
            }
            else
            {
                lineVector = Vector3.zero;
            }
        }
        #endregion

        #region Input & Movement
        horizontalInput = Input.GetAxisRaw("Horizontal");//左右，左-1，右1
        verticalInput = Input.GetAxisRaw("Vertical");//前后
        processedInput = Vector3.forward * verticalInput + Vector3.right * horizontalInput;
        #endregion

        #region E to switch control
        if (!synchronous)
        {
            if (Input.GetKeyDown(KeyCode.E) && otherOne.GetComponent<BasicControl>().alive)
            {
                controlled = !controlled;
                if (controlled)
                {
                    GetComponent<MeshRenderer>().material.color = Color.green;
                }
                else
                {
                    GetComponent<MeshRenderer>().material.color = Color.blue;
                }
            }
        }
        #endregion

        #region Q to make companion follow
        if (Input.GetKeyDown(KeyCode.Q) && companion.alive && controlled && !synchronous)
        {
            companion.following = !companion.following;

            if (companion.following)
            {
                companion.GetComponent<MeshRenderer>().material.color = Color.yellow;
            }
            else
            {
                companion.GetComponent<MeshRenderer>().material.color = Color.blue;
            }
        }
        #endregion

        #region lineRenderer
        lr.SetPosition(0, transform.position);
        if (connected)
        {
            lr.SetPosition(1, otherOne.transform.position);
        }
        else
        {
            lr.SetPosition(1, transform.position);
        }
        #endregion

        #region UI
        lightValueUI.text = "Light: " + (int)lightValue;
        #endregion
    }
    //void FixedUpdate()
    //{
        //transform.Rotate(new Vector3(0, mouseInputX, 0) * cameraSpeed, Space.Self);
        //transform.GetChild(0).transform.Rotate(new Vector3(-mouseInputY, 0, 0) * cameraSpeed, Space.Self);

        //if (CheckAngle(transform.GetChild(0).transform.localEulerAngles.x) < -60)
        //{
        //    transform.GetChild(0).transform.localEulerAngles = new Vector3(-60, 0, 0);
        //}
        //if (CheckAngle(transform.GetChild(0).transform.localEulerAngles.x) > 30)
        //{
        //    transform.GetChild(0).transform.localEulerAngles = new Vector3(30, 0, 0);
        //}
    //}
}