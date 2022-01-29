using GameJam2022.JekyllHyde.Domain;
using GameJam2022.JekyllHyde.Domain.Interface;
using GameJam2022.JekyllHyde.Service.Interface;
using GameJam2022.JekyllHyde.Util;

namespace GameJam2022.JekyllHyde.Service
{
    public class GameplayService : IGameplayService
    {
        public IPlayer Player { get; set; }
        
        public GameplayService()
        {
            Player = Factory.CreatePlayer(PlayerOrientation.Right);
        }
    }
}