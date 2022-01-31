using UnityEngine;

namespace JekyllHyde.Entity.Player
{
    public class PlayerHide : MonoBehaviour
    {
        [field: SerializeField] private PlayerMovement Movement { get; set; }
        [field: SerializeField] private PlayerSprite Sprite { get; set; }

        public bool IsHidden { get; private set; }

        private bool CanHide { get; set; }

        private void FixedUpdate()
        {
            if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && !IsHidden && CanHide)
            {
                IsHidden = true;
                Movement.EnabledMovement = false;
                Sprite.HideAnimation(true);
            }

            if (!(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && IsHidden && CanHide)
            {
                IsHidden = false;
                Movement.EnabledMovement = true;
                Sprite.HideAnimation(false);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Hideable")
            {
                CanHide = true;
                Debug.Log("PlayerHide: Hide is enabled.");
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "Hideable")
            {
                CanHide = false;
                Debug.Log("PlayerHide: Hide is disabled.");
            }
        }
    }
}
