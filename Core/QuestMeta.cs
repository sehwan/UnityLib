using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestMeta : Rsc
{
    public string id;
    public int req;
}
public class QuestData
{
    public int v; // value
    public int l; // finished or level
}