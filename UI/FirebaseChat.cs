// using System;
// using System.Threading.Tasks;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using Firebase.Database;
// using Firebase.Extensions;

// public class FirebaseChat : MonoBehaviour
// {
//     [Header("Settings")]
//     public float FOCUSED_Y_POS = -680;
//     public const int LIMIT_MSG_LENGTH = 120;
//     public const int Unlock_Chat_Progress = 8;
//     public const int SEC_COUNT_CHAT = 6;
//     public const int MAX_TIMES_CHAT = 5;
//     public const int SEC_WAIT_TALKATIVE = 10;

//     [Header("Data")]
//     public string room;
//     DatabaseReference refer;

//     [Header("Chatter Properties")]
//     DateTime chattedAt = DateTime.Now;
//     DateTime chatAbleAt = DateTime.Now;
//     public int cnt_chat;

//     [Header("UI")]
//     public RectTransform box;
//     public InputField input;
//     static public Text txt_lastMsg;

//     [Header("Bubble Type")]
//     public Transform contents;
//     public ScrollRect sr;

//     [Header("Items")]
//     public int iter;
//     public UIChatItem[] items;
//     public Queue<FirebaseChatData> queue = new Queue<FirebaseChatData>();

//     public void Init(string reference)
//     {
//         items = contents.GetComponentsInChildren<UIChatItem>();
//         items.ForEach(e => e.gameObject.SetActive(false));
//         refer = FirebaseDatabase.DefaultInstance.GetReference($"chat/{reference}");
//         refer.ChildAdded += OnChildAdded;
//         txt_lastMsg = UM.Scene<MainScenePanel>().txt_lastMsg;
//     }
//     void OnChildAdded(object sender, ChildChangedEventArgs args)
//     {
//         if (args.DatabaseError != null)
//         {
//             Debug.LogError(args.DatabaseError.Message);
//             return;
//         }
//         if (args.Snapshot.Exists == false) return;
//         AddMessage(args.Snapshot.GetRawJsonValue().ToObject<FirebaseChatData>());
//     }

//     void OnEnable()
//     {
//         ScrollToLast();
//     }

//     public void Btn_SendChatMessage()
//     {
//         if (input.text.IsNullOrEmpty()) return;
//         if (input.text.IsStringAvailable(LIMIT_MSG_LENGTH) == false) return;
//         if (User.data.Progress < Unlock_Chat_Progress)
//         {
//             ToastGroup.Show("It is available after Stage {0}".LF(Unlock_Chat_Progress).TagColor("yellow"));
//             return;
//         }
//         if (DateTime.Now < chatAbleAt)
//         {
//             ToastGroup.Alert("{0}초 이후 가능".LF((int)((chatAbleAt - DateTime.Now).TotalSeconds + 1)));
//             return;
//         }
//         var mute = (int)(UM.Get<ChatPanel>().mute.ToDateTime() - DateTime.Now).TotalSeconds;
//         if (mute > 0)
//         {
//             ToastGroup.Show("{0} 이후 가능".LF(mute.ToTimeString()));
//             return;
//         }
//         // Send
//         Send(FirebaseChatData.GetNick(), null, input.text);
//     }

//     public void Send(string nick, string type, string msg)
//     {
//         var dic = new Dictionary<string, object>();
//         dic.Add("n", nick);
//         dic.Add("t", type);
//         dic.Add("m", msg);
//         dic.Add("h", User.data.hero.equip_now[3]);
//         // Room
//         refer.Push().SetValueAsync(dic).ContinueWithOnMainThread(task =>
//          {
//              if (task.IsCanceled || task.IsFaulted)
//              {
//                  ToastGroup.Show("Failed...".L());
//                  return;
//              }
//              ToastGroup.Show("Sended");
//              input.text = null;
//          });

//         int passed = (int)(DateTime.Now - chattedAt).TotalSeconds;
//         if (passed <= 3) cnt_chat++;
//         else cnt_chat = 0;
//         if (cnt_chat > 2) chatAbleAt = DateTime.Now.AddSeconds(8);
//         chattedAt = DateTime.Now;
//     }

//     // Output
//     public void AddMessage(FirebaseChatData msg)
//     {
//         items[iter].Init(msg);
//         items[iter].transform.SetAsLastSibling();
//         iter++;
//         if (iter >= items.Length) iter = 0;

//         txt_lastMsg.text = msg.n.IsFilled() ? msg.To1LineString() : msg.m;
//         ScrollToLast();
//     }

//     public async void ScrollToLast()
//     {
//         if (gameObject.activeInHierarchy == false) return;
//         // if (Input.GetMouseButton(0)) return;
//         await Task.Delay(50);

//         sr.verticalNormalizedPosition = 0f;
//     }

//     public void OnBeginScrolling()
//     {
//         CameraWork.inst.obstructors.AddIfNotExists(gameObject);
//     }
//     public void OnEndScrolling()
//     {
//         CameraWork.inst.obstructors.Remove(gameObject);
//     }
//     public void OnPointerDown()
//     {
//         CameraWork.inst.obstructors.AddIfNotExists(sr.gameObject);
//     }
//     public void OnPointerUp()
//     {
//         CameraWork.inst.obstructors.Remove(sr.gameObject);
//     }

//     void CheckBoxPos()
//     {
//         // UI Positioning
//         if (input.isFocused)
//         {
//             box.anchorMin = new Vector2(0, 1);
//             box.anchorMax = new Vector2(1, 1);
//         }
//         else
//         {
//             box.anchorMin = new Vector2(0, 0);
//             box.anchorMax = new Vector2(1, 0);
//         }
//     }
// }