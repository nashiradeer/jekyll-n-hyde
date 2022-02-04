using DG.Tweening;
using JekyllHyde.Entity.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JekyllHyde.World
{
    public class QuestManager : MonoBehaviour
    {
        [field: SerializeField] private Text QuestOnScreen { get; set; }
        [field: SerializeField] private Image WorldLoading;
        [field: SerializeField] private PlayerMovement Movement { get; set; }
        [field: SerializeField] private PlayerInteract Interaction { get; set; }
        [field: SerializeField] private PlayerHide Hide { get; set; }

        public int Step { get; private set; }
        [field: SerializeField] public List<string> StepDescriptions { get; private set; }

        public void NextStep()
        {
            Step++;
            QuestOnScreen.text = StepDescriptions[Step];
            GameUpdate();
        }

        private void GameUpdate()
        {
            switch (Step)
            {
                case 0:
                    NextStep();
                    break;
                case 4:
                    NextStep();
                    break;
                case 6:
                    LockPlayer();
                    StartCoroutine(Sleeping());
                    break;
                case 8:
                    LockPlayer();
                    StartCoroutine(BlinkLight());
                    break;
                case 10:
                    LockPlayer();
                    StartCoroutine(FindExit());
                    break;
            }
        }

        private void LockPlayer(bool unlocked = false)
        {
            Movement.EnabledMovement = unlocked;
            Interaction.EnabledInteract = unlocked;
            Hide.EnabledHide = unlocked;
        }

        private IEnumerator FindExit()
        {
            // Find Exit
            yield return null;

            LockPlayer(true);
            NextStep();
        }

        private IEnumerator BlinkLight()
        {
            // Lucy!

            WorldLoading.color = new Color(0, 0, 0, 1);
            yield return new WaitForSeconds(0.5f);
            WorldLoading.color = new Color(0, 0, 0, 0);

            yield return new WaitForSeconds(1f);
            LockPlayer(true);
            NextStep();
        }

        private IEnumerator Sleeping()
        {
            WorldLoading.color = new Color(0, 0, 0, 1);
            yield return WorldLoading.DOFade(0, 2).WaitForCompletion();

            yield return new WaitForSeconds(1f);

            WorldLoading.color = new Color(0, 0, 0, 1);
            yield return WorldLoading.DOFade(0, 2).WaitForCompletion();

            yield return new WaitForSeconds(1f);

            // Sleep

            WorldLoading.color = new Color(0, 0, 0, 1);

            yield return new WaitForSeconds(3f);
            // Wake Up and Scream

            LockPlayer(true);
            NextStep();
        }

        private void Start()
        {
            GameUpdate();
        }
    }
}
