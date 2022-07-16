using System;
using System.Collections;
using UnityEngine;

namespace Mechanics
{
    public class PressurePlate : MonoBehaviour
    {
        [SerializeField] private ProgressionControlledObject ControlledObject;
        [SerializeField] private GameObject Mesh;
        [SerializeField] private bool HasSet;

        private Vector3 _originalPosition;
        private Vector3 _targetPosition;

        private void Start()
        {
            _originalPosition = Mesh.transform.position;
            _targetPosition = _originalPosition - Vector3.up * 0.25f;
        }

        private void Update()
        {
            if (HasSet)
            {
                Mesh.transform.position = Vector3.MoveTowards(Mesh.transform.position, _targetPosition, 1f * Time.deltaTime);
            }
            else
            {
                Mesh.transform.position = Vector3.MoveTowards(Mesh.transform.position, _originalPosition, 1f * Time.deltaTime);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Terrain"))
            {
                return;
            }
            
            StopAllCoroutines();
            
            if (!HasSet && other.GetComponent<PressureableComponent>())
            {
                if (ControlledObject)
                {
                    ControlledObject.BeActivated();
                }
                SetPlate();
            }

            StartCoroutine(ResetPlate());
        }

        private void SetPlate()
        {
            HasSet = true;
            
        }

        private IEnumerator ResetPlate()
        {
            yield return new WaitForSeconds(0.2f);

            HasSet = false;
            
            ControlledObject.Deactivated();
            
            Mesh.SetActive(true);
        }
    }
}