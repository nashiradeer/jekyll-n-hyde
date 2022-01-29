using GameJam2022.JekyllHyde.Domain.Interface;

namespace GameJam2022.JekyllHyde.Domain
{
    public class Getable : IGetable
    {
        public int Identifier { get; private set; }

        public Getable(int identifier)
        {
            Identifier = identifier;
        }

        public bool Interact()
        {
            return true;
        }
    }
}
