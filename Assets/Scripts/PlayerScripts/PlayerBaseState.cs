
using UnityEngine;

public abstract class PlayerBaseState
{
    protected PlayerStateManager _player;

    public PlayerBaseState(PlayerStateManager player)
    {
        _player = player;
    }

    public PlayerStateManager Player{ get{return _player;} set{_player = value;} }

    public abstract void EnterState();

    public abstract void UpdateState();
}
