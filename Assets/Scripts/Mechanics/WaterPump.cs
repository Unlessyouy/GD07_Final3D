using System;
using System.Collections;
using UnityEngine;

namespace Mechanics
{
    public class WaterPump : MonoBehaviour
    {
        [Header("FlowZone")] [SerializeField] private float Intensity;
        [SerializeField] private Vector3 Direction;

        [Header("Player Adjust Flow")] [SerializeField]
        private float FatherAdjustSpeed = 1f;

        [SerializeField] private float SonAdjustAmount = 0.5f;

        [SerializeField] private Vector3 MaxFlowScale;
        private Vector3 _originalScale;
        [SerializeField] private float ResetSpeed = 0.25f;

        [Header("Don't Change this")] [SerializeField]
        private Transform RootTransform;

        private void Start()
        {
            _originalScale = RootTransform.localScale;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0, 1, 0, 0.25f);
            Gizmos.DrawCube(transform.position, transform.lossyScale);
            Gizmos.DrawLine(transform.position, transform.position + Direction * 5f);
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.GetComponent<WindBlowable>() && other.GetComponent<WindBlowable>().IsBlowable)
            {
                var turtle = other.GetComponent<TurtleController>();
                if (turtle)
                {
                    turtle.BlowByWater(Intensity * Direction);
                }
            }
        }

        public void AdjustFlowByFather()
        {
            StopAllCoroutines();
            if (RootTransform.localScale.sqrMagnitude < MaxFlowScale.sqrMagnitude)
            {
                RootTransform.localScale += Direction * (FatherAdjustSpeed * Time.deltaTime);
            }
            StartCoroutine(ResetFlow());
        }
        
        public void AdjustFlowBySon()
        {
            StopAllCoroutines();
            if (RootTransform.localScale.sqrMagnitude < MaxFlowScale.sqrMagnitude)
            {
                RootTransform.localScale += Direction * SonAdjustAmount;
            }
            StartCoroutine(ResetFlow());
        }

        private IEnumerator ResetFlow()
        {
            while (RootTransform.localScale.sqrMagnitude > _originalScale.sqrMagnitude)
            {
                RootTransform.localScale =
                    Vector3.MoveTowards(RootTransform.localScale, _originalScale, ResetSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
}