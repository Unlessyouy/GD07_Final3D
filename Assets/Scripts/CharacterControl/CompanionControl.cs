using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CompanionControl : BasicControl
{
    Vector3 processedInput;

    public TextMeshProUGUI lightValueUI;
    public Transform playerPosition;
    protected override void Start()
    {
        base.Start();
        rb = transform.parent.GetComponent<Rigidbody>();
    }
    protected override void Update()
    {
        base.Update();

        lightValueUI.text = "Companion\nLight: " + (int)lightValue;
    }
    private void FixedUpdate()
    {
        processedInput = playerPosition.position - transform.position;

        if (alive && connected && processedInput.sqrMagnitude >= 3)
        {
            rb.MovePosition(transform.position + (movingSpeed * Time.deltaTime * processedInput.normalized));
        }
    }
}