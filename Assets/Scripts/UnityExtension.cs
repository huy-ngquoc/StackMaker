#nullable enable

namespace Game;

public static class UnityExtension
{
    public static void UnityAccess<T>(this T? obj, System.Action<T>? action)
        where T : UnityEngine.Object
    {
        if ((obj == null) || (action == null))
        {
            return;
        }

        action(obj);
    }

    public static TResult? UnityAccessRef<T, TResult>(this T? obj, System.Func<T, TResult>? func)
        where T : UnityEngine.Object
        where TResult : class?
    {
        if ((obj == null) || (func == null))
        {
            return default;
        }

        return func(obj);
    }

    public static TResult UnityAccessRef<T, TResult>(this T? obj, System.Func<T, TResult>? func, TResult defaultValue)
       where T : UnityEngine.Object
       where TResult : class?
    {
        var result = UnityAccessRef(obj, func);

        if (result == null)
        {
            return defaultValue;
        }

        return result;
    }

    public static TResult? UnityAccessVal<T, TResult>(this T? obj, System.Func<T, TResult>? func)
        where T : UnityEngine.Object
        where TResult : struct
    {
        if ((obj == null) || (func == null))
        {
            return default;
        }

        return func(obj);
    }

    public static TResult UnityAccessVal<T, TResult>(this T? obj, System.Func<T, TResult>? func, TResult defaultValue)
       where T : UnityEngine.Object
       where TResult : struct
    {
        var result = UnityAccessVal(obj, func);

        if (!result.HasValue)
        {
            return defaultValue;
        }

        return result.Value;
    }
}
