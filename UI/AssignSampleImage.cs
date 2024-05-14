using UnityEngine;
using UnityEngine.UI;

public class AssignSampleImage : MonoBehaviour
{
    public Sprite[] sampleImages;
    Image image;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    void OnEnable()
    {
        image.sprite = sampleImages.Sample();
    }
}
