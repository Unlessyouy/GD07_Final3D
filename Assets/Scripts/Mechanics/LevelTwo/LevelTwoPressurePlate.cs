using System.Collections;
using UnityEngine;

namespace Mechanics.LevelTwo
{
    public class LevelTwoPressurePlate : MonoBehaviour
    {
        [SerializeField] private Transform controlledObjectTransform;
        [SerializeField] private Transform TargetObjectTransform;
        [SerializeField] private GameObject Mesh;
        [SerializeField] private bool HasSet;

        private Vector3 _originalPosition;
        private Vector3 _targetPosition;

        private Vector3 _originalControlledPosition;

        private void Start()
        {
            _originalControlledPosition = controlledObjectTransform.position;
            _originalPosition = Mesh.transform.position;
            _targetPosition = _originalPosition - Vector3.up * 0.25f;
        }

        private void Update()
        {
            if (HasSet)
            {
                Mesh.transform.position =
                    Vector3.MoveTowards(Mesh.transform.position, _targetPosition, 1f * Time.deltaTime);
                controlledObjectTransform.position = Vector3.MoveTowards(controlledObjectTransform.position,
                    TargetObjectTransform.position, 1f * Time.deltaTime);
            }
            else
            {
                Mesh.transform.position =
                    Vector3.MoveTowards(Mesh.transform.position, _originalPosition, 1f * Time.deltaTime);
                controlledObjectTransform.position = Vector3.MoveTowards(controlledObjectTransform.position,
                    _originalControlledPosition, 1f * Time.deltaTime);
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

            Mesh.SetActive(true);
        }
    }
}