using JekyllHyde.World;
using UnityEngine;

namespace JekyllHyde.Entity.Player
{
    public class PlayerMovement : EntityMovement
    {
        [field: SerializeField] private PlayerAudio Audio { get; set; }
        [field: SerializeField] private WorldManager WorldManager { get; set; }
        [field: SerializeField] private QuestManager QuestManager { get; set; }

        protected void FixedUpdate()
        {
            Move(Input.GetAxisRaw("Horizontal") * Speed);

            if (Moving) Audio.PlayWalk();
            else Audio.WalkSound.Stop();


        }
    }
}
