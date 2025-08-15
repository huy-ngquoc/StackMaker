#nullable enable

namespace Game
{
    using UnityEngine;

    [DisallowMultipleComponent]
    public sealed class LevelController : MonoBehaviour
    {
        [SerializeField]
        private Vector2Int startPosition = Vector2Int.zero;

        public Vector2Int StartPosition => this.startPosition;
    }
}
