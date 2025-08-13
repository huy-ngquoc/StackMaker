#nullable enable

namespace Game
{
    using UnityEngine;

    [DisallowMultipleComponent]
    public sealed class UnbrickController : MonoBehaviour
    {
        [SerializeReference]
        [RequireReference]
        private GameObject brickPrefab = null!;

        public void Execute()
        {
            Object.Instantiate(this.brickPrefab, this.transform.position, this.brickPrefab.transform.rotation, this.transform.parent);
            Object.Destroy(this.gameObject);
        }
    }
}
