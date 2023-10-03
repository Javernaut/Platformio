using UnityEngine;

namespace Platformio.Sound
{
    public abstract class SoundBank : ScriptableObject
    {
        [SerializeField] [Range(0, 1)] private float volume = 1;

        public float Volume => volume;
        public abstract AudioClip GetClip();
    }
}