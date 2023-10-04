using UnityEngine;

namespace Platformio.Sound
{
    /// <summary>
    /// Base class for spawning a local audio source at the current location,
    /// so all sounds will be heard fine even when player or camera move.
    /// </summary>
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