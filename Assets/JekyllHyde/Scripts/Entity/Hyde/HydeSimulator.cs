using JekyllHyde.World.Manager;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace JekyllHyde.Entity.Hyde
{
    public class HydeSimulator : MonoBehaviour
    {
        [field: SerializeField] private float Speed { get; set; }
        [field: SerializeField] private WorldManager WorldManager { get; set; }
        [field: SerializeField] private List<HydeWorldZone> Worlds { get; set; }

        [field: SerializeField] private float X { get; set; }
        [field: SerializeField] public int CurrentZone { get; private set; }

        public bool EnabledHyde { get; set; }

        public EntityDirection Direction { get; private set; }


        private int TargetZone { get; set; } = -1;

        public void CallHyde(int exposedZone)
        {
            int worldZone = -1;
            
            for(int i = 0;i < Worlds.Count;i++)
            {
                if (Worlds[i].ExposedZone == exposedZone)
                {
                    worldZone = i;
                    break;
                }
            }

            if (worldZone == -1) return;

            TargetZone = worldZone;
        }
        
        private void UpdateHydeZone()
        {
            WorldManager.DeleteHyde();
            int newZone = (Direction == EntityDirection.Left) ? CurrentZone - 1 : CurrentZone + 1;
            Debug.Log($"HydeSimulator: Current Zone {CurrentZone}, New Zone {newZone}, Direction {Direction}.");

            if (TargetZone > -1)
            {
                if (CurrentZone > TargetZone && Direction != EntityDirection.Left)
                {
                    Direction = EntityDirection.Left;
                    newZone = newZone - 1;
                }
                else if (CurrentZone < TargetZone && Direction != EntityDirection.Right)
                {
                    Direction = EntityDirection.Right;
                    newZone = newZone + 1;
                }

                TargetZone = -1;

                Debug.Log($"HydeSimulator: Target Zone, Current Zone {CurrentZone}, New Zone {newZone}, Direction {Direction}.");
            }

            if (newZone < 0) Direction = EntityDirection.Right;
            else if (newZone >= Worlds.Count) Direction = EntityDirection.Left;
            else CurrentZone = newZone;


            Debug.Log($"HydeSimulator: Final Result, Current Zone {CurrentZone}, Direction {Direction}.");

            if (Direction == EntityDirection.Right) X = 0;
            else X = Worlds[CurrentZone].Size;
        }

        private void FixedUpdate()
        {
            if (!EnabledHyde) return;

            X = (Direction == EntityDirection.Right) ? X + Speed : X - Speed;

            if (X < 0) UpdateHydeZone();
            else if (X > Worlds[CurrentZone].Size) UpdateHydeZone();

            if (Worlds[CurrentZone].ExposedZone > -1) WorldManager.SyncHyde(Worlds[CurrentZone].ExposedZone, X, Direction);
        }
    }

    [Serializable]
    public class HydeWorldZone
    {
        [field: SerializeField] public int ExposedZone { get; set; }
        [field: SerializeField] public int Size { get; set; }
    }
}
