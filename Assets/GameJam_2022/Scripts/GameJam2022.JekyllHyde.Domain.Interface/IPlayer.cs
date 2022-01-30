namespace GameJam2022.JekyllHyde.Domain.Interface
{
    public interface IPlayer
    {
        bool[] Items { get; }

        bool IsHidden { get; }

        bool CanHide { get; set; }

        PlayerOrientation Orientation { get; }

        bool ChangeDirection(float direction);

        bool ChangeHide(bool hide);

        bool PickupItem(int itemId);
    }

    public enum PlayerOrientation
    {
        Unknown = 0,
        Right = 1,
        Left = -1,
    }
}