using UnityEngine;

namespace Mechanics
{
    public class WarmableDoor : WarmableComponent
    {
        protected override void CompletelyWarm()
        {
            gameObject.SetActive(false);
        }
    }
}