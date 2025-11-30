using UnityEngine;

public class InputKeyActivity : MonoBehaviour
{
    void Update()
    {
#if UNITY_ANDROID
        EscapeOnAndroid();
#endif
    }

    void EscapeOnAndroid()
    {
        if (Application.isMobilePlatform == false) return;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (UM.i.windows.Count > 0)
            {
                UM.i.windows[^1].HideManually();
                return;
            }
            // Quit App
            MessageBox.Show("QuitGame".L(), null, null, null,
                () =>
                {
                    // User.data.device = null;
                    // User.inst.SaveImmediately();
                    Application.Quit();
                });
        }
    }
}