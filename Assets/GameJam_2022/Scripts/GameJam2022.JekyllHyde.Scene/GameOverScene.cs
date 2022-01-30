using GameJam2022.JekyllHyde.Manager;
using GameJam2022.JekyllHyde.Manager.Interface;
using UnityEngine;

namespace GameJam2022.JekyllHyde.Scene
{
    public class GameOverScene : MonoBehaviour
    {
        [field: SerializeField] private AudioClip GameOverMusic;

        //private IAudioManager AudioManager;
        private ISceneManager SceneManager;

        private void Awake()
        {
            //AudioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
            SceneManager = GameObject.FindWithTag("SceneManager").GetComponent<SceneManager>();
        }

        public void ReturnMainScreen()
        {
            SceneManager.LoadScene("Menu");
            //AudioManager.Play(GameOverMusic);
        }
    }
}
