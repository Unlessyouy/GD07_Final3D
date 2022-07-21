using System;
using CharacterControl;
using UnityEngine;

namespace Systems
{
    public class SynchronousControlSingleton : MonoBehaviour
    {
        [SerializeField] private BasicControl Father;

        //[SerializeField] private FatherClimbComponent FatherClimbComponent;
        [SerializeField] private BasicControl Son;
        private float _horizontalInput;
        private float _verticalInput;
        private float _rightHorizontalInput;
        private float _rightVerticalInput;

        public bool IsHoldingHands { get; set; }
        public bool CanSonMove { get; set; }
        public bool CanFatherMove { get; set; }

        private bool _isFatherInteract;
        private bool _isSonInteract;
        private bool _isCloseEnough;
        [SerializeField] private float CloseDistance = 2f;

        public static SynchronousControlSingleton Instance;

        private const float SON_SPEED = 100f;
        private const float FATHER_SPEED = 125f;

        public bool IsInteractWithOceanObject = false;

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
            CanFatherMove = true;
            CanSonMove = true;
        }

        private void Update()
        {
            if (CanSonMove)
            {
                _rightHorizontalInput = Input.GetAxisRaw("Horizontal B");
                _rightVerticalInput = Input.GetAxisRaw("Vertical B");
            }

            if (CanFatherMove)
            {
                _horizontalInput = Input.GetAxisRaw("Horizontal");
                _verticalInput = Input.GetAxisRaw("Vertical");
            }

            _isFatherInteract = Input.GetButton("HoldHandFather") ||
                                Math.Abs(Input.GetAxisRaw("HoldHandFather") - 1) < 0.1f;
            ;
            _isSonInteract = Input.GetButton("HoldHandSon") || Math.Abs(Input.GetAxisRaw("HoldHandSon") - 1) < 0.1f;
            _isCloseEnough = Vector3.Distance(Father.transform.position, Son.transform.position) <= CloseDistance;

            IsHoldingHands = _isSonInteract && _isFatherInteract && _isCloseEnough;
            Father.IsHoldingHands = IsHoldingHands;
            Son.IsHoldingHands = IsHoldingHands;

            if (_isCloseEnough)
            {
                Son.movingSpeed = FATHER_SPEED;
                if (!IsInteractWithOceanObject && Father.isInOcean)
                {
                    Son.isInOcean = true;
                }
            }
            else
            {
                Son.movingSpeed = SON_SPEED;
                if (!IsInteractWithOceanObject && Father.isInOcean)
                {
                    Son.isInOcean = false;
                }
            }

            Son.SetAnimMoveSpeed(Mathf.Abs(_rightHorizontalInput));
            Father.SetAnimMoveSpeed(Mathf.Abs(_horizontalInput));
        }

        private void FixedUpdate()
        {
            //if (!Father.isClimbing && !FatherClimbComponent.IsHanging && !Father.isInOcean && Father.alive)
            if (!Father.isClimbing && !Father.isInOcean && Father.alive)
            {
                Father.Move(_horizontalInput);
            }
            //else if (!Father.isClimbing && !FatherClimbComponent.IsHanging && Father.isInOcean && Father.alive)
            else if (!Father.isClimbing && Father.isInOcean && Father.alive)
            {
                Father.MoveInOcean(_horizontalInput, _verticalInput);
            }
            else
            {
                Father.Move(0);
            }

            if (!Son.isClimbing && !Son.isInOcean && Son.alive)
            {
                Son.Move(_rightHorizontalInput);
            }
            else if (!Son.isClimbing && Son.isInOcean && Son.alive)
            {
                Son.MoveInOcean(_rightHorizontalInput, _rightVerticalInput);
            }
            else
            {
                Son.Move(0);
            }
        }

        public void Freeze()
        {
            CanSonMove = false;
            CanFatherMove = false;
            _horizontalInput = 0;
            _verticalInput = 0;
            _rightHorizontalInput = 0;
            _rightVerticalInput = 0;
        }

        public Transform GetSonTrans()
        {
            return Son.transform;
        }
        
        public Transform GetFatherTrans()
        {
            return Father.transform;
        }
    }
}