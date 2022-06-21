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
            if (!((!Input.GetKeyDown(KeyCode.RightControl) && interactInput != 1) || interactType != 2))
            {
                canBeActed = false;
                if (interactedObject.GetComponent<Animator>() != null)
                {
                    interactedObject.GetComponent<Animator>().SetBool("isInteracting", true);
                    Invoke("ExitInteracting", interactTime);
                }
            }
            
            NewEventSystem.Instance.Publish(new SwitchTriggerEvent(RicketyObjects));
        }
    }
}