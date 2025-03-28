using UnityEngine;

public class ObjectLifeLinker : MonoBehaviour
{
    public GameObject[] sync;
    public GameObject[] inverse;


    void OnEnable()
    {
        foreach (var obj in sync) obj.SetActive(true);
        foreach (var obj in inverse) obj.SetActive(false);
    }
    void OnDisable()
    {
        foreach (var obj in sync) obj.SetActive(false);
        foreach (var obj in inverse) obj.SetActive(true);
    }

    void OnDestroy()
    {
        foreach (var obj in sync) Destroy(obj);
        foreach (var obj in inverse) Destroy(obj);
    }
}
