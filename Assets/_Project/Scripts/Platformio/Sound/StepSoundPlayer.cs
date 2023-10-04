using System;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Platformio.Sound
{
    /// <summary>
    /// Plays a random sound of a step according to the current <see cref="SoundBank"/> that comes from the current theme.
    /// </summary>
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