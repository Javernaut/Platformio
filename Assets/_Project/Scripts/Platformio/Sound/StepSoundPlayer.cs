using System;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Platformio.Sound
{
    public class StepSoundPlayer : IInitializable, IDisposable
    {
        [Inject] private AudioListener _currentAudioListener;
        [Inject] private SoundBank _stepsSounds;

        private AudioSource LocalAudioSource { get; set; }

        public void Dispose()
        {
            Object.Destroy(LocalAudioSource);
        }

        public void Initialize()
        {
            LocalAudioSource = _currentAudioListener.gameObject.AddComponent<AudioSource>();
        }

        public void PlayStepSound()
        {
            LocalAudioSource.PlayOneShot(
                _stepsSounds.GetClip(),
                _stepsSounds.Volume
            );
        }
    }
}