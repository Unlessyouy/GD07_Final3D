using UnityEngine;

namespace CharacterControl
{
    public class CompanionControl : BasicControl
    {
        //[HideInInspector]
        public bool hiding;
        public MeshRenderer ColorMesh;

        public bool following;


        protected override void Start()
        {
            base.Start();

            following = false;
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
            
            #endregion

        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }
    }
}