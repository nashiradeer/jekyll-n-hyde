using GameJam2022.JekyllHyde.Domain.Interface;

namespace GameJam2022.JekyllHyde.Domain
{
    public class Interactable : IInteractable
    {
        public int Identifier { get; private set; }

        public Interactable(int identifier)
        {
            Identifier = identifier;
        }

        public bool Interact()
        {
            return true;
        }
    }
}
