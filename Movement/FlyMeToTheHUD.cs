using DG.Tweening;
using UnityEngine;

public class FlyMeToTheHUD : MonoBehaviour
{
    public float speed = 100;
    public float xDiff = 1;
    public float height = 5;
    public Transform target;

    public void OnClick()
    {
        // var time = Vector2.Distance(transform.position.V2(), target.position.V2()) / speed;
        // transform.DOJump(target.position.V2(), 1, 1, time)
        //     .OnComplete(() => gameObject.SetActive(false));


        Vector3[] path = new Vector3[] {
            transform.position.V2(),

            new(Mathf.Sign(transform.position.x - target.position.x) * xDiff,
                transform.position.y + height/2),

            new((transform.position.x + target.position.x)/2,
                transform.position.y + height),

            target.position.V2()
        };

        var time = Vector2.Distance(transform.position.V2(), target.position.V2()) / speed;
        var tween = transform.DOLocalPath(path, time, PathType.CatmullRom)
            .SetEase(Ease.Linear)
            .OnComplete(() => gameObject.SetActive(false));

        tween.SetLink(CameraWork.i.gameObject);

    }
}
