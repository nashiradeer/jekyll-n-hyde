using JekyllHyde.Player;
using UnityEngine;

namespace JekyllHyde.World
{
    public class InventoryItemInteraction : MonoBehaviour, IInteractable
    {
        [field: SerializeField] private bool EnabledInteraction { get; set; }
        [field: SerializeField] private int InventoryIndex { get; set; }
        private PlayerInventory Inventory { get; set; }

        [field: SerializeField] public int MinimumQuest { get; private set; }

        public void Interact(PlayerInteract player, bool alternativeKey)
        {
            if (!alternativeKey && EnabledInteraction)
            {
                Inventory.Inventory[InventoryIndex] = true;
                EnabledInteraction = false;
                gameObject.SetActive(false);
            }
        }

        private void Start()
        {
            Inventory = GameObject.FindGameObjectWithTag("PlayerInventory").GetComponent<PlayerInventory>();

            if (Inventory.Inventory[InventoryIndex])
            {
                EnabledInteraction = false;
                gameObject.SetActive(false);
            }
        }
    }
}
