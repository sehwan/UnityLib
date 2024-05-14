// using UnityEngine;
// using Sirenix.OdinInspector;

// public abstract class SeriealizedMonoSingleton<T> : SerializedMonoBehaviour where T : SeriealizedMonoSingleton<T>
// {
//     protected static T _instance = null;
//     public static T inst
//     {
//         get
//         {
//             if (_instance == null)
//             {
//                 _instance = GameObject.FindObjectOfType(typeof(T)) as T;

//                 if (_instance == null)
//                 {
//                     _instance = new GameObject(typeof(T).ToString(), typeof(T)).GetComponent<T>();
//                     if (_instance == null)
//                     {
//                         Debug.LogError("Problem during the creation of " + typeof(T).ToString());
//                     }
//                 }
//             }
//             return _instance;
//         }
//     }

//     protected virtual void Awake()
//     {
//         if (_instance == null)
//         {
//             _instance = this as T;
//             _instance.Init();
//         }
//         else
//             Destroy(gameObject);
//     }


//     public virtual void Init() { }


//     private void OnApplicationQuit()
//     {
//         _instance = null;
//     }
// }