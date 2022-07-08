using UnityEngine;

namespace CharacterControl
{
    public class CompanionControl : BasicControl
    {
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

            #endregion

            #region interactTimer

            if (interactInput == 1)
            {
                interactTimer += Time.deltaTime;

                if (interactTimer >= interactTime)
                {
                    if (interactingObject != null)
                    {
                        interactingObject.InteractTrigger(interactType, gameObject);
                    }
                    interactTimer = 0;
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
                }
                interactTimer = 0;
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