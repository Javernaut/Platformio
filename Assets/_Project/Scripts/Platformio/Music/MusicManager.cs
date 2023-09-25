using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Platformio.Music
{
    // Assume the listener is right where we are
    [RequireComponent(typeof(AudioListener))]
    public class MusicManager : MonoBehaviour
    {
        [Inject] private AudioClip _audioClip;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = gameObject.AddComponent<AudioSource>();

            _audioSource.loop = true;
            _audioSource.clip = _audioClip;
            _audioSource.Play();
        }

        public void FadeOut(float time)
        {
            StartCoroutine(FadeRoutine(0, time));
        }

        private IEnumerator FadeRoutine(float target, float time)
        {
            while (!Mathf.Approximately(_audioSource.volume, target))
            {
                _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, target, Time.unscaledDeltaTime / time);
                yield return null;
            }
        }

        [Serializable]
        public class Settings
        {
            public AudioClip[] mainMenuMusic;
            public AudioClip[] gameLoopMusic;
        }
    }
}