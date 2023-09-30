using System;
using Platformio.DI;
using UnityEngine;
using Zenject;

namespace Platformio.Sound
{
    public class SoundPlayer : EarWhisperingPlayer
    {
        [SerializeField] private StepsSounds stepsSounds;

        [Inject] private Settings _settings;

        // TODO Create separate StepSoundPlayer, spawn it at the main camera's audio listener,
        // don't forget to destroy it along with its audio source
        public void OverrideStepsSounds(StepsSounds stepsSounds)
        {
            this.stepsSounds = stepsSounds;
        }

        public void PlayScorePickupAcquiredSound()
        {
            PlaySound(_settings.coinAcquiredSound);
        }

        public void PlayStepSound()
        {
            PlaySound(stepsSounds.variations.GetRandomItem());
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