using UnityEngine;
using DG.Tweening;

public class ShrinkDestroy : MonoBehaviour
{
    [Immutable] public float passed;
    [Immutable] public bool isShrinking;

    [Header("Setting")]
    public float LIFE;
    public float SHRINK_TIME;



    void Update()
    {
        if (isShrinking == true) return;
        passed += Time.deltaTime;
        if (LIFE < passed)
        {
            isShrinking = true;
            transform.DOScale(Vector3.zero, SHRINK_TIME).OnComplete(() =>
            {
                DestroyImmediate(gameObject);
            });
        }
    }
}
