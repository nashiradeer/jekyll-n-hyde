using JekyllHyde.Entity.Player.Mechanics;
using UnityEngine;

namespace JekyllHyde.Entity.Player.Manager
{
    public class PlayerManager : MonoBehaviour
    {
        [field: SerializeField] public DialogManager DialogManager { get; set; }
        [field: SerializeField] public PlayerMovement Movement { get; set; }
        [field: SerializeField] public PlayerInteract Interact { get; set; }
        [field: SerializeField] public PlayerHide Hide { get; set; }
        [field: SerializeField] public PlayerInventory Inventory { get; set; }

        public void Mechanics(bool enabled)
        {
            Movement.EnabledMovement = enabled;
            Interact.EnabledInteract = enabled;
            Hide.EnabledHide = enabled;
        }
    }
}
