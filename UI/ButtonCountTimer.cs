// using System;
// using System.Collections;
// using UnityEngine;
// using UnityEngine.UI;

// public class ButtonCountTimer : MonoBehaviour
// {
//     public int COOLTIME = 200;
//     public int remain;

//     public int COOL_CHARGE = 3600;
//     public int MAX_CHANCE = 20;


//     public Button btn;
//     public Image img_enabled;
//     public Text txt_remain;

//     public Text txt_adCount;


//     void Start()
//     {
//         btn = GetComponent<Button>();
//         // btn.onClick.AddListener(() =>
//         // {
//         //     Inactivate();
//         // });
//         Inactivate();
//     }
//     public void Init()
//     {
//         GM.inst.StartCoroutine(Co_Timer());
//     }

//     IEnumerator Co_Timer()
//     {
//         WaitForSecondsRealtime w = new WaitForSecondsRealtime(1);
//         while (true)
//         {
//             //Cooltime
//             if (remain > 0)
//             {
//                 txt_remain.text = string.Format("{0}s", remain);
//                 remain--;
//             }
//             else txt_remain.SetActive(false);
//             //Chance
//             int chance = User.data.dt_ads.GetChance(COOL_CHARGE, MAX_CHANCE);
//             if (chance > 0) txt_adCount.FractionalText(chance, MAX_CHANCE);
//             else txt_adCount.text = (User.data.dt_ads.AddSeconds(COOL_CHARGE + 1) - DateTime.Now).ToFormattedString();
//             //
//             if (remain <= 0 && chance > 0) Activate();
//             yield return w;
//         }
//     }
//     public void Inactivate()
//     {
//         btn.interactable = false;
//         img_enabled.SetActive(false);
//         remain = COOLTIME;
//         txt_remain.text = string.Format("{0}s", remain);
//         txt_remain.SetActive(true);
//     }

//     public void Activate()
//     {
//         btn.interactable = true;
//         img_enabled.SetActive(true);
//         txt_remain.SetActive(false);
//     }
// }