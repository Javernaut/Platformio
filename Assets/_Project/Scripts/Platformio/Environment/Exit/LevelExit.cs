using Platformio.Loop;
using UnityEngine;
using Zenject;

namespace Platformio.Environment.Exit
{
    public class LevelExit : MonoBehaviour
    {
        [Inject] private GameSession _gameSession;

        private bool _isTriggered;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && !_isTriggered)
            {
                _isTriggered = true;
                _gameSession.LoadNextLevel();
            }
        }
    }
}