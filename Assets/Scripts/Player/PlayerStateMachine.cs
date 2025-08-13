#nullable enable

namespace Game
{
    using UnityEngine;

    public sealed class PlayerStateMachine : MonoBehaviour
    {
        [SerializeField]
        [ResolveComponent]
        private PlayerController controller = null!;

        private PlayerState currentState;
        private PlayerState? stateToChangeTo = null;

        public PlayerStateMachine()
        {
            this.IdleState = new PlayerIdleState(this);
            this.MoveState = new PlayerMoveState(this);

            this.currentState = this.IdleState;
        }

        public PlayerIdleState IdleState { get; }

        public PlayerMoveState MoveState { get; }

        public PlayerController Controller => this.controller;

        public void SetStateToChangeTo(PlayerState newState)
        {
            this.stateToChangeTo = newState;
        }

        public void CancelChangingState()
        {
            this.stateToChangeTo = null;
        }

        private void Awake()
        {
            this.currentState.Enter();
        }

        private void Update()
        {
            if (this.stateToChangeTo == null)
            {
                this.currentState.Update();
                return;
            }

            do
            {
                this.currentState.Exit();
                this.currentState = this.stateToChangeTo;
                this.stateToChangeTo = null;
                this.currentState.Enter();
            }
            while (this.stateToChangeTo != null);
        }
    }
}
