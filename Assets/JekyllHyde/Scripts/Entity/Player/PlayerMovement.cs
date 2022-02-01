using UnityEngine;

namespace JekyllHyde.Entity.Player
{
    public class PlayerMovement : EntityMovement
    {
        [field: SerializeField] private PlayerAudio Audio { get; set; }

        protected void FixedUpdate()
        {
            Move(Input.GetAxisRaw("Horizontal") * Speed);
            if (Moving) Audio.PlayWalk();
            else Audio.WalkSound.Stop();
        }
    }
}
