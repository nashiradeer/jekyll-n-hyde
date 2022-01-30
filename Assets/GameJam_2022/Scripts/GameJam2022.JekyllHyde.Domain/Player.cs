using GameJam2022.JekyllHyde.Domain.Interface;
using System;
using UnityEngine;

namespace GameJam2022.JekyllHyde.Domain
{
    public class Player : IPlayer
    {
        public bool[] Items { get; protected set; } = new bool[3];
        public bool IsHidden { get; private set; }
        public bool CanHide { get; set; }
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

        public bool ChangeHide(bool hide)
        {
            Debug.Log($"Player Hide: current {IsHidden} | new {hide} - can hide: {CanHide}");
            if (hide == IsHidden) return false;

            if (!IsHidden && CanHide) IsHidden = true;
            else IsHidden = false;

            return true;
        }

        public bool PickupItem(int itemId)
        {
            try
            {
                Items[itemId] = true;
                Debug.Log($"item picked: {itemId}");
                Debug.Log($"inventory updated ({string.Join(",", Items)})");
                return true;
            }
            catch (IndexOutOfRangeException e)
            {
                Debug.LogException(e);
                return false;
            }
        }
    }
}