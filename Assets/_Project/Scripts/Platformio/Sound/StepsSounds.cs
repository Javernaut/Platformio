using UnityEngine;

namespace Platformio.Sound
{
    // TODO Setup the attribute better
    [CreateAssetMenu]
    public class StepsSounds : ScriptableObject
    {
        [Range(0, 1)] public float volume = 1;
        public AudioClip[] variations;
    }
}