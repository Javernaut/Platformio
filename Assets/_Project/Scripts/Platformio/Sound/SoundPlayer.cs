using System;
using UnityEngine;
using Zenject;

namespace Platformio.Sound
{
    public class SoundPlayer : EarWhisperingPlayer
    {
        [Inject] private Settings _settings;

        public void PlayScorePickupAcquiredSound()
        {
            PlaySound(_settings.coinAcquiredSound);
        }

        public void PlayJumpSound()
        {
            PlaySound(_settings.jumpSound);
        }

        private void PlaySound(AudioClip audioClip)
        {
            LocalAudioSource.PlayOneShot(audioClip, _settings.defaultVolumeScale);
        }

        [Serializable]
        public class Settings
        {
            [Range(0, 1)] public float defaultVolumeScale = 1;
            public AudioClip coinAcquiredSound;
            public AudioClip jumpSound;
        }
    }
}