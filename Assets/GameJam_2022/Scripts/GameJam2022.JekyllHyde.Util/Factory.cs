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
    }
}