using System.Collections;
using Platformio.Home.PlayerSelection;
using Platformio.Pickup;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Platformio.Home
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI highestScoreText;

        [Inject] private Fader _fader;
        [Inject] private ScoreCounter _scoreCounter;
        [Inject] private PlayerSelectionWindowController.Factory _playerSelectionWindowFactory;

        private IEnumerator Start()
        {
            _fader.FadeOutImmediate();
            SetupMaxScoreLabel();
            yield return _fader.FadeIn(1);
        }

        private void SetupMaxScoreLabel()
        {
            var lastMaxScore = _scoreCounter.MaxScore;
            if (lastMaxScore > 0)
            {
                highestScoreText.gameObject.SetActive(true);
                // TODO Consider basic localization mechanism
                highestScoreText.text = $"Highest Score:\n{lastMaxScore}";
            }
        }

        public void OnStartNewGameClicked()
        {
            var playerSelectionWindow = _playerSelectionWindowFactory.Create();
            playerSelectionWindow.OnSelectionConfirmed +=
                () => StartCoroutine(StartNewGameRoutine());
        }

        private IEnumerator StartNewGameRoutine()
        {
            yield return _fader.FadeOut(1);
            SceneManager.LoadScene(1);
        }
    }
}