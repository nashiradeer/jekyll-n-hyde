using GameJam2022.JekyllHyde.Domain.Interface;
using System;
using UnityEngine;

namespace GameJam2022.JekyllHyde.Controller
{
    public class InteractiveController : MonoBehaviour
    {
        public IInteractable Interactable;

        public void Init(IInteractable interactable)
        {
            Interactable = interactable;
        }

        public void Interact(IPlayer player)
        {
            if (Interactable.Interact(player))
            {
                if (Interactable is IGettable) Destroy(gameObject);
            }
        }
    }
}
