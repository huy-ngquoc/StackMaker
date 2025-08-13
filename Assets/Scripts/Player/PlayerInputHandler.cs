#nullable enable

namespace Game
{
    using System;
    using UnityEngine;

    public sealed class PlayerInputHandler : MonoBehaviour, System.IDisposable
    {
        [SerializeField]
        [Range(10, 100)]
        private float minSwipeDistance = 50;

        private InputSystemActions inputSystemActions = null!;
        private InputSystemActions.TouchActions touchActions = new();
        private InputSystemActions.KeyboardActions keyboardActions = new();
        private InputSystemActions.MouseActions mouseActions = new();

        private SwipeInfo touchSwipeInfo = new();
        private SwipeInfo mouseSwipeInfo = new();
        private KeyboardInfo keyboardInfo = new();
        private MoveDirection currentMoveDirection = MoveDirection.None;

        public event Action Moved
        {
            add => this.MovedAction += value;
            remove => this.MovedAction -= value;
        }

        private event Action? MovedAction = null;

        public enum MoveDirection
        {
            None,
            Left,
            Right,
            Forward,
            Backward,
        }

        public MoveDirection CurrentMoveDirection => this.currentMoveDirection;

        public Vector3 MoveInput => this.currentMoveDirection switch
        {
            MoveDirection.None => Vector3.zero,
            MoveDirection.Left => Vector3.left,
            MoveDirection.Right => Vector3.right,
            MoveDirection.Forward => Vector3.forward,
            MoveDirection.Backward => Vector3.back,
            _ => Vector3.zero,
        };

        public Vector3Int MoveInputInt => this.currentMoveDirection switch
        {
            MoveDirection.None => Vector3Int.zero,
            MoveDirection.Left => Vector3Int.left,
            MoveDirection.Right => Vector3Int.right,
            MoveDirection.Forward => Vector3Int.forward,
            MoveDirection.Backward => Vector3Int.back,
            _ => Vector3Int.zero,
        };

        public void CancelMoveInputAction() => this.currentMoveDirection = MoveDirection.None;

        public void Dispose()
        {
            this.inputSystemActions.Dispose();
        }

        private void Awake()
        {
            this.inputSystemActions = new InputSystemActions();
            this.touchActions = this.inputSystemActions.Touch;
            this.keyboardActions = this.inputSystemActions.Keyboard;
            this.mouseActions = this.inputSystemActions.Mouse;

            this.touchActions.Press.started += _ => this.touchSwipeInfo.StartPosition = this.touchActions.Position.ReadValue<Vector2>();
            this.touchActions.Press.canceled += _ =>
            {
                this.touchSwipeInfo.EndPosition = this.touchActions.Position.ReadValue<Vector2>();
                this.currentMoveDirection = this.touchSwipeInfo.GetMoveDirection(this.minSwipeDistance);
                this.MovedAction?.Invoke();
            };

            this.keyboardActions.Move.started += context =>
            {
                this.keyboardInfo.Direction = context.ReadValue<Vector2>();
                this.currentMoveDirection = this.keyboardInfo.MoveDirection;
                this.MovedAction?.Invoke();
            };
            this.keyboardActions.Move.canceled += _ => this.currentMoveDirection = MoveDirection.None;

            this.mouseActions.Press.started += _ => this.mouseSwipeInfo.StartPosition = this.mouseActions.Position.ReadValue<Vector2>();
            this.mouseActions.Press.canceled += _ =>
            {
                this.mouseSwipeInfo.EndPosition = this.mouseActions.Position.ReadValue<Vector2>();
                this.currentMoveDirection = this.mouseSwipeInfo.GetMoveDirection(this.minSwipeDistance);
                this.MovedAction?.Invoke();
            };
        }

        private void OnEnable()
        {
            this.inputSystemActions.Enable();

            this.touchActions.Enable();
            this.keyboardActions.Enable();
            this.mouseActions.Enable();
        }

        private void OnDisable()
        {
            this.touchActions.Disable();
            this.keyboardActions.Disable();
            this.mouseActions.Disable();

            this.inputSystemActions.Disable();
        }

        private void OnDestroy()
        {
            this.inputSystemActions.Dispose();
        }

        private struct SwipeInfo : IEquatable<SwipeInfo>
        {
            public Vector2 StartPosition = Vector2.zero;
            public Vector2 EndPosition = Vector2.zero;

            public SwipeInfo()
            {
            }

            public readonly MoveDirection GetMoveDirection(float minSwipeDistance)
            {
                var delta = this.EndPosition - this.StartPosition;

                if (delta.sqrMagnitude < (minSwipeDistance * minSwipeDistance))
                {
                    return MoveDirection.None;
                }

                if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
                {
                    if (delta.x < 0)
                    {
                        return MoveDirection.Left;
                    }

                    return MoveDirection.Right;
                }
                else
                {
                    if (delta.y < 0)
                    {
                        return MoveDirection.Backward;
                    }

                    return MoveDirection.Forward;
                }
            }

            public readonly bool Equals(SwipeInfo other)
            {
                return (this.StartPosition == other.StartPosition) && (this.EndPosition == other.EndPosition);
            }
        }

        private struct KeyboardInfo : IEquatable<KeyboardInfo>
        {
            public Vector2 Direction = Vector2.zero;

            public KeyboardInfo()
            {
            }

            public readonly MoveDirection MoveDirection
            {
                get
                {
                    if (this.Direction.x < 0)
                    {
                        return MoveDirection.Left;
                    }

                    if (this.Direction.x > 0)
                    {
                        return MoveDirection.Right;
                    }

                    if (this.Direction.y < 0)
                    {
                        return MoveDirection.Backward;
                    }

                    if (this.Direction.y > 0)
                    {
                        return MoveDirection.Forward;
                    }

                    return MoveDirection.None;
                }
            }

            public readonly bool Equals(KeyboardInfo other)
            {
                return this.Direction == other.Direction;
            }
        }
    }
}
