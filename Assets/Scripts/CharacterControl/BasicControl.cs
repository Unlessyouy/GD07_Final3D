using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BasicControl : MonoBehaviour
{
    protected Rigidbody rb;
    protected Animator anim;

    protected Vector3 lineVector;

    public GameObject otherOne;

    public float movingSpeed;
    protected float horizontalInput;
    protected float verticalInput;
    protected float interactInput;

    protected Vector3 processedInput = new(0, 0, 0);

    protected float lightValue = 100;

    public bool alive;
    public bool controlled;
    protected bool lightNear;

    public bool isClimbing;
    public bool onRopeTop;

    [Header("¼ÓËÙÐ§¹û¼Ð½Ç")]
    public float speedUpAngle;
    [Header("¼ÓËÙÐ§¹û±¶ÂÊ")]
    public float speedUpRatio;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        alive = true;
        lightNear = false;
    }
    protected virtual void Update()
    {
        if (alive)
        {
            //if (lightNear)
            //{
            //    lightValue += 1f * Time.deltaTime;
            //}
            //else
            //{
            //    lightValue -= 5f * Time.deltaTime;
            //}

            if (lightValue >= 100)
            {
                lightValue = 100;
            }
            if (lightValue <= 0)
            {
                alive = false;
                GetComponent<MeshRenderer>().material.color = Color.red;
            }
        }
    }
    protected virtual void FixedUpdate()
    {
        float angleBetweenLineAndInput = Vector3.Angle(processedInput, lineVector);

        if (alive && controlled)
        {
            if (angleBetweenLineAndInput <= (speedUpAngle) / 2 && lineVector != Vector3.zero)
            {
                rb.MovePosition(transform.position + (movingSpeed * speedUpRatio * Time.deltaTime * processedInput.normalized));
            }
            else
            {
                rb.MovePosition(transform.position + (movingSpeed * Time.deltaTime * processedInput.normalized));
            }
        }
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<LightRadius>() != null)
        {
            lightNear = true;
        }
    }
    protected virtual void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<LightRadius>() != null)
        {
            lightNear = true;
        }
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<LightRadius>() != null)
        {
            lightNear = false;
        }
    }
    protected float CheckAngle(float Value)
    {
        float Angle = Value - 180;
        if (Angle > 0)
        {
            return Angle - 180;
        }
        return Angle + 180;
    }
}