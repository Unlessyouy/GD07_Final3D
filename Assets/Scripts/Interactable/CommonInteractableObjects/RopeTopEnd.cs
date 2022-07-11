using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeTopEnd : InteractableObject
{
    bool comeFromTop;
    BasicControl climber;
    [SerializeField]
    float JumpHeight;

    protected override void Start()
    {
        base.Start();
        actable = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.15f);
        Gizmos.DrawSphere(transform.position, 2f);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BasicControl>() != null)
        {
            climber = other.GetComponent<BasicControl>();
            climber.onRopeTopEnd = true;
            if (climber != null && climber.isClimbing == true && !comeFromTop)
            {
                climber.isClimbing = false;
                climber.GetComponent<Rigidbody>().velocity = Vector3.up * JumpHeight;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<BasicControl>() != null)
        {
            other.GetComponent<BasicControl>().onRopeTopEnd = false;
            comeFromTop = false;
        }
    }
    public override void InteractTrigger(int interactType, GameObject interactingCharacter)
    {
        if (actable)
        {
            if (interactingCharacter.GetComponent<BasicControl>() != null)
            {
                climber = interactingCharacter.GetComponent<BasicControl>();
                climber.onRopeTopEnd = true;
                if (climber != null && climber.isClimbing == true && !comeFromTop)
                {
                    climber.isClimbing = false;
                }
            }

            climber = interactingCharacter.GetComponent<BasicControl>();
            comeFromTop = true;

            if (!climber.isClimbing)
            {
                climber.isClimbing = true;
                climber.GetComponent<Rigidbody>().useGravity = false;
                climber.transform.position = transform.position;
            }
        }
    }
}