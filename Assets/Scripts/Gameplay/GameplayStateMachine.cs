#nullable enable

namespace Game
{
    using UnityEngine;

    public sealed class GameplayStateMachine : MonoBehaviour
    {
        [SerializeReference]
        [ResolveComponent]
        private GameplayManager gameplayManager = null!;

        private GameplayState currentState;
        private GameplayState? stateToChangeTo = null;

        public GameplayStateMachine()
        {
            this.PlayingState = new GameplayPlayingState(this);
            this.WinState = new GameplayWinState(this);
            this.LoseState = new GameplayLoseState(this);
            this.ExitState = new GameplayExitState(this);

            this.currentState = this.PlayingState;
        }

        public GameplayManager GameplayManager => this.gameplayManager;

        public GameplayPlayingState PlayingState { get; }

        public GameplayWinState WinState { get; }

        public GameplayLoseState LoseState { get; }

        public GameplayExitState ExitState { get; }

        public void SetStateToChangeTo(GameplayState newState)
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
