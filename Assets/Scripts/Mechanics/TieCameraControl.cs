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
            _cameraPositionX = (Player.position.x + Companion.position.x) / 2;

            // if (Mathf.Abs(Player.position.y - Companion.position.y) >= YModifier)
            // {
            //     var cameraUpPos = YModifier / 2 + 2 * Mathf.Min(Player.position.y, Companion.position.y);
            //
            //     _cameraPositionY = cameraUpPos;
            // }
            // else
            // {
                _cameraPositionY = (Player.position.y + Companion.position.y) / 2;
            // }

            _cameraPositionZ = (Player.position.z + Companion.position.z) / 2;
            transform.position = new Vector3(_cameraPositionX, _cameraPositionY, _cameraPositionZ);
        }
    }
}