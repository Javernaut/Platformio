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

        private IEnumerator Start()
        {
            textLabel.text = $"Level {_levelNumber}";
            yield return new WaitForSeconds(2);
            Destroy(gameObject);
        }

        public class Factory : PlaceholderFactory<int, LevelAnnouncement>
        {
        }
    }
}