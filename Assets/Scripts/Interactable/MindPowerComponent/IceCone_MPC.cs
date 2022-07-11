using UnityEngine;

namespace Interactable.MindPowerComponent
{
    public class IceCone_MPC : global::MindPowerComponent
    {
        [SerializeField] private Rigidbody ParentRigidbody;
        
        public override void MindPowerTrigger()
        {
            ParentRigidbody.useGravity = true;
            ParentRigidbody.isKinematic = false;
        }
    }
}