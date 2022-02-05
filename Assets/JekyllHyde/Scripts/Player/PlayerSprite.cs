using DG.Tweening;
using JekyllHyde.Entity;
using UnityEngine;

namespace JekyllHyde.Player
{
    public class PlayerSprite : MonoBehaviour
    {
        [field: SerializeField] protected Animator AnimatorCtrl { get; set; }

        public EntityDirection Direction { get; set; }
        public bool Moving { get; set; }
        [field: SerializeField] protected EntityDirection CurrentDirection { get; set; }

        public void HideAnimation(bool hide)
        {
            AnimatorCtrl.SetBool("Hide", hide);
        }

        protected void Start()
        {
            if (CurrentDirection != EntityDirection.Left)
            {
                transform.Rotate(new Vector2(0, 180));
                Direction = CurrentDirection;
            }
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
