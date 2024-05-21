using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarRatingPanel : MonoBehaviour
{
    public int stars = 0;

    [Header("UI")]
    public Transform tr_stars;


    public static void Show()
    {
        Instantiate(Resources.Load<GameObject>("Prefabs/starRating_pn"));
    }


    void Start()
    {
        Starring(0);
    }

    public void Starring(int star)
    {
        stars = star;
        foreach (Transform c in tr_stars) c.GetImage().color = Color.gray;
        for (int i = 0; i < star; i++) tr_stars.GetChild(i).GetImage().color = ColorEx.gold;
    }


    public void Summit()
    {
        User.i.data.starRating = stars;
        if (stars == 0)
        {
            ToastGroup.Show("pressStar".L());
            return;
        }
        else if (stars >= 5) Application.OpenURL(Def.URL_Market);
        ToastGroup.Show("thanks".L());
        ESC();
    }

    public void ESC()
    {
        Destroy(gameObject);
    }
}
