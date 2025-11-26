// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.ComponentModel;
// using UnityEngine;
// using Unity.Services.Authentication;
// using Unity.Services.Core;
// using Unity.Services.Vivox;
// using VivoxUnity;
// using System.Threading.Tasks;

// public class VivoxMng : MonoBehaviour
// {
//     public static VivoxMng inst;
//     [Immutable] public string curChannel;
//     ILoginSession loginSession;


//     #region Base & Login
//     void Awake()
//     {
//         inst = this;
//     }
//     async void Start()
//     {
//         while (
//             UnityServicesMng.inst.wasSigned == false ||
//             User.WasInit() == false)
//             await Task.Yield();
//         LoginAndJoin(User.data.name, User.data.force.ToString());
//     }

//     public async void LoginAndJoin(string displayName, string channel)
//     {
//         // Login
//         Login(displayName);
//         while (loginSession != null && loginSession.State == LoginState.LoggingIn) await Task.Yield();
//         if (loginSession?.State != LoginState.LoggedIn)
//         {
//             print("failed login");
//             return;
//         }
//         // Join
//         JoinChannel(channel, ChannelType.NonPositional, false, true);
//         while (channelSession?.ChannelState == ConnectionState.Connecting) await Task.Yield();
//         if (channelSession?.ChannelState != ConnectionState.Connected)
//         {
//             print("failed Join");
//             return;
//         }
//     }

//     public void Login(string displayName)
//     {
//         if (loginSession != null && loginSession.State != LoginState.LoggedOut)
//         {
//             print($"Current State ({loginSession.State}) so Can't Login");
//             return;
//         }
//         var account = new Account(displayName);
//         loginSession = VivoxService.Instance.Client.GetLoginSession(account);
//         loginSession.PropertyChanged += OnLoginSessionPropertyChanged;
//         loginSession.BeginLogin(loginSession.GetLoginToken(), SubscriptionMode.Accept, null, null, null, ar =>
//         {
//             try
//             {
//                 loginSession.EndLogin(ar);
//             }
//             catch (Exception e)
//             {
//                 Debug.LogError(e);
//             }
//         });
//     }
//     public void LogOut()
//     {
//         loginSession.Logout();
//     }
//     void OnLoginSessionPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
//     {
//         print($"chat {((ILoginSession)sender).State}");
//     }
//     #endregion


//     #region Channel
//     IChannelSession channelSession;
//     public void JoinChannel(string ch, ChannelType type, bool audio, bool text, bool transmissionSwitch = true, Channel3DProperties properties = null)
//     {
//         if (loginSession?.State != LoginState.LoggedIn)
//         {
//             Debug.LogError("Can't join a channel when not logged in.");
//             return;
//         }
//         Channel channel = new Channel(ch, type, properties);
//         channelSession = loginSession.GetChannelSession(channel);
//         BindChannelHandlers(true, channelSession);
//         channelSession.BeginConnect(audio, text, transmissionSwitch, channelSession.GetConnectToken(), ar =>
//         {
//             try
//             {
//                 channelSession.EndConnect(ar);
//                 curChannel = ch;
//             }
//             catch (Exception e)
//             {
//                 Debug.LogError($"Could not connect to channel: {e.Message}");
//                 return;
//             }
//         });
//     }
//     public void LeaveChannel()
//     {
//         channelSession.i.disconnect();
//         BindChannelHandlers(false, channelSession);
//     }

