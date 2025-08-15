#nullable enable

namespace Game
{
    using UnityEngine;

    [RequireComponent(typeof(PlayerInputHandler))]
    public sealed class PlayerController : MonoBehaviour
    {
        [SerializeReference]
        [ResolveComponentInChildren]
        private Animator animator = null!;

        [SerializeReference]
        [ResolveComponent]
        private PlayerInputHandler inputHandler = null!;

        [SerializeReference]
        [ResolveComponent]
        private PlayerStateMachine stateMachine = null!;

        [SerializeReference]
        [ResolveComponent]
        private StackManager stackManager = null!;

        [SerializeField]
        private float speed = 10;

        public Animator Animator => this.animator;

        public PlayerInputHandler InputHandler => this.inputHandler;

        public PlayerStateMachine StateMachine => this.stateMachine;

        public StackManager StackManager => this.stackManager;

        public float Speed => this.speed;

        private void Start()
        {
            if (Physics.Raycast(this.transform.position, Vector3.down, out var hitInfo))
            {
                var go = hitInfo.transform.gameObject;
                if (go.CompareTag(this.StackManager.BrickTag))
                {
                    Object.Destroy(go);
                    this.StackManager.AddBrick();
                }
            }
        }
    }
}
