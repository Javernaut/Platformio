using System.Collections;
using Platformio.Home.PlayerSelection;
using Platformio.Score;
using Platformio.Sound;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Zenject;

namespace Platformio.Home
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI highestScoreText;
        [SerializeField] private GameObject startNewGameButton;

        [Inject] private Fader _fader;
        [Inject] private ScoreCounter _scoreCounter;
        [Inject] private PlayerSelectionWindowController.Factory _playerSelectionWindowFactory;
        [Inject] private MusicPlayer _musicPlayer;

        private IEnumerator Start()
        {
            EventSystem.current.SetSelectedGameObject(startNewGameButton);
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
            // TODO Extract the fade out/in settings
            _musicPlayer.FadeOut(1);
            yield return _fader.FadeOut(1);
            SceneManager.LoadScene(1);
        }
    }
}