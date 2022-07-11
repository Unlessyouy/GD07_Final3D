using CharacterControl;
using UnityEngine;

namespace Mechanics
{
    public class TieCameraControl : MonoBehaviour
    {
        [SerializeField] private Transform Player;
        [SerializeField] private Transform Companion;

        private float _cameraPositionX;
        private float _cameraPositionY;
        private float _cameraPositionZ;

        private void LateUpdate()
        {
            _cameraPositionX = (Player.localPosition.x + Companion.localPosition.x) / 2;

            _cameraPositionY = (Player.localPosition.y + Companion.localPosition.y) / 2;

            _cameraPositionZ = (Player.localPosition.z + Companion.localPosition.z) / 2;

            transform.localPosition = new Vector3(_cameraPositionX, _cameraPositionY, _cameraPositionZ);
        }
    }
}