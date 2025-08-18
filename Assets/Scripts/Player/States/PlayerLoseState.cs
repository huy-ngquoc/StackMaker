#nullable enable

namespace Game;

using UnityEngine;

public sealed class PlayerLoseState : PlayerState
{
    private float stateTimer = 0;

    public PlayerLoseState(PlayerStateMachine stateMachine)
    {
        this.StateMachine = stateMachine;
    }

    public override PlayerStateMachine StateMachine { get; }

    public override void Enter()
    {
        this.stateTimer = 1;
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
            this.GameplayManager.EndLose();
            this.StateMachine.SetStateToChangeTo(this.StateMachine.IdleState);
        }
    }
}
