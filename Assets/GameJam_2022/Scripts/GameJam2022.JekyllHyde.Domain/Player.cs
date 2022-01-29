using GameJam2022.JekyllHyde.Domain.Interface;
using UnityEngine;

namespace GameJam2022.JekyllHyde.Domain
{
    public class Player : IPlayer
    {
        public PlayerOrientation Orientation { get; set; }
        private float CurrentDirection { get; set; }

        public Player(PlayerOrientation orientation)
        {
            Orientation = orientation;
            CurrentDirection = 1;
        }
        
        public bool ChangeDirection(float direction)
        {
            Debug.Log($"current: {CurrentDirection} | new: {direction} - current orientation: {Orientation}");
            
            if (CurrentDirection < 0 && direction > 0)
            {
                CurrentDirection = direction;
                Orientation = PlayerOrientation.Right;
                return true;
            }

            if (CurrentDirection > 0 && direction < 0)
            {
                CurrentDirection = direction;
                Orientation = PlayerOrientation.Left;
                return true;
            }

            CurrentDirection = direction;
            
            return false;
        }
    }
}