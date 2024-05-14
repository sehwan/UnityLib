// using UnityEngine;

// [RequireComponent(typeof(ProjectileFlat2D))]
// public class Skill_FlatRange : SkillObject
// {
//     protected override void Awake()
//     {
//         base.Awake();
//     }
//     protected override void OnInit()
//     {
//         var dir = (to - tr.position).normalized;
//         GetComponent<ProjectileFlat2D>().Init(dir * 20f + tr.position, Hide);
//     }

//     void OnTriggerEnter2D(Collider2D other)
//     {
//         if (OnCollision(other)) Hide();
//     }
// }