using System;
using CharacterControl;
using UnityEngine;

namespace Systems
{
    public class SynchronousControlSingleton : MonoBehaviour
    {
        [SerializeField] private BasicControl Father;
        [SerializeField] private FatherClimbComponent FatherClimbComponent;
        [SerializeField] private BasicControl Son;
        private float _horizontalInput;
        private float _verticalInput;
        private float _rightHorizontalInput;
        private float _rightVerticalInput;

        public bool IsHoldingHands { get; set; }

        private bool _isFatherInteract;
        private bool _isSonInteract;
        private bool _isCloseEnough;
        [SerializeField] private float CloseDistance = 2f;

        public static SynchronousControlSingleton Instance;

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
        
        private void Update()
        {
            _horizontalInput = Input.GetAxisRaw("Horizontal");
            _rightHorizontalInput = Input.GetAxisRaw("Horizontal B");

            _verticalInput = Input.GetAxisRaw("Vertical");
            _rightVerticalInput = Input.GetAxisRaw("Vertical B");

            _isFatherInteract = Input.GetButton("HoldHandFather") || Math.Abs(Input.GetAxisRaw("HoldHandFather") - 1) < 0.1f;;
            _isSonInteract = Input.GetButton("HoldHandSon") || Math.Abs(Input.GetAxisRaw("HoldHandSon") - 1) < 0.1f;
            _isCloseEnough = Vector3.Distance(Father.transform.position, Son.transform.position) <= CloseDistance;

            IsHoldingHands = _isSonInteract && _isFatherInteract && _isCloseEnough;
            Father.IsHoldingHands = IsHoldingHands;
            Son.IsHoldingHands = IsHoldingHands;
        }

        private void FixedUpdate()
        {
            if (!Father.isClimbing && !FatherClimbComponent.IsHanging && !Father.isInOcean && Father.alive)
            {
                Father.Move(_horizontalInput);
            }
            else if (!Father.isClimbing && !FatherClimbComponent.IsHanging && Father.isInOcean && Father.alive)
            {
                Father.MoveInOcean(_horizontalInput, _verticalInput);
            }
            else
            {
                Father.Move(0);
            }


            if(!Son.isClimbing && !Son.isInOcean && Son.alive)
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
    }
}