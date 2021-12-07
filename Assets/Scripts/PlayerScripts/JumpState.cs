
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

        Player.rigid.AddForce((jumpVelocity - Player.rigid.velocity.y) * Vector3.up, ForceMode.VelocityChange);

        Player.SwitchState(Player.aerialState);
    }

}
