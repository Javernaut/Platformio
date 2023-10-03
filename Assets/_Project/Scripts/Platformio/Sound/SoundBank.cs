using Platformio.DI;
using UnityEngine;

namespace Platformio.Sound
{
    [CreateAssetMenu(fileName = "NewSoundBank", menuName = "Sound/Creat Sound Bank", order = 0)]
    public class SoundBank : ScriptableObject
    {
        [SerializeField] [Range(0, 1)] private float volume = 1;
        [SerializeField] private AudioClip[] variations;

        public float Volume => volume;

        public int Size => variations.Length;

        public AudioClip this[int position] => variations[position];

        public AudioClip GetRandomClip()
        {
            return variations.GetRandomItem();
        }
    }
}