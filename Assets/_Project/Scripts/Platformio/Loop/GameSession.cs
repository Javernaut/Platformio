using System.Collections;
using Cinemachine;
using Platformio.DI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Platformio.Loop
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private Transform levelRoot;

        [SerializeField] private CinemachineConfiner2D[] cameraConfiners;
        [SerializeField] private CinemachineVirtualCamera[] cameras;
        [SerializeField] private CinemachineStateDrivenCamera stateDrivenCamera;

        [Inject] private PlayerStats _playerStats;
        [Inject] private Level.Level.Factory _levelFactory;
        [Inject] private GameLoopSettingsInstaller.ThemeConfiguration _themeConfiguration;

        private int _currentThemeIndex;

        void Start()
        {
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
            // TODO Restart the SAME level
            SpawnNewLevel();
        }

        public void QuitToMainMenu()
        {
            SceneManager.LoadScene(0);
            // TODO Cleanup
            // FindObjectOfType<ScenePersist>().ResetScenePersist();
        }

        public void LoadNextLevel()
        {
            SpawnNewLevel();
        }

        private void SpawnNewLevel()
        {
            StartCoroutine(StartNewLevelAsCoroutine());
        }

        private IEnumerator StartNewLevelAsCoroutine()
        {
            var fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(1);
            foreach (Transform child in levelRoot)
            {
                Destroy(child.gameObject);
            }
            var settings = new Level.Level.Settings(_themeConfiguration.themes[_currentThemeIndex]);
            var level = _levelFactory.Create(settings);
            level.InitWith(cameras, cameraConfiners, stateDrivenCamera);

            _currentThemeIndex++;
            if (_currentThemeIndex == _themeConfiguration.themes.Length)
            {
                _currentThemeIndex = 0;
            }

            yield return fader.FadeIn(1);
        }
    }
}