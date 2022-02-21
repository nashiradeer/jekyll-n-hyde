using JekyllHyde.World.Manager;
using UnityEngine;

namespace JekyllHyde.UI
{
    public class TutorialTriggerController : MonoBehaviour
    {
        [field: SerializeField] private QuestManager QuestManager { get; set; }

        private void FixedUpdate()
        {
            if (QuestManager.Step == 0 && Input.GetMouseButton(0))
            {
                QuestManager.TutorialTrigger();
            }
        }
    }
}