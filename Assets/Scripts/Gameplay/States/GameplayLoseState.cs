#nullable enable

namespace Game;

public sealed class GameplayLoseState : GameplayState
{
    public GameplayLoseState(GameplayStateMachine stateMachine)
    {
        this.StateMachine = stateMachine;
    }

    public override GameplayStateMachine StateMachine { get; }

    public override void Enter()
    {
        this.UiManager.ShowLosingCanvas();
    }

    public override void Exit()
    {
        this.UiManager.HideLosingCanvas();
    }
}
