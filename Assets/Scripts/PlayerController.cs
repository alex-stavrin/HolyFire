using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    InputAction moveAction;
    InputAction jumpAction;

    [SerializeField] float mouseLookSensitivity;
    [SerializeField] float moveSpeed;
    [SerializeField] float maxVelocityMag;
    [SerializeField] float defaultLinearDamping = 6.5f;
    [SerializeField] float airResistance = 0.4f;
    [SerializeField] float jumpForce;
    [SerializeField] float jumpCooldown = 0.4f;
    [SerializeField] Transform cameraTransform;
    [SerializeField] float cameraLag = 0.3f;
    [SerializeField] Animator animator;
    [SerializeField] float additionalGravity = 20f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float coyoteDelay = 1f;

    private Rigidbody _rigidbody;
    private CapsuleCollider _capsuleCollider;

    Vector2 moveValue;

    float jumpTimer = 0;

    private Vector3 cameraVelocity;
    private float cameraOffsetZ;
    private float cameraOffsetY;

    private float coyoteTimer;

    public Vector3 lastCheckpoint;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Player/Move");
        jumpAction = InputSystem.actions.FindAction("Player/Jump");

        _rigidbody = GetComponent<Rigidbody>();
        _capsuleCollider = GetComponent<CapsuleCollider>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cameraOffsetZ = Mathf.Abs(cameraTransform.transform.position.z - transform.position.z);
        cameraOffsetY = Mathf.Abs(cameraTransform.transform.position.y - transform.position.y);

        lastCheckpoint = transform.position;
    }

    void Update()
    {
        Input();
        LimitVelocity();
        JumpLogic();

        Vector3 xzVelocity = new Vector3(_rigidbody.linearVelocity.x, 0, _rigidbody.linearVelocity.z);
        animator.SetBool("walking", IsGrounded() && xzVelocity.magnitude > 0.25);
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void LateUpdate()
    {
        Vector3 cameraTargetPosition = new Vector3(transform.position.x, transform.position.y + cameraOffsetY, transform.position.z - cameraOffsetZ);
        cameraTransform.position = Vector3.SmoothDamp(cameraTransform.transform.position, cameraTargetPosition, ref cameraVelocity, cameraLag);
    }

    private void Input()
    {
        moveValue = moveAction.ReadValue<Vector2>();
    }

    private void MovePlayer()
    {

        Vector3 movementDirection =  transform.forward * moveValue.y + transform.right * moveValue.x;
        movementDirection.Normalize();

        _rigidbody.AddForce(movementDirection * moveSpeed);
        
        if(!IsGrounded())
        {
            Vector3 linearVelocityXZ = new Vector3(_rigidbody.linearVelocity.x, 0, _rigidbody.linearVelocity.z);
            _rigidbody.AddForce(-linearVelocityXZ * airResistance);

            // add some more gravity
            _rigidbody.AddForce(0, -additionalGravity, 0, ForceMode.Acceleration);
        }
    }

    void LimitVelocity()
    {
        Vector3 xzVelocity = new Vector3(_rigidbody.linearVelocity.x, 0, _rigidbody.linearVelocity.z);
        if (xzVelocity.magnitude > moveSpeed)
        {
            xzVelocity = xzVelocity.normalized * moveSpeed;
            _rigidbody.linearVelocity = new Vector3(xzVelocity.x, _rigidbody.linearVelocity.y, xzVelocity.z);
        }
    }

    private bool IsGrounded()
    {
        RaycastHit hit;
        return Physics.Raycast(transform.position, -transform.up, out hit, (_capsuleCollider.height / 2) + 0.1f, groundLayer);
    }

    private void JumpLogic()
    {
        jumpTimer -= Time.deltaTime;
        coyoteTimer -= Time.deltaTime;
        if (IsGrounded())
        {
            coyoteTimer = coyoteDelay;
            _rigidbody.linearDamping = defaultLinearDamping;

            // default friction stuff
            _capsuleCollider.material.staticFriction = 0.6f;
            _capsuleCollider.material.dynamicFriction = 0.6f;
            _capsuleCollider.material.frictionCombine = PhysicsMaterialCombine.Average;
        }
        else
        {
            _rigidbody.linearDamping = 0;

            // remove any friction so we dont stuck to walls
            _capsuleCollider.material.staticFriction = 0.0f;
            _capsuleCollider.material.dynamicFriction = 0.0f;
            _capsuleCollider.material.frictionCombine = PhysicsMaterialCombine.Minimum;
        }

        if (jumpAction.IsPressed() && coyoteTimer >= 0 && jumpTimer <= 0)
        {
            jumpTimer = jumpCooldown;
            _rigidbody.linearVelocity = new Vector3(_rigidbody.linearVelocity.x, 0, _rigidbody.linearVelocity.z);
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    public void GoToCheckpoint()
    {
        _rigidbody.position = lastCheckpoint;
        _rigidbody.linearVelocity = Vector3.zero;
    }

    public void Bounce(float bounceForce)
    {
        _rigidbody.linearVelocity = new Vector3(_rigidbody.linearVelocity.x, 0, _rigidbody.linearVelocity.z);
        _rigidbody.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
    }
}
