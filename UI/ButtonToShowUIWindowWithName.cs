using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class ButtonToShowUIWindowWithName : MonoBehaviour
{
    [Header("Onclick will be changed to This!")]
    [Space]
    public string targetToShow;


    void Awake()
    {
        if (targetToShow.IsNullOrEmpty()) return;
        var btn = GetComponent<Button>();
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() =>
        {
            var target = UM.i.windows.Find(e => e.gameObject.name == targetToShow);
            if (target != null) target.Show();
            else print($"There is no {targetToShow}");
        });
    }
}
