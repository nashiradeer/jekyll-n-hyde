using GameJam2022.JekyllHyde.Manager;
using GameJam2022.JekyllHyde.Manager.Interface;
using UnityEngine;

namespace GameJam2022.JekyllHyde.Scene
{
    public class GameOverScene : MonoBehaviour
    {
        private ISceneManager SceneManager;

        private void Awake()
        {
            SceneManager = GameObject.FindWithTag("SceneManager").GetComponent<SceneManager>();
        }

        public void ReturnMainScreen()
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
