using System;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Platformio
{
    public class GameSession : MonoBehaviour
    {
        private int _playerLives;
        private int _score;
        
        [SerializeField] TextMeshProUGUI livesText;
        [SerializeField] TextMeshProUGUI scoreText;

        [SerializeField] private Level.Level levelPrefab; 
        [SerializeField] private Transform levelRoot;

        [SerializeField] private CinemachineConfiner2D[] cameraConfiners;
        [SerializeField] private CinemachineVirtualCamera[] cameras;
        [SerializeField] private CinemachineStateDrivenCamera stateDrivenCamera;

        [Inject]
        public void Construct(Settings settings)
        {
            _playerLives = settings.initialLives;
            _score = settings.initialScore;
        }
        
        void Start() 
        {
            livesText.text = _playerLives.ToString();
            scoreText.text = _score.ToString();

            var level = Instantiate(levelPrefab, levelRoot);
            level.InitWith(cameras, cameraConfiners, stateDrivenCamera);
        }

        public void ProcessPlayerDeath()
        {
            if (_playerLives > 1)
            {
                TakeLife();
            }
            else
            {
                // TODO Show modal Game over UI instead
                QuitToMainMenu();
            }
        }

        void TakeLife()
        {
            _playerLives--;
            livesText.text = _playerLives.ToString();

            foreach (Transform child in levelRoot)
            {
                Destroy(child.gameObject);
            }
            var level = Instantiate(levelPrefab, levelRoot);
            level.InitWith(cameras, cameraConfiners, stateDrivenCamera);
        }

        public void QuitToMainMenu()
        {
            SceneManager.LoadScene(0);
            // TODO Cleanup
            // FindObjectOfType<ScenePersist>().ResetScenePersist();
        }
        
        public void AddToScore(int pointsToAdd)
        {
            _score += pointsToAdd;
            scoreText.text = _score.ToString(); 
        }

        public void LoadNextLevel()
        {
            foreach (Transform child in levelRoot)
            {
                Destroy(child.gameObject);
            }
            var level = Instantiate(levelPrefab, levelRoot);
            level.InitWith(cameras, cameraConfiners, stateDrivenCamera);
        }

        [Serializable]
        public class Settings
        {
            [Min(0)]
            public int initialLives;
            [Min(0)]
            public int initialScore;
        }
    }
}