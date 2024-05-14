using UnityEngine;
using UnityEngine.UI;

// GetNextSlot and Init or InitToNextSlot
// And Show Finally
public class RewardsMsg : PrefabSignleton<RewardsMsg>
{
    public Text txt;
    public Transform tr_content;
    public ItemSlot[] items;

    int iter;

    public static RewardsMsg inst
    {
        get
        {
            prefabPath = "Prefabs/RewardsMsg";
            return instance;
        }
    }

    public override void Init()
    {
        base.Init();
        items = tr_content.GetComponentsInChildren<ItemSlot>();
    }
    
    public ItemSlot GetNextSlot() => items[iter++];
    public void InitToNextSlot(string type, int cnt)
    {
        var s = items[iter++];
        s.NormalSlotBase(null, null);
        s.SetActive(true);

        // Icon
        s.img_frame.color = Color.black;
        s.SetIcons(UIUtil.GetIcon(type));
        s.txt_5.text = $"x{cnt:n0}";
    }


    public void Show(string msg)
    {
        gameObject.SetActive(true);
        txt.text = msg;
        for (int i = 0; i < items.Length; i++)
            items[i].SetActive(i < iter);
        // if (Application.isEditor) this.InvokeEx(ESC, 0.5f);
        iter = 0;
    }

    public void ESC()
    {
        gameObject.SetActive(false);
    }
}
