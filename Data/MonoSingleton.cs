using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    // Static
    protected static T _inst = null;
    public static T i
    {
        get
        {
            if (_inst == null)
            {
                _inst = GameObject.FindObjectOfType(typeof(T)) as T;
                if (_inst == null)
                {
                    _inst = new GameObject(typeof(T).ToString(), typeof(T)).GetComponent<T>();
                    _inst.Init();
                }
            }
            return _inst;
        }
    }
    // For Instance
    [Immutable] public bool didInit;
    protected virtual void Awake()
    {
        if (_inst == null)
        {
            _inst = this as T;
            if (didInit == false) Init();
        }
        else if (_inst != this)
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
        if (_inst == null)
        {
            _inst = this as T;
            _inst.Init();
            DontDestroyOnLoad(gameObject);
        }
    }
}