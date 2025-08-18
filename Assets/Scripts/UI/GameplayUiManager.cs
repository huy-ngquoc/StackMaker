#nullable enable

namespace Game
{
    using UnityEngine;

    public sealed class GameplayUiManager : MonoBehaviour
    {
        [SerializeReference]
        [ResolveComponentInChildren(true)]
        private InGameUiController inGameUiController = null!;

        [SerializeReference]
        [ResolveComponentInChildren(true)]
        private WinningUiController winningUiController = null!;

        [SerializeReference]
        [ResolveComponentInChildren(true)]
        private LosingUiController losingUiController = null!;

        public void ShowInGameCanvas()
        {
            this.inGameUiController.gameObject.SetActive(true);
        }

        public void HideInGameCanvas()
        {
            this.inGameUiController.gameObject.SetActive(false);
        }

        public void ShowWinningCanvas()
        {
            this.winningUiController.gameObject.SetActive(true);
        }

        public void HideWinningCanvas()
        {
            this.winningUiController.gameObject.SetActive(false);
        }

        public void ShowLosingCanvas()
        {
            this.losingUiController.gameObject.SetActive(true);
        }

        public void HideLosingCanvas()
        {
            this.losingUiController.gameObject.SetActive(false);
        }

        private void Awake()
        {
            this.inGameUiController.gameObject.SetActive(true);

            this.winningUiController.gameObject.SetActive(false);
            this.losingUiController.gameObject.SetActive(false);
        }
    }
}
