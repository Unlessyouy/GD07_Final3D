using CharacterControl;
using EventClass;
using Systems;
using UnityEngine;

namespace Mechanics
{
    public class TieCameraControl : MonoBehaviour
    {
        [SerializeField] private Cinemachine.CinemachineVirtualCamera CM1;

        [SerializeField] SynchronousControlSingleton CharacterControlSystem;

        [SerializeField] private Transform Player;
        [SerializeField] private Transform Companion;

        private float _cameraPositionX;
        private float _cameraPositionY;
        private float _cameraPositionZ;

        private float charactersHorizontalDistance;
        private float charactersVerticalDistance;

        public float maxCharactersHorizontalDistance;
        public float maxCharactersVerticalDistance;

        float camChangeTimer = 0;//between 0 to 1;
        float targetCamFOV = 45;
        float tempCamFOV;
        private void Update()
        {
            _cameraPositionX = (Player.position.x + Companion.position.x) / 2;

            _cameraPositionY = (Player.position.y + Companion.position.y) / 2;

            _cameraPositionZ = (Player.position.z + Companion.position.z) / 2;

            charactersHorizontalDistance = Mathf.Abs(Player.position.x - Companion.position.x);

            charactersVerticalDistance = Mathf.Abs(Player.position.y - Companion.position.y);

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

            if (charactersHorizontalDistance >= maxCharactersHorizontalDistance)
            {
                if (Player.position.x - Companion.position.x > 0)
                {
                    CharacterControlSystem.CanFatherRight = false;
                    CharacterControlSystem.CanSonLeft = false;
                }
                else
                {
                    CharacterControlSystem.CanFatherLeft = false;
                    CharacterControlSystem.CanSonRight = false;
                }
            }
            else
            {
                CharacterControlSystem.CanFatherRight = true;
                CharacterControlSystem.CanFatherLeft = true;
                CharacterControlSystem.CanSonRight = true;
                CharacterControlSystem.CanSonLeft = true;
            }

            //if (charactersVerticalDistance >= maxCharactersVerticalDistance)
            //{
            //    if (Player.position.y - Companion.position.y > 0)
            //    {
            //        CharacterControlSystem.CanFatherUp = false;
            //        CharacterControlSystem.CanSonDown = false;
            //    }
            //    else
            //    {
            //        CharacterControlSystem.CanFatherDown = false;
            //        CharacterControlSystem.CanSonUp = false;
            //    }
            //    NewEventSystem.Instance.Publish(new GameEndEvent(true));
            //}
            //else
            //{
            //    CharacterControlSystem.CanFatherUp = true;
            //    CharacterControlSystem.CanFatherDown = true;
            //    CharacterControlSystem.CanSonUp = true;
            //    CharacterControlSystem.CanSonDown = true;
            //}
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
        protected void BanGoLeftForFather(SynchronousControlSingleton synchronousControlSingleton)
        {
            if (synchronousControlSingleton._horizontalInput < 0)
            {
                synchronousControlSingleton._horizontalInput = 0;
            }
        }
        protected void BanGoLeftForSon(SynchronousControlSingleton synchronousControlSingleton)
        {
            if (synchronousControlSingleton._rightHorizontalInput < 0)
            {
                synchronousControlSingleton._rightHorizontalInput = 0;
            }
        }
        protected void BanGoRightForFather(SynchronousControlSingleton synchronousControlSingleton)
        {
            if (synchronousControlSingleton._horizontalInput > 0)
            {
                synchronousControlSingleton._horizontalInput = 0;
            }
        }
        protected void BanGoRightForSon(SynchronousControlSingleton synchronousControlSingleton)
        {
            if (synchronousControlSingleton._rightHorizontalInput > 0)
            {
                synchronousControlSingleton._rightHorizontalInput = 0;
            }
        }
        protected void BanGoUpForFather(SynchronousControlSingleton synchronousControlSingleton)
        {
            if (synchronousControlSingleton._verticalInput > 0)
            {
                synchronousControlSingleton._verticalInput = 0;
            }
        }
        protected void BanGoUpForSon(SynchronousControlSingleton synchronousControlSingleton)
        {
            if (synchronousControlSingleton._rightVerticalInput > 0)
            {
                synchronousControlSingleton._rightVerticalInput = 0;
            }
        }
        protected void BanGoDownForFather(SynchronousControlSingleton synchronousControlSingleton)
        {
            if (synchronousControlSingleton._verticalInput < 0)
            {
                synchronousControlSingleton._verticalInput = 0;
            }
        }
        protected void BanGoDownForSon(SynchronousControlSingleton synchronousControlSingleton)
        {
            if (synchronousControlSingleton._rightVerticalInput < 0)
            {
                synchronousControlSingleton._rightVerticalInput = 0;
            }
        }
    }
}