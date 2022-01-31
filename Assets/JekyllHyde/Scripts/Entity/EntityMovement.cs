using UnityEngine;

namespace JekyllHyde.Entity
{
    public class EntityMovement : MonoBehaviour
    {
        [field: SerializeField] public bool EnabledMovement { get; set; }
        [field: SerializeField] public float Speed { get; set; }
        [field: SerializeField] protected Rigidbody2D Body { get; set; }
        [field: SerializeField] protected EntitySprite Sprite { get; set; }

        public void Move(float x)
        {
            if (EnabledMovement) Body.velocity = new Vector2(x, Body.velocity.y);
            else Body.velocity = new Vector2(0, Body.velocity.y);

            if (Body.velocity.x != 0) Sprite.Moving = true;
            else Sprite.Moving = false;

            if (Body.velocity.x > 0) Sprite.Direction = EntityDirection.Right;
            if (Body.velocity.x < 0) Sprite.Direction = EntityDirection.Left;
        }
    }
}
