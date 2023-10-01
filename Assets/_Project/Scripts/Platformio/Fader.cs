using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Platformio
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Fader : MonoBehaviour
    {
        [Inject] private readonly Settings _settings;

        private CanvasGroup _canvasGroup;
        private Coroutine _currentlyActiveFade;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void FadeOutImmediate()
        {
            _canvasGroup.alpha = 1f;
        }

        public Coroutine FadeOut()
        {
            return Fade(1, _settings.fadeOutTime);
        }

        public Coroutine FadeIn()
        {
            return Fade(0, _settings.fadeInTime);
        }

        private Coroutine Fade(float target, float time)
        {
            if (_currentlyActiveFade != null)
            {
                StopCoroutine(_currentlyActiveFade);
            }

            _currentlyActiveFade = StartCoroutine(FadeRoutine(target, time));
            return _currentlyActiveFade;
        }

        private IEnumerator FadeRoutine(float target, float time)
        {
            return FadeRoutine(_canvasGroup, target, time);
        }

        public static IEnumerator FadeRoutine(CanvasGroup canvasGroup, float target, float time)
        {
            while (!Mathf.Approximately(canvasGroup.alpha, target))
            {
                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, target, Time.unscaledDeltaTime / time);
                yield return null;
            }
        }

        [Serializable]
        public class Settings
        {
            public float fadeInTime = 1;
            public float fadeOutTime = 1;
        }
    }
}