using System.Collections;
using Platformio.UI;
using TMPro;
using UnityEngine;
using Zenject;

namespace Platformio.Level
{
    /// <summary>
    /// Script for announcing the level by its number. Fades out and destroys itself after a delay.
    /// </summary>
    public class LevelAnnouncement : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textLabel;
        [SerializeField] private CanvasGroup canvasGroup;

        [SerializeField] private float visibleTime = 1.5f;
        [SerializeField] private float fadingTime = .5f;
        [Inject] private readonly int _levelNumber;

        private IEnumerator Start()
        {
            textLabel.text = $"Level\n{_levelNumber}";
            yield return new WaitForSeconds(visibleTime);
            yield return Fader.FadeRoutine(canvasGroup, 0, fadingTime);
            Destroy(gameObject);
        }

        public class Factory : PlaceholderFactory<int, LevelAnnouncement>
        {
        }
    }
}