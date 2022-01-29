using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam2022.JekyllHyde.Domain.Interface
{
    public interface IPlayer
    {
        bool[] Items { get; }

        bool Hidden { get; set; }

        bool ToggleHide();
    }
}