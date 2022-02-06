using JekyllHyde.Player;
using UnityEngine;

namespace JekyllHyde.World
{
    public class PotionMixerInteraction : MonoBehaviour, IInteractable
    {
        [field: SerializeField] public int MinimumQuest { get; private set; }

        private QuestManager Manager { get; set; }

        public void Interact(PlayerInteract player, bool alternativeKey)
        {
            if (Manager.Step == 2) Manager.NextStep();
            if (Manager.Step == 4) Manager.NextStep();
            if (Manager.Step == 7) Manager.NextStep();
        }

        private void Start()
        {
            Manager = GameObject.FindGameObjectWithTag("QuestManager").GetComponent<QuestManager>();
        }
    }
}
