using GameJam2022.JekyllHyde.Domain;
using GameJam2022.JekyllHyde.Domain.Interface;

namespace GameJam2022.JekyllHyde.Util
{
    public static class Factory
    {
        public static IPlayer CreatePlayer(PlayerOrientation playerOrientation)
        {
            return new Player(playerOrientation);
        }

        public static IEnemy CreateEnemy(PlayerOrientation enemyOrientation)
        {
            return new Enemy(enemyOrientation);
        }

        public static IInteractable CreateGettableItem(int itemId)
        {
            return new GettableItem(itemId);
        }
    }
}