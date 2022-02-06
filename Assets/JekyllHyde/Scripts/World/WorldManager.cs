using DG.Tweening;
using JekyllHyde.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace JekyllHyde.World
{
    public class WorldManager : MonoBehaviour
    {
        [field: SerializeField] private Image WorldLoading;
        [field: SerializeField] private PlayerCamera Camera;

        [field: SerializeField] public int InitialWorld { get; set; }
        [field: SerializeField] public GameObject Player { get; set; }
        [field: SerializeField] public GameObject HydePrefab { get; set; }
        //[field: SerializeField] public HydeSimulator HydeAi { get; set; } = null;
        [field: SerializeField] public List<WorldManagerElement> Worlds { get; set; }
        [field: SerializeField] public List<WorldExposedZone> ExposedZones { get; set; }

        public int CurrentWorldIndex { get; set; }

        private GameObject CurrentHyde = null;
        private WorldManagerElement CurrentWorld = null;
        private GameObject CurrentWorldObj = null;
        private Tween FadeIn = null;

        public void LoadWorld(int room, int? lastRoom = null, bool unload = true)
        {
            Debug.Log($"WorldManager: Loading room {room} from {lastRoom}. (Unload? {unload})");
            if (unload) UnloadWorld();
            else WorldLoading.color = new Color(0, 0, 0, 1);

            CurrentWorldIndex = room;

            CurrentWorld = Worlds[room];
            CurrentWorldObj = Instantiate(CurrentWorld.World, transform);

            Camera.CameraY = CurrentWorld.Header.CameraY;
            Camera.RightX = CurrentWorld.Header.CameraLimitRight;
            Camera.LeftX = CurrentWorld.Header.CameraLimitLeft;
            Camera.transform.position = new Vector3(0, CurrentWorld.Header.CameraY, Camera.transform.position.z);

            float x = (lastRoom == null) ? CurrentWorld.Header.Spawns[0].SpawnX : (from selectRoom in CurrentWorld.Header.Spawns where selectRoom.LastRoom == lastRoom select selectRoom).FirstOrDefault().SpawnX;

            Player.transform.position = new Vector3(x, CurrentWorld.Header.JekyllY);

            // Pass Hyde from Simulator to World if he are in here

            FadeIn = WorldLoading.DOFade(0, 1);
        }

        private void UnloadWorld()
        {
            if (FadeIn != null) FadeIn.Kill();
            WorldLoading.color = new Color(0, 0, 0, 1);

            if (CurrentHyde != null)
            {
                // Pass Hyde from World to Simulator
            }

            if (CurrentWorldObj != null)
            {
                Destroy(CurrentWorldObj);
                CurrentWorldObj = null;
            }
        }

        private void Start()
        {
            LoadWorld(InitialWorld, unload: false);
        }
    }

    [Serializable]
    public class WorldManagerElement
    {
        [field: SerializeField] public GameObject World { get; set; }
        [field: SerializeField] public WorldHeader Header { get; set; }
    }

    [Serializable]
    public class WorldHeader
    {
        [field: SerializeField] public float JekyllY { get; set; }
        [field: SerializeField] public float CameraY { get; set; }
        [field: SerializeField] public float CameraLimitRight { get; set; }
        [field: SerializeField] public float CameraLimitLeft { get; set; }
        [field: SerializeField] public List<WorldSpawn> Spawns { get; set; }
    }

    [Serializable]
    public class WorldSpawn
    {
        [field: SerializeField] public int LastRoom { get; set; }
        [field: SerializeField] public float SpawnX { get; set; }
    }

    [Serializable]
    public class WorldExposedZone
    {
        [field: SerializeField] public int RelatedWorld { get; set; }
        [field: SerializeField] public float WorldY { get; set; }
        [field: SerializeField] public string ZoneLeft { get; set; }
        [field: SerializeField] public string ZoneRight { get; set; }
        [field: SerializeField] public float LeftX { get; set; }
        [field: SerializeField] public float RightX { get; set; }
    }
}
