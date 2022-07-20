using System;
using CharacterControl;
using UnityEngine;

namespace Mechanics.LevelThree
{
    public class NewRopeDown : MonoBehaviour
    {
        [SerializeField] private float ChildClimbPositionX = -0.1f;
        [SerializeField] private float FatherClimbPositionX = -0.15f;

        private void OnTriggerStay(Collider other)
        {
            var climber = other.GetComponent<BasicControl>();
            if (!climber) return;
            climber.InRopeRadius();

            if (climber as CompanionControl)
            {
                if (Input.GetAxisRaw("Vertical B") >= 0.5f)
                {
                    climber.isClimbing = true;
                    climber.transform.position = new Vector3(transform.position.x + ChildClimbPositionX,
                        climber.transform.position.y, climber.transform.position.z);
                }

                if (Input.GetAxisRaw("Vertical B") <= -0.5f)
                {
                    climber.isClimbing = false;
                }
            }

            if (climber as PlayerControl)
            {
                if (Input.GetAxisRaw("Vertical") >= 0.5f)
                {
                    climber.isClimbing = true;
                    climber.transform.position = new Vector3(transform.position.x + FatherClimbPositionX,
                        climber.transform.position.y, climber.transform.position.z);
                }

                if (Input.GetAxisRaw("Vertical") <= -0.5f)
                {
                    climber.isClimbing = false;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var climber = other.GetComponent<BasicControl>();
            if (!climber) return;
            climber.OutRopeRadius();
        }
    }
}