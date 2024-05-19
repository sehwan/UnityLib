using UnityEngine;
using UnityEngine.UI;
using System;

public class PopNumberInput : MonoBehaviour
{
    // Refrences
    public Text txt_message;
    public InputField input;

    // Data
    public float value = 0;
    public float min, max;
    public float step;
    Action<float> cb;


    public static void Show(string message, int min, int max, int step, Action<float> cb, int def = 0)
    {
        var PATH = "Prefabs/PopNumberInput";
        var _ = ((GameObject)Instantiate(Resources.Load(PATH))).GetComponent<PopNumberInput>();

        _.min = min;
        _.max = max;
        _.step = step;
        _.cb = cb;
        _.value = def;

        _.txt_message.text = message;
        _.input.placeholder.GetComponent<Text>().text = def.ToString();
        _.input.text = def.ToString();
    }
    public static void Show(string message, float min, float max, float step, Action<float> cb, float def = 0)
    {
        var PATH = "Prefabs/PopNumberInput";
        var _ = ((GameObject)Instantiate(Resources.Load(PATH))).GetComponent<PopNumberInput>();

        _.min = min;
        _.max = max;
        _.step = step;
        _.cb = cb;
        _.value = def;

        _.txt_message.text = message;
        _.input.placeholder.GetComponent<Text>().text = def.ToString();
        _.input.text = def.ToString();
    }


    // Value -> Text
    public void Btn_Up()
    {
        value += step;
        if (value > max) value = min;
        input.text = value.ToString();
        Verify();
    }
    public void Btn_Down()
    {
        value -= step;
        if (value < min) value = max;
        input.text = value.ToString();
        Verify();
    }
    // Text -> Value
    public void Verify()
    {
        bool b = float.TryParse(input.text, out value);
        if (b == false) return;
        value = Mathf.Clamp(value, min, max);
        input.text = value.ToString();
    }


    public void Btn_Ok()
    {
        cb?.Invoke(value);
        ESC();
    }
    public void ESC()
    {
        Destroy(gameObject);
    }
}
