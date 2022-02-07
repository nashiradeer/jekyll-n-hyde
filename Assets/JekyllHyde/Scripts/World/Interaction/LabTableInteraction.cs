using JekyllHyde.Entity.Player.Mechanics;
using JekyllHyde.World.Manager;
using UnityEngine;

namespace JekyllHyde.World.Interaction
{
    public class LabTableInteraction : MonoBehaviour, IInteractable
    {
        [field: SerializeField] public int MinimumStep { get; private set; }

        private QuestManager QuestManager { get; set; }

        public void Interact(PlayerInteract player, bool alternativeKey)
        {
            if (QuestManager.Step >= 8) player.Manager.DialogManager.Show("Senha da sala secreta 413....", 1.2f, 0.7f);
            else if (QuestManager.Step == 4) QuestManager.NextStep();
            else if (player.Manager.Inventory.Items[3] && QuestManager.Step == 2) QuestManager.NextStep();
            else if (player.Manager.Inventory.Items[2] && QuestManager.Step == 1) QuestManager.NextStep();
        }

        private void Start()
        {
            QuestManager = GameObject.FindGameObjectWithTag("QuestManager").GetComponent<QuestManager>();
        }
    }
}
