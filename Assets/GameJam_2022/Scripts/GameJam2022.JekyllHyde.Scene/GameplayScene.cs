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
        [field: SerializeField] private PlayerController PlayerController { get; set; }
        [field: SerializeField] private KeyboardController KeyboardController { get; set; }
        [field: SerializeField] private Camera MainCamera { get; set; }
        private IGameplayService GameplayService { get; set; }
        
        private void Start()
        {
            GameplayService = new GameplayService();
            
            PlayerController.Init(GameplayService.Player);
            MainCamera.transform.SetParent(PlayerController.transform);
            
            KeyboardController.OnMove += PlayerController.Move;
            KeyboardController.OnHide += PlayerController.Hide;
        }

        private void OnDestroy()
        {
            KeyboardController.OnMove -= PlayerController.Move;
        }
    }
}