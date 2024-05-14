using UnityEngine;

public class LoadPoolingObjOnHide : MonoBehaviour
{
    public string objectKey;

    void OnDisable()
    {
        ObjectPools.Get(objectKey).transform.position = transform.position;
    }
}