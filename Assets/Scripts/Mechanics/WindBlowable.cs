using UnityEngine;

namespace Mechanics
{
    [RequireComponent(typeof(Rigidbody))]
    public class WindBlowable : MonoBehaviour
    {
        public bool IsBlowable = true;
    }
}