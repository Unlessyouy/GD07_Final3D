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

        private void OnTriggerStay(Collider other)
        {
            StopAllCoroutines();
            
            if (!HasSet)
            {
                ControlledObject.BeActivated();
                SetPlate();
            }

            StartCoroutine(ResetPlate());
        }

        private void SetPlate()
        {
            HasSet = true;
            Mesh.SetActive(false);
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