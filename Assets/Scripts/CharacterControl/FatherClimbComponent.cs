using System;
using UnityEngine;

namespace CharacterControl
{
    public class FatherClimbComponent : MonoBehaviour
    {
        [SerializeField] private float FullHeight;
        [SerializeField] private float EyeHeight;
        [SerializeField] private float GrabDistance = 0.4f;
        [SerializeField] private float ReachOffset = 0.7f;

        public bool IsHanging { get; set; } 

        [SerializeField] private Vector3 HangingOffset;
        private Rigidbody _rigidbody;
        private PlayerControl _playerControl;
        [SerializeField] private float JumpForce;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _playerControl = GetComponent<PlayerControl>();
        }

        private void Update()
        {
            Hang();

            if (!_playerControl.isClimbing && IsHanging && (Input.GetButtonDown("HangUp") || Mathf.Abs(Input.GetAxisRaw("HangUp") - 1) < 0.1f))
            {
                IsHanging = false;
                _rigidbody.isKinematic = false;
                _rigidbody.velocity = new Vector3(0, JumpForce, 0);
            }
        }

        private void Hang()
        {
            if (!IsFacingWall(out var eyeHitInfo) || IsHeadBlocked() || !IsLedge(out var ledgeHitInfo) ||
                _rigidbody.velocity.y > 0f || _playerControl.isClimbing) return;

            _rigidbody.velocity = Vector3.zero;

            transform.position += new Vector3((eyeHitInfo.distance * transform.right).x, -ledgeHitInfo.distance,
                0) + HangingOffset;

            IsHanging = true;
            _rigidbody.isKinematic = true;
        }

        private bool IsFacingWall(out RaycastHit eyeHitInfo)
        {
            var eyePosition = transform.position + EyeHeight * transform.up;
            Debug.DrawRay(eyePosition, transform.right * GrabDistance, Color.green);
            return Physics.Raycast(eyePosition, transform.right, out eyeHitInfo, GrabDistance) &&
                   eyeHitInfo.collider.CompareTag("Terrain");
        }

        private bool IsHeadBlocked()
        {
            var headPosition = transform.position + transform.up * FullHeight;
            Debug.DrawRay(headPosition, transform.right * GrabDistance, Color.green);
            return Physics.Raycast(headPosition, transform.right, out var headHitInfo, GrabDistance) &&
                   headHitInfo.collider.CompareTag("Terrain");
        }

        private bool IsLedge(out RaycastHit ledgeHitInfo)
        {
            var headPosition = transform.position + transform.up * FullHeight + ReachOffset * transform.right;
            Debug.DrawRay(headPosition, -transform.up * GrabDistance);
            return Physics.Raycast(headPosition, -transform.up, out ledgeHitInfo, GrabDistance) &&
                   ledgeHitInfo.collider.CompareTag("Terrain");
        }
    }
}