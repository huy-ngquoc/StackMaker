#nullable enable

namespace Game;

public sealed class PlayerLoseState : PlayerState
{
    public PlayerLoseState(PlayerStateMachine stateMachine)
    {
        this.StateMachine = stateMachine;
    }

    public override PlayerStateMachine StateMachine { get; }
}
