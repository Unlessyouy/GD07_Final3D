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

    [HideInInspector]
    public bool canJump;
    [HideInInspector]
    public bool canInteract;

    CompanionControl companion;

    protected override void Start()
    {
        base.Start();
        Cursor.lockState = CursorLockMode.Locked;
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
        interactInput = -Input.GetAxisRaw("Interact");

        if (isClimbing)
        {
            if (onRopeTop)
            {
                verticalInput = -1;
                processedInput = Vector3.up * verticalInput;
            }
            else
            {
                processedInput = Vector3.up * verticalInput;
            }
        }
        else
        {
            processedInput = Vector3.forward * verticalInput + Vector3.right * horizontalInput;
        }

        if (alive && canJump && !canInteract && controlled && !isClimbing)
        {
            if (Input.GetKeyDown(KeyCode.Space) || interactInput == 1)
            {
                rb.velocity = Vector3.up * 5;
            }
        }
        #endregion

        #region E to switch control
        if (!synchronous)
        {
            if (  (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton8))  && otherOne.GetComponent<BasicControl>().alive)
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
        else if (companion.alive && controlled && !synchronous && Input.GetKeyDown(KeyCode.JoystickButton0))
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

        #region UI
        lightValueUI.text = "Light: " + (int)lightValue;
        #endregion
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            canJump = true;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            canJump = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            canJump = false;
        }
    }
}