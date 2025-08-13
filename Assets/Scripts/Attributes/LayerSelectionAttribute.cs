#nullable enable

namespace Game;

using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]
public sealed class LayerSelectionAttribute : PropertyAttribute
{
}
