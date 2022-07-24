using UnityEngine;

namespace Interactable.CommonInteractableObjects
{
    public class PushableObject : InteractableObject
    {
        [SerializeField] private GameObject PushTarget;



        public override void InteractTrigger(int interactType, GameObject interactingCharacter)
        {
            transform.parent.parent = interactingCharacter.transform;
        }
        public override void DeInteractTrigger(int interactType, GameObject interactingCharacter)
        {
            transform.parent.parent = null;
        }
    }
}