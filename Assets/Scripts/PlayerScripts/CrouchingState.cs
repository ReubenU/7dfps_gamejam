
using UnityEngine;

public class CrouchingState : PlayerBaseState
{

    public CrouchingState(PlayerStateManager player) : base(player){}

    public override void EnterState()
    {
        Debug.Log("I'm now crouching!");
    }

    public override void UpdateState()
    {
        // if (Input.GetKeyUp(Player.crouchKey))
        // {
        //     Player.SwitchState(Player.standState);
        // }
    }
}
