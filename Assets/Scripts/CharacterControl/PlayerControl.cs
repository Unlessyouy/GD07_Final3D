using System.Collections;
using System.Collections.Generic;
using CharacterControl;
using UnityEngine;
using TMPro;

public class PlayerControl : BasicControl
{
    [Header("ͬ�����ƵĿ���")]
    public bool synchronous;

    public bool CanJump { get; set; }
    [Header("Jump Related")]
    [SerializeField] private float FootOffset = 0.5f;
    [SerializeField] private float RayLength = 0.75f;
    [SerializeField] private float JumpHeight = 5f;
    [SerializeField] private float GravityMultiplier = 1.5f;
    
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
        CanJump = JumpRay();
        
        #region Other One
        // if (companion != null)
        // {
        //     if (companion.alive)
        //     {
        //         lineVector = companion.transform.position - transform.position;
        //     }
        //     else
        //     {
        //         lineVector = Vector3.zero;
        //     }
        // }
        #endregion

        #region Input & Movement
        horizontalInput = Input.GetAxisRaw("Horizontal");
        interactInput = Input.GetButtonDown("Interact");

        if (rb.velocity.y < 0)
        {
            rb.velocity -= GravityMultiplier * Time.deltaTime * transform.up;
        }
        
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

        if (alive && CanJump && !canInteract && controlled && !isClimbing & !IsHoldingHands)
        {
            if (Input.GetButtonDown("Jump"))
            {
                rb.velocity = Vector3.up * JumpHeight;
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
        // if (Input.GetKeyDown(KeyCode.Q) && companion.alive && controlled && !synchronous)
        // {
        //     companion.following = !companion.following;
        //
        //     if (companion.following)
        //     {
        //         companion.GetComponent<MeshRenderer>().material.color = Color.yellow;
        //     }
        //     else
        //     {
        //         companion.GetComponent<MeshRenderer>().material.color = Color.blue;
        //     }
        // }
        // else if (companion.alive && controlled && !synchronous && Input.GetKeyDown(KeyCode.JoystickButton0))
        // {
        //     companion.following = !companion.following;
        //
        //     if (companion.following)
        //     {
        //         companion.GetComponent<MeshRenderer>().material.color = Color.yellow;
        //     }
        //     else
        //     {
        //         companion.GetComponent<MeshRenderer>().material.color = Color.blue;
        //     }
        // }
        #endregion

        #region UI
        //lightValueUI.text = "Light: " + (int)lightValue;
        #endregion
    }

    private bool JumpRay()
    {
        var isRightFootGrounded = false;
        var isLeftFootGrounded = false;
        
        if (Physics.Raycast(transform.position + FootOffset * transform.right, -transform.up, out var rightFootHitInfo, RayLength))
        {
            if (rightFootHitInfo.collider.CompareTag("Terrain"))
            {
                isRightFootGrounded = true;
            }
        }
        
        if (Physics.Raycast(transform.position + FootOffset * -transform.right, -transform.up, out var leftFootHitInfo, RayLength))
        {
            if (leftFootHitInfo.collider.CompareTag("Terrain"))
            {
                isLeftFootGrounded = true;
            }
        }
        return isLeftFootGrounded || isRightFootGrounded;
    }
}