
using UnityEngine;

public class JumpState : PlayerBaseState
{

    public JumpState(PlayerStateManager player) : base(player) {}

    Vector3 refVelocity = new Vector3();

    public override void UpdateState()
    {

    }

    public override void EnterState()
    {
        Debug.Log("Jumped!");


        if (Player.isPlayerGrounded)
        {
            float jumpVelocity = Mathf.Sqrt(4 * Player.jumpHeight * Player.aerialGravity);

            Player.rigid.velocity += jumpVelocity * Vector3.up;

            refVelocity = Player.rigid.velocity;

            refVelocity.y = Mathf.Clamp(refVelocity.y, -refVelocity.y, jumpVelocity);

            Player.rigid.velocity = refVelocity;

            //Debug.Log(string.Format("{0}, {1}",jumpVelocity, Player.rigid.velocity.y));
        }

        Player.SwitchState(Player.aerialState);
    }

}
