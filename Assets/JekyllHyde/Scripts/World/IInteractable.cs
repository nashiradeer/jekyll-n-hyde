using JekyllHyde.Player;

namespace JekyllHyde.World
{
    public interface IInteractable
    {
        int MinimumQuest { get; }

        void Interact(PlayerInteract player, bool alternativeKey);
    }
}
