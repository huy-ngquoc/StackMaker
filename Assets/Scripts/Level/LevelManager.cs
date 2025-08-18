#nullable enable

namespace Game
{
    using System.Collections.Generic;
    using UnityEngine;

    public sealed class LevelManager : MonoBehaviour
    {
        [SerializeReference]
        private List<LevelController> levelControllersPrefab = new();

        [SerializeReference]
        [ResolveComponentInChildren("Player", true)]
        private PlayerController playerController = null!;

        private LevelController? currentLevelController = null;

        public int AmountLevels => this.levelControllersPrefab.Count;

        public bool LoadLevel(int levelIdx)
        {
            if (levelIdx >= this.levelControllersPrefab.Count)
            {
                return false;
            }

            if (this.currentLevelController != null)
            {
                Object.Destroy(this.currentLevelController.gameObject);
            }
            else
            {
                this.playerController.gameObject.SetActive(true);
            }

            this.currentLevelController = Object.Instantiate(this.levelControllersPrefab[levelIdx], this.transform);
            this.playerController.transform.localPosition = new Vector3(
                this.currentLevelController.StartPosition.x + 0.5F,
                this.currentLevelController.StartPosition.y,
                this.currentLevelController.StartPosition.z + 0.5F);

            return true;
        }

        public void UnloadCurrentLevel()
        {
            if (this.currentLevelController != null)
            {
                this.playerController.gameObject.SetActive(false);

                Object.Destroy(this.currentLevelController.gameObject);
                this.currentLevelController = null;
            }
        }

        private void Awake()
        {
            this.playerController.gameObject.SetActive(false);
        }
    }
}
