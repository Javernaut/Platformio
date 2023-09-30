using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Platformio.Sound
{
    public class MusicPlayer : EarWhisperingPlayer
    {
        [Inject] private AudioClip _audioClip;

        protected override void Awake()
        {
            base.Awake();

            LocalAudioSource.loop = true;
            LocalAudioSource.clip = _audioClip;
            LocalAudioSource.Play();
        }

        public void FadeOut(float time)
        {
            StartCoroutine(FadeRoutine(0, time));
        }

        private IEnumerator FadeRoutine(float target, float time)
        {
            while (!Mathf.Approximately(LocalAudioSource.volume, target))
            {
                LocalAudioSource.volume =
                    Mathf.MoveTowards(LocalAudioSource.volume, target, Time.unscaledDeltaTime / time);
                yield return null;
            }
        }
    }

    [Serializable]
    public class GlobalMusicSettings
    {
        public AudioClip[] mainMenuMusic;
        public AudioClip[] gameLoopMusic;
    }
}