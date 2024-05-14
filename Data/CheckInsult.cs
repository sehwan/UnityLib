using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class CheckInsult : MonoSingleton<CheckInsult>
{
    public TextAsset asset;
    public static string[] insults;


    protected override void Awake()
    {
        insults = asset.text.Split(new char[0], System.StringSplitOptions.RemoveEmptyEntries);
    }


    public static bool CheckedString(string text)
    {
        bool isOK = insults.Any(text.Contains);
        return isOK;
    }
}
