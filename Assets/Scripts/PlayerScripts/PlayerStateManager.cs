
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateManager : MonoBehaviour
{
    // Current State of the player
    PlayerBaseState currentState;
    
    // Player's defined states as instances
    public StandingState standState;
    public CrouchingState crouchState;
    public JumpState jumpState;
    public AerialState aerialState;

    // Player's components
    [Header("Player Components")]
    public Rigidbody rigid;
    public Transform trans;

    public Transform PlayerCamera;

    // Player settings
    [Header("Player Settings")]
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    public float aerialSpeed = 2.5f;

    [Header("Player Jump Settings")]
    public float jumpHeight = 3f;
    public bool isUnderObject = false;

    public float groundGravity = 50f;
    public float aerialGravity = 25f;

    public float maxStepHeight = 0.25f;
    public float maxSlopeAngle = 45f;
    public float stepEdge = 0f;
    public Vector3 groundNormal = Vector3.up;
    public Vector3 foot2Ground = Vector3.down;
    public bool isPlayerGrounded = false;
    public bool isPlayerColliding = false;
    public bool isPlayerFeetNearGround = false;
    
    // Crouch variables
    [Header("Crouch Settings")]
    public CapsuleCollider stand_collision;
    public SphereCollider crouch_collision;

    public float crouchSpeed = 2.5f;
    public float cameraStandPos = 0.5f;
    public float cameraCrouchPos = -0.5f;

    
    // Player Controls
    [Header("Player Controls")]
    public Vector2 walkInputVector;
    public bool isJumping = false;
    public bool isCrouching = false;


    // Start is called before the first frame update
    void Start()
    {
        // Get the player's transform and rigidbody.
        rigid = GetComponent<Rigidbody>();
        trans = GetComponent<Transform>();

        // Initialize the player's states.
        standState = new StandingState(this);
        crouchState = new CrouchingState(this);
        jumpState = new JumpState(this);
        aerialState = new AerialState(this);

        currentState = standState;

        currentState.EnterState();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentState.UpdateState();

        // Player's crouch state.
        // Crouching state must be independent of other states.
        crouchState.UpdateState();
    }

    // Switch state to a new state.
    public void SwitchState(PlayerBaseState state)
    {
        currentState = state;
        currentState.EnterState();
    }

    // Walk input handling....
    public void GetWalkInputAxes(InputAction.CallbackContext context)
    {
        walkInputVector = context.ReadValue<Vector2>();
    }

    // Jump input handling...
    public void GetJumpInput(InputAction.CallbackContext context)
    {
        isJumping = context.ReadValueAsButton();
    }

    // Crouch input handling...
    public void GetCrouchInput(InputAction.CallbackContext context)
    {
        isCrouching = context.ReadValueAsButton();
    }


    // Physics Handling...
    void OnCollisionEnter(Collision collision)
    {
        int numContacts = collision.GetContacts(collision.contacts);

        ContactPoint groundContact = collision.GetContact(0);

        if (numContacts > 0)
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                if (trans.position.y-contact.point.y >= 1-maxStepHeight)
                {
                    groundContact = contact;
                }
            }
        }


        // For calculating the angle of the vector from the player's
        // spherical feet to the point.
        Vector3 feet = trans.position - (Vector3.down*.5f);
        foot2Ground = (groundContact.point - feet).normalized;

        stepEdge = trans.position.y-groundContact.point.y;
        groundNormal = groundContact.normal;

        if (trans.position.y-groundContact.point.y < 1-maxStepHeight)
        {
            groundNormal = Vector3.up;
        }

        isPlayerColliding = true;
    }

    void OnCollisionStay(Collision collision)
    {
        int numContacts = collision.GetContacts(collision.contacts);

        ContactPoint groundContact = collision.GetContact(0);

        if (numContacts > 0)
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                if (trans.position.y-contact.point.y >= 1-maxStepHeight)
                {
                    groundContact = contact;
                }
            }
        }


        // For calculating the angle of the vector from the player's
        // spherical feet to the point.
        Vector3 feet = trans.position - (Vector3.down*.5f);
        foot2Ground = (groundContact.point - feet).normalized;

        stepEdge = trans.position.y-groundContact.point.y;
        groundNormal = groundContact.normal;

        if (trans.position.y-groundContact.point.y < 1-maxStepHeight)
        {
            groundNormal = Vector3.up;
        }

        isPlayerColliding = true;
    }

    void OnCollisionExit()
    {
        groundNormal = Vector3.up;
        isPlayerColliding = false;
    }

    // Feet Trigger handling
    void OnTriggerEnter()
    {
        isPlayerFeetNearGround = true;
    }

    void OnTriggerStay()
    {
        isPlayerFeetNearGround = true;
    }

    void OnTriggerExit()
    {
        isPlayerFeetNearGround = false;
    }
    
}
