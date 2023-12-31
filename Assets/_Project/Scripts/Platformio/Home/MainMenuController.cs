using System.Collections;
using Platformio.Home.PlayerSelection;
using Platformio.Score;
using Platformio.Sound;
using Platformio.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Zenject;

namespace Platformio.Home
{
    /// <summary>
    /// The UI controller for the Main Menu.
    /// </summary>
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI highestScoreText;
        [SerializeField] private GameObject startNewGameButton;

        [Inject] private Fader _fader;
        [Inject] private MusicPlayer _musicPlayer;
        [Inject] private PlayerSelectionWindowController.Factory _playerSelectionWindowFactory;
        [Inject] private ScoreCounter _scoreCounter;

        private IEnumerator Start()
        {
            EventSystem.current.SetSelectedGameObject(startNewGameButton);
            _fader.FadeOutImmediate();
            SetupMaxScoreLabel();
            yield return _fader.FadeIn();
        }

        private void SetupMaxScoreLabel()
        {
            var lastMaxScore = _scoreCounter.MaxScore;
            if (lastMaxScore > 0)
            {
                highestScoreText.gameObject.SetActive(true);
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
            _musicPlayer.FadeOut();
            yield return _fader.FadeOut();
            SceneManager.LoadScene(1);
        }
    }
}