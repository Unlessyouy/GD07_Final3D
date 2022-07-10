using UnityEngine;

namespace Interactable.MindPowerComponent
{
    public class Snowball_MPC : global::MindPowerComponent
    {
        [SerializeField] private GameObject SnowballActivated;
        [SerializeField] private GameObject SnowballBefore;
        public override void MindPowerTrigger()
        {
            SnowballBefore.SetActive(false);
            SnowballActivated.SetActive(true);
        }
    }
}