using System.Collections;
using Platformio.Loop;
using UnityEngine;
using Zenject;

namespace Platformio.Environment.Exit
{
    public class LevelExit : MonoBehaviour
    {
        [Inject] private GameSession _gameSession;
        
        [SerializeField] private float levelLoadDelay = 1f;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(LoadNextLevel());
            }
        }

        private IEnumerator LoadNextLevel()
        {
            yield return new WaitForSecondsRealtime(levelLoadDelay);
            // FindObjectOfType<ScenePersist>().ResetScenePersist();
            _gameSession.LoadNextLevel();
        }
    }
}