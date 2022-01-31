using DG.Tweening;
using UnityEngine;

namespace JekyllHyde.Entity
{
    public class EntitySprite : MonoBehaviour
    {
        [field: SerializeField] protected Animator AnimatorCtrl { get; set; }

        public EntityDirection Direction { get; set; }
        public bool Moving { get; set; }
        [field: SerializeField] protected EntityDirection CurrentDirection { get; set; }

        protected void Start()
        {
            if (CurrentDirection != EntityDirection.Left) transform.DORotate(new Vector2(0, 180), 1f);
        }

        protected void Update()
        {
            if (Direction != CurrentDirection)
            {
                CurrentDirection = Direction;
                transform.DORotate(new Vector2(0, (CurrentDirection == EntityDirection.Left) ? 0 : 180), 0.5f);
            }

            AnimatorCtrl.SetBool("Moving", Moving);
        }
    }
}
