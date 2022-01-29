namespace GameJam2022.JekyllHyde.Domain.Interface
{
    public interface IPlayer
    {
        bool[] Items { get; }

        bool Hidden { get; set; }

        bool ToggleHide();

        PlayerOrientation Orientation { get; }

        bool ChangeDirection(float direction);
    }

    public enum PlayerOrientation
    {
        Unknown = 0,
        Right = 1,
        Left = -1,
    }
}