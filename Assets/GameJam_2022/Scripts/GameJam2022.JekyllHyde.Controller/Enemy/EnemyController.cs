using GameJam2022.JekyllHyde.Domain.Interface;
using System;
using UnityEngine;

namespace GameJam2022.JekyllHyde.Controller.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        public float KillDistance = 0f;
        public float Speed = 2f;
        public float RunSpeed = 3f;

        public event Action OnKillPlayer
        {
            add => _onKillPlayer += value;
            remove => _onKillPlayer -= value;
        }

        private Action _onKillPlayer { get; set; }

        public event Action<bool> OnChasingUpdate
        {
            add => _onChasingUpdate += value;
            remove => _onChasingUpdate -= value;
        }

        private Action<bool> _onChasingUpdate { get; set; }

        private Transform PlayerPos;
        public IPlayer Player;
        private IEnemy Enemy;
        public float RoomLimit;

        public void Init(IEnemy enemy, IPlayer player, Transform playerPos)
        {
            PlayerPos = playerPos;
            Enemy = enemy;
            Player = player;
        }

        // Calculos de distancia do player
        private void FixedUpdate()
        {
            float distance = PlayerPos.position.x - gameObject.transform.position.x;

            if (distance < 0)
                distance = -distance;

            if (!Player.IsHidden && distance < KillDistance)
            {
                _onKillPlayer?.Invoke();
                return;
            }

            if (Enemy.ChaseUpdate(Player.IsHidden, PlayerPos.position.x, distance))
                _onChasingUpdate?.Invoke(Enemy.Chasing);
        }

        // Movimentação do personagem no Update para ser consistente com a do player
        private void Update()
        {
            float currentSpeed;
            if (Enemy.Chasing)
                currentSpeed = RunSpeed;
            else
                currentSpeed = Speed;

            Vector3 move = new Vector3(Enemy.CurrentDirection, 0);
            transform.position += move * (currentSpeed * Time.deltaTime);

        }
    }
}
