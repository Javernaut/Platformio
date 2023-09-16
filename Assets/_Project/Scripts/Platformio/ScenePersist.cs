using UnityEngine;

namespace Platformio
{
    public class ScenePersist : MonoBehaviour
    {
        void Awake()
        {
            int numScenePersists = FindObjectsOfType<ScenePersist>().Length;
            if (numScenePersists > 1)
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }
        }
        public void ResetScenePersist()
        {
            Destroy(gameObject);
        }

    }
}
