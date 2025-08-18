#nullable enable

namespace Game;

public abstract class PlayerState
{
    public abstract PlayerStateMachine StateMachine { get; }

    public PlayerController Controller => this.StateMachine.Controller;

    public PlayerInputHandler InputHandler => this.Controller.InputHandler;

    public GameplayManager GameplayManager => this.Controller.GameplayManager;

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
