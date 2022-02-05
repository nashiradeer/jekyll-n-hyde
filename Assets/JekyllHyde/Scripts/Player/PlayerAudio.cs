using UnityEngine;

namespace JekyllHyde.Entity.Player
{
    public class PlayerAudio : MonoBehaviour
    {
        [field: SerializeField] public AudioSource WalkSound { get; private set; }
        [field: SerializeField] public AudioSource HideSound { get; private set; }

        public void PlayWalk()
        {
            if (!WalkSound.isPlaying) WalkSound.Play();
        }
    }
}
