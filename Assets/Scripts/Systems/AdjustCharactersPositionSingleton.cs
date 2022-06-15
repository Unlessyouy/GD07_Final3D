using System;
using CharacterControl;
using UnityEngine;

namespace Systems
{
    public class AdjustCharactersPositionSingleton : MonoBehaviour
    {
        public static AdjustCharactersPositionSingleton Instance;

        private PlayerControl _father;
        private CompanionControl _son;
        
        private float _fatherPosZ;
        private float _sonPosZ;
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        private void Start()
        {
            _father = FindObjectOfType<PlayerControl>();
            _son = FindObjectOfType<CompanionControl>();

            _fatherPosZ = _father.transform.position.z;
            _sonPosZ = _son.transform.position.z;
        }

        private void Update()
        {
            if (_father.transform.position.z != _fatherPosZ)
            {
          
            }
        }
    }
}