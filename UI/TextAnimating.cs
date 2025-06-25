using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// 각 표시 문자별 효과 정보
struct CharEffectInfo
{
    public List<float> shakeIntensities; // 누적합
    public List<float> pulseScales;      // 누적곱
    public bool rainbow;
}

public class TextAnimating : MonoBehaviour
{
    TMP_Text tmp;
    string rawText;
    float defaultShakeIntensity = 2f;
    float defaultPulse = 1f;


    void Awake()
    {
        tmp = GetComponent<TMP_Text>();
    }


    List<CharEffectInfo> charEffects = new();


    public void Text(string str)
    {
        rawText = str;
        ParseTagsAndBuildCharEffects();
        tmp.text = RemoveEffectTags(rawText);
    }

    void Update()
    {
        tmp.ForceMeshUpdate();
        ApplyAllEffects();
    }

    void ApplyAllEffects()
    {
        if (tmp.textInfo.characterCount == 0) return;
        var info = tmp.textInfo;
        int charCount = info.characterCount;
        for (int i = 0; i < charCount; i++)
        {
            var cinfo = info.characterInfo[i];
            if (!cinfo.isVisible) continue;
            int vi = cinfo.vertexIndex;
            int mi = cinfo.materialReferenceIndex;
            Vector3[] vertices = info.meshInfo[mi].vertices;
            Color32[] colors = info.meshInfo[mi].colors32;
            Vector3[] origVerts = new Vector3[4];
            for (int j = 0; j < 4; j++) origVerts[j] = vertices[vi + j];
            Vector3 center = (origVerts[0] + origVerts[1] + origVerts[2] + origVerts[3]) / 4f;

            // 효과 정보 가져오기
            if (i >= charEffects.Count) continue;
            var eff = charEffects[i];

            // 펄스: scale 누적 곱
            float scale = 1f;
            foreach (var s in eff.pulseScales) scale *= s;
            for (int j = 0; j < 4; j++)
            {
                Vector3 dir = origVerts[j] - center;
                vertices[vi + j] = center + dir * (1f + Mathf.Sin(Time.time * 4f + i) * 0.5f * (scale - 1f));
            }
            // 흔들림: intensity 누적 합
            float intensity = 0f;
            foreach (var inten in eff.shakeIntensities) intensity += inten;
            if (intensity > 0f)
            {
                float x = (Mathf.PerlinNoise(i, Time.time * 8f) - 0.5f) * 2f;
                float y = (Mathf.PerlinNoise(i + 100f, Time.time * 8f) - 0.5f) * 2f;
                Vector3 offset = new Vector3(x, y, 0f) * intensity;
                for (int j = 0; j < 4; j++)
                    vertices[vi + j] += offset;
            }
            // 무지개: 마지막 값만 적용(덮어쓰기)
            if (eff.rainbow)
            {
                Color c = Color.HSVToRGB((Time.time * 0.5f + i * 0.08f) % 1f, 1f, 1f);
                for (int j = 0; j < 4; j++)
                    colors[vi + j] = c;
            }
            Mesh mesh = info.meshInfo[mi].mesh;
            mesh.vertices = vertices;
            mesh.colors32 = colors;
            tmp.UpdateGeometry(mesh, mi);
        }
    }

    string RemoveEffectTags(string input)
    {
        input = Regex.Replace(input, "<shake(=[0-9.]+)?>|</shake>", "");
        input = Regex.Replace(input, "<rainbow>|</rainbow>", "");
        input = Regex.Replace(input, "<pulse(=[0-9.]+)?>|</pulse>", "");
        return input;
    }

    // 태그를 파싱하여 charEffects를 구축
    void ParseTagsAndBuildCharEffects()
    {
        charEffects.Clear();
        var shakeStack = new Stack<float>();
        var pulseStack = new Stack<float>();
        var rainbowStack = new Stack<bool>();
        int i = 0; // rawText 인덱스
        int v = 0; // 표시 문자 인덱스
        while (i < rawText.Length)
        {
            if (rawText[i] == '<')
            {
                // shake
                var shakeOpen = Regex.Match(rawText[i..], "^<shake(=([0-9.]+))?>");
                if (shakeOpen.Success)
                {
                    float inten = defaultShakeIntensity;
                    if (shakeOpen.Groups[2].Success)
                        float.TryParse(shakeOpen.Groups[2].Value, out inten);
                    shakeStack.Push(inten);
                    i += shakeOpen.Length;
                    continue;
                }
                if (rawText[i..].StartsWith("</shake>"))
                {
                    if (shakeStack.Count > 0) shakeStack.Pop();
                    i += "</shake>".Length;
                    continue;
                }

                // pulse
                var pulseOpen = Regex.Match(rawText[i..], "^<pulse(=([0-9.]+))?>");
                if (pulseOpen.Success)
                {
                    float scale = defaultPulse;
                    if (pulseOpen.Groups[2].Success)
                        float.TryParse(pulseOpen.Groups[2].Value, out scale);
                    pulseStack.Push(scale);
                    i += pulseOpen.Length;
                    continue;
                }
                if (rawText[i..].StartsWith("</pulse>"))
                {
                    if (pulseStack.Count > 0) pulseStack.Pop();
                    i += "</pulse>".Length;
                    continue;
                }

                // rainbow
                if (rawText[i..].StartsWith("<rainbow>"))
                {
                    rainbowStack.Push(true);
                    i += "<rainbow>".Length;
                    continue;
                }
                if (rawText[i..].StartsWith("</rainbow>"))
                {
                    if (rainbowStack.Count > 0) rainbowStack.Pop();
                    i += "</rainbow>".Length;
                    continue;
                }
            }
            // 표시 문자라면 효과 기록
            if (!char.IsHighSurrogate(rawText[i]) && rawText[i] != '\u200B') // zero-width 등 제외
            {
                CharEffectInfo eff = new()
                {
                    shakeIntensities = new List<float>(shakeStack),
                    pulseScales = new List<float>(pulseStack),
                    rainbow = rainbowStack.Count > 0
                };
                charEffects.Add(eff);
                v++;
            }
            i++;
        }
    }
}
