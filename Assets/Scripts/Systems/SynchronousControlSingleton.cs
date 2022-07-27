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
        public float _horizontalInput;//left = -1; right = 1;
        public float _verticalInput;
        public float _rightHorizontalInput;
        public float _rightVerticalInput;

        public bool IsHoldingHands;

        public bool CanSonMove;
        public bool CanFatherMove;

        [HideInInspector] public bool CanSonLeft;
        [HideInInspector] public bool CanSonRight;
        //[HideInInspector] public bool CanSonUp;
        //[HideInInspector] public bool CanSonDown;

        [HideInInspector] public bool CanFatherLeft;
        [HideInInspector] public bool CanFatherRight;
        //[HideInInspector] public bool CanFatherUp;
        //[HideInInspector] public bool CanFatherDown;


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
                if (!CanSonLeft)
                {
                    if (_rightHorizontalInput < 0)
                    {
                        _rightHorizontalInput = 0;
                    }
                }
                else if (!CanSonRight)
                {
                    if (_rightHorizontalInput > 0)
                    {
                        _rightHorizontalInput = 0;
                    }
                }
                //else if (!CanSonUp)
                //{
                //    if (_rightVerticalInput > 0)
                //    {
                //        _rightVerticalInput = 0;
                //    }
                //}
                //else if (!CanSonDown)
                //{
                //    if (_rightVerticalInput < 0)
                //    {
                //        _rightVerticalInput = 0;
                //    }
                //}
            }

            if (CanFatherMove)
            {
                _horizontalInput = Input.GetAxisRaw("Horizontal");
                _verticalInput = Input.GetAxisRaw("Vertical");
                if (!CanFatherLeft)
                {
                    if (_horizontalInput < 0)
                    {
                        _horizontalInput = 0;
                    }
                }
                else if (!CanFatherRight)
                {
                    if (_horizontalInput > 0)
                    {
                        _horizontalInput = 0;
                    }
                }
                //else if (!CanFatherUp)
                //{
                //    if (_verticalInput > 0)
                //    {
                //        _verticalInput = 0;
                //    }
                //}
                //else if (!CanFatherDown)
                //{
                //    if (_verticalInput < 0)
                //    {
                //        _verticalInput = 0;
                //    }
                //}
            }

            _isFatherInteract = Input.GetButton("HoldHandFather") ||
                                Math.Abs(Input.GetAxisRaw("HoldHandFather") - 1) < 0.1f;

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
            if (!Father.isClimbing && !Father.isInOcean && Father.alive)
            {
                Father.Move(_horizontalInput);
            }
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