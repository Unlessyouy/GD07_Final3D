using UnityEngine;

namespace Interactable.MindPowerComponent
{
    public class Snowball_MPC : global::MindPowerComponent
    {
        [SerializeField] private GameObject SnowballActivated;
        [SerializeField] private GameObject SnowballBefore;
        [SerializeField] private GameObject SnowballPositionAdjust;
        public override void MindPowerTrigger()
        {
            SnowballBefore.SetActive(false);
            SnowballActivated.transform.position = SnowballPositionAdjust.transform.position;
            SnowballActivated.SetActive(true);
        }
    }
}