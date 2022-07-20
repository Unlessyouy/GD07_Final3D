using UnityEngine;

namespace Mechanics.LevelThree
{
    public class NewRopeUp : MonoBehaviour
    {
        [SerializeField] private Transform TargetTransform;
        void OnTriggerEnter(Collider other)
        {
            var climber = other.GetComponent<BasicControl>();
            if (!climber) return;
            
            if (climber.isClimbing)
            {
                climber.isClimbing = false;
                climber.transform.position = new Vector3(TargetTransform.position.x, TargetTransform.position.y,
                    climber.transform.position.z);
            }
        }
    }
}