using UnityEngine;

namespace Platformio.Sound
{
    // Assume the listener is right where we are
    [RequireComponent(typeof(AudioListener))]
    public abstract class EarWhisperingPlayer : MonoBehaviour
    {
        protected AudioSource LocalAudioSource { get; private set; }

        protected virtual void Awake()
        {
            LocalAudioSource = gameObject.AddComponent<AudioSource>();
        }
    }
}