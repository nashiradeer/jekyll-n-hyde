using JekyllHyde.Player;
using UnityEngine;

namespace JekyllHyde.World
{
    public class NextStepInteraction : MonoBehaviour, IInteractable
    {
        [field: SerializeField] private bool EnabledInteraction { get; set; }
        [field: SerializeField] private int InventoryIndex { get; set; }

        private QuestManager Manager { get; set; }
        private PlayerInventory Inventory { get; set; }

        [field: SerializeField] public int MinimumQuest { get; private set; }

        public void Interact(PlayerInteract player, bool alternativeKey)
        {
            if (!alternativeKey && EnabledInteraction)
            {
                EnabledInteraction = false;
                Inventory.Inventory[InventoryIndex] = true;
                Manager.NextStep();
                gameObject.SetActive(false);
            }
        }

        private void Start()
        {
            Manager = GameObject.FindGameObjectWithTag("QuestManager").GetComponent<QuestManager>();
            Inventory = GameObject.FindGameObjectWithTag("PlayerInventory").GetComponent<PlayerInventory>();

            if (Inventory.Inventory[InventoryIndex])
            {
                Debug.Log($"{gameObject.name}: Disabling, player already has enabled.");
                EnabledInteraction = false;
                gameObject.SetActive(false);
            }
        }
    }
}
