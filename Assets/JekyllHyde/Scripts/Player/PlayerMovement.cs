using JekyllHyde.Entity;
using JekyllHyde.World;
using UnityEngine;

namespace JekyllHyde.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [field: SerializeField] public bool EnabledMovement { get; set; }
        [field: SerializeField] public float Speed { get; set; }
        [field: SerializeField] protected Rigidbody2D Body { get; set; }
        [field: SerializeField] protected PlayerSprite Sprite { get; set; }
        [field: SerializeField] protected bool Moving { get; set; }
        [field: SerializeField] private PlayerAudio Audio { get; set; }
        [field: SerializeField] private WorldManager WorldManager { get; set; }
        [field: SerializeField] private QuestManager QuestManager { get; set; }

        public void Move(float x)
        {
            if (EnabledMovement) Body.velocity = new Vector2(x, Body.velocity.y);
            else Body.velocity = new Vector2(0, Body.velocity.y);

            if (Body.velocity.x != 0)
            {
                Sprite.Moving = true;
                Moving = true;
            }
            else
            {
                Sprite.Moving = false;
                Moving = false;
            }

            if (Body.velocity.x > 0) Sprite.Direction = EntityDirection.Right;
            if (Body.velocity.x < 0) Sprite.Direction = EntityDirection.Left;
        }

        protected void FixedUpdate()
        {
            Move(Input.GetAxisRaw("Horizontal") * Speed);

            if (Moving) Audio.PlayWalk();
            else Audio.WalkSound.Stop();
        }
    }
}
