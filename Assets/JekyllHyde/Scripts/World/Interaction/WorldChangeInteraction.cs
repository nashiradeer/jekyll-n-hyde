using JekyllHyde.Entity.Player.Mechanics;
using JekyllHyde.World.Manager;
using UnityEngine;

namespace JekyllHyde.World.Interaction
{
    public class WorldChangeInteraction : MonoBehaviour, IInteractable
    {
        [field: SerializeField] public int MinimumStep { get; private set; }

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
