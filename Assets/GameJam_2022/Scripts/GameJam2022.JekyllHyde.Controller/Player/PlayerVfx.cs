using System;
using DG.Tweening;
using GameJam2022.JekyllHyde.Domain.Interface;
using UnityEngine;

namespace GameJam2022.JekyllHyde.Controller.Player
{
    public class PlayerVfx : MonoBehaviour
    {
        [field: SerializeField] private SpriteRenderer PlayerSprite { get; set; }
        [field: SerializeField] private Animator PlayerAnimator { get; set; }

        public void Rotate(IPlayer player)
        {
            var endValue = player.Orientation == PlayerOrientation.Left ? new Vector3(0,0,0) : new Vector3(0,180,0);
            PlayerSprite.transform.DORotate(endValue, 0.5f);
        }

        public void Hide(IPlayer player)
        {
            PlayerAnimator.SetBool("Escondido", player.IsHidden);
        }

        private void Update()
        {
            if (Input.GetAxis("Horizontal") != 0)
            {
                PlayerAnimator.speed = 1;
                return;
            }

            PlayerAnimator.speed = 0;
        }
    }
}