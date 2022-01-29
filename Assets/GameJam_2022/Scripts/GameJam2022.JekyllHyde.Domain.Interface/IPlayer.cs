namespace GameJam2022.JekyllHyde.Domain.Interface
{
    public interface IPlayer
    {
        bool[] Items { get; }

        bool IsHidden { get; }

        PlayerOrientation Orientation { get; }

        bool ChangeDirection(float direction);

        bool ChangeHide(bool hide);
    }

    public enum PlayerOrientation
    {
        Unknown = 0,
        Right = 1,
        Left = -1,
    }
}