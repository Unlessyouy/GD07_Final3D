using Mechanics;
using UnityEngine;

namespace Interactable.CommonInteractableObjects
{
    public class WaterPumpFatherInteractable : InteractableObject
    {
        [SerializeField] private WaterPump WaterPump;

        public override void InteractTrigger(int interactType, GameObject interactingCharacter)
        {
            if (interactType == 1)
            {
                WaterPump.AdjustFlowByFather();
            }
        }
    }
}