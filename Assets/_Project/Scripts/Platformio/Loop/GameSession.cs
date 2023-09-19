using Cinemachine;
using Platformio.DI;
using TMPro;
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

        private PlayerStats _playerStats;
        private Level.Level.Factory _levelFactory;
        private GameLoopSettingsInstaller.ThemeConfiguration _themeConfiguration;

        private int _currentThemeIndex;

        [Inject]
        public void Construct(PlayerStats playerStats, Level.Level.Factory levelFactory,
            GameLoopSettingsInstaller.ThemeConfiguration themeConfiguration)
        {
            _playerStats = playerStats;
            _levelFactory = levelFactory;
            _themeConfiguration = themeConfiguration;
        }

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
            foreach (Transform child in levelRoot)
            {
                Destroy(child.gameObject);
            }

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
            foreach (Transform child in levelRoot)
            {
                Destroy(child.gameObject);
            }

            SpawnNewLevel();
        }

        private void SpawnNewLevel()
        {
            var level = _levelFactory.Create(
                new Level.Level.Settings(_themeConfiguration.themes[_currentThemeIndex])
            );
            level.InitWith(cameras, cameraConfiners, stateDrivenCamera);
            
            _currentThemeIndex++;
            if (_currentThemeIndex == _themeConfiguration.themes.Length)
            {
                _currentThemeIndex = 0;
            }
        }
    }
}