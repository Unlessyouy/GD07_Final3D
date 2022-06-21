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

    protected float towardsY;

    public bool alive;
    public bool controlled;

    public bool isClimbing;
    public bool onRopeTop;
    public bool IsHoldingHands;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        alive = true;
    }
    protected virtual void Update()
    {
        anim.SetBool("isClimbing", isClimbing);

        if (alive)
        {
            if (rb.velocity.x > 1)
            {
                towardsY = 270;
                anim.SetBool("isWalking", true);
            }
            else if (rb.velocity.x < -1)
            {
                towardsY = 90;
                anim.SetBool("isWalking", true);
            }
            else
            {
                anim.SetBool("isWalking", false);
            }
            float rotateDifference = towardsY - transform.rotation.eulerAngles.y;

            if (Mathf.Abs(rotateDifference) >= 2.5)
            {
                if (rotateDifference > 0 && rotateDifference < 180 || rotateDifference < -180)
                {
                    transform.Rotate(0, 2, 0);
                }
                else
                {
                    transform.Rotate(0, -2, 0);
                }
            }
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
}