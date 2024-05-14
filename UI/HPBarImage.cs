using UnityEngine;
using UnityEngine.UI;

public class HPBarImage : MonoBehaviour
{
    public Image bar;
    public HPBarImageFollower follower;


    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    public void SetColor(Color color)
    {
        bar.color = color;
    }

    public void FillAmount(float cur, float max)
    {
        bar.FillAmount(cur, max);
        follower.Notify();
    }
    public void FillAmount(float ratio)
    {
        bar.fillAmount = ratio;
        follower.Notify();
    }
}