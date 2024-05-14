
//자식 텍스트가 초기값 보다 크다면 스케일을 자동으로 키워줌.

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AutoUIScalingWithText : MonoBehaviour
{
    LayoutElement _layout;
    RectTransform _other;
    float _origin_height;

    void Awake()
    {
        _layout = GetComponent<LayoutElement>();
        _origin_height = _layout.preferredHeight;
        _other = transform.Find("Text").GetComponent<RectTransform>();
    }

    // void Update()
    void OnEnable()
    {
        // if (_other.rect.height + 20 > _origin_height)
        // {
        //     _layout.preferredHeight = _other.rect.height + 20;
        // }
        StartCoroutine(Co_Fit());
    }
    IEnumerator Co_Fit()
    {
        yield return null;
        if (_other.rect.height + 20 > _origin_height)
        {
            _layout.preferredHeight = _other.rect.height + 20;
        }
    }
}
