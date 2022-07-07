using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeTopEnd : InteractableObject
{
    BasicControl climber;
    [SerializeField] private int TopForce = 600;

    private void Start()
    {
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
            climber.onRopeTop = true;
            if (climber != null && climber.isClimbing == true)
            {
                climber.isClimbing = false;
                climber.GetComponent<Rigidbody>().AddForce(Vector3.up * TopForce);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<BasicControl>() != null)
        {
            other.GetComponent<BasicControl>().onRopeTop = false;
        }
    }
    public override void InteractTrigger(int interactType, GameObject interactingCharacter)
    {
        if (actable)
        {
            climber = interactingCharacter.GetComponent<BasicControl>();

            if (!climber.isClimbing)
            {
                climber.isClimbing = true;
                climber.GetComponent<Rigidbody>().useGravity = false;
                climber.transform.position = transform.position;
            }
        }
    }
}