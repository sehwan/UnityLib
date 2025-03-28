// #define I2LOC

using System;
using System.Text;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;

public static class StringEx
{
    public static string ToTitleCase(this string s) =>
        System.Globalization.CultureInfo.InvariantCulture.TextInfo.ToTitleCase(s.ToLower());

    public static T ToEnum<T>(this string me)
    {
        return (T)Enum.Parse(typeof(T), me);
    }


    #region Localization
    public static void ChangeLanguage(string me)
    {
#if I2LOC
        Debug.Log($"<color=cyan>{me}</color>");
        I2.Loc.LocalizationManager.CurrentLanguage = me;
#endif
        if (GM.i.state == GameState.Playing)
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        else ToastGroup.Alert("NeedsToRestart".L());
    }
    public static string L(this string me)
    {
        if (me.IsNullOrEmpty()) return me;
#if I2LOC
        string r = I2.Loc.LocalizationManager.GetTranslation(me);
        if (r.IsNullOrEmpty())
        {
            Debug.Log("No Translation " + me.TagColor("yellow"));
            return me;
        }
        return r.Replace("<br>", "\n");
#else
        return me;
#endif
    }
    public static string TryL(this string me)
    {
        if (me.IsNullOrEmpty()) return null;
#if I2LOC
        string r = I2.Loc.LocalizationManager.GetTranslation(me);
        if (r.IsNullOrEmpty()) return me;
        return r.Replace("<br>", "\n");
#else
        return null;
#endif
    }
    public static string LF(this string me, params object[] arg)
    {
        if (me.IsNullOrEmpty()) return me;
#if I2LOC
        string r = I2.Loc.LocalizationManager.GetTranslation(me);
        if (r.IsNullOrEmpty())
        {
            Debug.Log("No Translation " + me.TagColor("yellow"));
            return string.Format(me, arg);
        }
        return string.Format(r.Replace("<br>", "\n"), arg);
#else
        return string.Format(me, arg);
#endif
    }
    public static string L(this string me, string add) => (me + add).L();


    public static void TranslateI2(this string me, Action err, Action<string> cb)
    {
#if I2LOC
        string languageCode = System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
        I2.Loc.GoogleTranslation.Translate(me, "",
            languageCode,
            (result, err) =>
            {
                if (err != null) Debug.LogError(err);
                else cb(result);
            }
        );
#endif
    }
    public async static Task TranslateFirebase(this string me, Action err, Action<string> cb)
    {
        var r = await NetworkMng.i.FuncAsync("user-translate", false,
            ("q", me),
            ("to", Application.systemLanguage));
        if (r.IsError()) err();
        else cb(r.Text());
    }
    #endregion


    #region Tag
    public static string TagColor(this string me, string color)
    {
        if (color == "gray") color = "grey";
        return $"<color={color}>{me}</color>";
    }
    public static string TagColor(this string me, Color color)
    {
        return $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{me}</color>";
    }
    public static string TagSize(this string me, int size)
    {
        return $"<size={size}>{me}</size>";
    }
    public static string TagBold(this string me)
    {
        return $"<b>{me}</b>";
    }
    #endregion


    #region Log
    public static void Log(this object me, string color = "cyan")
    {
#if UNITY_EDITOR
        Debug.Log($"--<color={color}>{me}</color>");
#else
        Debug.Log($"--{me}");
#endif
    }
    #endregion

    ///<summary>
    /// string.IsNullOrEmpty
    ///</summary>
    public static bool IsNullOrEmpty(this string me)
    {
        return string.IsNullOrEmpty(me);
    }
    public static bool IsFilled(this string me)
    {
        return !string.IsNullOrEmpty(me);
    }
    public static bool IsStringAvailable(this string me, int length = 40)
    {
        me = me.Trim();
        if (string.IsNullOrEmpty(me)) return false;
        if (me.Length > length)
        {
            ToastGroup.Show("LimitTextLength".LF(me.Length, length));
            return false;
        }
        if (me.Contains("</"))
        {
            ToastGroup.Show("It Contains Impossible Characters");
            return false;
        }
        // if (CheckInsult.CheckedString(str))
        // {
        // ToastGroup.Show("It Contains Impossible Characters");
        //     return false;
        // }
        return true;
    }


