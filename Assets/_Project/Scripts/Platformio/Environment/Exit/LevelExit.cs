using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformio.Environment.Exit
{
    public class LevelExit : MonoBehaviour
    {
        [SerializeField] float levelLoadDelay = 1f;
    
        void OnTriggerEnter2D(Collider2D other) 
        {
            StartCoroutine(LoadNextLevel());
        }

        IEnumerator LoadNextLevel()
        {
            yield return new WaitForSecondsRealtime(levelLoadDelay);
            var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            var nextSceneIndex = currentSceneIndex + 1;

            if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            {
                nextSceneIndex = 0;
            }

            SceneManager.LoadScene(nextSceneIndex);
        }

    }
}