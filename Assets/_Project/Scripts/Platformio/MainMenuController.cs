using System.Collections;
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

        private IEnumerator Start()
        {
            _fader.FadeOutImmediate();
            yield return _fader.FadeIn(1);
            // TODO Setup the Highest Score label
            // highestScoreText.enabled = false;
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