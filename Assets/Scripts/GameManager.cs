#nullable enable

namespace Game
{
    using UnityEngine;
    using UnityEngine.SceneManagement;

    [DisallowMultipleComponent]
    public sealed class GameManager : SingletonMonoBehaviour<GameManager>
    {
        public static string MainMenuSceneName => "Main Menu";

        public static string GameplaySceneName => "Gameplay";

        public static int CurrentLevelIdx
        {
            get
            {
                return PlayerPrefs.GetInt(nameof(GameManager.CurrentLevelIdx), 0);
            }

            set
            {
                PlayerPrefs.SetInt(nameof(GameManager.CurrentLevelIdx), value);
            }
        }

        protected override GameManager LocalInstance => this;

        public static void GoToMainMenu()
        {
            SceneManager.LoadScene(GameManager.MainMenuSceneName);
        }

        public static void GoToGameplay()
        {
            SceneManager.LoadScene(GameManager.GameplaySceneName);
        }
    }
}
