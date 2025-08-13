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
    }
}
