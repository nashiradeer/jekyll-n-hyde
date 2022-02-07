using DG.Tweening;
using UnityEngine;

namespace JekyllHyde.Entity.Player.World
{
    public class PlayerSprite : MonoBehaviour
    {
        [field: SerializeField] private Animator Animator { get; set; }
        [field: SerializeField] public EntityDirection CurrentDirection { get; set; }

        public void HideAnimation(bool hide)
        {
            Animator.SetBool("Hide", hide);
        }

        public void MoveAnimation(bool moving, EntityDirection direction)
        {
            if (direction != CurrentDirection)
            {
                CurrentDirection = direction;
                transform.DORotate(new Vector2(0, (CurrentDirection == EntityDirection.Left) ? 0 : 180), 0.5f);
            }

            Animator.SetBool("Moving", moving);
        }

        protected void Start()
        {
            if (CurrentDirection != EntityDirection.Left)
            {
                transform.Rotate(new Vector2(0, 180));
            }
        }
    }
}
