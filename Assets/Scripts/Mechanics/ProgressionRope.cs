using UnityEngine;

namespace Mechanics
{
    public class ProgressionRope : ProgressionControlledObject
    {
        [SerializeField] private GameObject Rope;
        public override void BeActivated()
        {
            base.BeActivated();
            
            Rope.SetActive(true);
        }

        public override void Deactivated()
        {
            base.Deactivated();
            
            Rope.SetActive(false);
        }
    }
}