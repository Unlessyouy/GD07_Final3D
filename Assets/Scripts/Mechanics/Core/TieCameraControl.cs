using CharacterControl;
using UnityEngine;

namespace Mechanics
{
    public class TieCameraControl : MonoBehaviour
    {
        [SerializeField] private Transform Player;
        [SerializeField] private Transform Companion;
        [SerializeField] private Cinemachine.CinemachineVirtualCamera CM1;

        private float _cameraPositionX;
        private float _cameraPositionY;
        private float _cameraPositionZ;

        float camChangeTimer = 0;//between 0 to 1;
        float targetCamFOV = 45;
        float tempCamFOV;

        private void LateUpdate()
        {
            _cameraPositionX = (Player.position.x + Companion.position.x) / 2;

            _cameraPositionY = (Player.position.y + Companion.position.y) / 2;

            _cameraPositionZ = (Player.position.z + Companion.position.z) / 2;

            transform.position = new Vector3(_cameraPositionX, _cameraPositionY, _cameraPositionZ);

            if (CM1.m_Lens.FieldOfView != targetCamFOV)
            {
                camChangeTimer += 3f * Time.deltaTime;//speed of FOV changing, 1 too slow, 3 bit too fast?
                CM1.m_Lens.FieldOfView = Mathf.Lerp(tempCamFOV, targetCamFOV, camChangeTimer);
            }
            else
            {
                camChangeTimer = 0;
                tempCamFOV = CM1.m_Lens.FieldOfView;
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                LengthenCameraFOV();
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                ShortenCameraFOV();
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                NormalizeCameraFOV();
            }
        }

        public void LengthenCameraFOV()
        {
            targetCamFOV = 60;
        }
        public void ShortenCameraFOV()
        {
            targetCamFOV = 30;
        }
        public void NormalizeCameraFOV()
        {
            targetCamFOV = 45;
        }
    }
}