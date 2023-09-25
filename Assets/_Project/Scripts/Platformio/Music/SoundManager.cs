using System;
using UnityEngine;
using Zenject;

namespace Platformio.Music
{
    [RequireComponent(typeof(AudioListener))]
    public class SoundManager : MonoBehaviour
    {
        [Inject] private Settings _settings;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }

        public void PlayCoinAcquiredSound()
        {
            _audioSource.PlayOneShot(_settings.coinAcquiredSound, _settings.defaultVolumeScale);
        }

        [Serializable]
        public class Settings
        {
            [Range(0, 1)] public float defaultVolumeScale = 1;
            public AudioClip coinAcquiredSound;
        }
    }
}