    // 0보다 높으면 lime, 낮으면 orange
    public static string MarkColor(this int me, string plus = "lime", string minus = "orange")
    {
        if (me > 0) return $"<color={plus}>+{me:n0}</color>";
        else if (me < 0) return $"<color={minus}>{me:n0}</color>";
        return me.ToString("n0");
    }
    public static string MarkColor(this float me, int decimalPlaces = 2)
    {
        var format = "f" + decimalPlaces;
        if (me > 0) return $"<color=lime>+{me.ToString(format)}</color>";
        else if (me < 0) return $"<color=orange>{me.ToString(format)}</color>";
        return me.ToString(format);
    }
    public static string MarkColor(this int me, string sign)
    {
        if (me == 0) return $"{me}{sign}";
        else if (me > 0) return $"<color=lime>+{me:n0}{sign}</color>";
        else return $"<color=red>{me:n0}{sign}</color>";
    }
    public static string MarkColor(this float me, string sign, int decimalPlaces = 2)
    {
        var format = "f" + decimalPlaces;
        if (me == 0) return $"{me}{sign}";
        else if (me > 0) return $"<color=lime>+{me.ToString(format)}{sign}</color>";
        else return $"<color=red>{me.ToString(format)}{sign}</color>";
    }
    // public static string MarkColorAndSign(BigNumber n, string sign)
    // {
    //     if (n == 0) return $"{n}";
    //     else if (n > 0) return $"<color=lime>+{n:n0}{sign}</color>";
    //     else return $"<color=red>{n:n0}{sign}</color>";
    // }


    public static bool GetPrefsBool(this string me, int def = 0)
    {
        return PlayerPrefs.GetInt(me, def).ToBool();
    }
    public static void SetPrefsBool(this string me, bool b)
    {
        PlayerPrefs.SetInt(me, b.ToInt());
    }


    static public string GetDiffFromJson(string a, string b)
    {
        var setA = a.Split(',');
        var setB = b.Split(',');
        StringBuilder r = new();
        setB.Except(setA).ToList().ForEach(e => r.AppendLine(e));
        setA.Except(setB).ToList().ForEach(e => r.AppendLine(e));
        return r.ToString();
    }


    static public Dictionary<string, string> Dictionarize(this string me)
    {
        return Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(me);
    }


    static public string UnStringrize(this string me)
    {
        string r = me;
        r = r.Replace("\\\"", "\"");
        // r = r.Replace("\"\\", "\"");
        // r = r.Replace("\"\"", "\"");

        r = r.Replace("\"{", "{");
        r = r.Replace("}\"", "}");
        r = r.Replace("\"[", "[");
        r = r.Replace("]\"", "]");

        r = r.Replace("\"null\"", "\"\"");
        r = r.Replace("null", "\"\"");
        return r;
    }


    static public string BreakLine(this string me, int limit)
    {
        if (me.Length < limit) return me;
        string r = me;
        int line = r.Length / limit;
        for (int i = 1; i <= line; i++)
        {
            r = r.Insert(i * 40, "\n");
        }
        return r;
    }


    public static string Decrypt(this string textToDecrypt, string key)
    {
        RijndaelManaged rijndaelCipher = new()
        {
            Mode = CipherMode.CBC,
            Padding = PaddingMode.PKCS7,
            KeySize = 128,
            BlockSize = 128
        };
        byte[] encryptedData = Convert.FromBase64String(textToDecrypt);
        byte[] pwdBytes = Encoding.UTF8.GetBytes(key);
        byte[] keyBytes = new byte[16];
        int len = pwdBytes.Length;
        if (len > keyBytes.Length)
        {
            len = keyBytes.Length;
        }
        Array.Copy(pwdBytes, keyBytes, len);
        rijndaelCipher.Key = keyBytes;
        rijndaelCipher.IV = keyBytes;
        byte[] plainText = rijndaelCipher.CreateDecryptor().TransformFinalBlock(encryptedData, 0, encryptedData.Length);
        return Encoding.UTF8.GetString(plainText);
    }

    public static string Encrypt(this string textToEncrypt, string key)
    {
        RijndaelManaged rijndaelCipher = new()
        {
            Mode = CipherMode.CBC,
            Padding = PaddingMode.PKCS7,
            KeySize = 128,
            BlockSize = 128
        };
        byte[] pwdBytes = Encoding.UTF8.GetBytes(key);
        byte[] keyBytes = new byte[16];
        int len = pwdBytes.Length;
        if (len > keyBytes.Length)
        {
            len = keyBytes.Length;
        }

        Array.Copy(pwdBytes, keyBytes, len);
        rijndaelCipher.Key = keyBytes;
        rijndaelCipher.IV = keyBytes;
        ICryptoTransform transform = rijndaelCipher.CreateEncryptor();
        byte[] plainText = Encoding.UTF8.GetBytes(textToEncrypt);
        return Convert.ToBase64String(transform.TransformFinalBlock(plainText, 0, plainText.Length));
    }


    // public static string Diff(this JObject source, JObject target)
    // {
    //     var sourceKeys = source.Properties().Select(p => p.Name).ToArray();
    //     var targetKeys = target.Properties().Select(p => p.Name).ToArray();
    //     var keys = new List<string>();
    //     keys.AddRange(sourceKeys);
    //     keys.AddRange(targetKeys);
    //     keys = keys.Distinct().ToList();

