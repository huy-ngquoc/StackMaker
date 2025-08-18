#nullable enable

namespace Game;

public sealed class GameplayWinState : GameplayState
{
    public GameplayWinState(GameplayStateMachine stateMachine)
    {
        this.StateMachine = stateMachine;
    }

    public override GameplayStateMachine StateMachine { get; }

    public override void Enter()
    {
        var newLevelIdx = GameManager.CurrentLevelIdx;
        ++newLevelIdx;
        if (newLevelIdx < this.LevelManager.AmountLevels)
        {
            GameManager.CurrentLevelIdx = newLevelIdx;
        }

        this.UiManager.ShowWinningCanvas();
    }

    public override void Exit()
    {
        this.UiManager.HideWinningCanvas();
    }
}
