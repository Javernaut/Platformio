using System.Collections;
using Cinemachine;
using Platformio.Level;
using Platformio.Player;
using Platformio.Sound;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Platformio.Loop
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private CinemachineConfiner2D[] cameraConfiners;
        [SerializeField] private CinemachineStateDrivenCamera stateDrivenCamera;
        [SerializeField] private PlayerController playerController;

        [Inject] private readonly PlayerStats _playerStats;
        [Inject] private readonly LevelAnnouncement.Factory _levelAnnouncementFactory;
        [Inject] private readonly LevelFacade.Factory _levelFactory;
        [Inject] private readonly MusicPlayer _musicPlayer;
        [Inject] private readonly SoundPlayer _soundPlayer;
        [Inject] private readonly Fader _fader;

        private LevelFacade _currentLevel;
        private int _currentLevelNumber = 1;

        void Start()
        {
            _fader.FadeOutImmediate();
            SpawnNewLevel();
        }

        private void OnEnable()
        {
            _playerStats.OnLivesNumberChanged += ProcessPlayerDeath;
        }

        private void OnDisable()
        {
            _playerStats.OnLivesNumberChanged -= ProcessPlayerDeath;
        }

        private void ProcessPlayerDeath(int newLives)
        {
            if (newLives > 0)
            {
                _soundPlayer.PlayPlayerHitSound();
                ResetLevelOnceLifeIsTaken();
            }
            else
            {
                _soundPlayer.PlayGameOverSound();
                QuitToMainMenu();
            }
        }

        private void ResetLevelOnceLifeIsTaken()
        {
            StartCoroutine(ReloadCurrentLevel());
        }

        public void QuitToMainMenu()
        {
            StartCoroutine(QuitToMainMenuRoutine());
        }

        private IEnumerator QuitToMainMenuRoutine()
        {
            _musicPlayer.FadeOut();
            yield return _fader.FadeOut();
            SceneManager.LoadScene(0);
            // TODO Cleanup
            // TODO Dispose current level
        }

        public void LoadNextLevel()
        {
            _soundPlayer.PlayLevelCompletedSound();
            SpawnNewLevel();
        }

        private void SpawnNewLevel()
        {
            StartCoroutine(StartNewLevelAsCoroutine());
        }

        private IEnumerator ReloadCurrentLevel()
        {
            yield return _fader.FadeOut();

            _currentLevel.Reload();
            ForceRepositionCameraToPlayer();

            yield return _fader.FadeIn();
        }

        private IEnumerator StartNewLevelAsCoroutine()
        {
            if (_currentLevel != null)
            {
                yield return _fader.FadeOut();
                _currentLevel.Destroy();
            }

            _currentLevel = _levelFactory.Create();
            _currentLevel.InitWith(cameraConfiners, playerController);
            ForceRepositionCameraToPlayer();

            _levelAnnouncementFactory.Create(_currentLevelNumber++);

            yield return _fader.FadeIn();
        }

        private void ForceRepositionCameraToPlayer()
        {
            stateDrivenCamera.ForceCameraPosition(playerController.transform.position, Quaternion.identity);
        }
    }
}