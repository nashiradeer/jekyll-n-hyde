using JekyllHyde.Entity.Player.Manager;
using JekyllHyde.Entity.Player.Mechanics;
using UnityEngine;

namespace JekyllHyde.World.Interaction
{
    public class InventoryItemInteraction : MonoBehaviour, IInteractable
    {
        [field: SerializeField] public int MinimumStep { get; private set; }

        [field: SerializeField] private int InventoryNumber { get; set; }

        public void Interact(PlayerInteract player, bool alternativeKey)
        {
            if (!alternativeKey)
            {
                PlayerInventory.Items[InventoryNumber] = true;
                gameObject.SetActive(false);
            }
        }

        private void Start()
        {
            if (PlayerInventory.Items[InventoryNumber]) gameObject.SetActive(false);
        }
    }
}