    //     string temp = 
    //     foreach (var k in keys)
    //     {
    //         if (source[k].Type == JTokenType.Object ||
    //             source[k].Type == JTokenType.Array ||
    //             target[k].Type == JTokenType.Object ||
    //             target[k].Type == JTokenType.Array)
    //         {

    //             if (source[k] != null && JToken.DeepEquals(source[k], target[k]) == false)
    //             {
    //                 Debug.Log($"<color=blue>removed {k}</color>");
    //                 // temp.Property(k).Remove();
    //             }
    //         }
    //     }
    //     return temp.ToString();
    // }
    // void RecursiveGetDiff(JToken a, JToken b)
    // {
    //     if (a.Type == JTokenType.Object ||
    //         a.Type == JTokenType.Array ||
    //         b.Type == JTokenType.Object ||
    //         b.Type == JTokenType.Array)
    //     {

    //     }
    // }

    public static StringBuilder CompareObjects(this JObject source, JObject target)
    {
        StringBuilder returnString = new();
        foreach (KeyValuePair<string, JToken> sourcePair in source)
        {
            if (sourcePair.Value.Type == JTokenType.Object)
            {
                if (target.GetValue(sourcePair.Key) == null)
                {
                    returnString.Append(sourcePair.Key + " not found" + Environment.NewLine);
                }
                else if (target.GetValue(sourcePair.Key).Type != JTokenType.Object)
                {
                    returnString.Append(sourcePair.Key + " is not an object in target" + Environment.NewLine);
                }
                else
                {
                    returnString.Append(CompareObjects(sourcePair.Value.ToObject<JObject>(),
                        target.GetValue(sourcePair.Key).ToObject<JObject>()));
                }
            }
            else if (sourcePair.Value.Type == JTokenType.Array)
            {
                if (target.GetValue(sourcePair.Key) == null)
                {
                    returnString.Append(sourcePair.Key + " not found" + Environment.NewLine);
                }
                else
                {
                    returnString.Append(CompareArrays(sourcePair.Value.ToObject<JArray>(),
                        target.GetValue(sourcePair.Key).ToObject<JArray>(), sourcePair.Key));
                }
            }
            else
            {
                JToken expected = sourcePair.Value;
                var actual = target.SelectToken(sourcePair.Key);
                if (actual == null)
                {
                    returnString.Append(sourcePair.Key + " not found" + Environment.NewLine);
                }
                else
                {
                    if (!JToken.DeepEquals(expected, actual))
                    {
                        returnString.Append(sourcePair.Key + ": "
                                            + sourcePair.Value + "->"
                                            + target.Property(sourcePair.Key).Value
                                            + Environment.NewLine);
                    }
                }
            }
        }
        return returnString;
    }
    public static StringBuilder CompareArrays(JArray source, JArray target, string arrayName = "")
    {
        var returnString = new StringBuilder();
        for (var index = 0; index < source.Count; index++)
        {

            var expected = source[index];
            if (expected.Type == JTokenType.Object)
            {
                var actual = (index >= target.Count) ? new JObject() : target[index];
                returnString.Append(CompareObjects(expected.ToObject<JObject>(),
                    actual.ToObject<JObject>()));
            }
            else
            {

                var actual = (index >= target.Count) ? "" : target[index];
                if (!JToken.DeepEquals(expected, actual))
                {
                    if (String.IsNullOrEmpty(arrayName))
                    {
                        returnString.Append("Index " + index + ": " + expected
                                            + " != " + actual + Environment.NewLine);
                    }
                    else
                    {
                        returnString.Append("Key " + arrayName
                                            + "[" + index + "]: " + expected
                                            + " != " + actual + Environment.NewLine);
                    }
                }
            }
        }
        return returnString;
    }


    public static string[] Split(this string me, string separator)
    {
        return Regex.Split(me, separator);
    }
    public static string LeaveOnlyLastLines(this string ori, int maxLines)
    {
        var lines = ori.Split('\n');
        if (lines.Length > maxLines)
        {
            var sb = new System.Text.StringBuilder();
            for (int i = lines.Length - maxLines; i < lines.Length; i++)
                if (lines[i].IsFilled()) sb.AppendLine(lines[i]);
            return sb.ToString();
        }
        return ori;
    }

    public static IEnumerable<string> SplitByLength(this string me, int length)
    {
        while (me.Length > length)
        {
            yield return me.Substring(0, length);
            me = me.Substring(length);
        }
        if (me.Length > 0) yield return me;
    }

    public static string IconTag(this string me)
    {
        return $"<sprite name=\"{me}\">";
    }

    public static StringBuilder AppendLine(this StringBuilder sb, int number)
    {
        return sb.AppendLine(number.ToString());
    }
    public static StringBuilder AppendLine(this StringBuilder sb, float number)
    {
        return sb.AppendLine(number.ToString());
    }
}