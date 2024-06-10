using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorPlayerForButton : MonoBehaviour
{
    public string clipName;

    public void _Play()
    {
        GetComponent<Animator>().Play(clipName);
    }
}
