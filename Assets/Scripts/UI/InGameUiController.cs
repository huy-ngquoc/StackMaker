#nullable enable

namespace Game
{
    using UnityEngine;
    using UnityEngine.UI;

    public sealed class InGameUiController : MonoBehaviour
    {
        [SerializeReference]
        [ResolveComponentInParent]
        private GameplayManager gameplayManager = null!;

        [Header("Buttons")]

        [SerializeReference]
        [ResolveComponentInChildren("Back Button")]
        private Button backButton = null!;

        private void Awake()
        {
            this.backButton.onClick.AddListener(this.OnBackButtonPressed);
        }

        private void OnDestroy()
        {
            this.backButton.onClick.RemoveListener(this.OnBackButtonPressed);
        }

        private void OnBackButtonPressed()
        {
            SoundManager.Instance.UnityAccess(s => s.PlayClickSound());

            this.gameplayManager.BackToMainMenu();
        }
    }
}
