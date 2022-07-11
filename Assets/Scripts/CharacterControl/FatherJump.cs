using System;
using System.Collections;
using Systems;
using UnityEngine;

namespace CharacterControl
{
    public class FatherJump : MonoBehaviour
    {
        private PlayerControl _playerControl;
        private Rigidbody _rigidbody;

        [SerializeField] private float JumpHeight = 2f;
        [SerializeField] private float JumpLow = 0.5f;
        [SerializeField] private float RayHeight = 1.7f;
        [SerializeField] private float JumpDistance = 0.5f;
        private RaycastHit _hitInfo;

        private bool isJump;

        private void Start()
        {
            _playerControl = GetComponent<PlayerControl>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (CanJump())
            {
                if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.S))
                {
                    isJump = true;
                }
            }
        }

        private void FixedUpdate()
        {
            if (isJump)
            {
                Jump();
            }
        }

        private void Jump()
        {
            isJump = false;

            transform.position = _hitInfo.collider.gameObject.transform.position;
            // _playerControl.canInteract = false;
            // SynchronousControlSingleton.Instance.CanFatherMove = false;
            // StartCoroutine(StartJump());
        }

        private IEnumerator StartJump()
        {
            var originalPos = transform.position;
            var newPos = new Vector3(transform.position.x, _hitInfo.point.y, transform.position.z);
            var t = 0f;
            _rigidbody.useGravity = false;
            _rigidbody.AddForce(JumpHeight * Vector3.up);


            yield return new WaitForSeconds(0.5f);
            _rigidbody.AddForce(900f * -transform.forward);
            yield return new WaitForSeconds(0.5f);
            _playerControl.canInteract = true;
            SynchronousControlSingleton.Instance.CanFatherMove = true;
            _rigidbody.useGravity = true;
        }

        private bool CanJump()
        {
            var highestPos = transform.position + RayHeight * transform.up;
            var lowestPos = transform.position + JumpLow * transform.up;

            Debug.DrawRay(transform.position, transform.up * RayHeight, Color.red);
            
            if (Physics.Raycast(transform.position, transform.up, out var hitInfo, RayHeight) && hitInfo.collider.CompareTag("Terrain"))
            {
                return false;
            }
            
            Debug.DrawRay(highestPos, -transform.forward * JumpDistance, Color.green);
            Debug.DrawRay(lowestPos, -transform.forward * JumpDistance, Color.green);
            return (Physics.Raycast(highestPos, -transform.forward, out _hitInfo, JumpDistance) &&
                    _hitInfo.collider.CompareTag("JumpPoint")) ||
                   (Physics.Raycast(lowestPos, -transform.forward, out _hitInfo, JumpDistance) &&
                    _hitInfo.collider.CompareTag("JumpPoint"));
        }
    }
}