#nullable enable

namespace Game;

using UnityEngine;

public sealed class PlayerMoveState : PlayerState
{
    private Vector3 destinationPoint = Vector3.zero;
    private bool winning = false;

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
            if (!Physics.Raycast(nextPoint, Vector3.down, out var hitInfo))
            {
                this.destinationPoint = nextPoint - moveInput;
                this.winning = false;
                break;
            }

            var hitGameObject = hitInfo.transform.gameObject;
            if (hitGameObject.CompareTag(this.Controller.StackManager.ObstacleTag))
            {
                this.destinationPoint = nextPoint - moveInput;
                this.winning = false;
                break;
            }

            if (hitGameObject.CompareTag(this.Controller.StackManager.WinningTag))
            {
                this.destinationPoint = nextPoint - moveInput;
                this.winning = true;
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
            if (!this.winning)
            {
                this.StateMachine.SetStateToChangeTo(this.StateMachine.IdleState);
            }
            else
            {
                this.StateMachine.SetStateToChangeTo(this.StateMachine.WinState);
            }
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
            else if (go.CompareTag(this.Controller.StackManager.UnbrickTag))
            {
                if (go.TryGetComponent<UnbrickController>(out var unbrickController))
                {
                    this.Controller.StackManager.RemoveBrick();
                    unbrickController.Execute();
                }
                else
                {
                    Debug.LogError($"Game object has tag {this.Controller.StackManager.UnbrickTag} " +
                        $"but doesn't have a component {nameof(UnbrickController)}");
                }
            }
        }
    }
}
