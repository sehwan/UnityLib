
using UnityEngine;

public class HPBarSpriteFollower : MonoBehaviour
{
    public Transform me;
    public Transform target;
    const float SPEED = 0.9f;

    public void Notify()
    {
        gameObject.SetActive(true);
        if (me.localScale.x < target.localScale.x) me.LocalScaleX(target.localScale.x);
    }

    void Update()
    {
        if (me.localScale.x > target.localScale.x)
            me.LocalScaleX(me.localScale.x - Time.deltaTime * SPEED);
        else gameObject.SetActive(false);
    }
}