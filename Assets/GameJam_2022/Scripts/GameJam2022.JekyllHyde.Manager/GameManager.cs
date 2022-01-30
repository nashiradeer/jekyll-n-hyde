using System;
using GameJam2022.JekyllHyde.Service;
using GameJam2022.JekyllHyde.Service.Interface;
using UnityEngine;

namespace GameJam2022.JekyllHyde.Manager
{
    public class GameManager : MonoBehaviour
    {
        [field: SerializeField] private SceneManager SceneManager { get; set; }
        public IGameplayService GameplayService { get; set; }
        

        private void Awake()
        {
            DontDestroyOnLoad(this);

            GameplayService = new GameplayService();
        }

        private void Start()
        {
            SceneManager.LoadScene("GamePlay");
        }
    }
}