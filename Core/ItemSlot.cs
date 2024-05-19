using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class ItemSlot : MonoBehaviour
{
    public Image img_frame;
    public Image[] icons;
    public GameObject go_lock;
    public Text txt_1;
    public Text txt_5;
    public Text txt_7;
    public Text txt_11;
    public GameObject go_selected;
    public GameObject go_lockedSlot;
    public GameObject go_addSlot;
    public Button btn;
    public Button btn_delete;
    public Action onClick;
    public Action onClickDelete;

    void Awake()
    {
        CheckSelect(false);
    }
    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
        Update();
    }

    public void SetIcons(Sprite s0, Sprite s1 = null, Sprite s2 = null)
    {
        icons[0].SetActive(true);
        icons[0].sprite = s0;

        if (s1 != null)
        {
            icons[1].SetActive(true);
            icons[1].sprite = s1;
        }
        else icons[1].SetActive(false);

        if (s2 != null)
        {
            icons[2].SetActive(true);
            icons[2].sprite = s2;
        }
        else icons[2].SetActive(false);
    }


    public Action updateAction;
    void Update()
    {
        updateAction?.Invoke();
    }


    public void SetEmpty()
    {
        updateAction = null;
        gameObject.SetActive(true);
        btn.interactable = false;
        // btn.onClick.RemoveAllListeners();
        this.onClick = null;
        this.onClickDelete = null;
        btn_delete.SetActive(false);
        // btn_delete.onClick.RemoveAllListeners();

        img_frame.SetActive(false);
        icons.ForEach(e => e.SetActive(false));
        txt_1.text = null;
        txt_5.text = null;
        txt_7.text = null;
        txt_11.text = null;
        go_lock.SetActive(false);
        go_selected.SetActive(false);
        go_addSlot.SetActive(false);
        go_lockedSlot.SetActive(false);
    }
    public void SetUnlockSlot(Action onClick)
    {
        updateAction = null;
        gameObject.SetActive(true);
        btn.interactable = true;
        // btn.onClick.RemoveAllListeners();
        this.onClick = onClick;
        this.onClickDelete = null;
        // btn.onClick.AddListener(() => cb());
        btn_delete.SetActive(false);

        img_frame.SetActive(true);
        img_frame.color = Color.black;
        icons.ForEach(e => e.SetActive(false));
        txt_1.text = null;
        txt_5.text = null;
        txt_7.text = null;
        txt_11.text = null;
        go_lock.SetActive(false);
        go_selected.SetActive(false);
        go_addSlot.SetActive(true);
        go_lockedSlot.SetActive(false);
    }
    public void SetLockedSlot()
    {
        updateAction = null;
        gameObject.SetActive(true);
        btn.interactable = true;
        // btn.onClick.RemoveAllListeners();
        this.onClick = null;
        this.onClickDelete = null;
        btn_delete.SetActive(false);

        img_frame.SetActive(true);
        img_frame.color = Color.black;
        icons.ForEach(e => e.SetActive(false));
        txt_1.text = null;
        txt_5.text = null;
        txt_7.text = null;
        txt_11.text = null;
        go_lock.SetActive(false);
        go_selected.SetActive(false);
        go_addSlot.SetActive(false);
        go_lockedSlot.SetActive(true);
    }


    public void NormalSlotBase(Action onClick, Action onClickDelete)
    {
        SetActive(true);

        btn.interactable = onClick != null;
        // btn.onClick.RemoveAllListeners();
        this.onClick = onClick;
        // btn.onClick.AddListener(() => onClick());

        btn_delete.SetActive(false);
        // btn_delete.onClick.RemoveAllListeners();
        this.onClickDelete = onClickDelete;
        // btn_delete.onClick.AddListener(() => onClickDelete());

        updateAction = null;

        img_frame.SetActive(true);
        txt_1.text = null;
        txt_5.text = null;
        txt_7.text = null;
        txt_11.text = null;

        go_lock.SetActive(false);
        go_addSlot.SetActive(false);
        go_lockedSlot.SetActive(false);
    }
    public void ShowUnlockButton(Action onClick)
    {
        btn.interactable = true;
        // btn.onClick.RemoveAllListeners();
        this.onClick = onClick;
        // btn.onClick.AddListener(() => cb());
        go_addSlot.SetActive(true);
    }
    public void ShowLock()
    {
        btn.onClick.RemoveAllListeners();
        go_lockedSlot.SetActive(true);
    }

    public bool IsNormalSlot()
    {
        return
            img_frame.gameObject.activeSelf &&
            go_addSlot.activeSelf == false &&
            go_lockedSlot.activeSelf == false;
    }
    public void CheckSelect(bool isSelected)
    {
        go_selected.SetActive(isSelected);
        if (isSelected == true)
        {
            txt_1.text = null;
        }
    }


    public void OnClick()
    {
        onClick?.Invoke();
    }
    public void OnClickDelete()
    {
        onClickDelete?.Invoke();
    }
}
