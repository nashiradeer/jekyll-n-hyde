using DG.Tweening;
using GameJam2022.JekyllHyde.Domain.Interface;
using UnityEngine;

namespace GameJam2022.JekyllHyde.Controller.Enemy
{
    public class EnemyVfx : MonoBehaviour
    {
        [field: SerializeField] private SpriteRenderer EnemySprite { get; set; }
        [field: SerializeField] private Animator EnemyAnimator { get; set; }
        public bool Moving = false;

        private float AnimatorSpeed = 1f;

        public void Rotate(IEnemy enemy)
        {
            Vector3 endValue = enemy.Orientation == PlayerOrientation.Left ? new Vector3(0, 0, 0) : new Vector3(0, 180, 0);
            EnemySprite.transform.DORotate(endValue, 0.5f);
        }

        public void ChaseAnimation(bool isChasing)
        {
            if (!isChasing)
                AnimatorSpeed = 1f;
            else
                AnimatorSpeed = 3f;
        }

        private void Update()
        {
            if (Moving)
            {
                EnemyAnimator.speed = AnimatorSpeed;
                return;
            }

            EnemyAnimator.speed = 0;
            EnemyAnimator.Rebind();
        }
    }
}