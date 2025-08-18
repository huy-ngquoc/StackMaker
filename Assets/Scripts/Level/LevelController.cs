#nullable enable

namespace Game
{
    using UnityEngine;

    [DisallowMultipleComponent]
    public sealed class LevelController : MonoBehaviour
    {
        [SerializeField]
        private Vector3Int startPosition = Vector3Int.zero;

        public Vector3Int StartPosition => this.startPosition;
    }
}
