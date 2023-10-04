using Platformio.Loop;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Zenject;

namespace Platformio
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenuUI;
        [SerializeField] private GameObject resumeButton;
        [Inject] private GameSession _gameSession;
        [Inject] private InputActionAsset _globalInputActionAsset;

        private bool _isGamePaused;

        private void Awake()
        {
            // Assume we are the only piece of UI on the screen and nothing can override us
            _globalInputActionAsset["UI/Cancel"].performed += onCancelActionPerformed;
        }

        private void OnDestroy()
        {
            _globalInputActionAsset["UI/Cancel"].performed -= onCancelActionPerformed;
            SetPlayerActionMapEnabled(true);
        }

        private void onCancelActionPerformed(InputAction.CallbackContext _)
        {
            TogglePauseMenu();
        }

        private void TogglePauseMenu()
        {
            if (_isGamePaused)
                ResumeGame();
            else
                PauseGame();
        }

        private void PauseGame()
        {
            Time.timeScale = 0f;
            pauseMenuUI.SetActive(true);
            _isGamePaused = true;
            SetPlayerActionMapEnabled(false);
            EventSystem.current.SetSelectedGameObject(resumeButton);
        }

        public void ResumeGame()
        {
            Time.timeScale = 1f;
            pauseMenuUI.SetActive(false);
            _isGamePaused = false;
            SetPlayerActionMapEnabled(true);
        }

        public void QuitGame()
        {
            // TODO Consider wrapping the time scale access
            Time.timeScale = 1f;
            _gameSession.QuitToMainMenu();
        }

        private void SetPlayerActionMapEnabled(bool isEnabled)
        {
            var playerActionMap = _globalInputActionAsset.FindActionMap("Player");
            if (isEnabled)
                playerActionMap.Enable();
            else
                playerActionMap.Disable();
        }
    }
}