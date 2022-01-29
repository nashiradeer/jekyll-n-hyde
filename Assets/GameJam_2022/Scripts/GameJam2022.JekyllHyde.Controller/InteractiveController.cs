using GameJam2022.JekyllHyde.Domain.Interface;
using System;
using UnityEngine;

namespace GameJam2022.JekyllHyde.Controller
{
    public class InteractiveController : MonoBehaviour
    {
        public event Action<bool> OnHideStatusChange
        {
            add => _onHideStatusChange += value;
            remove => _onHideStatusChange -= value;
        }

        private Action<bool> _onHideStatusChange { get; set; }

        public IInteractable Interactable;
        public IPlayer Player;

        public void Init(IInteractable interactable, IPlayer player)
        {
            Interactable = interactable;
            Player = player;
        }

        public void Interact()
        {
            if (Interactable.Interact())
            {
                if (Interactable is IGetable)
                {
                    Player.Items[(Interactable as IGetable).Identifier] = true;

                    Destroy(gameObject);
                }
                else
                {
                    if (Player.ToggleHide()) _onHideStatusChange?.Invoke(Player.Hidden);
                }
            }
        }
    }
}
