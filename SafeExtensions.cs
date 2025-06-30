using UnityEngine;

public static class SafeExtensions
{
    public static bool IsSafe<T>(this T obj) where T : Object
    {
        return obj != null;
    }

    public static T Safe<T>(this T obj) where T : Object
    {
        return obj.IsSafe() ? obj : null;
    }

    public static void SafeInvoke<T>(this T obj, System.Action<T> action) where T : Object
    {
        if (obj != null) action(obj);
    }
}