using System.Collections;
using UnityEngine;

namespace Interactable.CommonInteractableObjects
{
    public class OnlyFatherInteractRock : InteractableObject
    {
        [SerializeField] private float Offset;
        private bool _isInteracted;
        private Rigidbody _rb;
        
        protected override void Start()
        {
            base.Start();

            _rb = GetComponent<Rigidbody>();
        }

        public override void InteractTrigger(int interactType, GameObject interactingCharacter)
        {
            if (interactType == 1)
            {
                StopAllCoroutines();

                _isInteracted = true;
                transform.position = interactingCharacter.transform.position -
                                     interactingCharacter.transform.forward * Offset;

                _rb.useGravity = false;
                StartCoroutine(StopInteract());
            }
        }

        private IEnumerator StopInteract()
        {
            yield return new WaitForSeconds(0.5f);
            _rb.useGravity = true;
            transform.parent = null;
        }

        public void DestroySelf()
        {
            gameObject.SetActive(false);
        }

        public float GetRockVelocitySqrMag()
        {
            return _rb.velocity.sqrMagnitude;
        }
    }
}