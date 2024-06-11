using UnityEngine;

public class UserBase : MonoBehaviour
{
    virtual public void UpdateDataForNewVersion() { }
    virtual public void OnRecord(string key) { }
    virtual public void GetOfflineReward(double sec) { }
}
