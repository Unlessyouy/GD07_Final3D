using Mechanics;
using UnityEngine;

namespace Interactable.MindPowerComponent
{
    public class WaterPumpSon_MPC : EventMPCBase
    {
        [SerializeField] private WaterPump WaterPump;
        
        public override void OnMentalPowerActivate(Transform sonTransform)
        {
            var distance = Vector3.Distance(sonTransform.position, transform.position);

            if (distance <= 2.7f)
            {
                WaterPump.AdjustFlowBySon();
            }
        }
    }
}