
// 토스트 메세지.
// Toast.i.Show("가나다라마");
// Toast.i._duration = 1f;

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class Radio : MonoBehaviour
{
    public List<string> tips = new();
    public List<string> breaking = new();

    public GameObject go;
    public Text text;


    string[] texts = new string[]{
        "리빙에센스! 돈이 부족할 땐 알바를 하면 좋다.",
        "리빙에센스! 능력치를 올리고 싶을 땐 훈련을 하면 좋다.",
        "리빙에센스! 스트레스가 쌓였을 땐 외출을 하면 좋다.",
        "리빙에센스! 자유시간이 부족할 땐 광고를 보거나 충전을 기다리면 좋다.",
        "리빙에센스! 빨리 성장을 시키고 싶을 땐 결제를 하면 좋다",
        "리빙에센스! 게임의 별점을 높게 주는 것이 좋다",
        "리빙에센스! 구입한 장비가 마음에 들지 않는다면 평화나라에 되파는 것이 좋다.",
        "리빙에센스! 오는 말이 고우려면 가는 가는 말이 고운편이 좋다.",
        "리빙에센스! 음식이 싱거울 땐 소금을 넣으면 좋다.",
        "리빙에센스! 게임을 더욱 재미있게 즐기려면 친구들과 같이 하면 좋다.",
        "리빙에센스! 밸런스는 주기적으로 업데이트 된다.",
        "리빙에센스! 더 효율적인 활동을 선택하는 편이 좋다.",
        "리빙에센스! 능력치는 공격과 방어 모두에 쓰인다",
        "리빙에센스! 전투 스타일은 시간이 지나면 효과가 줄어드니 때가되면 교체해주는 것이 좋다",
        "리빙에센스! 요리를 할 때 음식이 싱겁다면 소금을 뿌리면 좋다",
        "리빙에센스! 맛을 봤을 때 단맛이 나는 포도가 맛이 좋다",
        "리빙에센스! 게임이 잘 동작하지 않을 때엔 재실행 하면 좋다.",
        "리빙에센스! 너무 더울 땐 에어컨을 켜는 편이 좋다.",
        "리빙에센스! 게임이 마음에 들지 않아도 응원해주는 편이 좋다.",
        "리빙에센스! 게임은 게임일 뿐 오해하지 않는 편이 좋다.",
        "리빙에센스! 조직 1위가 되면 조직 내 다른 유저를 무소속으로 쫓아버릴 수 있다.",
        "리빙에센스! 지역 1위가 되면 지역 내 다른 유저의 이름과 한마디를 지울 수 있다.",
        "리빙에센스! 스트레스는 몸에 좋지 않다.",
        "리빙에센스! 광고를 보는 데에는 돈이 들지 않는다.",
        "리빙에센스! 즐길 수 없다면 피하는 편이 좋다.",
        "리빙에센스! 광고가 이 게임보다 재미있다.",
        "리빙에센스! 새로운 아이템을 사려면 오래된 아이템을 구매해 슬롯을 비워야 한다.",
        "리빙에센스! 장비는 많이 사용하면 쓸 수 없게 된다.",
        "리빙에센스! 활동 목록이 갱신되면 활동 횟수도 갱신된다.",
        "리빙에센스! 랭킹은 하루에 한번, 활동 목록은 4번 갱신된다.",

        "오늘의 명언! 개발자는 광고가 필요해요",
        "오늘의 명언! 광고는 무료로 줍니다",
        "오늘의 명언! 이 게임은 무료로 해줍니다",
        "오늘의 명언! 행복한 사람들은 집에서 악플다는 일을 안 해요",
        "오늘의 명언! 게임은 잘하려고 하는 것이 아니라 친구를 이기려고 하는 것이다",
        "오늘의 명언! 분노의 시대는 무료로 즐길 수 있지만 배틀그라운드는 그렇지 않다",
        "오늘의 명언! 인터넷에서 읽은 모든 것을 그대로 믿지 마라 - 에이브러험 링컨",
        "오늘의 명언! 소년이여 야망을 가져라 - 30세 백수",
        "오늘의 명언! 포기하면 시합은 거기서 끝이 난다 - 30세 백수",
        "오늘의 명언! 세상이 널 가졌다고 생각하는가, 세상은 아직 널 가진 적이 없다 - 30세 무직",
        "오늘의 명언! 게임을 하면 이겨야지 - d.va",
        "오늘의 명언! 천재는 1%의 영감과 99%의 노력으로 이루어진다 (30세 무직)",
        "오늘의 명언! 너 자신을 알라 (30세, 무직)",
        "오늘의 명언! 류요 와가 테키오 쿠라에 - Genzi",
        "오늘의 명언! 속초로 가자! - 루시우",
        "오늘의 명언! 더 나은 세상을 위해 싸워요 - MEI",
        "오늘의 명언! 깽판치기 딱 좋은 날이네 - 정크랫",
        "오늘의 명언! 아곤 빠가 또바나스티 - Zarya",
        "오늘의 명언! 이 시간부로 우린 모두 군인이다 - 논산 훈련소 조교",
        "오늘의 명언! 쀼쀼~ - Bastion",
        "오늘의 명언! 당신의 적을 죽이겠어요. 재밌겠네요 - 오리아나",
        "오늘의 명언! 명을 받들겠습니다 - 소령(진) 티모",
    };


    void Start()
    {
        foreach (var item in texts) Add_Tip(item);
        StartCoroutine(Co_Show());
    }


    // 버튼.
    public void Button()
    {
    }

    public void Add_Breaking(string text)
    {
        breaking.Add(text);
    }
    public void Add_Tip(string text)
    {
        tips.Add(text);
    }

    IEnumerator Co_Show()
    {
        go.SetActive(false);
        text.gameObject.SetActive(false);
        while (true)
        {
            if (tips.Count > 300) tips.RemoveAt(0);

            //내용 설정.
            // _isAds = false;
            // _txt_button.text = "뉴스";
            if (breaking.Count > 0)
            {
                text.text = breaking[0];
                breaking.RemoveAt(0);
            }
            else if (tips.Count > 0)
            {
                // if (Random.Range(0, 100) >= 99)
                // {
                //     _isAds = true;
                //     _txt_button.text = "광고";
                //     _text.text = "광고! 지금 옆 버튼을 클릭하여 광고를 보시면 일반 광고 보상의 2배를 획득합니다! 주문 폭주중!";
                // }
                // else
                // {
                int r = Random.Range(0, tips.Count);
                string str = tips[r];
                text.text = str;
                // }
            }

            text.gameObject.SetActive(true);
            text.transform.localPosition = new Vector2(-120, 0);

            yield return new WaitForSeconds(3.5f);

            if (text.rectTransform.sizeDelta.x > 305)
            {
                while (text.transform.localPosition.x >= -130 - text.rectTransform.sizeDelta.x)
                {
                    yield return null;
                    text.transform.Translate(Vector2.left * 0.3f * Time.deltaTime);
                }
            }

            go.SetActive(false);
            text.gameObject.SetActive(false);
            yield return new WaitForSeconds(1);

            if (breaking.Count > 0)
                yield return new WaitForSeconds(0.5f);
            else
                yield return new WaitForSeconds(5f);
        }
    }
}
