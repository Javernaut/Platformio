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
                ResetGameSession();
            }
        }

        void TakeLife()
        {
            playerLives--;
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
            livesText.text = playerLives.ToString();
        }

        void ResetGameSession()
        {
            // TODO Clear all references, reset to default. Or just respawn the whole prefab
            // FindObjectOfType<ScenePersist>().ResetScenePersist();
            // SceneManager.LoadScene(0);
            // Destroy(gameObject);
        }
        
        public void AddToScore(int pointsToAdd)
        {
            score += pointsToAdd;
            scoreText.text = score.ToString(); 
        }
    }
}