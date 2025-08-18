#nullable enable

namespace Game;

public sealed class PlayerIdleState : PlayerState
{
    public PlayerIdleState(PlayerStateMachine stateMachine)
    {
        this.StateMachine = stateMachine;
    }

    public override PlayerStateMachine StateMachine { get; }

    public override void Update()
    {
        if (this.InputHandler.CurrentMoveDirection != PlayerInputHandler.MoveDirection.None)
        {
            this.StateMachine.SetStateToChangeTo(this.StateMachine.MoveState);
        }
    }
}
