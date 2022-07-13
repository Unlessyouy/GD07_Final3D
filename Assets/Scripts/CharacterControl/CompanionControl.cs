using UnityEngine;

namespace CharacterControl
{
    public class CompanionControl : BasicControl
    {
        [SerializeField] private ParticleSystem MindPowerVFX;
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
            }
            else
            {
                processedInput = Vector3.forward * verticalInput + Vector3.right * horizontalInput;
            }

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

                    MindPowerVFX.transform.position = transform.position;
                    MindPowerVFX.Play();
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

            #endregion

            base.Update();
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Terrain"))
            {
                anim.SetBool("isGrounded", true);
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