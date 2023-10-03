using System;
using Zenject;

namespace Platformio.Sound
{
    public class SoundPlayer : EarWhisperingPlayer
    {
        [Inject] private Settings _settings;

        public void PlayScorePickupAcquiredSound() => PlaySound(_settings.coinAcquired);

        public void PlayJumpSound() => PlaySound(_settings.jump);

        public void PlayEnemyHitSound() => PlaySound(_settings.enemyHit);

        public void PlayPlayerHitSound() => PlaySound(_settings.playerHit);

        private void PlaySound(SoundBank soundBank)
        {
            LocalAudioSource.PlayOneShot(soundBank.GetRandomClip(), soundBank.Volume);
        }

        [Serializable]
        public class Settings
        {
            public SoundBank coinAcquired;
            public SoundBank jump;
            public SoundBank enemyHit;
            public SoundBank playerHit;
        }
    }
}