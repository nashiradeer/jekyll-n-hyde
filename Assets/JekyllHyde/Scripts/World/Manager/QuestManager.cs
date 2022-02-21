using DG.Tweening;
using JekyllHyde.Entity.Hyde;
using JekyllHyde.Entity.Player.Manager;
using JekyllHyde.Entity.Player.Mechanics;
using JekyllHyde.UI.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace JekyllHyde.World.Manager
{
    public class QuestManager : MonoBehaviour
    {
        [field: SerializeField] private Camera MainCamera { get; set; }
        [field: SerializeField] private Image VisionOverlay { get; set; }
        [field: SerializeField] private Image Tutorial { get; set; }
        [field: SerializeField] private Text QuestOnScreen { get; set; }
        [field: SerializeField] private PlayerManager PlayerManager { get; set; }
        [field: SerializeField] private DialogManager DialogManager { get; set; }
        [field: SerializeField] private GameplayManager GameplayManager { get; set; }
        [field: SerializeField] private HydeSimulator HydeAi { get; set; }
        [field: SerializeField] private GameObject EndGameScreen { get; set; }
        [field: SerializeField] private Text EndGameText1 { get; set; }
        [field: SerializeField] private Text EndGameText2 { get; set; }
        [field: SerializeField] private Text EndGameText3 { get; set; }
        [field: SerializeField] private GameObject GlobalLight { get; set; }
        [field: SerializeField] private GameObject PlayerLight { get; set; }
        [field: SerializeField] private List<string> StepDescriptions { get; set; }

        public static int Step { get; set; }

        private bool TriggerLocked { get; set; }

        public void NextStep()
        {
            Step++;
            Debug.Log($"QuestManager: Triggering the next step {Step}.");
            GameUpdate();
            TriggerLocked = false;
        }

        private void GameUpdate()
        {
            Debug.Log($"QuestManager: Executing the step {Step}.");
            QuestOnScreen.text = StepDescriptions[Step];
            switch (Step)
            {
                case 0:
                    Tutorial.gameObject.SetActive(true);

                    Cursor.lockState = CursorLockMode.Confined;
                    Cursor.visible = true;

                    break;
                case 5:
                    PlayerManager.Mechanics(false);
                    StartCoroutine(Sleeping());
                    break;
                case 8:
                    HydeAi.EnabledHyde = true;

                    PlayerLight.SetActive(true);
                    GlobalLight.SetActive(false);

                    MainCamera.backgroundColor = Color.black;

                    PlayerManager.Mechanics(true);
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

            Step = 0;
            for (int i = 0; i <= 8; i++) PlayerInventory.Items[i] = false;

            SceneManager.LoadScene(1);
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

            PlayerLight.SetActive(true);
            GlobalLight.SetActive(false);

            MainCamera.backgroundColor = Color.black;

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

            VisionOverlay.color = new Color(0, 0, 0, 1);

            yield return new WaitForSeconds(2f);

            yield return VisionOverlay.DOFade(0, 2).WaitForCompletion();

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
            yield return Tutorial.DOFade(0f, 1.5f).WaitForCompletion();
            Tutorial.gameObject.SetActive(false);

            yield return DialogManager.ShowNow("Eu devo terminar a pocao antes que seja tarde demais e ele se liberte", 2f, 1.25f);

            PlayerManager.Mechanics(true);
            NextStep();
        }

        public void TutorialTrigger()
        {
            if (TriggerLocked) return;
            TriggerLocked = true;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            StartCoroutine(GameplayStart());
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

            GameplayManager.EnabledPause = false;
            HydeAi.EnabledHyde = false;

            PlayerManager.Mechanics(false);

            StartCoroutine(EndGame());
        }

        private void Start()
        {
            GameUpdate();
        }
    }
}