//     void BindChannelHandlers(bool bBind, IChannelSession session)
//     {
//         if (bBind)
//         {
//             session.PropertyChanged += OnChannelPropertyChanged;
//             // Participants
//             session.Participants.AfterKeyAdded += OnParticipantAdded;
//             session.Participants.BeforeKeyRemoved += OnParticipantRemoved;
//             session.Participants.AfterValueUpdated += OnParticipantValueUpdated;
//             //Messaging
//             session.MessageLog.AfterItemAdded += OnChannelMessageReceived;
//         }
//         else
//         {
//             session.PropertyChanged -= OnChannelPropertyChanged;
//             // Participants
//             session.Participants.AfterKeyAdded -= OnParticipantAdded;
//             session.Participants.BeforeKeyRemoved -= OnParticipantRemoved;
//             session.Participants.AfterValueUpdated -= OnParticipantValueUpdated;
//             //Messaging
//             session.MessageLog.AfterItemAdded -= OnChannelMessageReceived;
//         }
//     }
//     void OnChannelPropertyChanged(object sender, PropertyChangedEventArgs args)
//     {
//         IChannelSession source = (IChannelSession)sender;
//         print($"ch {source.ChannelState}");
//     }
//     void OnParticipantAdded(object sender, KeyEventArg<string> arg)
//     {
//         ValidateArgs(new object[] { sender, arg });
//         var source = (VivoxUnity.IReadOnlyDictionary<string, IParticipant>)sender;
//         var participant = source[arg.Key];
//         var username = participant.Account.Name;
//         var channel = participant.ParentChannelSession.Key;
//         var channelSession = participant.ParentChannelSession;
//         //Do what you want with the information
//     }
//     void OnParticipantRemoved(object sender, KeyEventArg<string> arg)
//     {
//         ValidateArgs(new object[] { sender, arg });
//         var source = (VivoxUnity.IReadOnlyDictionary<string, IParticipant>)sender;
//         var participant = source[arg.Key];
//         var username = participant.Account.Name;
//         var channel = participant.ParentChannelSession.Key;
//         var channelSession = participant.ParentChannelSession;
//         // uIManager.DeleteUserMuteObjectUI(username);
//         // if (participant.IsSelf)
//         // {
//         //     BindHandlers(false, channelSession); //Unsubscribe from events here
//         //     currentChannelID = null;

//         //     var user = client.GetLoginSession(accountId);
//         //     user.DeleteChannelSession(channelSession.Channel);
//         // }
//     }
//     void OnParticipantValueUpdated(object sender, ValueEventArg<string, IParticipant> arg)
//     {
//         ValidateArgs(new object[] { sender, arg });
//         var source = (VivoxUnity.IReadOnlyDictionary<string, IParticipant>)sender;
//         var participant = source[arg.Key];
//         string username = arg.Value.Account.Name;
//         ChannelId channel = arg.Value.ParentChannelSession.Key;
//         string property = arg.PropertyName;
//         switch (property)
//         {
//             case "LocalMute":
//                 {
//                     if (username != User.inst.name) //can't local mute yourself, so don't check for it
//                     {
//                         //update their muted image
//                     }
//                     break;
//                 }
//             // case "LocalMute":
//             //     {
//             //         //update speaking indicator image
//             //         break;
//             //     }
//             default:
//                 break;
//         }
//     }
//     static void ValidateArgs(object[] objs)
//     {
//         foreach (var obj in objs)
//         {
//             if (obj == null)
//                 throw new ArgumentNullException(obj.GetType().ToString(), "Specify a non-null/non-empty argument.");
//         }
//     }
//     #endregion


//     #region Messaging
//     public void SendChannelMessage(string msg)
//     {
//         channelSession.BeginSendText("", msg, "stanza", "stanzabody", ar =>
//         {
//             try
//             {
//                 channelSession.EndSendText(ar);
//                 Debug.Log($"({curChannel}){User.data.name} : {msg}");
//             }
//             catch (Exception e)
//             {
//                 Debug.LogError(e);
//                 return;
//             }
//         });
//         // channelSession.BeginSendText(msg, ar =>
//         // {
//         //     try
//         //     {
//         //         channelSession.EndSendText(ar);
//         //         Debug.Log($"({curChannel}) {User.data.name} : {msg}");
//         //     }
//         //     catch (Exception e)
//         //     {
//         //         Debug.LogError(e);
//         //         return;
//         //     }
//         // });
//     }
//     void OnChannelMessageReceived(object sender, QueueItemAddedEventArgs<IChannelTextMessage> args)
//     {
//         var channelName = args.Value.ChannelSession.Channel.Name;
//         var senderName = args.Value.Sender.DisplayName;
//         var message = args.Value.Message;
//         Debug.Log($"({channelName}){senderName} : {message}");
//         if (args.Value.ApplicationStanzaNamespace.IsFilled())
//             print($"{args.Value.ApplicationStanzaNamespace} / {args.Value.ApplicationStanzaBody}");
//         UM.Get<UIChatbox>().AddText($"({channelName}) {senderName} : {message}");
//     }
//     #endregion
// }
