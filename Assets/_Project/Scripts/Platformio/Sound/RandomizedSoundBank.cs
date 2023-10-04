using Platformio.DI;
using UnityEngine;

namespace Platformio.Sound
{
    [CreateAssetMenu(fileName = "NewRandomizedSoundBank", menuName = "Sound/Creat Randomized Sound Bank", order = 0)]
    public class RandomizedSoundBank : SoundBank
    {
        [SerializeField] private AudioClip[] variations;

        public override AudioClip GetClip()
        {
            return variations.GetRandomItem();
        }
    }
}