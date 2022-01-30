using GameJam2022.JekyllHyde.Domain.Interface;
using UnityEngine;

namespace GameJam2022.JekyllHyde.Controller.Player
{
    public class PlayerController : MonoBehaviour
    {
        [field: SerializeField] private PlayerVfx PlayerSprite { get; set; }
        
        private IPlayer Player { get; set; }
        private float Speed = 2.0f;
        private Collider2D InteractiveObject = null;

        public void Init(IPlayer player)
        {
            Player = player;
        }

        public void Move()
        {
            var axis = Input.GetAxis("Horizontal");
            if (Player.ChangeDirection(axis))
            {
                PlayerSprite.Rotate(Player);
            }

            var move = new Vector3(axis, 0);
            transform.position += move * (Speed * Time.deltaTime);
        }

        public void Hide(bool isHide)
        {
            _ = Player.ChangeHide(isHide);
            //TODO: Travar o movimento do jogador caso Player.IsHidden for true.
            PlayerSprite.Hide(Player);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (InteractiveObject == null && collision.tag == "Interactable") InteractiveObject = collision;
            if (collision.tag == "Hideable") Player.CanHide = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (InteractiveObject == collision) InteractiveObject = null;
            if (collision.tag == "Hideable") Player.CanHide = false;
        }
    }
}