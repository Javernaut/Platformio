using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Platformio.Loop
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI livesText;
        [SerializeField] TextMeshProUGUI scoreText;

        [SerializeField] private Transform levelRoot;

        [SerializeField] private CinemachineConfiner2D[] cameraConfiners;
        [SerializeField] private CinemachineVirtualCamera[] cameras;
        [SerializeField] private CinemachineStateDrivenCamera stateDrivenCamera;

        private PlayerStats _playerStats;
        private Level.Level.Factory _levelFactory;

        [Inject]
        public void Construct(PlayerStats playerStats, Level.Level.Factory levelFactory) 
        {
            _playerStats = playerStats;
            _levelFactory = levelFactory;
        }

        void Start()
        {
            livesText.text = _playerStats.PlayerLives.ToString();
            scoreText.text = _playerStats.Score.ToString();

            var level = _levelFactory.Create(new Level.Level.Settings(1));
            level.InitWith(cameras, cameraConfiners, stateDrivenCamera);
        }

        private void OnEnable()
        {
            // TODO Move all this into a separate MonoBehaviour. And Extract this UI GameObjects out of GameSession prefab
            _playerStats.OnLivesNumberChanged += ProcessPlayerDeath;
            _playerStats.OnScoreChanged += OnScoreChanged;
        }

        private void OnDisable()
        {
            _playerStats.OnLivesNumberChanged -= ProcessPlayerDeath;
            _playerStats.OnScoreChanged -= OnScoreChanged;
        }

        private void OnScoreChanged(int newScore)
        {
            scoreText.text = newScore.ToString();
        }

        private void ProcessPlayerDeath(int newLives)
        {
            livesText.text = _playerStats.PlayerLives.ToString();
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

            var level = _levelFactory.Create(new Level.Level.Settings(1));
            level.InitWith(cameras, cameraConfiners, stateDrivenCamera);
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

            var level = _levelFactory.Create(new Level.Level.Settings(1));
            level.InitWith(cameras, cameraConfiners, stateDrivenCamera);
        }
    }
}