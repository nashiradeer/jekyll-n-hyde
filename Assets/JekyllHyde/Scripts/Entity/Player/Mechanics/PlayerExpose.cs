using JekyllHyde.Entity.Hyde;
using JekyllHyde.World.Manager;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace JekyllHyde.Entity.Player.Mechanics
{
    public class PlayerExpose : MonoBehaviour
    {
        [field: SerializeField] private WorldManager WorldManager { get; set; }
        [field: SerializeField] private PlayerMovement Movement { get; set; }
        [field: SerializeField] private HydeSimulator HydeAi { get; set; }
        [field: SerializeField] private Image ExposedImage { get; set; }

        [field: SerializeField] private Sprite ExposedIcon { get; set; }
        [field: SerializeField] private Sprite NoExposedIcon { get; set; }

        public bool IsExposed { get; private set; }

        private bool AlreadyCalledHyde { get; set; }

        private void FixedUpdate()
        {
            if (WorldManager.CurrentExposedZones != null && WorldManager.CurrentExposedZones.Length > 0)
            {
                IEnumerable<WorldExposedZone> worlds = from zone in WorldManager.CurrentExposedZones where zone.RightX < transform.position.x && zone.LeftX > transform.position.x select zone;
                if (worlds.Count() > 0)
                {
                    ExposedImage.gameObject.SetActive(true);
                    IsExposed = true;
                    if (Movement.Moving)
                    {
                        ExposedImage.sprite = ExposedIcon;

                        if (!AlreadyCalledHyde)
                        {
                            AlreadyCalledHyde = true;
                            HydeAi.CallHyde(WorldManager.GetExposedZoneIndex(worlds.First()));
                        }
                    }
                    else
                    {
                        ExposedImage.sprite = NoExposedIcon;
                        AlreadyCalledHyde = false;
                    }
                }
                else
                {
                    ExposedImage.gameObject.SetActive(false);
                    AlreadyCalledHyde = false;
                    IsExposed = false;
                }
            }
            else
            {
                ExposedImage.gameObject.SetActive(false);
                AlreadyCalledHyde = false;
                IsExposed = false;
            }
        }
    }
}
