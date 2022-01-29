using System;
using GameJam2022.JekyllHyde.Controller;
using GameJam2022.JekyllHyde.Controller.Player;
using GameJam2022.JekyllHyde.Service;
using GameJam2022.JekyllHyde.Service.Interface;
using UnityEngine;

namespace GameJam2022.JekyllHyde.Scene
{
    public class GameplayScene : MonoBehaviour
    {
        [field: SerializeField] private PlayerController Player { get; set; }
        [field: SerializeField] private KeyboardController KeyboardController { get; set; }

        private IGameplayService GameplayService { get; set; }
        private void Start()
        {
            GameplayService = new GameplayService();
            Player.Init(GameplayService.Player);
            
            KeyboardController.OnMove += Player.Move;
        }
    }
}