using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BlahBlah : MonoBehaviour
{
    public Image narrator;
    public GameObject bubble;
    public Text text;


    public void BeQuiet()
    {
        text.text = null;
        bubble.SetActive(false);
    }


    public void Blah(string str = null, float duration = 0)
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(narrator.transform.DOScale(0.995f, 0.15f));
        seq.Append(narrator.transform.DOScale(1.0f, 0.25f));
        StopAllCoroutines();
        if (gameObject.activeInHierarchy) StartCoroutine(Co_Blah(str, duration));
    }
    IEnumerator Co_Blah(string str = null, float duration = 0)
    {
        bubble.SetActive(true);
        string s = "";
        if (string.IsNullOrEmpty(str))
            s = texts[UnityEngine.Random.Range(0, texts.Length)];
        else
            s = str;
        text.text = s;

        bubble.GetComponent<RectTransform>().sizeDelta =
            new Vector2(text.rectTransform.rect.width, text.rectTransform.rect.height);
        // if (duration == -1)
        // {
        //     while (true)
        //     {
        //         yield return new WaitForSeconds(10000000);
        //     }
        // }
        if (duration < 0) yield break;
        duration = Mathf.Clamp(s.Length * 0.17f, 3f, 7f);
        yield return new WaitForSeconds(duration);
        bubble.SetActive(false);
    }



    string[] texts = new string[]{
            "돈은 항상 부족하네..",
            "시간은 항상 부족하네..",
            "흠...",
            "간지러워",
            "응?",
            "긁적긁적",
            "하지 말라구",
            "일하기 싫다",
            "한 귀로 듣고 한 귀로 흘리고",
            "밥은 먹고 다니냐?",
            "사롸있네~",
            "27도는 춥고 28도는 덥고...",
            "에어컨을 키면 춥고 끄면 덥고...",
            "에어컨이 인류 최대의 발명품이지",
            "나는 나보다 약한 녀석의 명령 따위는 듣지 않는다.",
            "끝나지 않아! 사람의 꿈은!",
            "나 떨고 있냐?",
            "한 사람 말만 듣지 말라구",
            "아버지한테도 맞은 적이 없는데..",
            "아옳옳옳",
            "뭣이 중헌디",
            "어이가 엄네~?",
            "월화수목금금금",
            "오늘이 무슨 요일이지?",
            "살아있네~",
            "퉤!",
            "에라이 써글넘들,,,,",
            "캬아아아아악~ 퉤이~!!",
            "ㅋㅋㅋㅋㅋㅋ",
            "ㅎㅎㅎㅎㅎ",
            "ㅎ",
            "ㅋ",
            "쓰댕~~",
            "띠용~!!",
            "자신의 온기를 감싸안을뿐",
            "이런 이런...",
            "라면 먹고 갈래?",
            "뭐해?",
            "훔...",
            "모두 죽여야겠어!!",
            "악당의 세상이지...",
            "인간의 본성은 악이야!",
            "악이야 말로 세상을 구원할 것이다!",
            "복수는 달콤한 것!",
            "널 지배해주마",
            "정의는 항상 승리한다고?",
            "승자가 정의를 만든다!",
            "이 세상은 약육강식",
            "인간은 바이러스야.",
            "친구? 친구 따윈 없다.",
            "너희에게 파멸을 내릴 것이다.",
            "내가 새 시대를 열 것이다.",
            "아무도 살려두지 않겠다.",
            "사라지게 해주마.",
            "감히 이 몸을 화나게 하다니.",
            "때가 오고 있다...",
            "...",
            "언젠간... 이루리라...",
            "세계는 내 것이다.",
            "10원에 한대",
            "그리 나쁘진 않아.",
            "괜찮아요? 많이 놀랐죠?",
            "오, 이런!",
            "채팅이나 하라구",
            "자꼬 누르면 화낸다~",
            "어디 다친 데 없어요?",
            "인생은 고통이야. 몰랐어?",
            "재미있네... 어디 한번 해보지",
            "호오, 이건 대단한걸!",
            "안심해라. 너도 곧 편하게 해주마",
            "왜 그렇게 심각해?",
            "난 이기기 위해선 수단과 방법을 가리지 않아.",
            "죽이진 않을 거야. 단지 괴롭힐 뿐이지",
            "뭐야? 설마 날 이겼다고 생각하는 건가?",
            "뭐 이딴 게임을 만들었지?",
            "무슨 생각으로 만든걸까?",
            "어쩔 땐 광고가 더 재밌어",
            "농담이야",
            "난 전설 같은 거 믿지 않아",
            "슬픈 전설이 있어",
            "할 거 없을 땐 광고나 보라구",
            "재밌는 광고가 한다는데...?",
            "광고! 광고를 보자!",
            "인간은 신을 두려워하는 것이 아냐. 공포가 바로 신이지.",
            "죄는 짓는 게 아니라 만드는 거야",
            "증오심은 목표를 만드는 데 훌륭한 동기가 된다",
            "난 정의 따위 믿지 않아",
            "정의라는 건 누가 판단하지?",
            "그야말로 분노의 시대군..",
            "진짜 절망은 헛된 희망을 동반하지",
            "정의보다 중요한 건 바로 승리다!",
            "강한 놈이 오래가는 게 아니라, 오래 가는 놈이 강한 거드라",
            "분.시.좋.아",
            "오늘 저녁은 치킨이닭!!",
            "Let's get it!,",
            "Let me do it again~",
            "짬을 무시하면 안 되는 거라",
            "목이 터져나가 고래고래요. Say ho ho",
            "치요싸코 보이싸웃",
            "there is no cow level",
            "Show me the money",
            "Operation CWAL",
            "Power Overwhelming",
            "Black Sheep Wall",
            "사랑은 비극이어라",
            "작두위를 걷",
            "전 모르겠어요"
        };
}
