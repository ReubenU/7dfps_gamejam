
using UnityEngine;

public abstract class PlayerBaseState
{
    public Player player;

    public PlayerBaseState nxtState;

    public PlayerBaseState(Player _player)
    {
        player = _player;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
}
