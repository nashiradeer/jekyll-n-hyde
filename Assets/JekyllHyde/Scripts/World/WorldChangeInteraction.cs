using JekyllHyde.Player;
using UnityEngine;

namespace JekyllHyde.World
{
    public class WorldChangeInteraction : MonoBehaviour, IInteractable
    {
        [field: SerializeField] private int CurrentWorld { get; set; }
        [field: SerializeField] private int NewWorld { get; set; }
        private WorldManager Manager { get; set; }

        public void Interact(PlayerInteract player, bool alternativeKey)
        {
            if (alternativeKey) Manager.LoadWorld(NewWorld, CurrentWorld);
        }

        private void Start()
        {
            Manager = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<WorldManager>();
        }
    }
}
