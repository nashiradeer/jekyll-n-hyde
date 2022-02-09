using JekyllHyde.Entity.Player.Mechanics;
using JekyllHyde.Entity.Player.World;
using JekyllHyde.World.Manager;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JekyllHyde.UI.Manager
{
    public class GameplayManager : MonoBehaviour
    {
        [field: SerializeField] public bool EnabledPause { get; set; } = true;

        [field: SerializeField] private GameObject GameplayScreen { get; set; }
        [field: SerializeField] private GameObject PauseScreen { get; set; }
        [field: SerializeField] private GameObject GameOverScreen { get; set; }
        [field: SerializeField] private PlayerAudio PlayerAudio { get; set; }

        private bool Paused { get; set; }
        private bool GameOver { get; set; }

        private bool PausePressed { get; set; }

        public void TriggerGameOver()
        {
            if (!GameOver)
            {
                Debug.Log("GameplayManager: Game Over.");

                GameOver = true;
                EnabledPause = false;

                PlayerAudio.EnableSound = false;
                PlayerAudio.StopWalk();

                Time.timeScale = 0;

                GameplayScreen.SetActive(false);
                GameOverScreen.SetActive(true);
            }
        }

        public void ResumeGame()
        {
            if (Paused)
            {
                Paused = false;
                
                PlayerAudio.EnableSound = true;

                Time.timeScale = 1;

                GameplayScreen.SetActive(true);
                PauseScreen.SetActive(false);
            }
        }

        public void ExitGame()
        {
            Time.timeScale = 1;

            for (int i = 4;i <= 8;i++) PlayerInventory.Items[i] = false;
            if (QuestManager.Step < 8) QuestManager.Step = 0;

            SceneManager.LoadScene(1);
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Escape) && EnabledPause && !PausePressed)
            {
                PausePressed = true;
                if (Paused)
                {
                    Paused = false;

                    PlayerAudio.EnableSound = true;

                    Time.timeScale = 1;

                    GameplayScreen.SetActive(true);
                    PauseScreen.SetActive(false);
                }
                else
                {
                    Paused = true;

                    PlayerAudio.EnableSound = false;
                    PlayerAudio.StopWalk();

                    Time.timeScale = 0;

                    GameplayScreen.SetActive(false);
                    PauseScreen.SetActive(true);
                }
            }

            if (!Input.GetKey(KeyCode.Escape)) PausePressed = false;
        }
    }
}
