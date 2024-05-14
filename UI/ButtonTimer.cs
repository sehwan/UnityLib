// using System.Collections;
// using UnityEngine;
// using UnityEngine.UI;

// public class ButtonTimer : MonoBehaviour
// {
//     public int COOLTIME = 200;
//     public int remain;


//     Button btn;
//     public Image img_enabled;
//     public Text txt;


//     void Start()
//     {
//         btn = GetComponent<Button>();
//         btn.onClick.AddListener(() =>
//         {
//             ReStart();
//         });
//         ReStart();
//     }
//     void ReStart()
//     {
//         if (co_timer != null) StopCoroutine(co_timer);
//         co_timer = GM.inst.StartCoroutine(Co_Timer());
//     }

//     Coroutine co_timer;
//     IEnumerator Co_Timer()
//     {
//         Inactivate();
//         //Waiting
//         WaitForSecondsRealtime w = new WaitForSecondsRealtime(1);
//         while (remain > 0)
//         {
//             txt.text = string.Format("{0}s", remain);
//             yield return w;
//             remain--;
//         }
//         Activate();
//     }

//     void Inactivate()
//     {
//         remain = COOLTIME;
//         btn.interactable = false;
//         img_enabled.SetActive(false);
//         txt.SetActive(true);
//         remain = COOLTIME;
//     }

//     void Activate()
//     {
//         btn.interactable = true;
//         img_enabled.SetActive(true);
//         txt.SetActive(false);
//     }
// }
