using JekyllHyde.Entity.Player.Mechanics;

namespace JekyllHyde.World.Interaction
{
    public interface IInteractable
    {
        int MinimumStep { get; }

        void Interact(PlayerInteract player, bool alternativeKey);
    }
}
