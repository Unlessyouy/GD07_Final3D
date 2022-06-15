using CharacterControl;
using UnityEngine;

namespace Mechanics
{
    public class TieCameraControl : MonoBehaviour
    {
        public PlayerControl Player;
        public CompanionControl Companion;

        private void LateUpdate()
        {
            var cameraPositionX = (Player.transform.position.x + Companion.transform.position.x) / 2;
            var cameraPositionY = (Player.transform.position.y + Companion.transform.position.y) / 2;
            var cameraPositionZ = (Player.transform.position.z + Companion.transform.position.z) / 2;
            transform.position = new Vector3(cameraPositionX, cameraPositionY, cameraPositionZ);
        }
    }
}