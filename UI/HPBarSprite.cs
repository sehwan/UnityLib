using UnityEngine;

public class HPBarSprite : MonoBehaviour
{
    public SpriteRenderer fill;
    public Transform tr_fill;
    public HPBarSpriteFollower follower;


    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    public void SetColor(Color color)
    {
        fill.color = color;
    }

    public void FillAmount(float val)
    {
        tr_fill.LocalScaleX(val);
        follower.Notify();
    }
    public void FillAmount(float cur, float max)
    {
        tr_fill.LocalScaleX(cur / max);
        follower.Notify();
    }
    public void FillAmount(int cur, int max)
    {
        tr_fill.LocalScaleX(cur / (float)max);
        follower.Notify();
    }
    public void FillAmount(BigNumber cur, BigNumber max)
    {
        tr_fill.LocalScaleX((cur / max).n);
        follower.Notify();
    }
}