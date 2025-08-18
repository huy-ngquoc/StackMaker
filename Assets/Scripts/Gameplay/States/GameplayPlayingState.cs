#nullable enable

namespace Game;

public sealed class GameplayPlayingState : GameplayState
{
    public GameplayPlayingState(GameplayStateMachine stateMachine)
    {
        this.StateMachine = stateMachine;
    }

    public override GameplayStateMachine StateMachine { get; }

    public override void Enter()
    {
        this.LevelManager.LoadLevel(GameManager.CurrentLevelIdx);
        this.UiManager.ShowInGameCanvas();
    }

    public override void Exit()
    {
        this.UiManager.HideInGameCanvas();
    }
}
