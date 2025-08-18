#nullable enable

namespace Game
{
    using UnityEngine;
    using UnityEngine.UI;

    public sealed class WinningUiController : MonoBehaviour
    {
        [SerializeReference]
        [ResolveComponentInParent]
        private GameplayManager gameplayManager = null!;

        [Header("Buttons")]

        [SerializeReference]
        [ResolveComponentInChildren("Continue Button")]
        private Button continueButton = null!;

        [SerializeReference]
        [ResolveComponentInChildren("Back Button")]
        private Button backButton = null!;

        private void Awake()
        {
            this.continueButton.onClick.AddListener(this.OnContinueButtonPressed);
            this.backButton.onClick.AddListener(this.OnBackButtonPressed);
        }

        private void OnDestroy()
        {
            this.continueButton.onClick.RemoveListener(this.OnContinueButtonPressed);
            this.backButton.onClick.RemoveListener(this.OnBackButtonPressed);
        }

        private void OnContinueButtonPressed()
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
