
using UnityEngine;

public class Player : MonoBehaviour
{
    public float walkspeed = 5f;

    public float crouch_height = 1f;
    public float crouch_walkspeed = 2.5f;

    public float jumpHeight = 7f;
    public float playerGravity = 9.81f;

    public float groundStickForce = 50f;

    // States

    // State references
    // We're using a concurrent state machine.
    public PlayerBaseState movementState;
    public PlayerBaseState heightState; // Is the player crouching, standing, or sliding?
    public PlayerBaseState armingState; // Is the player armed or unarmed?

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
