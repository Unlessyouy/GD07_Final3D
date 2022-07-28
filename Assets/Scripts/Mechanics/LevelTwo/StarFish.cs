using UnityEngine;

namespace Mechanics.LevelTwo
{
    public class StarFish : MonoBehaviour
    {
        [SerializeField] private GameObject LightGO;

        public void LitUP()
        {
            if (!LightGO.activeSelf)
            {
                LightGO.SetActive(true);
            }
        }

        public void Distinguish()
        {
            if (LightGO.activeSelf)
            {
                LightGO.SetActive(false);
            }
        }
    }
}