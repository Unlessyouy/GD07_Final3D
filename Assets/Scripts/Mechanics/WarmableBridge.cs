using UnityEngine;

namespace Mechanics
{
    public class WarmableBridge : WarmableComponent
    {
        private Collider _collider;

        protected override void Start()
        {
            base.Start();
            _collider = GetComponent<Collider>();
        }

        protected override void CompletelyWarm()
        {
            _collider.isTrigger = true;
            gameObject.SetActive(false);
        }

        protected override void CompletelyFrozen()
        {
            // if (_collider.isTrigger)
            // {
            //     _collider.isTrigger = false;
            // }
        }
    }
}