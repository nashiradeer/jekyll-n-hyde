using DG;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace NashiraDeer.Splash
{
    public class NashiraDeerSplash : MonoBehaviour
    {
        [field: SerializeField] private RectTransform Canvas { get; set; }
        [field: SerializeField] private RectTransform Background { get; set; }
        [field: SerializeField] private RectTransform BackgroundMask { get; set; }
        [field: SerializeField] private RectTransform Icon { get; set; }
        [field: SerializeField] private RectTransform Text { get; set; }
        [field: SerializeField] private Image Fade { get; set; }

        public Coroutine Start()
        {
            return StartCoroutine(Animation());
        }

        private IEnumerator Animation()
        {
            Background.sizeDelta = Canvas.sizeDelta;
            
            BackgroundMask.gameObject.SetActive(true);

            yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(ResizeAnim(Icon, 0.1f, 150));

            yield return new WaitForSeconds(0.025f);

            float maskMax = Mathf.Max(Canvas.sizeDelta.x + 500, Canvas.sizeDelta.y + 500);
            yield return StartCoroutine(ResizeAnim(BackgroundMask, 0.3f, maskMax));

            yield return new WaitForSeconds(10f);
        }

        private IEnumerator ResizeAnim(RectTransform obj, float duration, float size)
        {
            float currentTime = 0f;
            float startSize = obj.sizeDelta.x;

            while (currentTime < duration)
            {
                float value = Mathf.Lerp(startSize, size, currentTime / duration);

                obj.sizeDelta = new Vector2(value, value);
                currentTime += Time.deltaTime;

                yield return null;
            }
        }
    }
}