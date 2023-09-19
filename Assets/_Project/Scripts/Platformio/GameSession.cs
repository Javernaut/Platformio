using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformio
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] int playerLives = 3;
        [SerializeField] int score = 0;

        [SerializeField] TextMeshProUGUI livesText;
        [SerializeField] TextMeshProUGUI scoreText;

        [SerializeField] private Level.Level levelPrefab; 
        [SerializeField] private Transform levelRoot;

        [SerializeField] private CinemachineConfiner2D[] cameraConfiners;
        [SerializeField] private CinemachineVirtualCamera[] cameras;
        [SerializeField] private CinemachineStateDrivenCamera stateDrivenCamera;
        
        void Start() 
        {
            livesText.text = playerLives.ToString();
            scoreText.text = score.ToString();

            var level = Instantiate(levelPrefab, levelRoot);
            level.InitWith(cameras, cameraConfiners, stateDrivenCamera);
        }

        public void ProcessPlayerDeath()
        {
            if (playerLives > 1)
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
            playerLives--;
            livesText.text = playerLives.ToString();

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
            score += pointsToAdd;
            scoreText.text = score.ToString(); 
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
    }
}