using System;
using System.Collections;
using UnityEngine;

public class ResolutionMgr : MonoBehaviour
{
    private int _minHeightRes = 1220;
    private int _resolutionChangeAmt = 100;
    private int _targetFrameRate = 55;
    private float _frameCheckInterval = 2f;
    private float _frameRate;

    void Awake()
    {
        Application.targetFrameRate = 60;
        SetAutoResolution();
    }

    public void SetAutoResolution()
    {
        StartCoroutine(AdjustResolutionCoroutine());
    }

    IEnumerator AdjustResolutionCoroutine()
    {
        // Max 해상도
        int maxHeightRes = Screen.height;
        // 현재 설정된 해상도
        int height = Screen.currentResolution.height;
        // 해상도 비율
        float screenRatio = (float)Screen.currentResolution.width / Screen.currentResolution.height;
        // 초기 시간과 누적 프레임 초기화
        float timer = 0f;
        int totalFrames = 0;

        // 처음 1.5초 이후에 계산 시작
        yield return new WaitForSeconds(1.5f);
        while (true)
        {
            // 프레임 수 갱신
            totalFrames++;
            float deltaTime = Time.deltaTime;
            timer += deltaTime;
            if (timer >= _frameCheckInterval)
            {
                //float frameRate = totalFrames / timer;
                _frameRate = totalFrames / timer;
                totalFrames = 0;
                timer = 0f;

                // 타겟 프레임 속도에 도달하면 해상도 조정 종료
                if (_frameRate >= _targetFrameRate || height <= _minHeightRes)
                    yield break;

                // 프레임 속도가 타겟에 미달하는 경우, 해상도를 감소
                if (_frameRate <= 20)
                    height = Mathf.Clamp(height - _resolutionChangeAmt * 4, _minHeightRes, maxHeightRes);
                else if (_frameRate <= 40)
                    height = Mathf.Clamp(height - _resolutionChangeAmt * 2, _minHeightRes, maxHeightRes);
                else
                    height = Mathf.Clamp(height - _resolutionChangeAmt, _minHeightRes, maxHeightRes);

                int width = Mathf.RoundToInt(screenRatio * height);
                SetResolution(width, height);
            }
            yield return null;
        }
    }

    void SetResolution(int width, int height)
    {
        Screen.SetResolution(width, height, Screen.fullScreen);
    }
}