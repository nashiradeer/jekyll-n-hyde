using JekyllHyde.Entity.Player;

namespace JekyllHyde.World
{
    public interface IInteractable
    {
        void Interact(PlayerInteract player, bool alternativeKey);
    }
}
