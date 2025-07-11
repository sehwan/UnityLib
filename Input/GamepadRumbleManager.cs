using UnityEngine;
using UnityEngine.InputSystem;
using System.Threading;
using System.Collections.Generic;

public static class GamepadRumbleManager
{
    static CancellationTokenSource cts;
    static readonly List<RumbleRequest> rumbles = new List<RumbleRequest>();

    struct RumbleRequest
    {
        public float endTime;
        public float low;
        public float high;
        public bool IsExpired => Time.realtimeSinceStartup >= endTime;

        public RumbleRequest(float duration, float low, float high)
        {
            this.endTime = Time.realtimeSinceStartup + duration;
            this.low = low;
            this.high = high;
        }
    }

    /// <summary>
    /// 진동 실행 (duration: 초, low: 저주파, high: 고주파)
    /// 여러 진동 요청 시 가장 강한 주파수로 합성됨
    /// </summary>
    public static async void Rumble(float duration, float lowFrequency = 0.5f, float highFrequency = 0.5f)
    {
        if (Gamepad.current == null) return;
        rumbles.Add(new RumbleRequest(duration, lowFrequency, highFrequency));
        if (cts == null)
        {
            cts = new CancellationTokenSource();
            await ManageRumble();
        }
    }

    static async Awaitable ManageRumble()
    {
        var token = cts.Token;
        try
        {
            while (token.IsCancellationRequested == false)
            {
                var maxLow = 0f;
                var maxHigh = 0f;
                var nextExpireTime = float.MaxValue;

                // 만료된 진동 제거
                rumbles.RemoveAll(r => r.IsExpired);

                // 가장 강한 주파수 계산 & 다음 만료 시간 찾기
                foreach (var rumble in rumbles)
                {
                    maxLow = Mathf.Max(maxLow, rumble.low);
                    maxHigh = Mathf.Max(maxHigh, rumble.high);
                    nextExpireTime = Mathf.Min(nextExpireTime, rumble.endTime);
                }

                // 진동 적용
                if (rumbles.Count > 0)
                {
                    Gamepad.current?.SetMotorSpeeds(maxLow, maxHigh);

                    // 가장 빠른 만료 시간까지 대기
                    var remaining = nextExpireTime - Time.realtimeSinceStartup;
                    if (remaining > 0) await Awaitable.WaitForSecondsAsync(remaining, token);
                }
                else
                {
                    Gamepad.current?.SetMotorSpeeds(0, 0);
                    break;
                }
            }
        }
        finally
        {
            Gamepad.current?.SetMotorSpeeds(0, 0);
            rumbles.Clear();
            cts?.Dispose();
            cts = null;
        }
    }


    public static void RumbleLight() => Rumble(0.2f, 0.3f, 0.3f);
    public static void RumbleMedium() => Rumble(0.3f, 0.6f, 0.6f);
    public static void RumbleHeavy() => Rumble(0.5f, 1.0f, 1.0f);

    /// <summary>
    /// 짧은 펄스 진동 (피드백용)
    /// </summary>
    public static void RumblePulse() => Rumble(0.1f, 0.8f, 0.8f);

    public static void StopRumble()
    {
        rumbles.Clear();

        if (cts != null)
        {
            cts.Cancel();
            cts.Dispose();
            cts = null;
        }

        Gamepad.current?.SetMotorSpeeds(0, 0);
    }
}
