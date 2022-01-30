using GameJam2022.JekyllHyde.Domain.Interface;
using System;
using UnityEngine;

namespace GameJam2022.JekyllHyde.Controller
{
    public class InteractiveController : MonoBehaviour
    {
        public IInteractable Interactable;
        public IPlayer Player;

        public void Init(IInteractable interactable, IPlayer player)
        {
            Interactable = interactable;
            Player = player;
        }

        public void Interact()
        {
            if (Interactable.Interact(Player))
            {
                if (Interactable is IGettable) Destroy(gameObject);
            }
        }
    }
}
