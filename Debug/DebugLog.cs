
//화면에 디버그 로그

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DebugLog : MonoSingleton<DebugLog>
{
    List<string> _list = new();
    Image _image;
    Text _text;



    // Use this for initialization
    new void Awake()
    {
        transform.SetParent(GameObject.Find("Canvas").transform, false);
        transform.localPosition = new Vector3(Def.RESOULUTION.x * -0.5f, Def.RESOULUTION.y * 0.5f, 0);
        GameObject image = new("Image");
        image.transform.SetParent(gameObject.transform, false);
        _image = image.AddComponent<Image>();
        _image.rectTransform.pivot = new Vector2(0, 1);
        _image.color = new Color(0,0,0,0.4f);

        GameObject text = new("Text");
        text.transform.SetParent(gameObject.transform, false);
        _text = text.AddComponent<Text>();
        _text.rectTransform.pivot = new Vector2(0, 1);
        _text.alignment = TextAnchor.UpperLeft;
        ContentSizeFitter fitter = text.AddComponent<ContentSizeFitter>();

        fitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        _text.fontSize = 10;
        _text.font = (Font)Resources.Load("Etc/NanumGothicBold");
    }





    //인터페이스.
    public void Show(string str)
    {
        transform.SetAsLastSibling();

        if (_list.Count != 0)
        {
            str = '\n' + str;
        }
        _list.Add(str);

        if (_list.Count >= 10)
        {
            _list.RemoveAt(0);
        }

        StopCoroutine("Co_Show");
        StartCoroutine("Co_Show");
    }
    IEnumerator Co_Show()
    {
        //자동 \n추가.
        int length = _list[_list.Count-1].Length;
        for (int iter_str = 0; iter_str < length; iter_str++)
        {
            if (iter_str != 0 &&
                iter_str % 80 == 0)
            {
                _list[_list.Count - 1] = _list[_list.Count - 1].Insert(iter_str, "\n");
                length++;
            }
        }

        //글 추가.
        _text.text = "";
        for (int i = 0; i < _list.Count; i++)
        {

            _text.text += _list[i];
        }


        yield return null; //sizeDelta때문에.
        _image.rectTransform.sizeDelta = new Vector2(
            _text.rectTransform.sizeDelta.x,
            _text.rectTransform.sizeDelta.y);


        _text.gameObject.SetActive(true);
        _image.gameObject.SetActive(true);

        yield return new WaitForSeconds(15f);

        _text.gameObject.SetActive(false);
        _image.gameObject.SetActive(false);
    }
}
