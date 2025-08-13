#nullable enable

namespace Game;

using UnityEngine;

public sealed class PlayerWinState : PlayerState
{
    private float stateTimer = 0;

    public PlayerWinState(PlayerStateMachine stateMachine)
    {
        this.StateMachine = stateMachine;
    }

    public override PlayerStateMachine StateMachine { get; }

    public override void Enter()
    {
        this.stateTimer = 2;

        this.Controller.StackManager.ClearBrick();

        this.Controller.Animator.ResetTrigger("Idle");
        this.Controller.Animator.SetTrigger("Win");
    }

    public override void Update()
    {
        var deltaTime = Time.deltaTime;
        if (this.stateTimer > deltaTime)
        {
            this.stateTimer -= deltaTime;
        }
        else
        {
            this.stateTimer = 0;
        }

        if (this.stateTimer <= 0)
        {
            // TODO: Do stuff
            this.StateMachine.SetStateToChangeTo(this.StateMachine.IdleState);
        }
    }

    public override void Exit()
    {
        this.Controller.Animator.ResetTrigger("Win");
        this.Controller.Animator.SetTrigger("Idle");
    }
}
