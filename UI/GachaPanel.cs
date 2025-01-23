using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public enum StateGachaResult { STARTED, OPENING, DONE }
public class GachaPanel : UIWindow
{
    public Text txt_title;
    public StateGachaResult state;
    public Transform tr_slots;
    public GachaItem[] slots;
    public GameObject btn_esc;
    public GameObject btn_skip;
    public int SlotCount => slots.Length;


    public override void Init()
    {
        base.Init();
        slots = tr_slots.GetComponentsInChildren<GachaItem>();
    }


    public void Show(int cnt, string title)
    {
        base.Show();
        txt_title.text = title;
        // state = StateGachaResult.STARTED;
        btn_esc.SetActive(false);
        btn_skip.SetActive(false);
        foreach (var item in slots) item.gameObject.SetActive(false);
        StartCoroutine(Co_ShowSlots(cnt));
    }
    IEnumerator Co_ShowSlots(int cnt)
    {
        // SFX.Play("gacha");
        Fade.i.Dim(Color.white);
        Fade.i.Out(Color.white, 0.3f);
        // btn_skip.SetActive(true);
        yield return CoroutineEx.GetWait(0.3f);
        for (int i = 0; i < cnt; i++)
        {
            SFX.Play("gachaShow");
            slots[i].gameObject.SetActive(true);
            yield return CoroutineEx.GetWait(0.08f);
        }
        yield return CoroutineEx.GetWait(0.2f);
        // state = StateGachaResult.OPENING;
        btn_esc.SetActive(true);
    }

    public override void Hide()
    {
        base.Hide();
        // UM.Get<EquipPanel>().RefreshNoti();
    }


    // void Update()
    // {
    //     if (state != StateGachaResult.OPENING) return;
    //     var isAllOpen = slots.All(e => e.gameObject.activeSelf == false || e.screen.gameObject.activeSelf == false);
    //     if (isAllOpen) state = StateGachaResult.DONE;
    //     else return;
    //     btn_skip.SetActive(false);
    //     btn_esc.SetActive(true);
    // }


    public void Skip()
    {
        StartCoroutine(Co_Skip());
    }
    IEnumerator Co_Skip()
    {
        var w = new WaitForSeconds(0.085f);
        btn_skip.SetActive(false);
        foreach (var e in slots)
        {
            if (e.screen.gameObject.activeInHierarchy) yield return w;
            e.OnClick();
        }
    }
}
