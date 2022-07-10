using UnityEngine;

namespace Interactable.CommonInteractableObjects
{
    public class PushableObject : InteractableObject
    {
        [SerializeField] private Rigidbody PushTarget;

        public override void InteractTrigger(int interactType, GameObject interactingCharacter)
        {
            var pushSpeed = 0f;

            if (interactType == 1)
            {
                pushSpeed += 2 * Input.GetAxisRaw("Horizontal");
            }
            else
            {
                pushSpeed += 1 * Input.GetAxisRaw("Horizontal B");
            }


            PushTarget.MovePosition(transform.position + Vector3.right * (pushSpeed * Time.deltaTime));
        }
    }
}