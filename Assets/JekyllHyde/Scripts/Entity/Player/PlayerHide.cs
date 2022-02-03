using UnityEngine;

namespace JekyllHyde.Entity.Player
{
    public class PlayerHide : MonoBehaviour
    {
        [field: SerializeField] private PlayerMovement Movement { get; set; }
        [field: SerializeField] private PlayerSprite Sprite { get; set; }
        [field: SerializeField] private PlayerAudio Audio { get; set; }

        public bool IsHidden { get; private set; }

        private bool CanHide { get; set; }

        private int CooldownTicks = 0;
        private int CooldownTotal = 13;

        private void FixedUpdate()
        {
            if (CooldownTicks > 0 && !IsHidden) CooldownTicks--;

            if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && CooldownTicks == 0 && !IsHidden && CanHide)
            {
                IsHidden = true;
                Movement.EnabledMovement = false;
                Sprite.HideAnimation(true);
                Audio.HideSound.Play();
                CooldownTicks = CooldownTotal;
            }

            if (!(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && IsHidden)
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
                Debug.Log("PlayerHide: Player can hide.");
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "Hideable")
            {
                CanHide = false;
                Debug.Log("PlayerHide: Player can't hide.");
            }
        }
    }
}
