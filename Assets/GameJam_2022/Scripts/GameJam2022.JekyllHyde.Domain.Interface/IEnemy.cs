namespace GameJam2022.JekyllHyde.Domain.Interface
{
    public interface IEnemy
    {
        PlayerOrientation Orientation { get; }
        int CurrentDirection { get; }
        bool Chasing { get; }
        float ChaseDistance { get; set; }

        bool ChaseUpdate(bool playerHidden, float playerX, float distance);
    }
}