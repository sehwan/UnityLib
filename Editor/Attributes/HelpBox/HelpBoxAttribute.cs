using UnityEngine;
using System;

/* Sample
[SerializeField]
[HelpBox("Values are 0 for the primary button (often the left button)")]
int _button;

[SerializeField]
[HelpBox("Lorem ipsum dolor sit amet, consectetur adipiscing elit",
    "http://diegogiacomelli.com.br/",
    HelpBoxType.Info)]
string _infoSample;

[SerializeField]
[HelpBox("Warning sample HelpBox with docs button",
    "http://diegogiacomelli.com.br/unitytips-helpbox-attribute/",
    HelpBoxType.Warning)]
string _warningSample;

[SerializeField]
[HelpBox("Error sample HelpBox with docs button",
    "http://diegogiacomelli.com.br/unitytips-helpbox-attribute/",
    HelpBoxType.Error)]
string _errorSample;
 */


public enum HelpBoxType { None, Info, Warning, Error }

[AttributeUsage(AttributeTargets.Field)]
public class HelpBoxAttribute : PropertyAttribute
{
    public HelpBoxAttribute(string text, string docsUrl = null, HelpBoxType type = HelpBoxType.Info)
    {
        Text = text;
        DocsUrl = docsUrl;
        Type = type;
    }

    public string Text { get; }
    public string DocsUrl { get; }
    public HelpBoxType Type { get; }
}