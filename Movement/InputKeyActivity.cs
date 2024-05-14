using UnityEngine;
using UnityEngine.SceneManagement;

public class InputKeyActivity : MonoBehaviour
{
    void Update()
    {


#if UNITY_ANDROID
        EscapeOnAndroid();
#endif
#if UNITY_EDITOR
        Cheat();
        if (Input.GetKeyDown(KeyCode.LeftShift))
            UM.i.canvas.enabled = !UM.i.canvas.enabled;
        else if (Input.GetKeyDown(KeyCode.C))
        {
            var savePath = "/Users/sehwanlim/Desktop/captures";
            if (!System.IO.Directory.Exists(savePath))
                System.IO.Directory.CreateDirectory(savePath);
            var fileName = $"{System.DateTime.Now:yyyyMMdd_HHmmss}.png";
            var filePath = System.IO.Path.Combine(savePath, fileName);
            ScreenCapture.CaptureScreenshot(filePath);
        }
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
    void Cheat()
    {
        if (Input.GetKey(KeyCode.BackQuote) == false) return;

        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        else if (Input.GetKeyDown(KeyCode.Alpha1))
            Time.timeScale = 0.25f;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            Time.timeScale = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            Time.timeScale = 3;
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            Time.timeScale = 10f;
    }
}
