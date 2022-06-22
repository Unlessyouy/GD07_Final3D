using System;
using UnityEngine;

namespace Mechanics
{
    public class DangerousPlatform : MonoBehaviour
    {
        [SerializeField] private int Hp = 5;
        
        private void OnCollisionEnter(Collision collision)
        {
            if (Physics.Raycast((transform.position + transform.up), Vector3.right, transform.localScale.x / 2) ||
                Physics.Raycast(transform.position + transform.up, Vector3.left, transform.localScale.x / 2))
            {
                if (collision.gameObject.GetComponent<Rigidbody>())
                {
                    Hp -= (int) collision.gameObject.GetComponent<Rigidbody>().mass;

                    if (Hp <= 0)
                    {
                        Crack();
                    }
                }
            }
        }

        private void Crack()
        {
            gameObject.SetActive(false);
        }
    }
}