using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformio.Environment.Exit
{
    public class LevelExit : MonoBehaviour
    {
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
            // TODO Use injection instead
            FindObjectOfType<GameSession>().LoadNextLevel();
        }
    }
}