using TMPro;
using UnityEngine;

namespace Platformio
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private GameSession gameSessionPrefab;
        [SerializeField] private TextMeshProUGUI highestScoreText;
        
        void Start()
        {
            // TODO Setup the Highest Score label
            // highestScoreText.enabled = false;
        }

        public void OnStartNewGameClicked()
        {
            // TODO Pick the player appearance
            // TODO Spawn the global game-state object
            var gameSessionInstance = Instantiate(gameSessionPrefab);
        }
    }
}
