#nullable enable

namespace Game
{
    using UnityEngine;

    public sealed class GameplayManager : MonoBehaviour
    {
        [SerializeReference]
        [ResolveComponent]
        private GameplayStateMachine stateMachine = null!;

        [SerializeReference]
        [ResolveComponentInChildren]
        private LevelManager levelManager = null!;

        [SerializeReference]
        [ResolveComponentInChildren]
        private GameplayUiManager uiManager = null!;

        public GameplayStateMachine StateMachine => this.stateMachine;

        public LevelManager LevelManager => this.levelManager;

        public GameplayUiManager UiManager => this.uiManager;

        public void EndWin()
        {
            this.StateMachine.SetStateToChangeTo(this.StateMachine.WinState);
        }

        public void EndLose()
        {
            this.StateMachine.SetStateToChangeTo(this.StateMachine.LoseState);
        }

        public void Play()
        {
            this.StateMachine.SetStateToChangeTo(this.StateMachine.PlayingState);
        }

        public void BackToMainMenu()
        {
            this.StateMachine.SetStateToChangeTo(this.StateMachine.ExitState);
        }
    }
}
