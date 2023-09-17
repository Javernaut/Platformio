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
            var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            var nextSceneIndex = currentSceneIndex + 1;

            if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            {
                nextSceneIndex = 1;
            }

            FindObjectOfType<ScenePersist>().ResetScenePersist();
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}