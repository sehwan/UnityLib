// using UnityEngine;

// public class Skill_MeleeAttack : SkillObject
// {
//     public bool willLookAt;
//     public float autoHide = 0.4f;

//     protected override void OnInit()
//     {
//         if (autoHide > 0) this.InvokeEx(Hide, autoHide);
//         if (willLookAt)
//         {
//             var rotation = Quaternion.LookRotation(Vector3.forward, to - from);
//             tr.rotation = rotation;
//         }
//     }

//     void OnTriggerEnter2D(Collider2D other)
//     {
//         OnCollision(other);
//         if (isSplash == false) col.enabled = false;
//     }
// }