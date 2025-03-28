using UnityEngine;

public class OnEnableRotateRandomly : MonoBehaviour
{
    public float limit = 30;
    void OnEnable()
    {
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(-limit, limit));
    }
}
