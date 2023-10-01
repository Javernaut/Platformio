using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

namespace Platformio.Level
{
    public class LevelAnnouncement : MonoBehaviour
    {
        [Inject] private readonly int _levelNumber;

        [SerializeField] private TextMeshProUGUI textLabel;
        [SerializeField] private CanvasGroup canvasGroup;

        [SerializeField] private float visibleTime = 1.5f;
        [SerializeField] private float fadingTime = .5f;

        private IEnumerator Start()
        {
            textLabel.text = $"Level {_levelNumber}";
            yield return new WaitForSeconds(visibleTime);
            yield return Fader.FadeRoutine(canvasGroup, 0, fadingTime);
            Destroy(gameObject);
        }

        public class Factory : PlaceholderFactory<int, LevelAnnouncement>
        {
        }
    }
}