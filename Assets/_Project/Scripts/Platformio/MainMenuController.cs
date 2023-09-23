using System.Collections;
using Platformio.Pickup;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Platformio
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI highestScoreText;

        [Inject] private Fader _fader;
        [Inject] private ScoreCounter _scoreCounter;

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
            StartCoroutine(StartNewGameRoutine());
        }

        private IEnumerator StartNewGameRoutine()
        {
            yield return _fader.FadeOut(1);
            // TODO Pick the player appearance
            SceneManager.LoadScene(1);
        }
    }
}