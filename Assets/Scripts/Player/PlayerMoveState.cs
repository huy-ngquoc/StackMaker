#nullable enable

namespace Game;

using UnityEngine;

public sealed class PlayerMoveState : PlayerState
{
    private Vector3 destinationPoint = Vector3.zero;

    public PlayerMoveState(PlayerStateMachine stateMachine)
    {
        this.StateMachine = stateMachine;
    }

    public override PlayerStateMachine StateMachine { get; }

    public override void Enter()
    {
        var moveInput = this.InputHandler.MoveInput;
        this.InputHandler.CancelMoveInputAction();

        for (int i = 1; ; ++i)
        {
            var nextPoint = this.Controller.transform.position + (i * moveInput);
            if ((!Physics.Raycast(nextPoint, Vector3.down, out var hitInfo))
                || hitInfo.transform.gameObject.CompareTag(this.Controller.StackManager.ObstacleTag))
            {
                this.destinationPoint = nextPoint - moveInput;
                break;
            }
        }
    }

    public override void Update()
    {
        var newPosition = Vector3.MoveTowards(
            this.Controller.transform.position,
            this.destinationPoint,
            this.Controller.Speed * Time.deltaTime);

        this.Controller.transform.position = newPosition;

        if (newPosition == this.destinationPoint)
        {
            this.StateMachine.SetStateToChangeTo(this.StateMachine.IdleState);
            return;
        }

        if (Physics.Raycast(newPosition, Vector3.down, out var hitInfo))
        {
            var go = hitInfo.transform.gameObject;
            if (go.CompareTag(this.Controller.StackManager.BrickTag))
            {
                Object.Destroy(go);
                this.Controller.StackManager.AddBrick();
            }
        }
    }
}
