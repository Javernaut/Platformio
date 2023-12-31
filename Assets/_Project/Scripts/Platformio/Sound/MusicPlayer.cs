using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Platformio.Sound
{
    /// <summary>
    /// Plays a single track out of the injected <see cref="SoundBank"/> in a loop.
    /// Can be faded out. 
    /// </summary>
    public class MusicPlayer : EarWhisperingPlayer
    {
        [Inject] private float _fadeOutTime;
        [Inject] private SoundBank _soundBank;

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