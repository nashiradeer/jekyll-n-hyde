using DG.Tweening;
using JekyllHyde.Entity.Player.Manager;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JekyllHyde.World.Manager
{
    public class QuestManager : MonoBehaviour
    {
        [field: SerializeField] private Image VisionOverlay { get; set; }
        [field: SerializeField] private Text QuestOnScreen { get; set; }
        [field: SerializeField] private PlayerManager PlayerManager { get; set; }
        [field: SerializeField] private DialogManager DialogManager { get; set; }
        [field: SerializeField] private GameObject EndGameScreen { get; set; }
        [field: SerializeField] private Text EndGameText1 { get; set; }
        [field: SerializeField] private Text EndGameText2 { get; set; }
        [field: SerializeField] private Text EndGameText3 { get; set; }
        [field: SerializeField] private List<string> StepDescriptions { get; set; }

        public int Step { get; private set; }

        private bool TriggerLocked { get; set; }

        public void NextStep()
        {
            QuestOnScreen.text = StepDescriptions[++Step];
            Debug.Log($"QuestManager: Executing update for step {Step}.");
            GameUpdate();
            TriggerLocked = false;
        }

        private void GameUpdate()
        {
            Debug.Log($"QuestManager: Update requested for step {Step}.");
            switch (Step)
            {
                case 0:
                    StartCoroutine(GameplayStart());
                    break;
                case 5:
                    PlayerManager.Mechanics(false);
                    StartCoroutine(Sleeping());
                    break;
                case 13:
                    // Enable Hyde
                    break;
            }
        }

        private IEnumerator EndGame()
        {
            QuestOnScreen.text = "";
            EndGameScreen.SetActive(true);
            yield return VisionOverlay.DOFade(1, 5f).WaitForCompletion();

            yield return new WaitForSeconds(1.5f);
            yield return EndGameText1.DOFade(1, 5f).WaitForCompletion();
            yield return EndGameText2.DOFade(1, 5f).WaitForCompletion();
            yield return EndGameText3.DOFade(1, 5f).WaitForCompletion();
            yield return new WaitForSeconds(2f);

            EndGameText1.DOFade(0, 5f);
            EndGameText2.DOFade(0, 5f);
            yield return EndGameText3.DOFade(0, 5f).WaitForCompletion();

            Application.Quit();
        }

        private IEnumerator FindExit()
        {
            yield return DialogManager.ShowNow("Hyde, aquele monstro!", 1.2f, 0.7f);

            PlayerManager.Mechanics(true);

            NextStep();
        }

        private IEnumerator BlinkLight()
        {
            yield return DialogManager.ShowNow("Lucy!", 1.2f, 0.7f);

            VisionOverlay.color = new Color(0, 0, 0, 1);
            yield return new WaitForSeconds(0.5f);
            VisionOverlay.color = new Color(0, 0, 0, 0);

            yield return new WaitForSeconds(1f);
            PlayerManager.Mechanics(true);
            NextStep();
        }

        private IEnumerator Sleeping()
        {
            VisionOverlay.color = new Color(0, 0, 0, 1);
            yield return VisionOverlay.DOFade(0, 2).WaitForCompletion();

            yield return new WaitForSeconds(1.2f);

            VisionOverlay.color = new Color(0, 0, 0, 1);
            yield return VisionOverlay.DOFade(0, 2).WaitForCompletion();

            yield return new WaitForSeconds(1.2f);

            // Sleep

            VisionOverlay.color = new Color(0, 0, 0, 1);

            yield return new WaitForSeconds(2f);

            yield return VisionOverlay.DOFade(0, 2).WaitForCompletion();
            // Wake Up and Scream

            yield return new WaitForSeconds(0.5f);

            yield return DialogManager.ShowNow("Oh nao, Lucy!", 1.2f, 0.7f);

            PlayerManager.Mechanics(true);
            NextStep();
        }

        private IEnumerator DrinkPotion()
        {
            yield return DialogManager.ShowNow("Acabou...", 1.2f, 0.7f);
            yield return DialogManager.ShowNow("Eu nao tenho mais tempo...", 1.2f, 0.7f);

            PlayerManager.Mechanics(true);
            NextStep();
        }

        private IEnumerator GameplayStart()
        {
            yield return DialogManager.ShowNow("Eu devo terminar a pocao antes que seja tarde demais e ele se liberte", 2f, 1.25f);

            PlayerManager.Mechanics(true);
            NextStep();
        }

        public void GreenPotionTrigger()
        {
            if (TriggerLocked) return;
            TriggerLocked = true;

            PlayerManager.Mechanics(false);
            StartCoroutine(DrinkPotion());
        }

        public void LucyTrigger()
        {
            if (TriggerLocked) return;
            TriggerLocked = true;

            PlayerManager.Mechanics(false);
            StartCoroutine(BlinkLight());
        }

        public void LockedDoorTrigger()
        {
            if (TriggerLocked) return;
            TriggerLocked = true;

            PlayerManager.Mechanics(false);
            StartCoroutine(FindExit());
        }

        public void EndGameTrigger()
        {
            if (TriggerLocked) return;
            TriggerLocked = true;

            PlayerManager.Mechanics(false);
            // Disable Hyde
            StartCoroutine(EndGame());
        }

        private void Start()
        {
            GameUpdate();
        }
    }
}
