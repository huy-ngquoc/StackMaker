#nullable enable

namespace Game;

using System.Runtime.CompilerServices;
using Debug = UnityEngine.Debug;
using NotNullWhenAttribute = System.Diagnostics.CodeAnalysis.NotNullWhenAttribute;

public static class QuickLog
{
    public static bool WarnIfAccessNull<T>(
        [NotNullWhen(false)] T? obj,
        [CallerMemberName] string callerName = "",
        [CallerFilePath] string callerFilePath = "")
        where T : class
    {
        if (obj == null)
        {
            Debug.LogWarning($"Attempting to access a value of type `{typeof(T).FullName}` in function `{callerName}` on file `{callerFilePath}` when it is null!");
            return true;
        }

        return false;
    }

    public static bool WarnIfAccessNull<T>(
        [NotNullWhen(false)] T? obj,
        [CallerMemberName] string callerName = "",
        [CallerFilePath] string callerFilePath = "")
        where T : struct
    {
        return WarnIfAccessNoValue(obj, callerName, callerFilePath);
    }

    public static bool WarnIfAccessNoValue<T>(
        [NotNullWhen(false)] T? obj,
        [CallerMemberName] string callerName = "",
        [CallerFilePath] string callerFilePath = "")
        where T : struct
    {
        if (!obj.HasValue)
        {
            Debug.LogWarning($"Attempting to access a value of type `{typeof(T).FullName}` in function `{callerName}` on file `{callerFilePath}` when it has no value!");
            return true;
        }

        return false;
    }
}
