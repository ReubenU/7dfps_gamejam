
using UnityEngine;

public class AerialState : PlayerBaseState
{
    public AerialState(PlayerStateManager player) : base(player){}

    public override void UpdateState()
    {
        // Check if player has touched the ground.
        IsGrounded();

        // Switch state back to standState (more like grounded state)
        // when the player is grounded.
        if (Player.isPlayerGrounded)
        {
            Player.SwitchState(Player.standState);
        }

        // Aerial movement
        Move();

        // Player's gravity while in the air.
        Player.rigid.AddForce(Vector3.down * Player.aerialGravity, ForceMode.Force);

    }

    public override void EnterState()
    {
        Player.groundNormal = Vector3.up;

        player_move_vector = Vector3.zero;

    }

    // This function enables the player to smoothly move around the world.
    Vector3 player_move_vector = new Vector3(0, 0, 0);
    void Move()
    {
        Vector3 friction = new Vector3(Player.rigid.velocity.x, 0f, Player.rigid.velocity.z);
        Vector2 walkDirection = Player.walkInputVector;

        // Set aerial speed so that the player would have some control
        // while in the air.
        float maxSpeed = Player.aerialSpeed;

        Vector3 newVelocity = new Vector3(walkDirection.x, 0f, walkDirection.y) * Player.walkSpeed;

        player_move_vector = Vector3.Lerp(player_move_vector, newVelocity, Player.aerialSpeed * Time.fixedDeltaTime);

        Player.rigid.AddForce(
            Player.trans.TransformDirection(player_move_vector),
            ForceMode.Force
        );
    }


    // If the player's feet and capsule are touching the ground, then
    // the player is grounded.
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
