namespace GameJam2022.JekyllHyde.Domain.Interface
{
    public interface IEnemy
    {
        PlayerOrientation Orientation { get; }
        int CurrentDirection { get; set; }
        bool Chasing { get; }

        bool ChaseUpdate(bool playerHidden, float distance, float playerX, float enemyX);
    }
}