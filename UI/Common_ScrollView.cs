
//공용 리스트 뷰.
//Common_ScrollView.inst.Show("제목", 프리팹리스트);
//리스트 모두 초기화까지 해서 보내줘야함.


using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


public class Common_ScrollView : MonoSingleton<Common_ScrollView>
{
    public UIWindow w;
    public Transform tr_content;


    override public void Init()
    {
        DontDestroyOnLoad(gameObject);
        if (w == null)
        {
            var prefab = Resources.Load<GameObject>("Prefabs/Common_ScrollView");
            var go = (GameObject)Instantiate(prefab,
                Vector3.zero, Quaternion.identity, GameObject.Find("Canvas").transform);
            w = go.AddComponent<UIWindow>();
            // _go.transform.SetParent(GameObject.Find("Canvas").transform, false);
            tr_content = w.transform.Find("frame/scroll/Viewport/Content");
        }
        w.transform.Find("frame/title/btn_esc").GetComponent<Button>().onClick.AddListener(Btn_ESC);
        w.transform.GetComponent<Button>().onClick.AddListener(Btn_ESC);
        w.SetActive(false);
    }



    public void Btn_ESC()
    {
        UM.i.showings.Remove(w);
        w.SetActive(false);
    }


    public void Show(string title, List<GameObject> list, bool esc = true)
    {
        if (UM.i.showings.Contains(w) == false) UM.i.showings.Add(w);

        //기존 정보 제거.
        for (var i = 0; i < tr_content.childCount; i++)
        {
            Destroy(tr_content.GetChild(i).gameObject);
        }

        //신규.
        w.transform.Find("frame/title/txt").GetComponent<Text>().text = title;

        for (int i = 0; i < list.Count; i++)
        {
            list[i].transform.SetParent(tr_content, false);
        }

        w.SetActive(true);
        w.transform.Find("frame/title/btn_esc").gameObject.SetActive(esc);
    }
}
