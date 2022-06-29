//using System;
//using CharacterControl;
//using UnityEngine;

//namespace Systems
//{
//    public class HoldHandsSingleton : MonoBehaviour
//    {
//        [SerializeField] private BasicControl Father;
//        [SerializeField] private FatherClimbComponent FatherClimbComponent;
//        [SerializeField] private BasicControl Son;
//        private float _horizontalInput;
        
//        public bool IsHoldingHands { get; set; }

//        private bool _isFatherInteract;
//        private bool _isSonInteract;
//        private bool _isCloseEnough;
//        [SerializeField] private float CloseDistance = 2f;

//        public static HoldHandsSingleton Instance;

//        private void Awake()
//        {
//            if (Instance != null && Instance != this)
//            {
//                Destroy(gameObject);
//            }
//            else
//            {
//                Instance = this;
//            }
//        }
        
//        private void Update()
//        {
//            _horizontalInput = Input.GetAxisRaw("Horizontal");
//            _isFatherInteract = Input.GetButton("HoldHandFather") || Math.Abs(Input.GetAxisRaw("HoldHandFather") - 1) < 0.1f;;
//            _isSonInteract = Input.GetButton("HoldHandSon") || Math.Abs(Input.GetAxisRaw("HoldHandSon") - 1) < 0.1f;
//            _isCloseEnough = Vector3.Distance(Father.transform.position, Son.transform.position) <= CloseDistance;

//            IsHoldingHands = _isSonInteract && _isFatherInteract && _isCloseEnough;
//            Father.IsHoldingHands = IsHoldingHands;
//            Son.IsHoldingHands = IsHoldingHands;
//        }

//        private void FixedUpdate()
//        {
//            if (IsHoldingHands && !Father.isClimbing && !Son.isClimbing)
//            {
//                var adjustmentMultiplier = Father.movingSpeed / Son.movingSpeed;
//                Father.Move(_horizontalInput);
//                Son.Move(_horizontalInput * adjustmentMultiplier);
//                return;
//            }

//            if (Father.controlled && !Father.isClimbing && !FatherClimbComponent.IsHanging)
//            {
//                Father.Move(_horizontalInput);
//                Son.Move(0);
//            }
//            if(Son.controlled && !Son.isClimbing)
//            {
//                Son.Move(_horizontalInput);
//                Father.Move(0);
//            }
//        }
//    }
//}
