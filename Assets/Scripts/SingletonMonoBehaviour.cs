#nullable enable

namespace Game;

using UnityDebug = UnityEngine.Debug;
using UnityDisallowMultipleComponent = UnityEngine.DisallowMultipleComponent;
using UnityMonoBehaviour = UnityEngine.MonoBehaviour;
using UnityObject = UnityEngine.Object;

[UnityDisallowMultipleComponent]
public abstract class SingletonMonoBehaviour<T> : UnityMonoBehaviour
    where T : SingletonMonoBehaviour<T>
{
    private static readonly object LockInstanceObj = new();

    private static T? instance = null;

    public static T? Instance
    {
        get
        {
            lock (LockInstanceObj)
            {
                QuickLog.WarnIfAccessNull(instance);
                return instance;
            }
        }
    }

    protected abstract T LocalInstance { get; }

    protected void Awake()
    {
        SingletonMonoBehaviour<T>.AssignInstance(this.LocalInstance);

        this.OnAwake();
    }

    protected virtual void OnAwake()
    {
    }

    private static void AssignInstance(T? newInstance)
    {
        var newInstanceTypeName = typeof(T).FullName;
        if (newInstance == null)
        {
            UnityDebug.LogWarning($"There has been an attempt of assigning null to the instance of {newInstanceTypeName}!");
            return;
        }

        lock (LockInstanceObj)
        {
            var newInstanceGameObject = newInstance.gameObject;
            var currentInstanceGameObject = instance.UnityAccessRef(i => i.gameObject);
            if (currentInstanceGameObject == null)
            {
                instance = newInstance;
                UnityObject.DontDestroyOnLoad(newInstanceGameObject);
                return;
            }

            var currentInstanceGameObjectName = currentInstanceGameObject.name;
            if (instance == newInstance)
            {
                UnityDebug.Log($"The same instance of {newInstanceTypeName} in Game Object \"{currentInstanceGameObjectName}\" has been assigned twice or more times!");
                return;
            }

            if (newInstanceGameObject == currentInstanceGameObject)
            {
                UnityDebug.Log($"There're two of more components of {newInstanceTypeName} in the same Game Object \"{currentInstanceGameObjectName}\"! " +
                    $"By continuing, the new components will be destroyed!");
                UnityObject.Destroy(newInstance);
                return;
            }

            UnityDebug.Log($"There have already been two or more instances of {newInstanceTypeName}! " +
                $"{newInstanceTypeName} is supposed to be a singleton. Hence, there must be only one instance of {newInstanceTypeName}! " +
                $"The Game Object \"{currentInstanceGameObjectName}\" has first been initialized and assigned to the instance of {newInstanceTypeName}. " +
                $"The Game Object \"{newInstanceGameObject}\" has been attempted to assign to the instance of {newInstanceTypeName}! " +
                $"By continuing, the Game Object \"{newInstanceGameObject}\" will be destroyed!");
            UnityObject.Destroy(newInstanceGameObject);
        }
    }
}
