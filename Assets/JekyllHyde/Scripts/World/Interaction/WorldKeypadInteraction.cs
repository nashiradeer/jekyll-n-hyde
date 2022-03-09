using JekyllHyde.Entity.Player.Mechanics;
using JekyllHyde.UI;
using JekyllHyde.World.Manager;
using UnityEngine;

namespace JekyllHyde.World.Interaction
{
    public class WorldKeypadInteraction : MonoBehaviour, IInteractable
    {
        [field: SerializeField] public int MinimumStep { get; private set; }

        [field: SerializeField] private int CurrentWorld { get; set; }
        [field: SerializeField] private int NewWorld { get; set; }
        [field: SerializeField] private int InventoryNumber { get; set; }
        [field: SerializeField] private string Password { get; set; }
        [field: SerializeField] private bool UseAlternativeKeypad { get; set; }

        private WorldManager WorldManager { get; set; }

        public void Interact(PlayerInteract player, bool alternativeKey)
        {
            if (alternativeKey)
            {
                if (PlayerInventory.Items[InventoryNumber])
                {
                    LoadWorld();
                }
                else
                {
                    KeypadController keypad = (!UseAlternativeKeypad) ? player.Keypad1 : player.Keypad2;
                    keypad.OnKeyCorrected.AddListener(LoadWorld);
                    keypad.Init(Password);
                    player.Manager.OpenMenu(keypad);
                }
            }
        }

        private void LoadWorld()
        {
            if (InventoryNumber >= 0) PlayerInventory.Items[InventoryNumber] = true;
            WorldManager.LoadWorld(NewWorld, CurrentWorld);
        }

        private void Start()
        {
            WorldManager = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<WorldManager>();
        }
    }
}
