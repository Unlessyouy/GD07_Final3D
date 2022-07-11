using System;
using Progression;
using Systems;
using UnityEngine;

namespace Mechanics.Core
{
    public class CheckpointComponent : MonoBehaviour
    {
        public string LevelName;
        public int BeatNumber;
        [SerializeField] private GameObject LightGameObject;
        [SerializeField] private Transform RespawnPoint;

        private bool _isLit;

        private void OnTriggerEnter(Collider other)
        {
            if (!_isLit && other.GetComponent<BasicControl>())
            {
                LitUp();
                var levelInfo = new SaveLevelInfo(LevelName, BeatNumber);
                AppDataSystem.Save(levelInfo, "SavedLevelInfo");
            }
        }

        public void LitUp()
        {
            _isLit = true;
            LightGameObject.SetActive(true);
        }

        public Vector3 GetRespawnPointPosition()
        {
            return RespawnPoint.transform.position;
        }
    }
}