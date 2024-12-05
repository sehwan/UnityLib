using UnityEngine;

// Usage
// public static ToastGroup Inst()
// {
//     prefabPath = "Prefabs/ToastGroup";
//     return inst;
// }
public abstract class PrefabSignleton<T> : MonoBehaviour where T : PrefabSignleton<T>
{
    // Static
    protected static string prefabPath;
    protected static T _inst = null;
    protected static T instance
    {
        get
        {
            if (_inst == null)
            {
                if (prefabPath.IsNullOrEmpty())
                {
                    Debug.LogError("PrefabPath is Empty");
                    return null;
                }
                var go = (GameObject)Instantiate(Resources.Load(prefabPath));
                go.name = go.name.Replace("(Clone)", "");
                _inst = go.GetComponent<T>();
                _inst.Init();
            }
            return _inst;
        }
    }
    // For Instance
    [HideInInspector] public bool didInit;
    protected virtual void Awake()
    {
        if (_inst == null)
        {
            _inst = this as T;
            if (didInit == false) Init();
        }
    }
    ///<summary>
    /// Use base.Init() at first
    ///</summary>
    public virtual void Init()
    {
        didInit = true;
    }
}

