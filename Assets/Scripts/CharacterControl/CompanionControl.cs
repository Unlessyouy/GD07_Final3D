using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CompanionControl : BasicControl
{
    public TextMeshProUGUI lightValueUI;

    int controlType;
    bool companionSynchronous;
    //1 = wasd; 2 = up down left right;

    //public bool following;
    protected override void Start()
    {
        base.Start();
        companionSynchronous = otherOne.GetComponent<PlayerControl>().synchronous;
        //following = false;
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
        processedInput = Vector3.forward * verticalInput + Vector3.right * horizontalInput;
        #endregion

        if (!companionSynchronous)
        {
            if (Input.GetKeyDown(KeyCode.Q) && otherOne.GetComponent<BasicControl>().alive)
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

        #region UI
        lightValueUI.text = "Companion\nLight: " + (int)lightValue;
        #endregion
    }
}