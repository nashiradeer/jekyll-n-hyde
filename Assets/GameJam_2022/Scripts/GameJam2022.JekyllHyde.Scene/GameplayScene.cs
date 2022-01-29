using System;
using GameJam2022.JekyllHyde.Controller;
using UnityEngine;

namespace GameJam2022.JekyllHyde.Scene
{
    public class GameplayScene : MonoBehaviour
    {
        [field: SerializeField] private PlayerController Player { get; set; }
        [field: SerializeField] private KeyboardController KeyboardController { get; set; }

        private void Start()
        {
            KeyboardController.OnMove += Player.Move;
        }
    }
}