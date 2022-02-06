using JekyllHyde.Player;
using UnityEngine;

namespace JekyllHyde.World
{
    public class EndGameInteraction : MonoBehaviour, IInteractable
    {
        [field: SerializeField] private GameObject Lock1 { get; set; }
        [field: SerializeField] private GameObject Lock2 { get; set; }

        private QuestManager Manager { get; set; }
        private PlayerInventory Inventory { get; set; }

        [field: SerializeField] public int MinimumQuest { get; private set; }

        public void Interact(PlayerInteract player, bool alternativeKey)
        {
            if (alternativeKey)
            {
                if (Manager.Step == 13 && Inventory.Inventory[PlayerInventory.Key1Used] && Inventory.Inventory[PlayerInventory.Key2Used] && Inventory.Inventory[PlayerInventory.Key3Picked])
                {
                    Manager.NextStep();
                }

                if (Manager.Step == 11) Manager.NextStep();
            }
            else
            {
                if (!Inventory.Inventory[PlayerInventory.Key1Used] && Inventory.Inventory[PlayerInventory.Key1Picked])
                {
                    Lock1.SetActive(false);
                    Inventory.Inventory[PlayerInventory.Key1Used] = true;
                }
                else if (!Inventory.Inventory[PlayerInventory.Key2Used] && Inventory.Inventory[PlayerInventory.Key2Picked])
                {
                    Lock2.SetActive(false);
                    Inventory.Inventory[PlayerInventory.Key2Used] = true;
                }
            }
        }

        private void Start()
        {
            Manager = GameObject.FindGameObjectWithTag("QuestManager").GetComponent<QuestManager>();
            Inventory = GameObject.FindGameObjectWithTag("PlayerInventory").GetComponent<PlayerInventory>();

            if (Inventory.Inventory[PlayerInventory.Key1Used]) Lock1.SetActive(false);
            if (Inventory.Inventory[PlayerInventory.Key2Used]) Lock2.SetActive(false);
        }
    }
}
