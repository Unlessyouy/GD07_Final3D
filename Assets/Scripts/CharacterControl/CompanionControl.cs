using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CompanionControl : BasicControl
{
    [Header("ÏÉ±´UI")]
    public TextMeshProUGUI lightValueUI;
    //[HideInInspector]
    public bool hiding;

    bool companionSynchronous;
    //1 = wasd; 2 = up down left right;

    public bool following;
    protected override void Start()
    {
        base.Start();
        companionSynchronous = otherOne.GetComponent<PlayerControl>().synchronous;
        following = false;
        if (companionSynchronous)
        {
            controlled = true;
            GetComponent<MeshRenderer>().material.color = Color.green;
        }
        else
        {
            controlled = false;
            GetComponent<MeshRenderer>().material.color = Color.blue;
        }
    }
    protected override void Update()
    {
        base.Update();

        #region Other One
        if (otherOne.GetComponent<PlayerControl>() != null)
        {
            PlayerControl player = otherOne.GetComponent<PlayerControl>();
            connected = (connected && otherOne.GetComponent<PlayerControl>().connected);

            if (player.alive)
            {
                lineVector = player.transform.position - transform.position;
            }
            else
            {
                lineVector = Vector3.zero;
            }
        }
        #endregion

        #region Input & Movement
        if (!hiding)
        {
            if (companionSynchronous)
            {
                horizontalInput = Input.GetAxisRaw("Horizontal B");//×óÓÒ£¬×ó-1£¬ÓÒ1
                verticalInput = Input.GetAxisRaw("Vertical B");//Ç°ºó
            }
            else
            {
                horizontalInput = Input.GetAxisRaw("Horizontal");//×óÓÒ£¬×ó-1£¬ÓÒ1
                verticalInput = Input.GetAxisRaw("Vertical");//Ç°ºó
            }
        }
        else
        {
            horizontalInput = 0;
            verticalInput = 0;
        }
        
        if (following)
        {
            processedInput = lineVector.normalized;
        }
        else
        {
            processedInput = Vector3.forward * verticalInput + Vector3.right * horizontalInput;
        }
        #endregion

        #region E to switch control
        if (!companionSynchronous)
        {
            if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton8)) && otherOne.GetComponent<BasicControl>().alive)
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

        #region UI
        lightValueUI.text = "Companion\nLight: " + (int)lightValue;
        #endregion
    }
    protected override void FixedUpdate()
    {
        float angleBetweenLineAndInput = Vector3.Angle(processedInput, lineVector);

        if (alive && controlled)
        {
            if (angleBetweenLineAndInput <= (speedUpAngle) / 2 && lineVector != Vector3.zero)
            {
                rb.MovePosition(transform.position + (movingSpeed * speedUpRatio * Time.deltaTime * processedInput.normalized));
            }
            else
            {
                rb.MovePosition(transform.position + (movingSpeed * Time.deltaTime * processedInput.normalized));
            }
        }
        else if (alive && following && (lineVector.magnitude >= 5)  )
        {
            rb.MovePosition(transform.position + (movingSpeed * Time.deltaTime * processedInput.normalized));
            rb.useGravity = true;
        }
    }
}