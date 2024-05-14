using UnityEngine;
using UnityEngine.UI;

public class HPBarImageFollower : MonoBehaviour
{
    public Image origin;
    const float SPEED = 0.9f;
    Image me;


    void Awake()
    {
        me = GetComponent<Image>();
    }

    public void Notify()
    {
        gameObject.SetActive(true);
        if (me.fillAmount < origin.fillAmount) me.fillAmount = origin.fillAmount;
    }

    void Update()
    {
        if (me.fillAmount > origin.fillAmount)
            me.fillAmount -= Time.deltaTime * SPEED;
        else gameObject.SetActive(false);
    }
}
