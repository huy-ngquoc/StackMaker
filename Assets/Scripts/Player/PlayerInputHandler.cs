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

        private TouchInfo touchInfo = new();
        private KeyboardInfo keyboardInfo = new();
        private MoveDirection currentMoveDirection = MoveDirection.None;

        public enum MoveDirection
        {
            None,
            Left,
            Right,
            Forward,
            Backward,
        }

        public MoveDirection CurrentMoveDirection => this.currentMoveDirection;

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

            this.touchActions.Press.started += _ => this.touchInfo.StartPosition = this.touchActions.Position.ReadValue<Vector2>();
            this.touchActions.Press.canceled += _ =>
            {
                this.touchInfo.EndPosition = this.touchActions.Position.ReadValue<Vector2>();
                this.currentMoveDirection = this.touchInfo.GetMoveDirection(this.minSwipeDistance);
            };

            this.keyboardActions.Move.started += context =>
            {
                this.keyboardInfo.Direction = context.ReadValue<Vector2>();
                this.currentMoveDirection = this.keyboardInfo.MoveDirection;
            };
            this.keyboardActions.Move.canceled += _ => this.currentMoveDirection = MoveDirection.None;
        }

        private void OnEnable()
        {
            this.inputSystemActions.Enable();
            this.touchActions.Enable();
            this.keyboardActions.Enable();
        }

        private void OnDisable()
        {
            this.touchActions.Disable();
            this.keyboardActions.Disable();
            this.inputSystemActions.Disable();
        }

        private void OnDestroy()
        {
            this.inputSystemActions.Dispose();
        }

        private struct TouchInfo : IEquatable<TouchInfo>
        {
            public Vector2 StartPosition = Vector2.zero;
            public Vector2 EndPosition = Vector2.zero;

            public TouchInfo()
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

            public readonly bool Equals(TouchInfo other)
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
