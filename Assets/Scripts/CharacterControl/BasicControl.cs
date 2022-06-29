using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class BasicControl : MonoBehaviour
{
    protected Rigidbody rb;
    protected Animator anim;

    public float movingSpeed;
    [SerializeField] protected float ClimbSpeed = 5f;

    protected Vector3 processedInput = new(0, 0, 0);

    protected float interactInput;

    protected float towardsY;

    public bool alive;

    public bool isClimbing;
    public bool onRopeTop;
    public bool IsHoldingHands;

    public bool isInOcean;

    protected float interactTimer = 0;
    public float interactTime = 0.8f;

    protected virtual void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            isInOcean = true;
        }
        else
        {
            isInOcean = false;
        }
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        alive = true;
    }
    protected virtual void Update()
    {
        #region Character Animation & Rotation

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

        #endregion

        #region interactTimer

        if (interactInput == 1)
        {
            interactTimer += Time.deltaTime;
        }
        else if (interactInput == 0)
        {
            if (interactTimer >= interactTime)
            {
                Debug.Log("长按触发");
            }
            else if (interactTimer > 0 && interactTimer <= interactTime)
            {
                Debug.Log("短按触发");
            }
            interactTimer = 0;
        }

        #endregion
    }
    protected virtual void FixedUpdate()
    {
        if (isClimbing)
        {
            Climb();
        }

        if (!isClimbing && !isInOcean)
        {
            rb.useGravity = true;
        }
    }

    public void Move(float direction)
    {
        rb.velocity = new Vector3(movingSpeed * Time.deltaTime * direction, rb.velocity.y);
    }
    public void MoveInOcean(float directionX, float directionY)
    {
        rb.velocity = new Vector3(movingSpeed * Time.deltaTime * directionX, movingSpeed * Time.deltaTime * directionY);
    }

    protected void Climb()
    {
        rb.velocity = Vector3.zero;
        rb.MovePosition(transform.position + ClimbSpeed * Time.deltaTime * processedInput);
    }
}