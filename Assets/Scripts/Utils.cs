#nullable enable

namespace Game;

using UnityEngine;

public static class Utils
{
    public static bool IsLayerInMask(GameObject obj, LayerMask mask)
    {
        return ((1 << obj.layer) & mask) != 0;
    }

    public static int ModPositive(int a, int b)
    {
        return ((a % b) + b) % b;
    }
}
