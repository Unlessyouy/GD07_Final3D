using System.Collections;
using System.Collections.Generic;
using CharacterControl;
using UnityEngine;
using TMPro;

public class PlayerControl : BasicControl
{
    [Header("ͬ�����ƵĿ���")]
    public bool synchronous;
    public MeshRenderer ColorMesh;
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
        ColorMesh.material.color = Color.green;
    }
    protected override void Update()
    {
        CanJump = JumpRay();
        
        #region Input & Movement
        horizontalInput = Input.GetAxisRaw("Horizontal");
        interactInput = Input.GetButtonDown("Interact");
        verticalInput = Input.GetAxisRaw("Vertical");
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
                    ColorMesh.material.color = Color.green;
                }
                else
                {
                    ColorMesh.material.color = Color.blue;
                }
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
        return isLeftFootGrounded || isRightFootGrounded;
    }
}