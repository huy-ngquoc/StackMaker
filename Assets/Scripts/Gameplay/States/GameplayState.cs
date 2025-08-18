#nullable enable

namespace Game;

public abstract class GameplayState
{
    public GameplayManager GameplayManager => this.StateMachine.GameplayManager;

    public LevelManager LevelManager => this.GameplayManager.LevelManager;

    public GameplayUiManager UiManager => this.GameplayManager.UiManager;

    public abstract GameplayStateMachine StateMachine { get; }

    public virtual void Enter()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void Exit()
    {
    }
}
