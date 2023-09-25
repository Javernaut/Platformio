using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Platformio.Music
{
    [RequireComponent(typeof(AudioListener))]
    public class MusicManager : MonoBehaviour
    {
        [Inject] private AudioClip _audioClip;
        
        private AudioListener _audioListener;
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioListener = GetComponent<AudioListener>();
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