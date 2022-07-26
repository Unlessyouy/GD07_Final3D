using System;
using UnityEngine;

namespace Mechanics.LevelTwo
{
    public class BalancedDoor : MonoBehaviour
    {
        [SerializeField] private BalanceComponent Balance1;
        [SerializeField] private BalanceComponent Balance2;

        [SerializeField] private Transform Mesh;
        [SerializeField] private Transform TargetTransform;

        private Vector3 _originalPosition;
        private bool _isActivated;

        private void Start()
        {
            _originalPosition = Mesh.position;
        }

        private void Update()
        {
            _isActivated = Balance1.IsBalanced && Balance2.IsBalanced;

            if (_isActivated)
            {
                Mesh.position =
                    Vector3.MoveTowards(Mesh.position, TargetTransform.position, 1f * Time.deltaTime);
            }
            else
            {
                Mesh.position =
                    Vector3.MoveTowards(Mesh.position, _originalPosition, 1f * Time.deltaTime);
            }
        }
    }
}