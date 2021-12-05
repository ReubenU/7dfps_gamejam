
using UnityEngine;

public class JumpState : PlayerBaseState
{

    public JumpState(PlayerStateManager player) : base(player) {}

    public override void UpdateState()
    {
        
    }

    public override void EnterState()
    {
        float jumpVelocity = Mathf.Sqrt(2 * Player.jumpHeight * Player.aerialGravity);

        Player.rigid.velocity = jumpVelocity * Vector3.up;

        Debug.Log(string.Format("{0}, {1}",jumpVelocity, Player.rigid.velocity.y));

        Player.SwitchState(Player.aerialState);
    }

}
