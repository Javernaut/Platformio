using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Platformio.Sound
{
    public class MusicPlayer : EarWhisperingPlayer
    {
        [Inject] private SoundBank _soundBank;
        [Inject] private float _fadeOutTime;

        protected override void Awake()
        {
            base.Awake();

            LocalAudioSource.loop = true;
            LocalAudioSource.clip = _soundBank.GetClip();
            LocalAudioSource.volume = _soundBank.Volume;
            LocalAudioSource.Play();
        }

        public void FadeOut()
        {
            StartCoroutine(FadeRoutine(0, _fadeOutTime));
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
        public float musicFadeOutTime = 1;
        public SoundBank mainMenuMusic;
        public SoundBank gameLoopMusic;
    }
}