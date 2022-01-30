using System;
using GameJam2022.JekyllHyde.Controller;
using GameJam2022.JekyllHyde.Controller.Player;
using GameJam2022.JekyllHyde.Manager;
using UnityEngine;

namespace GameJam2022.JekyllHyde.Scene
{
    public class GameplayScene : MonoBehaviour
    {
        [field: SerializeField] private PlayerController PlayerController { get; set; }
        [field: SerializeField] private KeyboardController KeyboardController { get; set; }
        [field: SerializeField] private Camera MainCamera { get; set; }
        
        private GameManager GameManager { get; set; }

        private void Awake()
        {
            GameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        }
        
        private void Start()
        {
            PlayerController.Init(GameManager.GameplayService.Player);
            MainCamera.transform.SetParent(PlayerController.transform);
            
            KeyboardController.OnMove += PlayerController.Move;
        }

        private void OnDestroy()
        {
            KeyboardController.OnMove -= PlayerController.Move;
        }
    }
}