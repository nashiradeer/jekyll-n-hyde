using JekyllHyde.Entity.Player.World;
using UnityEngine;

namespace JekyllHyde.Entity.Player.Mechanics
{
    public class PlayerHide : MonoBehaviour
    {
        [field: SerializeField] public bool EnabledHide { get; set; }
        [field: SerializeField] private PlayerMovement Movement { get; set; }
        [field: SerializeField] private PlayerSprite Sprite { get; set; }
        [field: SerializeField] private PlayerAudio Audio { get; set; }

        public bool IsHidden { get; private set; }

        private bool CanHide { get; set; }

        private float Cooldown = 0;
        private float CooldownTotal = 0.3f;

        private void Update()
        {
            if (Cooldown > 0 && !IsHidden) Cooldown -= Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (EnabledHide && Cooldown <= 0 && !IsHidden && CanHide)
                {
                    IsHidden = true;
                    Movement.EnabledMovement = false;
                    Sprite.HideAnimation(true);
                    Audio.HideSound.Play();
                    Cooldown = CooldownTotal;
                }
            } else if (IsHidden)
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
