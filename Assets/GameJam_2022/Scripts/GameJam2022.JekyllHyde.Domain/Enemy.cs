using GameJam2022.JekyllHyde.Domain.Interface;

namespace GameJam2022.JekyllHyde.Domain
{
    public class Enemy : IEnemy
    {
        public PlayerOrientation Orientation { get; private set; }
        public int CurrentDirection { get; private set; }
        public bool Chasing { get; private set; }
        public float ChaseDistance { get; set; }

        public Enemy(PlayerOrientation orientation, float chaseDistance)
        {
            Orientation = orientation;
            CurrentDirection = (orientation == PlayerOrientation.Right) ? 1 : -1;
            ChaseDistance = chaseDistance;
            Chasing = false;
        }

        public bool ChaseUpdate(bool playerHidden, float playerX, float distance)
        {
            if (!Chasing && playerHidden)
                return false;

            if (Chasing && playerHidden)
            {
                Chasing = false;
                return true;
            }

            if (playerX > 0 && CurrentDirection < 0)
                return false;

            if (playerX < 0 && CurrentDirection > 0)
                return false;

            if (distance < ChaseDistance && !Chasing)
            {
                Chasing = true;
                return true;
            }
            else if (distance > ChaseDistance && Chasing)
            {
                Chasing = false;
                return true;
            }

            return false;
        }
    }
}