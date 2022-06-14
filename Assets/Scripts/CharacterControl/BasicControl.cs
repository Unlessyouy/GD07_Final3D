using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class BasicControl : MonoBehaviour
{
    protected Rigidbody rb;
    protected Animator anim;

    protected Vector3 lineVector;

    public GameObject otherOne;

    public float movingSpeed;
    [SerializeField] protected float ClimbSpeed = 5f;
    protected float horizontalInput;
    protected float verticalInput;
    protected bool interactInput;

    protected Vector3 processedInput = new(0, 0, 0);



    public bool alive;
    public bool controlled;


    public bool isClimbing;
    public bool onRopeTop;
    public bool IsHoldingHands;

    #region PreviousLight

    // protected bool lightNear;
    // protected float lightValue = 100;
    // [Header("¼ÓËÙÐ§¹û¼Ð½Ç")]
    // public float speedUpAngle;
    // [Header("¼ÓËÙÐ§¹û±¶ÂÊ")]
    // public float speedUpRatio;

    #endregion

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        alive = true;
    }
    protected virtual void Update()
    {
        if (alive)
        {
            #region LightRelated

            //if (lightNear)
            //{
            //    lightValue += 1f * Time.deltaTime;
            //}
            //else
            //{
            //    lightValue -= 5f * Time.deltaTime;
            //}

            // if (lightValue >= 100)
            // {
            //     lightValue = 100;
            // }
            // if (lightValue <= 0)
            // {
            //     alive = false;
            //     GetComponent<MeshRenderer>().material.color = Color.red;
            // }

            #endregion
        }
    }
    protected virtual void FixedUpdate()
    {
        if (isClimbing)
        {
            Climb();
        }

        if (!isClimbing)
        {
            rb.useGravity = true;
        }
        #region PreviousMovement

        // float angleBetweenLineAndInput = Vector3.Angle(processedInput, lineVector);
        //
        // if (alive && controlled)
        // {
        //     if (angleBetweenLineAndInput <= (speedUpAngle) / 2 && lineVector != Vector3.zero)
        //     {
        //         rb.MovePosition(transform.position + (movingSpeed * speedUpRatio * Time.deltaTime * processedInput.normalized));
        //     }
        //     else
        //     {
        //         rb.MovePosition(transform.position + (movingSpeed * Time.deltaTime * processedInput.normalized));
        //     }
        // }

        #endregion
    }

    public void Move(float direction)
    {
        rb.velocity = new Vector3(movingSpeed * Time.deltaTime * direction, rb.velocity.y);
    }
    
    protected void Climb()
    {
        rb.velocity = Vector3.zero;
        rb.MovePosition(transform.position + ClimbSpeed * Time.deltaTime * processedInput);
    }

    #region Light

    // protected virtual void OnTriggerEnter(Collider other)
    // {
    //     if (other.GetComponent<LightRadius>() != null)
    //     {
    //         lightNear = true;
    //     }
    // }
    // protected virtual void OnTriggerStay(Collider other)
    // {
    //     if (other.GetComponent<LightRadius>() != null)
    //     {
    //         lightNear = true;
    //     }
    // }
    // protected virtual void OnTriggerExit(Collider other)
    // {
    //     if (other.GetComponent<LightRadius>() != null)
    //     {
    //         lightNear = false;
    //     }
    // }
    // protected float CheckAngle(float Value)
    // {
    //     float Angle = Value - 180;
    //     if (Angle > 0)
    //     {
    //         return Angle - 180;
    //     }
    //     return Angle + 180;
    // }

    #endregion

}