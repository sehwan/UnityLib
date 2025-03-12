using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.EventSystems;


public static class UnityEx
{
    ///<summary>
    /// how to use : this.Invoke(MethodName, 1)
    ///</summary>
    public static void InvokeEx(this MonoBehaviour me, UnityAction method, float delay)
    {
        me.Invoke(method.Method.Name, delay);
    }

    ///<summary>
    /// how to use : this.InvokeRepeat(A, 1, 1)
    ///</summary>
    public static void InvokeRepeatingEx(this MonoBehaviour me, UnityAction method, float delay, float cooltime)
    {
        me.InvokeRepeating(method.Method.Name, delay, cooltime);
    }

    ///<summary>
    /// how to use : this.InvokeRepeat(A, 1, 1)
    ///</summary>
    public static void InvokeTimesEx(this MonoBehaviour me, UnityAction method, float delay, float cooltime, int count)
    {
        for (int i = 0; i < count; i++)
        {
            me.Invoke(method.Method.Name, delay + cooltime * i);
        }
    }

    ///<summary>
    /// Cancel Invoke by method directly ex) this.CancelInvokeEx(A())
    ///</summary>
    public static void CancelInvokeEx(this MonoBehaviour me, UnityAction method)
    {
        me.CancelInvoke(method.Method.Name);
    }



    public static void InstantRandomly(this GameObject go, Transform parent, Vector2 pos, float rad)
    {
        GameObject.Instantiate(go, pos + VectorEx.Random2(rad), Quaternion.identity, parent);
    }

    public static GameObject InstantiatePooled(this MonoBehaviour me, Transform pool, Vector3 pos)
    {
        //Show
        foreach (Transform item in pool)
        {
            if (item.gameObject.activeSelf == false)
            {
                item.position = pos;
                item.gameObject.SetActive(true);
                return item.gameObject;
            }
        }
        return pool.GetChild(0).gameObject;
    }



    public static Transform Finds(this GameObject me, string s)
    {
        return me.transform.Find(s);
    }


    //PlayerPrefs
    public static void PlayerPrefsSetBool(string s, bool n)
    {
        if (n == true)
            PlayerPrefs.SetInt(s, 1);
        else
            PlayerPrefs.SetInt(s, 0);
    }
    public static void PlayerPrefsAddInt(string s, int n)
    {
        PlayerPrefs.SetInt(s, PlayerPrefs.GetInt(s, 0) + n);
    }
    public static void PlayerPrefsAddFloat(string s, float n)
    {
        PlayerPrefs.SetFloat(s, PlayerPrefs.GetFloat(s, 0) + n);
    }



    public static void FadeInOut(this Graphic me, float fadeIn, float showing, float fadeOut)
    {
        me.StartCoroutine(Co_FadeInOut(me, fadeIn, showing, fadeOut));
    }
    static IEnumerator Co_FadeInOut(Graphic me, float fadeIn, float showing, float fadeOut)
    {
        me.CrossFadeAlpha(1, fadeIn, true);
        yield return new WaitForSeconds(fadeIn + showing);
        me.CrossFadeAlpha(0, fadeOut, true);
        yield return new WaitForSeconds(fadeOut);
    }


    // Scene
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
    public static void AddScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }
    public static void ShowLayer(this Camera me, string name)
    {
        me.cullingMask |= 1 << LayerMask.NameToLayer(name);
    }
    public static void HideLayer(this Camera me, string name)
    {
        me.cullingMask &= ~(1 << LayerMask.NameToLayer(name));
    }
    public static bool IsLayerIncluded(this Camera me, string layerName)
    {
        return (me.cullingMask & (1 << LayerMask.NameToLayer(layerName))) != 0;
    }

    public static Coroutine StartCoroutine(this GameObject me, IEnumerator co)
    {
        if (me.activeInHierarchy) return me.StartCoroutine(co);
        else return null;
    }


    public static bool IsOK(this UnityWebRequest me)
    {
        return me.result == UnityWebRequest.Result.Success;
    }
    public static bool IsError(this UnityWebRequest me)
    {
        return me.result != UnityWebRequest.Result.Success;
    }
    public static string Text(this UnityWebRequest me)
    {
        return me.downloadHandler.text;
    }

    public static void SetActive(this SpriteRenderer me, bool isActive)
    {
        me.gameObject.SetActive(isActive);
    }

    // Particle
    public static void Play(this ParticleSystem me, Vector3 pos)
    {
        me.transform.position = pos;
        me.Play();
    }
    public static void Emit(this ParticleSystem me, Vector3 pos, int count = 1)
    {
        me.transform.position = pos;
        me.Emit(count);
    }
    public static void SetWidth(this LineRenderer me, float width)
    {
        me.startWidth = width;
        me.endWidth = width;
    }
    public static void SetWidth(this LineRenderer me, float start, float end)
    {
        me.startWidth = start;
        me.endWidth = end;
    }

    public static bool IsWrongClick()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) return true;
        }
        else if (EventSystem.current.IsPointerOverGameObject()) return true;
        return false;
    }
}
