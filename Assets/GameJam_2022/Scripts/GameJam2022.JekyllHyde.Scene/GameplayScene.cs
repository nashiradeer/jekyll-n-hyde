using System;
using GameJam2022.JekyllHyde.Controller;
using GameJam2022.JekyllHyde.Controller.Enemy;
using GameJam2022.JekyllHyde.Controller.Player;
using GameJam2022.JekyllHyde.Manager;
using GameJam2022.JekyllHyde.Manager.Interface;
using UnityEngine;

namespace GameJam2022.JekyllHyde.Scene
{
    public class GameplayScene : MonoBehaviour
    {
        [field: SerializeField] private PlayerController PlayerController { get; set; }
        [field: SerializeField] private EnemyController EnemyController { get; set; }
        [field: SerializeField] private KeyboardController KeyboardController { get; set; }
        [field: SerializeField] private Camera MainCamera { get; set; }
        
        private IGameManager GameManager { get; set; }
        private ISceneManager SceneManager { get; set; }

        private void Awake()
        {
            GameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
            SceneManager = GameObject.FindWithTag("SceneManager").GetComponent<SceneManager>();
        }
        
        private void Start()
        {
            PlayerController.Init(GameManager.GameplayService.Player);
            EnemyController.Init(GameManager.GameplayService.Enemy, GameManager.GameplayService.Player, PlayerController.transform, 30);
            MainCamera.transform.SetParent(PlayerController.transform);
            
            KeyboardController.OnMove += PlayerController.Move;
            KeyboardController.OnHide += PlayerController.Hide;
            KeyboardController.OnInteract += PlayerController.Interact;

            EnemyController.OnKillPlayer += GameOver;
        }

        private void OnDestroy()
        {
            KeyboardController.OnMove -= PlayerController.Move;
            KeyboardController.OnHide -= PlayerController.Hide;
            KeyboardController.OnInteract -= PlayerController.Interact;

            EnemyController.OnKillPlayer -= GameOver;
        }

        public void GameOver()
        {
            SceneManager.LoadOverlay("GameOver");
        }
    }
}