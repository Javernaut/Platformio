using System;
using UnityEngine;
using Zenject;

namespace Platformio.Music
{
    public class MusicManager: IInitializable
    {
        [Inject] private AudioClip _audioClip;
        [Inject] private AudioListener _audioListener;
        
        public void Initialize()
        {
            var parentGameObject = _audioListener.gameObject;
            var audioSource = parentGameObject.AddComponent<AudioSource>();
            
            audioSource.loop = true;
            audioSource.clip = _audioClip;
            audioSource.Play();
        }

        [Serializable]
        public class Settings
        {
            public AudioClip[] mainMenuMusic;
            public AudioClip[] gameLoopMusic;
        }
    }
}