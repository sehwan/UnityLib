using UnityEngine;
using UnityEngine.UI;

public abstract class UIPanel : MonoBehaviour
{
    public bool didInit;
    public virtual void Init()
    {
        didInit = true;
    }

    public bool IsActive => gameObject.activeSelf;
    public virtual void Show() => gameObject.SetActive(true);
    public virtual void Hide() => gameObject.SetActive(false);
    public void SetActive(bool b) => gameObject.SetActive(b);
    public void Toggle() => gameObject.SetActive(!gameObject.activeSelf);
    public void ShowWindow(string name) => UM.i.windows.Find(e => e.name == name).Show();
}


public class UIScene : UIPanel
{
    public override void Show()
    {
        UM.i.HideScenes();
        gameObject.SetActive(true);
    }
}
public class UIWindow : UIPanel
{
    [Immutable] public bool buttonWillBeESC;
    public bool isESCable = true;
    public bool isObstructingCamera = true;
    

    [ContextMenu("Make Dim to ESC Buttons")]
    public void MakeESCDim()
    {
        if (GetComponent<Image>() == false)
        {
            var dim = gameObject.AddComponent<Image>();
            dim.color = new Color(0, 0, 0, 0.37f);
        }
        if (GetComponent<Button>() == false)
        {
            gameObject.AddComponent<Button>();
        }
    }

    protected virtual void Awake()
    {
        if (buttonWillBeESC == false) return;
        var btn = GetComponent<Button>();
        if (btn == null) return;
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(Hide);
    }
    protected virtual void OnEnable()
    {
        UM.i?.RegisterWindow(this);
        SFX.Play("window");
    }
    protected virtual void OnDisable()
    {
        UM.i?.DeregisterWindow(this);
        SFX.Play("window");
    }
    public virtual void HideManually()
    {
        if (isESCable == false) return;
        gameObject.SetActive(false);
    }

    // Use this in place of OnDestroy(). OnDestroy() is Not Called when the Object is Inactive.
    public virtual void Destroy()
    {
        UM.i?.windows.Remove(this);
        Destroy(gameObject);
    }
}