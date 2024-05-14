// // trajectory script controls whether the collider is turned on or off

// using UnityEngine;

// [RequireComponent(typeof(ProjectileCurved2D))]
// public class Skill_CurvedRange : SkillObject
// {
//     ProjectileCurved2D move;

//     protected override void Awake()
//     {
//         base.Awake();
//         move = GetComponent<ProjectileCurved2D>();
// #if UNITY_EDITOR
//         if (willFlip && move.willLook)
//         {
//             Debug.LogError($"{gameObject.name} willFlip and willLook must not be true at the same time");
//             move.willLook = false;
//         }
// #endif
//     }

//     protected override void OnInit()
//     {
//         move.Init(to, Hide);
//     }

//     void OnTriggerEnter2D(Collider2D other)
//     {
//         OnCollision(other);
//     }
// }