using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicControl : MonoBehaviour
{
    protected Rigidbody rb;
    protected Animator anim;

    public float movingSpeed;
    [SerializeField] protected float ClimbSpeed = 5f;

    protected float horizontalInput;
    protected float verticalInput;
    protected Vector3 processedInput = new(0, 0, 0);

    public bool isInteracting;
    protected float interactInput;
    public InteractableObject interactingObject;
    public MindPowerComponent interactingMindPowerObject;

    protected float towardsY;
    [Header("Rotate")] public float rotateSpeed; //degree per second

    public bool alive;

    bool isMoving;
    [SerializeField] protected AK.Wwise.Event walkEvent;
    [SerializeField] protected AK.Wwise.Event runEvent;
    [SerializeField] protected AK.Wwise.Event landEvent;
    [SerializeField] protected AK.Wwise.Event jumpEvent;
    float walkingIntervalTimer = 0;
    [SerializeField] float walkingIntervalTime;

    public bool isClimbing;
    public bool onRopeTopEnd;
    public bool onRopeDownEnd;
    public bool IsHoldingHands;

    public bool isInOcean;

    public bool CanJump;
    public bool IsInRope;

    protected float interactTimer = 0;
    public float interactTime;

    protected int interactType; //1 = Father; 2 = Son;

    public bool IsInBounce = false;
    public bool IsHideInCoral = false;

    [SerializeField] private float FootOffset = 0.25f;
    [SerializeField] private float RayLength = 0.75f;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        alive = true;
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            Physics.gravity = new Vector3(0, -5);
            isInOcean = true;
            anim.SetBool("isInOcean", true);
        }
        else
        {
            isInOcean = false;
            anim.SetBool("isInOcean", false);
        }
    }
    protected virtual void Update()
    {
        #region Character Animation & Rotation

        anim.SetBool("isClimbing", isClimbing);

        if (alive && !isClimbing)
        {
            if (rb.velocity.x > 0.1)
            {
                towardsY = 270;
                anim.SetBool("isMoving", true);
                isMoving = true;
            }
            else if (rb.velocity.x < -0.1)
            {
                towardsY = 90;
                anim.SetBool("isMoving", true);
                isMoving = true;
            }
            else
            {
                anim.SetBool("isMoving", false);
                anim.SetFloat("MovingSpeed", 0);
                isMoving = false;
            }

            float rotateDifference = towardsY - transform.GetChild(0).rotation.eulerAngles.y;

            if (Mathf.Abs(rotateDifference) >= 2.5)
            {
                if (rotateDifference > 0 && rotateDifference < 180 || rotateDifference < -180)
                {
                    transform.GetChild(0).Rotate(0, rotateSpeed * Time.deltaTime, 0);
                }
                else
                {
                    transform.GetChild(0).Rotate(0, -rotateSpeed * Time.deltaTime, 0);
                }
            }
        }

        if (!isInOcean && !isClimbing)
        {
            rb.useGravity = !JumpRay();
        }

        if (isClimbing)
        {
            rb.useGravity = false;
        }

        #endregion

        if (isMoving)
        {
            walkingIntervalTimer += Time.deltaTime;
            if (walkingIntervalTimer >= walkingIntervalTime && !isClimbing)
            {
                walkingIntervalTimer = 0;
                runEvent.Post(gameObject);
            }
        }
    }
    protected virtual void FixedUpdate()
    {
        if (isClimbing)
        {
            rb.velocity = Vector3.zero;
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
        rb.velocity = IsInBounce
            ? new Vector3(movingSpeed * Time.deltaTime * directionX, rb.velocity.y)
            : new Vector3(movingSpeed * Time.deltaTime * directionX, movingSpeed * Time.deltaTime * directionY);
    }
    protected void Climb()
    {
        rb.MovePosition(transform.position + ClimbSpeed * Time.deltaTime * processedInput);
    }
    public void SetAnimMoveSpeed(float count)
    {
        anim.SetFloat("MovingSpeed", count);
    }
    protected void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<InteractableObject>())
        {
            interactingObject = other.GetComponent<InteractableObject>();
        }

        if (other.GetComponent<MindPowerComponent>())
        {
            interactingMindPowerObject = other.GetComponent<MindPowerComponent>();
        }
    }
    protected void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<InteractableObject>())
        {
            interactingObject = other.GetComponent<InteractableObject>();
        }

        if (other.GetComponent<MindPowerComponent>())
        {
            interactingMindPowerObject = other.GetComponent<MindPowerComponent>();
        }
    }
    protected void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<InteractableObject>())
        {
            interactingObject = null;
        }

        if (other.GetComponent<MindPowerComponent>())
        {
            interactingMindPowerObject = null;
        }
    }
    protected bool JumpRay()
    {
        var isRightFootGrounded = false;
        var isLeftFootGrounded = false;

        if (Physics.Raycast(transform.position + FootOffset * transform.right, -transform.up, out var rightFootHitInfo,
                RayLength))
        {
            if (rightFootHitInfo.collider.CompareTag("Terrain"))
            {
                isRightFootGrounded = true;
            }
        }

        if (Physics.Raycast(transform.position + FootOffset * -transform.right, -transform.up, out var leftFootHitInfo,
                RayLength))
        {
            if (leftFootHitInfo.collider.CompareTag("Terrain"))
            {
                isLeftFootGrounded = true;
            }
        }

        if (isLeftFootGrounded || isRightFootGrounded)
        {
            anim.SetBool("isGrounded", true);
        }
        else
        {
            anim.SetBool("isGrounded", false);
        }

        return isLeftFootGrounded || isRightFootGrounded;
    }
    protected bool SonJumpRay()
    {
        var isRightFootGrounded = false;
        var isLeftFootGrounded = false;

        if (Physics.Raycast(transform.position + FootOffset * transform.right, -transform.up, out var rightFootHitInfo,
                RayLength - 0.2f))
        {
            if (rightFootHitInfo.collider.CompareTag("Terrain"))
            {
                isRightFootGrounded = true;
            }
        }

        if (Physics.Raycast(transform.position + FootOffset * -transform.right, -transform.up, out var leftFootHitInfo,
                RayLength - 0.2f))
        {
            if (leftFootHitInfo.collider.CompareTag("Terrain"))
            {
                isLeftFootGrounded = true;
            }
        }

        if (isLeftFootGrounded || isRightFootGrounded)
        {
            anim.SetBool("isGrounded", true);
        }
        else
        {
            anim.SetBool("isGrounded", false);
        }

        return isLeftFootGrounded || isRightFootGrounded;
    }
    public Vector3 GetRigidbodyVelocity()
    {
        return rb.velocity;
    }
    public void SetRigidbodyVelocity(Vector3 velocity)
    {
        rb.velocity = velocity;
    }
    public void AddForceToRigidbody(Vector3 force, ForceMode forceMode)
    {
        rb.AddForce(force, forceMode);
    }
    public void InRopeRadius()
    {
        anim.ResetTrigger("IsJumping");
        CanJump = false;
        IsInRope = true;
    }
    public void OutRopeRadius()
    {
        IsInRope = false;
        anim.ResetTrigger("IsJumping");
    }
    public void PushObject()
    {
        anim.SetBool("IsPushing", true);
    }
    public void StopPushObject()
    {
        anim.SetBool("IsPushing", false);
    }
}