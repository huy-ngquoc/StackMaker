#nullable enable

namespace Game
{
    using UnityEngine;
    using UnityEngine.UI;

    [DisallowMultipleComponent]
    public sealed class MainMenuController : MonoBehaviour
    {
        [field: Header("Buttons")]

        [SerializeReference]
        [ResolveComponentInChildren("Play Button")]
        private Button playButton = null!;

        [SerializeReference]
        [ResolveComponentInChildren("Exit Button")]
        private Button exitButton = null!;

        private void Awake()
        {
            this.playButton.onClick.AddListener(this.OnPlayButtonClicked);
            this.exitButton.onClick.AddListener(this.OnExitButtonClicked);
        }

        private void OnDestroy()
        {
            this.playButton.onClick.RemoveListener(this.OnPlayButtonClicked);
            this.exitButton.onClick.RemoveListener(this.OnExitButtonClicked);
        }

        private void OnPlayButtonClicked()
        {
            SoundManager.Instance.UnityAccess(s => s.PlayClickSound());
            GameManager.GoToGameplay();
        }

        private void OnExitButtonClicked()
        {
            SoundManager.Instance.UnityAccess(s => s.PlayClickSound());

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
