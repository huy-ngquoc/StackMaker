#nullable enable

namespace Game;

public sealed class GameplayExitState : GameplayState
{
    public GameplayExitState(GameplayStateMachine stateMachine)
    {
        this.StateMachine = stateMachine;
    }

    public override GameplayStateMachine StateMachine { get; }

    public override void Enter()
    {
        GameManager.GoToMainMenu();
    }
}
