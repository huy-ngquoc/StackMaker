#nullable enable

namespace Game
{
    using System.Collections.Generic;
    using UnityEngine;

    public sealed class StackManager : MonoBehaviour
    {
        private readonly Stack<GameObject> bricks = new();

        [SerializeReference]
        [RequireReference]
        private GameObject brickPrefab = null!;

        [SerializeReference]
        [ResolveComponentInChildren("Bricks")]
        private Transform bricksTransformToStack = null!;

        [SerializeReference]
        [ResolveComponentInChildren("Animation")]
        private Transform animationTransform = null!;

        [SerializeField]
        private float brickHeight = 0.2F;

        [SerializeField]
        [TagSelection]
        private string brickTag = string.Empty;

        [SerializeField]
        [TagSelection]
        private string obstacleTag = string.Empty;

        [SerializeField]
        [TagSelection]
        private string unbrickTag = string.Empty;

        public string BrickTag => this.brickTag;

        public string ObstacleTag => this.obstacleTag;

        public string UnbrickTag => this.unbrickTag;

        public void AddBrick()
        {
            float yOffset = this.bricks.Count * this.brickHeight;
            var brick = Object.Instantiate(this.brickPrefab, this.bricksTransformToStack.transform);
            brick.transform.localPosition = Vector3.up * yOffset;
            this.bricks.Push(brick);

            this.animationTransform.localPosition = Vector3.up * (yOffset + this.brickHeight);
        }

        public void RemoveBrick()
        {
            if (this.bricks.TryPop(out var brick))
            {
                Object.Destroy(brick);
            }
        }

        public void ClearBrick()
        {
            while (this.bricks.TryPop(out var brick))
            {
                Object.Destroy(brick);
            }
        }
    }
}
