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

        public void Rotate(IEnemy enemy)
        {
            Vector3 endValue = enemy.Orientation == PlayerOrientation.Left ? new Vector3(0, 0, 0) : new Vector3(0, 180, 0);
            EnemySprite.transform.DORotate(endValue, 0.5f);
        }

        private void Update()
        {
            if (Moving)
            {
                EnemyAnimator.speed = 1;
                return;
            }

            EnemyAnimator.speed = 0;
            EnemyAnimator.Rebind();
        }
    }
}