namespace GameJam2022.JekyllHyde.Domain.Interface
{
    public interface IInteractable
    {
        int Identifier { get; }
        bool Interact();
    }
}
