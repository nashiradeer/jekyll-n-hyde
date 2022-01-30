using GameJam2022.JekyllHyde.Domain.Interface;

namespace GameJam2022.JekyllHyde.Domain
{
    public class GettableItem : IGettable
    {
        public int ItemId { get; private set; }

        public GettableItem(int itemId)
        {
            ItemId = itemId;
        }

        public bool Interact(IPlayer player)
        {
            return player.PickupItem(ItemId);
        }
    }
}
