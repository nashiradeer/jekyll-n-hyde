using DG.Tweening;
using JekyllHyde.Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JekyllHyde.World
{
    public class QuestManager : MonoBehaviour
    {
        [field: SerializeField] private Image WorldLoading { get; set; }
        [field: SerializeField] private TMP_Text DialogText { get; set; }
        [field: SerializeField] private Text QuestOnScreen { get; set; }
        [field: SerializeField] private PlayerMovement Movement { get; set; }
        [field: SerializeField] private PlayerInteract Interaction { get; set; }
        [field: SerializeField] private PlayerHide Hide { get; set; }
        [field: SerializeField] private GameObject EndGameScreen { get; set; }
        [field: SerializeField] private Text EndGameTxt1 { get; set; }
        [field: SerializeField] private Text EndGameTxt2 { get; set; }
        [field: SerializeField] private Text EndGameTxt3 { get; set; }
        [field: SerializeField] private List<string> StepDescriptions { get; set; }

        public int Step { get; private set; }

        public void NextStep()
        {
            QuestOnScreen.text = StepDescriptions[++Step];
            Debug.Log($"QuestManager: Executing update for step {Step}.");
            GameUpdate();
        }

        private void GameUpdate()
        {
            Debug.Log($"QuestManager: Update requested for step {Step}.");
            switch (Step)
            {
                case 0:
                    StartCoroutine(GameplayStart());
                    break;
                case 6:
                    LockPlayer();
                    StartCoroutine(DrinkPotion());
                    break;
                case 8:
                    LockPlayer();
                    StartCoroutine(Sleeping());
                    break;
                case 10:
                    LockPlayer();
                    StartCoroutine(BlinkLight());
                    break;
                case 12:
                    LockPlayer();
                    StartCoroutine(FindExit());
                    break;
                case 13:
                    // Enable Hyde
                    break;
                case 14:
                    StartCoroutine(EndGame());
                    break;
            }
        }

        private void LockPlayer(bool unlocked = false)
        {
            Movement.EnabledMovement = unlocked;
            Interaction.EnabledInteract = unlocked;
            Hide.EnabledHide = unlocked;
        }

        private IEnumerator EndGame()
        {
            EndGameScreen.SetActive(true);
            yield return WorldLoading.DOFade(1, 5f).WaitForCompletion();

            yield return new WaitForSeconds(1.5f);
            yield return EndGameTxt1.DOFade(1, 5f).WaitForCompletion();
            yield return EndGameTxt2.DOFade(1, 5f).WaitForCompletion();
            yield return EndGameTxt3.DOFade(1, 5f).WaitForCompletion();
            yield return new WaitForSeconds(2f);

            EndGameTxt1.DOFade(0, 5f);
            EndGameTxt2.DOFade(0, 5f);
            yield return EndGameTxt3.DOFade(0, 5f).WaitForCompletion();

            Application.Quit();
        }

        private IEnumerator FindExit()
        {
            DialogText.text = "Hyde, aquele monstro!";

            yield return DialogText.DOFade(1, 0.7f).WaitForCompletion();
            yield return new WaitForSeconds(1.2f);
            yield return DialogText.DOFade(0, 0.7f).WaitForCompletion();

            LockPlayer(true);
            NextStep();
        }

        private IEnumerator BlinkLight()
        {
            DialogText.text = "Lucy!";

            yield return DialogText.DOFade(1, 0.7f).WaitForCompletion();
            yield return new WaitForSeconds(1.2f);
            yield return DialogText.DOFade(0, 0.7f).WaitForCompletion();

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

            yield return new WaitForSeconds(1.2f);

            WorldLoading.color = new Color(0, 0, 0, 1);
            yield return WorldLoading.DOFade(0, 2).WaitForCompletion();

            yield return new WaitForSeconds(1.2f);

            // Sleep

            WorldLoading.color = new Color(0, 0, 0, 1);

            yield return new WaitForSeconds(2f);

            yield return WorldLoading.DOFade(0, 2).WaitForCompletion();
            // Wake Up and Scream

            yield return new WaitForSeconds(0.5f);

            DialogText.text = "Oh nao, Lucy!";

            yield return DialogText.DOFade(1, 0.7f).WaitForCompletion();
            yield return new WaitForSeconds(1.2f);
            yield return DialogText.DOFade(0, 0.7f).WaitForCompletion();

            LockPlayer(true);
            NextStep();
        }

        private IEnumerator DrinkPotion()
        {
            DialogText.text = "Acabou...";

            yield return DialogText.DOFade(1, 0.7f).WaitForCompletion();
            yield return new WaitForSeconds(1.2f);
            yield return DialogText.DOFade(0, 0.7f).WaitForCompletion();

            DialogText.text = "Eu nao tenho mais tempo...";

            yield return DialogText.DOFade(1, 0.7f).WaitForCompletion();
            yield return new WaitForSeconds(1.2f);
            yield return DialogText.DOFade(0, 0.7f).WaitForCompletion();

            LockPlayer(true);
            NextStep();
        }

        private IEnumerator GameplayStart()
        {
            DialogText.text = "Eu devo terminar a pocao antes que seja tarde demais e ele se liberte";

            yield return DialogText.DOFade(1, 0.7f).WaitForCompletion();
            yield return new WaitForSeconds(1.2f);
            yield return DialogText.DOFade(0, 0.7f).WaitForCompletion();

            LockPlayer(true);
            NextStep();
        }

        private void Start()
        {
            NextStep();
        }
    }
}
