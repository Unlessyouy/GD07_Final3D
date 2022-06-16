using UnityEngine;

namespace CharacterControl
{
    public class CompanionControl : BasicControl
    {
        //[HideInInspector]
        public bool hiding;
        public MeshRenderer ColorMesh;
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
                ColorMesh.material.color = Color.green;
            }
            else
            {
                controlled = false;
                ColorMesh.material.color = Color.blue;
            }
        }
        protected override void Update()
        {
            base.Update();

            #region Other One
            if (otherOne.GetComponent<PlayerControl>() != null)
            {
                PlayerControl player = otherOne.GetComponent<PlayerControl>();

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

            processedInput = Input.GetAxisRaw("Vertical") * transform.up;
            
            #region E to switch control
            if (!companionSynchronous)
            {
                if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton8)) && otherOne.GetComponent<BasicControl>().alive)
                {
                    controlled = !controlled;
                    following = false;
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

        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }



        #region SonPreviousMovementInFixedUpdate

            // float angleBetweenLineAndInput = Vector3.Angle(processedInput, lineVector);
            //
            // if (alive && controlled)
            // {
            //     if (angleBetweenLineAndInput <= (speedUpAngle) / 2 && lineVector != Vector3.zero)
            //     {
            //         rb.MovePosition(transform.position + (movingSpeed * speedUpRatio * Time.deltaTime * processedInput.normalized));
            //     }
            //     else
            //     {
            //         rb.MovePosition(transform.position + (movingSpeed * Time.deltaTime * processedInput.normalized));
            //     }
            // }
            // else if (alive && following && (lineVector.magnitude >= 5)  )
            // {
            //     rb.MovePosition(transform.position + (movingSpeed * Time.deltaTime * processedInput.normalized));
            //     rb.useGravity = true;
            // }

            #endregion

    }
}