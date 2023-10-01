using System.Collections;
using Cinemachine;
using Platformio.Level;
using Platformio.Sound;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Platformio.Loop
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private CinemachineConfiner2D[] cameraConfiners;
        [SerializeField] private CinemachineVirtualCamera[] cameras;
        [SerializeField] private CinemachineStateDrivenCamera stateDrivenCamera;

        [Inject] private readonly PlayerStats _playerStats;
        [Inject] private readonly LevelAnnouncement.Factory _levelAnnouncementFactory;
        [Inject] private readonly LevelFacade.Factory _levelFactory;
        [Inject] private readonly MusicPlayer _musicPlayer;
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
                ResetLevelOnceLifeIsTaken();
            }
            else
            {
                // TODO Show modal Game over UI instead
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
            // TODO Extract the fade out/in settings
            _musicPlayer.FadeOut(1);
            yield return _fader.FadeOut(1);
            SceneManager.LoadScene(0);
            // TODO Cleanup
            // FindObjectOfType<ScenePersist>().ResetScenePersist();
            // TODO Dispose current level
        }

        public void LoadNextLevel()
        {
            SpawnNewLevel();
        }

        private void SpawnNewLevel()
        {
            StartCoroutine(StartNewLevelAsCoroutine());
        }

        private IEnumerator ReloadCurrentLevel()
        {
            yield return _fader.FadeOut(1);

            _currentLevel.Reload();
            
            yield return _fader.FadeIn(1);
        }

        private IEnumerator StartNewLevelAsCoroutine()
        {
            if (_currentLevel != null)
            {
                yield return _fader.FadeOut(1);
            }

            _currentLevel?.Destroy();
            _currentLevel = _levelFactory.Create();
            _currentLevel.InitWith(cameras, cameraConfiners, stateDrivenCamera);

            _levelAnnouncementFactory.Create(_currentLevelNumber++);
            
            yield return _fader.FadeIn(1);
        }
    }
}