using System;
using Platformio.DI;
using UnityEngine;
using Zenject;

namespace Platformio.Music
{
    [RequireComponent(typeof(AudioListener))]
    public class SoundManager : MonoBehaviour
    {
        [Inject] private Settings _settings;
        [SerializeField] private StepsSounds stepsSounds;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }

        public void OverrideStepsSounds(StepsSounds stepsSounds)
        {
            this.stepsSounds = stepsSounds;
        }

        public void PlayCoinAcquiredSound()
        {
            PlaySound(_settings.coinAcquiredSound);
        }

        public void PlayStepSound()
        {
            PlaySound(stepsSounds.variations.GetRandomItem());
        }

        private void PlaySound(AudioClip audioClip)
        {
            _audioSource.PlayOneShot(audioClip, _settings.defaultVolumeScale);
        }

        [Serializable]
        public class Settings
        {
            [Range(0, 1)] public float defaultVolumeScale = 1;
            public AudioClip coinAcquiredSound;
        }
    }
}