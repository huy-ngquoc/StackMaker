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
            if (instance == null)
            {
                instance = newInstance;
                UnityObject.DontDestroyOnLoad(newInstanceGameObject);
                return;
            }

            var currentInstanceGameObject = instance.gameObject;
            var currentInstanceGameObjectName = currentInstanceGameObject.name;
            if (instance == newInstance)
            {
                UnityDebug.LogWarning($"The same instance of {newInstanceTypeName} in Game Object \"{currentInstanceGameObjectName}\" has been assigned twice or more times!");
                return;
            }

            if (newInstanceGameObject == currentInstanceGameObject)
            {
                UnityDebug.LogWarning($"There're two of more components of {newInstanceTypeName} in the same Game Object \"{currentInstanceGameObjectName}\"! " +
                    $"By continuing, one of the components will be destroyed!");
                UnityObject.Destroy(instance);
                instance = newInstance;
                return;
            }

            UnityDebug.LogAssertion($"There have already been two or more instances of {newInstanceTypeName}! " +
                $"{newInstanceTypeName} is supposed to be a singleton. Hence, there must be only one instance of {newInstanceTypeName}! " +
                $"The Game Object \"{currentInstanceGameObjectName}\" has first been initialized and assigned to the instance of {newInstanceTypeName}. " +
                $"The Game Object \"{newInstanceGameObject}\" has been attempted to assign to the instance of {newInstanceTypeName}! " +
                $"By continuing, the Game Object \"{currentInstanceGameObjectName}\" will be destroyed, and " +
                $"the Game Object \"{newInstanceGameObject}\" will be assigned to the instance of {newInstanceTypeName}!");

            instance = newInstance;
            UnityObject.DontDestroyOnLoad(newInstanceGameObject);
            UnityObject.Destroy(currentInstanceGameObject);
        }
    }
}
