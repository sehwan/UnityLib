// using UnityEngine;

// public class SkillObject : MonoBehaviour
// {
//     [Header("Settings")]
//     public string sfxClip = "attack";
//     public bool isSplash;
//     public bool isInstant;
//     public bool willFlip;

//     [Header("References")]
//     public Transform tr;
//     public Collider2D col;
//     public SpriteRenderer spr_core;

//     [Header("Data")]
//     public AttackData data;
//     public string targetTag;
//     public Vector3 from;
//     public Vector3 to;
//     [Immutable] public bool didFinishAttack;



//     protected virtual void Awake()
//     {
//         tr = transform;
//     }

//     public void Init(AttackData data, Vector2 from, Vector2 to, string targetTag, Unit target = null)
//     {
//         this.data = data;
//         this.from = from;
//         this.to = to;
//         this.targetTag = targetTag;
//         didFinishAttack = false;

//         if (willFlip) tr.FlipX(from.x > to.x);
//         OnInit();
//         SFX.Play(sfxClip);

//         // Be Last
//         if (isInstant == true)
//         {
//             tr.position = to;
//             AffectToUnit(target);
//         }
//     }
//     virtual protected void OnInit() { }
//     public void AffectToUnit(Unit unit)
//     {
//         if (data.Total() > 0) unit.GetAttack(data);
//         // if (data.aData.tdmg < 0) unit.GetHeal(-data.aData.tdmg);
//         // Status Effect
//         // if (data.fx != null && data.fx.meta.key.IsFilled()) unit.AddEffect(data);
//     }

//     protected bool OnCollision(Collider2D other)
//     {
//         if (other.tag != targetTag) return false;
//         var ps = Stage.inst.pss_attack[0];
//         ps.transform.position = other.ClosestPoint(tr.position);
//         ps.Emit(7);

//         var target = other.GetComponent<Unit>();

//         // Guard
//         if (target.CheckDodge(data))
//         {
//             Reflect();
//             return false;
//         }

//         AffectToUnit(target);
//         return true;
//     }
//     void Reflect()
//     {
//         SFX.Play("ting");
//         to = new Vector2(tr.position.x + 1f.Random(-2f), 2f.Random(1f));
//         OnInit();
//         tr.LocalScale(0.8f);
//     }
//     public void Hide()
//     {
//         // if (didFinishAttack) 
//         Destroy(gameObject);
//         // gameObject.SetActive(false);
//     }
// }