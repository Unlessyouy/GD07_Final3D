using System.Collections;
using EventClass;
using Systems;
using UnityEngine;

namespace Interactable.MindPowerComponent
{
    public class JellyfishLightup_MPC : MonoBehaviour
    {
        [SerializeField] private float MindPowerDistance = 4f;
        [SerializeField] private ParticleSystem LitUpVFX;
        [SerializeField] private GameObject LitUpMesh;

        [Header("Time")] [SerializeField] private float ResetLightTime = 5f;
        [SerializeField] private float WaitForResetTime = 1f;
        
        private bool _isLitUp;
        
        public void OnMentalPowerActivate(Transform sonTransform)
        {
            if (_isLitUp)
            {
                return;
            }
            
            var distance = Vector3.Distance(sonTransform.position, transform.position);

            if (distance <= MindPowerDistance)
            {
                LitUp();
            }
        }

        private void LitUp()
        {
            if (LitUpVFX)
            {
                LitUpVFX.Play();
            }
            
            LitUpMesh.SetActive(true);
            
            _isLitUp = true;
            
            NewEventSystem.Instance.Publish(new JellyfishLightEvent(transform));

            StartCoroutine(ResetLight());
        }

        private IEnumerator ResetLight()
        {
            yield return new WaitForSeconds(WaitForResetTime);

            yield return new WaitForSeconds(ResetLightTime);
            
            LitUpMesh.SetActive(false);
            _isLitUp = false;
        }
    }
}