#nullable enable

namespace Game
{
    using UnityEngine;
    using UnityEngine.UI;

    public sealed class LosingUiController : MonoBehaviour
    {
        [SerializeReference]
        [ResolveComponentInParent]
        private GameplayManager gameplayManager = null!;

        [Header("Buttons")]

        [SerializeReference]
        [ResolveComponentInChildren("Retry Button")]
        private Button retryButton = null!;

        [SerializeReference]
        [ResolveComponentInChildren("Back Button")]
        private Button backButton = null!;

        private void Awake()
        {
            this.retryButton.onClick.AddListener(this.OnRetryButtonPressed);
            this.backButton.onClick.AddListener(this.OnBackButtonPressed);
        }

        private void OnDestroy()
        {
            this.retryButton.onClick.RemoveListener(this.OnRetryButtonPressed);
            this.backButton.onClick.RemoveListener(this.OnBackButtonPressed);
        }

        private void OnRetryButtonPressed()
        {
            SoundManager.Instance.UnityAccess(s => s.PlayClickSound());

            this.gameplayManager.Play();
        }

        private void OnBackButtonPressed()
        {
            SoundManager.Instance.UnityAccess(s => s.PlayClickSound());

            this.gameplayManager.BackToMainMenu();
        }
    }
}
