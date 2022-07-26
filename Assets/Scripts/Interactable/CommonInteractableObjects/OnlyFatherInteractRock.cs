using System.Collections;
using UnityEngine;

namespace Interactable.CommonInteractableObjects
{
    public class OnlyFatherInteractRock : InteractableObject
    {
        [SerializeField] private float Offset;
        [SerializeField] private Transform RockHolder;
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

                transform.position = RockHolder.position;

                _rb.useGravity = false;
                StartCoroutine(StopInteract());
            }
        }

        private IEnumerator StopInteract()
        {
            yield return new WaitForSeconds(1f);
            _rb.useGravity = true;
            transform.parent = null;
            _isInteracted = true;
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