using JekyllHyde.Entity.Player.Manager;
using JekyllHyde.Entity.Player.Mechanics;
using JekyllHyde.Entity.Player.World;
using JekyllHyde.World.Manager;
using System.Collections;
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
        [field: SerializeField] private GameObject MenuButton { get; set; }
        [field: SerializeField] private AudioSource PlayerDie { get; set; }
        [field: SerializeField] private AudioSource Music { get; set; }

        private PauseMenu Pause { get; set; }
        private bool GameOver { get; set; }

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

                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;

                GameplayScreen.SetActive(false);
                GameOverScreen.SetActive(true);

                StartCoroutine(GameOverAnimation());
            }
        }

        private IEnumerator GameOverAnimation()
        {
            PlayerDie.Play();
            yield return new WaitForSecondsRealtime(PlayerDie.clip.length + 1);
            Music.Play();
            MenuButton.SetActive(true);
        }

        public void ResumeGame()
        {
            if (Pause.IsOpen) Pause.CurrentPlayer.CloseMenu();
        }

        public void PauseGame(PlayerManager player)
        {
            if (!Pause.IsOpen) player.OpenMenu(Pause);
        }

        public void ExitGame()
        {
            Time.timeScale = 1;

            for (int i = 4;i <= 8;i++) PlayerInventory.Items[i] = false;
            if (QuestManager.Step < 8) QuestManager.Step = 0;

            SceneManager.LoadScene(1);
        }

        private void Awake()
        {
            Pause = new PauseMenu(this);
        }

        private class PauseMenu : IUIMenu
        {
            public bool IsOpen { get; set; }
            public PlayerManager CurrentPlayer { get; set; }

            private GameplayManager GameplayManager { get; set; }

            public PauseMenu(GameplayManager gameplayManager)
            {
                GameplayManager = gameplayManager;
            }

            public void Resume()
            {
                CurrentPlayer.CloseMenu();
            }

            public void Close()
            {
                if (!IsOpen) return;

                IsOpen = false;
                CurrentPlayer = null;

                GameplayManager.PlayerAudio.EnableSound = true;

                Time.timeScale = 1;

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                GameplayManager.GameplayScreen.SetActive(true);
                GameplayManager.PauseScreen.SetActive(false);
            }

            public void Open(PlayerManager player)
            {
                if (IsOpen) return;

                IsOpen = true;
                CurrentPlayer = player;

                GameplayManager.PlayerAudio.EnableSound = false;
                GameplayManager.PlayerAudio.StopWalk();

                Time.timeScale = 0;

                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;

                GameplayManager.GameplayScreen.SetActive(false);
                GameplayManager.PauseScreen.SetActive(true);
            }
        }
    }
}
