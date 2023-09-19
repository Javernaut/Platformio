using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformio
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI highestScoreText;
        
        void Start()
        {
            // TODO Setup the Highest Score label
            // highestScoreText.enabled = false;
        }

        public void OnStartNewGameClicked()
        {
            // TODO Pick the player appearance
            SceneManager.LoadScene(1);
        }
    }
}
