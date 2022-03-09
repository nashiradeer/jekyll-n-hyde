using JekyllHyde.World.Manager;
using UnityEngine;

namespace JekyllHyde.UI
{
    public class TutorialTriggerController : MonoBehaviour
    {
        [field: SerializeField] private QuestManager QuestManager { get; set; }

        private void Update()
        {
            if (QuestManager.Step == 0 && Input.GetMouseButtonDown(0))
            {
                QuestManager.TutorialTrigger();
            }
        }
    }
}