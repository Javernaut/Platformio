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
        
        void Awake()
        {
            int numGameSessions = FindObjectsOfType<GameSession>().Length;
            if (numGameSessions > 1)
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }
        }
        
        void Start() 
        {
            livesText.text = playerLives.ToString();
            scoreText.text = score.ToString();
            SceneManager.LoadScene(1);
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
            FindObjectOfType<ScenePersist>().ResetScenePersist();
            SceneManager.LoadScene(0);
            Destroy(gameObject);
        }
        
        public void AddToScore(int pointsToAdd)
        {
            score += pointsToAdd;
            scoreText.text = score.ToString(); 
        }
    }
}