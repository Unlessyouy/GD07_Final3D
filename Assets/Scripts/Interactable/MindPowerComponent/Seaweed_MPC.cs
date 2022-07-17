using System;
using System.Collections;
using CharacterControl;
using UnityEngine;

namespace Interactable.MindPowerComponent
{
    public class Seaweed_MPC : global::MindPowerComponent
    {
        [SerializeField] private Transform Mesh;
        [SerializeField] private float ResetTime = 5f;
        [SerializeField] private float WaitForResetTime = 1f;
        
        private bool _isInteracted;
        private Vector3 _meshOriginalUp;
        private CompanionControl _son;

        private void Start()
        {
            _son = FindObjectOfType<CompanionControl>();
            _meshOriginalUp = Mesh.up;
        }

        public override void MindPowerTrigger()
        {
            if (_isInteracted) return;

            var position = _son.transform.position;
            Mesh.up = position - Mesh.position;

            var distance = Vector3.Distance(position, transform.position);
            Mesh.localScale = new Vector3(1, distance, 1);

            _isInteracted = true;
            Mesh.GetComponent<Collider>().enabled = true;
            
            StartCoroutine(ResetSeaweed());
        }

        private IEnumerator ResetSeaweed()
        {
            yield return new WaitForSeconds(WaitForResetTime);
            var timer = 0f;
            var currentMeshScale = Mesh.localScale;
            while (timer <= 1f)
            {
                timer += Time.deltaTime / ResetTime;
                Mesh.localScale = Vector3.Lerp(currentMeshScale, Vector3.one, timer);
                yield return null;
            }

            Mesh.GetComponent<Collider>().enabled = false;
            Mesh.up = _meshOriginalUp;
            _isInteracted = false;
            yield return null;
        }
    }
}