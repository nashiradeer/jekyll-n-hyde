using JekyllHyde.Entity;
using UnityEngine;

namespace JekyllHyde.Entity.Player
{
    public class PlayerMovement : MonoBehaviour, IMovement
    {
        [field: SerializeField] public bool EnabledMovement { get; set; }
        [field: SerializeField] public float Speed { get; set; }
        [field: SerializeField] private Rigidbody2D Body { get; set; }
        [field: SerializeField] private EntitySprite Sprite { get; set; }

        public void Move(float x)
        {
            Body.velocity = new Vector2(x, Body.velocity.y);

            if (Body.velocity.x != 0) Sprite.Moving = true;
            else Sprite.Moving = false;

            if (Body.velocity.x > 0) Sprite.Direction = EntityDirection.Right;
            if (Body.velocity.x < 0) Sprite.Direction = EntityDirection.Left;
        }

        private void FixedUpdate()
        {
            if (EnabledMovement)
            {
                Move(Input.GetAxisRaw("Horizontal") * Speed);
            }
        }
    }
}
