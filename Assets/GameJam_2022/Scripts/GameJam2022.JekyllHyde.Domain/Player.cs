using GameJam2022.JekyllHyde.Domain.Interface;
using UnityEngine;

namespace GameJam2022.JekyllHyde.Domain
{
    public class Player : IPlayer
    {
        public PlayerOrientation Orientation { get; set; }
        private float currentDirection { get; set; }

        public Player(PlayerOrientation orientation)
        {
            Orientation = orientation;
            currentDirection = 1;
        }
        
        public bool ChangeDirection(float direction)
        {
            Debug.Log($"current: {currentDirection} | new: {direction} - current orientation: {Orientation}");
            
            if (currentDirection < 0 && direction > 0)
            {
                currentDirection = direction;
                Orientation = PlayerOrientation.Right;
                return true;
            }

            if (currentDirection > 0 && direction < 0)
            {
                currentDirection = direction;
                Orientation = PlayerOrientation.Left;
                return true;
            }

            currentDirection = direction;
            
            return false;
        }
    }
}