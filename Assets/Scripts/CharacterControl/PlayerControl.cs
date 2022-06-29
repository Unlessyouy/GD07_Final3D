using System.Collections;
using System.Collections.Generic;
using CharacterControl;
using UnityEngine;
using TMPro;

public class PlayerControl : BasicControl
{
    public bool CanJump { get; set; }
    [Header("Jump Related")]
    [SerializeField] private float FootOffset = 0.5f;
    [SerializeField] private float RayLength = 0.75f;
    [SerializeField] private float JumpHeight = 5f;
    [SerializeField] private float GravityMultiplier = 1.5f;
    
    [HideInInspector]
    public bool canInteract;

    protected override void Start()
    {
        base.Start();
        Cursor.lockState = CursorLockMode.Locked;
        if (isInOcean)
        {
            rb.useGravity = false;
        }
    }
    protected override void Update()
    {
        #region Input & Movement

        interactInput = Input.GetAxisRaw("Interact");

        if (rb.velocity.y < 0)
        {
            rb.velocity -= GravityMultiplier * Time.deltaTime * transform.up;
        }

        CanJump = JumpRay();

        if (alive && CanJump && !canInteract && !isClimbing && !IsHoldingHands && !isInOcean)
        {
            if (Input.GetButtonDown("Jump"))
            {
                rb.velocity = Vector3.up * JumpHeight;
            }
        }
        #endregion

        base.Update();
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

        if (isLeftFootGrounded || isRightFootGrounded)
        {
            anim.SetBool("isGrounded", true);
        }
        else
        {
            anim.SetBool("isGrounded", false);
        }

        return isLeftFootGrounded || isRightFootGrounded;
    }
}