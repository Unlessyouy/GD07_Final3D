using UnityEngine;

namespace Mechanics
{
    public class WarmableSnowball : WarmableComponent
    {
        [SerializeField] private GameObject Activated;
        [SerializeField] private GameObject BeforeActivated;
        [SerializeField] private GameObject BeforeCollision;
        protected override void CompletelyWarm()
        {
            BeforeActivated.SetActive(true);
            if (BeforeCollision)
            {
                BeforeCollision.transform.position = transform.position;
            }
            Activated.SetActive(false);
        }
    }
}