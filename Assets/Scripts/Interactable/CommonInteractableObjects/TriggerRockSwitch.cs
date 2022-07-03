using System.Collections.Generic;
using CharacterControl;
using EventClass;
using HackMan.Scripts.Systems;
using Mechanics;
using UnityEngine;

namespace Interactable
{
    public class TriggerRockSwitch : InteractableObject
    {
        [SerializeField] private List<TriggerObject> RicketyObjects;
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0, 1, 0, 0.15f);
            Gizmos.DrawSphere(transform.position, 1.875f);
        }
        public override void InteractTrigger(int interactType, GameObject interactingCharacter)
        {
            if (interactType == 2 && actable)
            {
                actable = false;
                NewEventSystem.Instance.Publish(new SwitchTriggerEvent(RicketyObjects));
            }
        }
    }
}