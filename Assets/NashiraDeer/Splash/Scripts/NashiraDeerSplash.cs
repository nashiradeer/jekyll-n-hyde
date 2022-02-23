using DG;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace NashiraDeer.Splash
{
    public class NashiraDeerSplash : MonoBehaviour
    {
        [field: SerializeField] public Camera SceneCamera { get; set; }

        [field: SerializeField] private RectTransform Canvas { get; set; }
        [field: SerializeField] private RectTransform Background { get; set; }
        [field: SerializeField] private RectTransform BackgroundMask { get; set; }
        [field: SerializeField] private RectTransform Icon { get; set; }
        [field: SerializeField] private RectTransform Text { get; set; }
        [field: SerializeField] private RectTransform TextMask { get; set; }
        [field: SerializeField] private Image Fade { get; set; }

        [field: SerializeField] private AudioSource Boom { get; set; }
        [field: SerializeField] private AudioSource Move { get; set; }

        public Coroutine StartSplash()
        {
            return StartCoroutine(Animation());
        }

        private IEnumerator Animation()
        {
            if (SceneCamera != null) Fade.color = new Color(SceneCamera.backgroundColor.r, SceneCamera.backgroundColor.g, SceneCamera.backgroundColor.b, 1);

            Background.sizeDelta = Canvas.sizeDelta;

            Icon.gameObject.SetActive(true);
            Fade.gameObject.SetActive(true);

            yield return StartCoroutine(FadeAnim(Fade, 2f, 0));

            yield return new WaitForSeconds(0.2f);

            yield return StartCoroutine(ResizeAnim(Icon, 0.1f, 150));

            yield return new WaitForSeconds(0.1f);

            Boom.PlayDelayed(0.05f);

            BackgroundMask.gameObject.SetActive(true);

            float maskMax = Mathf.Max(Canvas.sizeDelta.x + 500, Canvas.sizeDelta.y + 500);
            yield return StartCoroutine(ResizeAnim(BackgroundMask, 0.3f, maskMax));

            TextMask.gameObject.SetActive(true);

            Move.Play();

            StartCoroutine(MoveXAnim(TextMask, 0.45f, -82));
            StartCoroutine(MoveXAnim(Text, 0.45f, 0));
            yield return StartCoroutine(MoveXAnim(Icon, 0.45f, 151));

            yield return new WaitForSeconds(3f);

            yield return StartCoroutine(FadeAnim(Fade, 1.7f, 1));

            BackgroundMask.gameObject.SetActive(false);
            Icon.gameObject.SetActive(false);
            TextMask.gameObject.SetActive(false);
            Fade.gameObject.SetActive(false);

            yield return new WaitForSeconds(0.5f);
        }

        private IEnumerator FadeAnim(Image obj, float duration, float alpha)
        {
            float currentTime = 0f;
            float startSize = obj.color.a;

            while (currentTime < duration)
            {
                float value = Mathf.Lerp(startSize, alpha, currentTime / duration);

                obj.color = new Color(obj.color.r, obj.color.g, obj.color.b, value);
                currentTime += Time.deltaTime;

                yield return null;
            }
        }

        private IEnumerator MoveXAnim(RectTransform obj, float duration, float newPos)
        {
            float currentTime = 0f;
            float startSize = obj.localPosition.x;

            while (currentTime < duration)
            {
                float value = Mathf.Lerp(startSize, newPos, currentTime / duration);

                obj.localPosition = new Vector3(value, obj.localPosition.y, obj.localPosition.z);
                currentTime += Time.deltaTime;

                yield return null;
            }
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