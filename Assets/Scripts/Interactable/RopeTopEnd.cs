using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeTopEnd : Interactable
{
    PlayerControl player;
    private void Start()
    {
        canBeActed = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.15f);
        Gizmos.DrawSphere(transform.position, 2f);
    }
    protected override void Update()
    {
        base.Update();

        if (interactedObject != null && interactedObject.GetComponent<PlayerControl>() != null)
        {
            player = interactedObject.GetComponent<PlayerControl>();
        }

        if (actable && canBeActed && !player.isClimbing)
        {
            if ((Input.GetKeyDown(KeyCode.Space) || interactInput == 1) && interactType == 1)
            {
                player.isClimbing = true;
                player.GetComponent<Rigidbody>().useGravity = false;
                player.transform.position = transform.position;
            }
        }
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.GetComponent<PlayerControl>() != null)
        {
            if (player != null && player.isClimbing == true)
            {
                player.isClimbing = false;
                interactedObject.GetComponent<Rigidbody>().useGravity = true;
                interactedObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 500, 0));
            }
        }
    }
}