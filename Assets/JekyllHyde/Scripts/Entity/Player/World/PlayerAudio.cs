using UnityEngine;

namespace JekyllHyde.Entity.Player.World
{
    public class PlayerAudio : MonoBehaviour
    {
        [field: SerializeField] private AudioSource WalkSound { get; set; }
        [field: SerializeField] public AudioSource HideSound { get; set; }

        public bool EnableSound { get; set; } = true;

        public void PlayWalk()
        {
            if (EnableSound && !WalkSound.isPlaying) WalkSound.Play();
        }

        public void StopWalk()
        {
            WalkSound.Stop();
        }
    }
}
