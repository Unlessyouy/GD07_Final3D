using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CompanionControl : BasicControl
{
    Vector3 processedInput;
    Vector3 lineVector;

    public TextMeshProUGUI lightValueUI;
    public PlayerControl player;

    public bool following;
    protected override void Start()
    {
        base.Start();
        following = false;
    }
    protected override void Update()
    {
        base.Update();

        if (player.alive)
        {
            lineVector = player.transform.position - transform.position;
        }
        else
        {
            lineVector = Vector3.zero;
        }

        connected = (connected && player.connected);

        lightValueUI.text = "Companion\nLight: " + (int)lightValue;
    }
    private void FixedUpdate()
    {
        processedInput = player.transform.position - transform.position;

        float angleBetweenLineAndInput = Vector3.Angle(processedInput, lineVector);

        if (alive && following && processedInput.sqrMagnitude >= 3)
        {
            if (angleBetweenLineAndInput <= 15 && lineVector != Vector3.zero && processedInput.sqrMagnitude >= 5)
            {
                rb.MovePosition(transform.position + (movingSpeed * 2 * Time.deltaTime * processedInput.normalized));
                rb.useGravity = false;
            }
            else
            {
                rb.MovePosition(transform.position + (movingSpeed * Time.deltaTime * processedInput.normalized));
                rb.useGravity = true;
            }
        }
    }
}