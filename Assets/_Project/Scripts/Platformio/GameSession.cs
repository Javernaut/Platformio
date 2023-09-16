using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformio
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] int playerLives = 3;
    
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
        }

        void ResetGameSession()
        {
            SceneManager.LoadScene(0);
            Destroy(gameObject);
        }

    }
}