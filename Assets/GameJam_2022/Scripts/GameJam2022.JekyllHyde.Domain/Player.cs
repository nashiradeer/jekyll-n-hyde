using GameJam2022.JekyllHyde.Domain.Interface;
using System;

namespace GameJam2022.JekyllHyde.Domain
{
    public class Player : IPlayer
    {
        public bool[] Items { get; protected set; } = new bool[3];

        public bool Hidden { get; set; }

        public bool ToggleHide()
        {
            Hidden = !Hidden;
            return true;
        }
    }
}