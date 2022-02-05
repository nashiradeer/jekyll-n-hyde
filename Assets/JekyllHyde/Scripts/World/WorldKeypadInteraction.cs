using JekyllHyde.Player;
using JekyllHyde.UI;
using UnityEngine;

namespace JekyllHyde.World
{
    public class WorldKeypadInteraction : MonoBehaviour, IInteractable
    {
        [field: SerializeField] private int CurrentWorld { get; set; }
        [field: SerializeField] private int NewWorld { get; set; }
        [field: SerializeField] private int InventoryIndex { get; set; }
        [field: SerializeField] private string Password { get; set; }
        [field: SerializeField] private bool UseAlternativeKeypad { get; set; }

        private WorldManager Manager { get; set; }
        private PlayerInventory Inventory { get; set; }

        public void Interact(PlayerInteract player, bool alternativeKey)
        {
            if (alternativeKey)
            {
                if (Inventory.Inventory[InventoryIndex])
                {
                    LoadWorld();
                }
                else
                {
                    KeypadController keypad = (!UseAlternativeKeypad) ? player.Keypad1 : player.Keypad2;
                    keypad.OnKeyCorrected.AddListener(LoadWorld);
                    keypad.Open(Password);
                }
            }
        }

        private void LoadWorld()
        {
            if (InventoryIndex >= 0) Inventory.Inventory[InventoryIndex] = true;
            Manager.LoadWorld(NewWorld, CurrentWorld);
        }

        private void Start()
        {
            Manager = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<WorldManager>();
            Inventory = GameObject.FindGameObjectWithTag("PlayerInventory").GetComponent<PlayerInventory>();
        }
    }
}
