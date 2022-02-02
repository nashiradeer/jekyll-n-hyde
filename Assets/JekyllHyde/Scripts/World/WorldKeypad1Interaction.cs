using JekyllHyde.Entity.Player;
using UnityEngine;

namespace JekyllHyde.World
{
    public class WorldKeypad1Interaction : MonoBehaviour, IInteractable
    {
        [field: SerializeField] private int CurrentWorld { get; set; }
        [field: SerializeField] private int NewWorld { get; set; }
        [field: SerializeField] private int InventoryIndex { get; set; }
        private WorldManager Manager { get; set; }
        private PlayerInventory Inventory { get; set; }

        public void Interact(PlayerInteract player, bool alternativeKey)
        {
            if (alternativeKey)
            {
                if (Inventory.Inventory[PlayerInventory.DEPOSIT_LOCK])
                {
                    LoadWorld();
                }
                else
                {
                    player.Keypad1.OnKeyCorrected.AddListener(LoadWorld);
                    player.Keypad1.Open("GRB");
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
