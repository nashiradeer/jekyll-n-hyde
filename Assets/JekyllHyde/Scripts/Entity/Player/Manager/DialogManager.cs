using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace JekyllHyde.Entity.Player.Manager
{
    public class DialogManager : MonoBehaviour
    {
        [field: SerializeField] private TMP_Text DialogText { get; set; }

        private Queue<DialogMachineObject> dialogList = new Queue<DialogMachineObject>();
        private Coroutine RunningDialogMachine = null;

        public void Show(string dialog, float onScreenTime, float fadeTime)
        {
            dialogList.Enqueue(new DialogMachineObject()
            {
                Description = dialog,
                OnScreenTime = onScreenTime,
                FadeTime = fadeTime
            });

            if (RunningDialogMachine == null) RunningDialogMachine = StartCoroutine(DialogMachine());
        }

        public Coroutine ShowNow(string dialog, float onScreenTime, float fadeTime)
        {
            return StartCoroutine(ImmediateDialog(dialog, onScreenTime, fadeTime)); 
        }

        private IEnumerator ImmediateDialog(string dialog, float onScreenTime, float fadeTime)
        {
            dialogList.Clear();
            
            if (RunningDialogMachine != null) yield return RunningDialogMachine;

            yield return StartCoroutine(DialogShow(dialog, onScreenTime, fadeTime));
        }

        private IEnumerator DialogMachine()
        {
            while (dialogList.Count > 0)
            {
                DialogMachineObject obj = dialogList.Dequeue();

                yield return StartCoroutine(DialogShow(obj.Description, obj.OnScreenTime, obj.FadeTime));

                yield return new WaitForSeconds(0.5f);
            }

            DialogText.text = "";
            RunningDialogMachine = null;
        }

        private IEnumerator DialogShow(string dialog, float onScreenTime, float fadeTime)
        {
            DialogText.text = dialog;

            yield return DialogText.DOFade(1f, fadeTime).WaitForCompletion();
            yield return new WaitForSeconds(onScreenTime);
            yield return DialogText.DOFade(0f, fadeTime).WaitForCompletion();
        }


        private struct DialogMachineObject
        {
            public string Description { get; set; }
            public float OnScreenTime { get; set; }
            public float FadeTime { get; set; }
        }
    }
}
