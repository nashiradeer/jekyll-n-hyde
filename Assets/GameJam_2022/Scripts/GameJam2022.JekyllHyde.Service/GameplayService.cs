using GameJam2022.JekyllHyde.Domain.Interface;
using GameJam2022.JekyllHyde.Service.Interface;
using GameJam2022.JekyllHyde.Util;

namespace GameJam2022.JekyllHyde.Service
{
    public class GameplayService : IGameplayService
    {
        public IPlayer Player { get; set; }
        public IEnemy Enemy { get; set; }
        
        public GameplayService()
        {
            Player = Factory.CreatePlayer(PlayerOrientation.Right);
            Enemy = Factory.CreateEnemy(PlayerOrientation.Left);
        }
    }
}