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

        [field: Header("Aduio clips")]

        [field: SerializeReference]
        private AudioClip clickAudioClip = null!;

        private void OnEnable()
        {
            this.playButton.onClick.AddListener(this.OnPlayButtonClicked);
        }

        private void OnPlayButtonClicked()
        {
            SoundManager.Instance.UnityAccess(s => s.PlaySound(this.clickAudioClip));
            GameManager.GoToGameplay();
        }
    }
}
