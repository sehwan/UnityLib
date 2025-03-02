using UnityEngine;

public class ObjectLifeLinker : MonoBehaviour
{
    public GameObject[] objects;

    void OnEnable()
    {
        foreach (var obj in objects) obj.SetActive(true);
    }
    void OnDisable()
    {
        foreach (var obj in objects) obj.SetActive(false);
    }
    void OnDestroy()
    {
        foreach (var obj in objects) Destroy(obj);
    }
}
