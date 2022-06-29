using UnityEngine;

namespace CharacterControl
{
    public class CompanionControl : BasicControl
    {
        public bool hiding;

        protected override void Update()
        {
            interactInput = Input.GetAxisRaw("Interact B");

            base.Update();
        }
    }
}