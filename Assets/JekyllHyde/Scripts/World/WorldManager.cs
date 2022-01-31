using JekyllHyde.Entity;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace JekyllHyde.World
{
    public class WorldManager : MonoBehaviour
    {
        [field: SerializeField] public int InitialWorld { get; set; }
        [field: SerializeField] public GameObject PlayerPrefab { get; set; }
        [field: SerializeField] public GameObject HydePrefab { get; set; }
        //[field: SerializeField] public HydeSimulator HydeAi { get; set; } = null;
        [field: SerializeField] public List<WorldManagerElement> Worlds { get; set; }

        private GameObject CurrentPlayer = null;
        private GameObject CurrentHyde = null;
        private WorldManagerElement CurrentWorld = null;

        public void LoadWorld(int roomId, EntityDirection playerDirection)
        {
            if (CurrentPlayer != null)
            {
                Destroy(CurrentPlayer);
            }

            if (CurrentHyde != null)
            {
                // Pass Hyde from World to Simulator
            }

            foreach (Transform child in transform)
            {
                Destroy(child);
            }

            CurrentWorld = Worlds[roomId];
            Instantiate(CurrentWorld.World, transform);

            float x;
            if (playerDirection == EntityDirection.Left) x = CurrentWorld.Header.JekyllLeft;
            else x = CurrentWorld.Header.JekyllRight;

            CurrentPlayer = Instantiate(PlayerPrefab, new Vector3(x, CurrentWorld.Header.JekyllY), Quaternion.identity);

            // Pass Hyde from Simulator to World if he are in here
        }

        private void Start()
        {
            LoadWorld(InitialWorld, EntityDirection.Right);
        }

        private void FixedUpdate()
        {
            if (CurrentPlayer != null)
            {
                if (CurrentPlayer.transform.position.x > CurrentWorld.Header.JekyllRight)
                {
                    LoadWorld(CurrentWorld.Header.RoomRight, EntityDirection.Left);
                }
                if (CurrentPlayer.transform.position.x < CurrentWorld.Header.JekyllLeft)
                {
                    LoadWorld(CurrentWorld.Header.RoomLeft, EntityDirection.Right);
                }
            }
        }
    }

    [Serializable] public class WorldManagerElement
    {
        [field: SerializeField] public GameObject World { get; set; }
        [field: SerializeField] public WorldHeader Header { get; set; }
    }

    [Serializable] public class WorldHeader
    {
        [field: SerializeField] public bool HasHyde { get; set; }
        [field: SerializeField] public int RoomLeft { get; set; }
        [field: SerializeField] public int RoomRight { get; set; }
        [field: SerializeField] public float HydeLeft { get; set; }
        [field: SerializeField] public float HydeRight { get; set; }
        [field: SerializeField] public float HydeY { get; set; }
        [field: SerializeField] public float JekyllLeft { get; set; }
        [field: SerializeField] public float JekyllRight { get; set; }
        [field: SerializeField] public float JekyllY { get; set; }
    }
}
