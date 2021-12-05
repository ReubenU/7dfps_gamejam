
using UnityEngine;

public class StandingState : PlayerBaseState
{

    public StandingState(PlayerStateManager player):base(player){}

    public override void EnterState()
    {
        Debug.Log("Standing!");
    }

    public override void UpdateState()
    {
        Move();

        // if (Input.GetKey(Player.crouchKey))
        // {
        //     Player.SwitchState(Player.crouchState);
        // }

        IsGrounded();

    }

    // This function enables the player to smoothly move around the world.
    Vector3 player_move_vector = new Vector3(0, 0, 0);
    void Move()
    {
        Vector3 friction = new Vector3(Player.rigid.velocity.x, 0f, Player.rigid.velocity.z);
        Vector2 walkDirection = Player.walkInputVector;


        Vector3 newVelocity = new Vector3(walkDirection.x, 0f, walkDirection.y) * Player.walkSpeed;

        player_move_vector = Vector3.Lerp(player_move_vector, newVelocity, 15f * Time.deltaTime);

        Player.rigid.AddForce(
            Player.trans.TransformDirection(player_move_vector)-friction,
            ForceMode.Acceleration
        );

        // Switch to jump state if player wishes to jump.
        if (Player.isJumping)
        {
            Player.SwitchState(Player.jumpState);
            
        }


        if (Player.isPlayerGrounded)
        {
            // Stick to ground
            Player.rigid.AddForce(-Player.groundNormal * Player.groundGravity, ForceMode.Acceleration);
        }

        // If player walks off ledge....
        if (!Player.isPlayerGrounded)
        {
            Player.SwitchState(Player.aerialState);
        }
        
    }

    void IsGrounded()
    {
        if (Player.isPlayerColliding && Player.isPlayerFeetNearGround)
        {
            Player.isPlayerGrounded = true;
        }
        else
        {
            Player.isPlayerGrounded = false;
            Player.groundNormal = Vector3.up;
            Player.foot2Ground = Vector3.down;
        }
    }
}
