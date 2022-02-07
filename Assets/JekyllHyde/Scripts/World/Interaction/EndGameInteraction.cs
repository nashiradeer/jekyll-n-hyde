using JekyllHyde.Entity.Player.Manager;
using JekyllHyde.Entity.Player.Mechanics;
using JekyllHyde.World.Manager;
using UnityEngine;

namespace JekyllHyde.World.Interaction
{
    public class EndGameInteraction : MonoBehaviour, IInteractable
    {
        [field: SerializeField] public int MinimumStep { get; private set; }
        [field: SerializeField] private GameObject Padlock1 { get; set; }
        [field: SerializeField] private GameObject Padlock2 { get; set; }

        private QuestManager QuestManager { get; set; }

        public void Interact(PlayerInteract player, bool alternativeKey)
        {
            if (alternativeKey)
            {
                if (QuestManager.Step == 8 && player.Manager.Inventory.Items[7] && player.Manager.Inventory.Items[8] && player.Manager.Inventory.Items[6])
                {
                    QuestManager.EndGameTrigger();
                }

                if (QuestManager.Step == 7) QuestManager.LockedDoorTrigger();
            }
            else
            {
                if (!player.Manager.Inventory.Items[7] && player.Manager.Inventory.Items[4])
                {
                    Padlock1.SetActive(false);
                    player.Manager.Inventory.Items[7] = true;
                }
                else if (!player.Manager.Inventory.Items[8] && player.Manager.Inventory.Items[5])
                {
                    Padlock2.SetActive(false);
                    player.Manager.Inventory.Items[8] = true;
                }
            }
        }

        private void Start()
        {
            QuestManager = GameObject.FindGameObjectWithTag("QuestManager").GetComponent<QuestManager>();

            PlayerManager manager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
            if (manager.Inventory.Items[7]) Padlock1.SetActive(false);
            if (manager.Inventory.Items[8]) Padlock2.SetActive(false);
        }
    }
}
