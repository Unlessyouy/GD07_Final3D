using CharacterControl;
using UnityEngine;

namespace Mechanics
{
    public class PhantomPlatform : MonoBehaviour
    {
        private BoxCollider _meCollider;
        private BoxCollider _checkCollider;
        
        private void Start()
        {
            _meCollider = GetComponent<BoxCollider>();
            _checkCollider = gameObject.AddComponent<BoxCollider>();
            _checkCollider.size = _meCollider.size * 1.05f;
            _checkCollider.center = _meCollider.center;
            _checkCollider.isTrigger = true;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.GetComponent<CompanionControl>())
            {
                Physics.IgnoreCollision(_meCollider, other, true);
            }
        }
    }
}