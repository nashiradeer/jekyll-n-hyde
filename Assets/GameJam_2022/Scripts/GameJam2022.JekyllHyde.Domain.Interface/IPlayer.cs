using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam2022.JekyllHyde.Domain.Interface
{
    public interface IPlayer
    {
        PlayerOrientation Orientation { get; }

        bool ChangeDirection(float direction);
    }

    public enum PlayerOrientation
    {
        Unknown = 0,
        Right = 1,
        Left = -1,
    }
}