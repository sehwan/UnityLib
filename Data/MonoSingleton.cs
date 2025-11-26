using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    // Static
    protected static T _i = null;
    public static T i
    {
        get
        {
            if (_i == null)
            {
                _i = FindFirstObjectByType<T>();
                if (_i == null)
                {
                    _i = new GameObject(typeof(T).ToString(), typeof(T)).GetComponent<T>();
                    _i.Init();
                }
            }
            return _i;
        }
    }
    // For Instance
    [Immutable] public bool didInit;
    protected virtual void Awake()
    {
        if (_i == null)
        {
            _i = this as T;
            if (didInit == false) Init();
        }
        else if (_i != this)
        {
            Destroy(gameObject);
        }
    }
    public virtual void Init()
    {
        // Use base.Init() at first
        didInit = true;
    }
}



public abstract class MonoSingletonDontDestroyed<T> : MonoSingleton<T> where T : MonoSingletonDontDestroyed<T>
{
    protected override void Awake()
    {
        if (_i == null)
        {
            _i = this as T;
            _i.Init();
            DontDestroyOnLoad(gameObject);
        }
    }
}