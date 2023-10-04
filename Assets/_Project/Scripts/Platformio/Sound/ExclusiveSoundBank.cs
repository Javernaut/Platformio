using UnityEngine;

namespace Platformio.Sound
{
    /// <summary>
    /// Represents a single <see cref="AudioClip"/> with the preferred volume.
    /// </summary>
    [CreateAssetMenu(fileName = "NewSoundBank", menuName = "Sound/Creat Sound Bank", order = 0)]
    public class ExclusiveSoundBank : SoundBank
    {
        [SerializeField] private AudioClip audioClip;

        public override AudioClip GetClip()
        {
            return audioClip;
        }
    }
}