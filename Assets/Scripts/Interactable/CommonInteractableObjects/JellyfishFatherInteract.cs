using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactable.CommonInteractableObjects
{
    public class JellyfishFatherInteract : InteractableObject
    {
        [SerializeField] private float Offset;
        private bool _isInteracted;
        
        public override void InteractTrigger(int interactType, GameObject interactingCharacter)
        {
            if (interactType == 1)
            {
                StopAllCoroutines();

                _isInteracted = true;
                transform.position = interactingCharacter.transform.position -
                                     interactingCharacter.transform.forward * Offset;

                StartCoroutine(StopInteract());
            }
        }

        private IEnumerator StopInteract()
        {
            yield return new WaitForSeconds(0.5f);
            transform.parent = null;
        }

        public void DestroySelf()
        {
            gameObject.SetActive(false);
        }
    }
}