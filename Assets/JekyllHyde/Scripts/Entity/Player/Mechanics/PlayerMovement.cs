using JekyllHyde.Entity.Player.World;
using JekyllHyde.UI.Manager;
using JekyllHyde.World.Manager;
using UnityEngine;

namespace JekyllHyde.Entity.Player.Mechanics
{
    public class PlayerMovement : MonoBehaviour
    {
        [field: SerializeField] public bool EnabledMovement { get; set; }
        [field: SerializeField] public float Speed { get; set; }
        [field: SerializeField] private Rigidbody2D Body { get; set; }
        [field: SerializeField] private PlayerSprite Sprite { get; set; }
        [field: SerializeField] private PlayerAudio Audio { get; set; }
        [field: SerializeField] private WorldManager WorldManager { get; set; }
        [field: SerializeField] private QuestManager QuestManager { get; set; }
        [field: SerializeField] private GameplayManager GameplayManager { get; set; }

        public bool Moving { get; set; }

        private void Update()
        {
            if (GameplayManager.IsPaused) return;

            if (EnabledMovement) Body.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * Speed, Body.velocity.y);
            else Body.velocity = new Vector2(0, Body.velocity.y);

            Moving = Body.velocity.x != 0;

            EntityDirection direction = Sprite.CurrentDirection;
            if (Body.velocity.x > 0) direction = EntityDirection.Right;
            else if (Body.velocity.x < 0) direction = EntityDirection.Left;

            Sprite.MoveAnimation(Moving, direction);

            if (Moving) Audio.PlayWalk();
            else Audio.StopWalk();

            if (WorldManager.CurrentWorldIndex == 0 && QuestManager.Step == 3 && transform.position.x > 2) QuestManager.GreenPotionTrigger();
            if (WorldManager.CurrentWorldIndex == 3 && QuestManager.Step == 6 && transform.position.x > -0.05) QuestManager.LucyTrigger();
        }
    }
}
