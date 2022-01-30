using GameJam2022.JekyllHyde.Domain.Interface;
using UnityEngine;

namespace GameJam2022.JekyllHyde.Controller.Player
{
    public class PlayerController : MonoBehaviour
    {
        [field: SerializeField] private PlayerVfx PlayerSprite { get; set; }
        
        private IPlayer Player { get; set; }
        private float Speed = 2.0f;
        private IInteractable InteractiveObject = null;

        public void Init(IPlayer player)
        {
            Player = player;
        }

        public void Move()
        {
            if (Player.IsHidden) return;

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
            Player.ChangeHide(isHide);
            PlayerSprite.Hide(Player);
        }

        public void Interact()
        {
            if (InteractiveObject != null) InteractiveObject.Interact(Player);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            IInteractable interactable = collision.GetComponent<IInteractable>();
            if (InteractiveObject == null && interactable != null) InteractiveObject = interactable;

            if (collision.tag == "Hideable") Player.CanHide = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (InteractiveObject == collision.GetComponent<IInteractable>()) InteractiveObject = null;
            if (collision.tag == "Hideable") Player.CanHide = false;
        }
    }
}