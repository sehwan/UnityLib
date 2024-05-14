using UnityEngine;
using UnityEngine.UI;

public class UIToggleFolding : MonoBehaviour
{
    public bool isFolding;

    public Image img_toggle;
    public Sprite spr_folding;
    public Sprite spr_unfolding;
    public Transform tr_target;



    void Awake()
    {
        Execute();
    }

    void Execute()
    {
        tr_target.SetActive(!isFolding);
        img_toggle.sprite = isFolding ? spr_folding : spr_unfolding;
    }


    public void Toggle()
    {
        isFolding = !isFolding;
        Execute();
    }
}

