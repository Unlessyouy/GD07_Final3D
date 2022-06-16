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
        protected override void Update()
        {
            base.Update();
            if (!actable || !canBeActed || interactedObject.GetComponent<CompanionControl>() == null) return;
            var companion = interactedObject.GetComponent<CompanionControl>();
            if ((!Input.GetKeyDown(KeyCode.RightControl) && interactInput != 1) || interactType != 2) return;
            canBeActed = false;
            
            NewEventSystem.Instance.Publish(new SwitchTriggerEvent(RicketyObjects));
        }
    }
}