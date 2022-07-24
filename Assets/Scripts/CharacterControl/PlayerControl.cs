using System.Collections;
using System.Collections.Generic;
using CharacterControl;
using UnityEngine;
using TMPro;

public class PlayerControl : BasicControl
{
    public bool CanPlayerInput { get; set; }

    public bool PullSon;
    
    [Header("Jump Related")]
    [SerializeField] private float JumpHeight;
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
        interactType = 1;
        CanPlayerInput = true;
    }
    protected override void Update()
    {
        #region Input & Movement

        if (CanPlayerInput)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");

            interactInput = Input.GetAxisRaw("Interact");
        }

        if (isClimbing)
        {
            processedInput = Vector3.up * verticalInput;
            anim.SetFloat("ClimbSpeed", verticalInput);
        }
        else
        {
            anim.SetFloat("ClimbSpeed", 0);
            processedInput = Vector3.forward * verticalInput + Vector3.right * horizontalInput;
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity -= GravityMultiplier * Time.deltaTime * transform.up;
        }

        if (!IsInRope)
        {
            CanJump = JumpRay();
        }

        #endregion

        PullSon = false;
        
        #region interactTimer

        if (interactInput == 1)
        {
            if (interactTimer <= 10)
            {
                interactTimer += Time.deltaTime;
            }

            if (interactTimer >= interactTime)
            {
                if (interactingObject != null)
                {
                    isInteracting = true;
                }
            }
        }
        else if (interactInput == 0)
        {
            if (interactTimer > 0 && interactTimer <= interactTime)
            {
                PullSon = true;
            }
            interactTimer = 0;
            isInteracting = false;
        }

        #endregion

        #region Interact With Objects

        if (isInteracting && interactingObject != null)
        {
            interactingObject.InteractTrigger(interactType, gameObject);
        }
        else if (!isInteracting && interactingObject != null)
        {
            interactingObject.DeInteractTrigger(interactType, gameObject);
        }

        #endregion

        if (verticalInput >= 0.25 && alive && CanJump && !canInteract && !isClimbing && !IsHoldingHands && !isInOcean)
        {
            rb.velocity = Vector3.up * JumpHeight;
            if (anim.GetBool("isGrounded"))
            {
                anim.SetTrigger("IsJumping");
                jumpEvent.Post(gameObject);
            }
        }
        
        base.Update();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Terrain"))
        {
            anim.ResetTrigger("IsJumping");
            landEvent.Post(gameObject);
        }
    }
}