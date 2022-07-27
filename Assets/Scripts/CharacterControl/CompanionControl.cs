using EventClass;
using Systems;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CharacterControl
{
    public class CompanionControl : BasicControl
    {
        [SerializeField] private ParticleSystem MindPowerVFX;
        [SerializeField] private float JumpHeight;

        private bool IsInOceanScene;
        protected override void Start()
        {
            base.Start();
            interactType = 2;
        }
        protected override void Update()
        {
            #region Input & Movement

            horizontalInput = Input.GetAxisRaw("Horizontal B");
            verticalInput = Input.GetAxisRaw("Vertical B");
            interactInput = Input.GetAxisRaw("Interact B");

            if (isClimbing)
            {
                if (onRopeTopEnd)
                {
                    verticalInput = -1;
                    processedInput = Vector3.up * verticalInput;
                }
                else if (onRopeDownEnd)
                {
                    verticalInput = 1;
                    processedInput = Vector3.up * verticalInput;
                }
                else
                {
                    processedInput = Vector3.up * verticalInput;
                }
                
                anim.SetFloat("ClimbSpeed", verticalInput);
            }
            else
            {
                anim.SetFloat("ClimbSpeed", 0);
                processedInput = Vector3.forward * verticalInput + Vector3.right * horizontalInput;
            }
            
            // if (!IsInRope)
            // {
            //     CanJump = SonJumpRay();
            // }

            #endregion

            #region InteractTimer

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
                    if (interactingMindPowerObject != null)
                    {
                        interactingMindPowerObject.MindPowerTrigger();
                    }

                    if (MindPowerVFX)
                    {
                       MindPowerVFX.Play(true);
                    }
                    
                    NewEventSystem.Instance.Publish(new MindPowerEvent(transform));
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
            
            if (verticalInput >= 0.25 && alive && CanJump && !isClimbing && !IsHoldingHands && !isInOcean && !IsInOceanScene)
            {
                rb.velocity = Vector3.up * JumpHeight;
                CanJump = false;
                if (anim.GetBool("isGrounded"))
                {
                    anim.SetTrigger("IsJumping");
                    jumpEvent.Post(gameObject);
                }
            }

            base.Update();

            if (isInOcean)
            {
                rb.useGravity = false;
            }
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Terrain"))
            {
                anim.SetBool("isGrounded", true);
                CanJump = true;
                if (!isClimbing)
                {
                    landEvent.Post(gameObject);
                }
            }
        }
        private void OnCollisionExit(Collision collision)
        {
            if (collision.collider.CompareTag("Terrain"))
            {
                anim.SetBool("isGrounded", false);
            }
        }
    }
}