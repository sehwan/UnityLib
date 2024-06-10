using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MessageSender : MonoBehaviour
{
    public enum ValueType { Int, Float, String }
    public string method;
    public ValueType valueType;
    public int integer;
    public float floating;
    public string str;

    public void _Send()
    {
        switch (valueType)
        {
            case ValueType.Int:
                SendMessage(method, integer);
                break;
            case ValueType.Float:
                SendMessage(method, floating);
                break;
            case ValueType.String:
                SendMessage(method, str);
                break;
        }
    }
}
