using System;
using Zenject;

namespace Platformio.Sound
{
    public class SoundPlayer : EarWhisperingPlayer
    {
        [Inject] private Settings _settings;

        public void PlayScorePickupAcquiredSound()
        {
            PlaySound(_settings.coinAcquired);
        }

        public void PlayJumpSound()
        {
            PlaySound(_settings.jump);
        }

        public void PlayEnemyHitSound()
        {
            PlaySound(_settings.enemyHit);
        }

        public void PlayPlayerHitSound()
        {
            PlaySound(_settings.playerHit);
        }

        public void PlayLaserHitSound()
        {
            PlaySound(_settings.laserHit);
        }

        public void PlayLaserShotSound()
        {
            PlaySound(_settings.laserShot);
        }


        public void PlayGameOverSound()
        {
            PlaySound(_settings.gameOver);
        }

        public void PlayLevelCompletedSound()
        {
            PlaySound(_settings.levelCompleted);
        }

        private void PlaySound(SoundBank soundBank)
        {
            LocalAudioSource.PlayOneShot(soundBank.GetClip(), soundBank.Volume);
        }

        [Serializable]
        public class Settings
        {
            public SoundBank coinAcquired;
            public SoundBank jump;
            public SoundBank enemyHit;
            public SoundBank playerHit;
            public SoundBank laserShot;
            public SoundBank laserHit;
            public SoundBank gameOver;
            public SoundBank levelCompleted;
        }
    }
}