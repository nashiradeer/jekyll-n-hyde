﻿using JekyllHyde.Entity.Player.Mechanics;
using JekyllHyde.UI;
using JekyllHyde.UI.Manager;
using UnityEngine;

namespace JekyllHyde.Entity.Player.Manager
{
    public class PlayerManager : MonoBehaviour
    {
        [field: SerializeField] public GameplayManager GameplayManager { get; set; }

        [field: SerializeField] public DialogManager DialogManager { get; set; }
        [field: SerializeField] public PlayerMovement Movement { get; set; }
        [field: SerializeField] public PlayerInteract Interact { get; set; }
        [field: SerializeField] public PlayerHide Hide { get; set; }
        
        public IUIMenu CurrentMenu { get; private set; }

        public void Mechanics(bool enabled)
        {
            Movement.EnabledMovement = enabled;
            Interact.EnabledInteract = enabled;
            Hide.EnabledHide = enabled;
        }

        public void OpenMenu(IUIMenu menu)
        {
            CurrentMenu = menu;
            menu.Open(this);
        }

        public void CloseMenu()
        {
            if (CurrentMenu == null) return;
            CurrentMenu.Close();
            CurrentMenu = null;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (CurrentMenu == null) GameplayManager.PauseGame(this);
                else CloseMenu();
            }
        }
    }
}
