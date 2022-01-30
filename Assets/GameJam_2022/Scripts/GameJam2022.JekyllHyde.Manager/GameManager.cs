using System;
using GameJam2022.JekyllHyde.Manager.Interface;
using GameJam2022.JekyllHyde.Service;
using GameJam2022.JekyllHyde.Service.Interface;
using UnityEngine;

namespace GameJam2022.JekyllHyde.Manager
{
    public class GameManager : MonoBehaviour, IGameManager
    {
        public IGameplayService GameplayService { get; set; }
        
        public void Init()
        {
            DontDestroyOnLoad(this);

            InicializarServicos();
        }

        private void InicializarServicos()
        {
            GameplayService = new GameplayService();
        }
    }
}