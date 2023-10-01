using Platformio.Loop;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Platformio
{
    public class PauseMenu : MonoBehaviour
    {
        [Inject] private GameSession _gameSession;
        
        [SerializeField] private GameObject pauseMenuUI;
        [SerializeField] private InputActionAsset playerInputActionAsset;
        
        private PauseMenuInputActions _inputActions;

        private bool _isGamePaused;

        private void Awake()
        {
            _inputActions = new PauseMenuInputActions();
            _inputActions.PauseMenu.TogglePauseMenu.performed += _ => TogglePauseMenu();
            _inputActions.PauseMenu.ClosePauseMenu.performed += _ => ResumeGame();
        }

        private void OnDestroy()
        {
            playerInputActionAsset.FindActionMap("Player").Enable();
        }

        private void TogglePauseMenu()
        {
            if (_isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        private void OnEnable()
        {
            _inputActions.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Disable();
        }

        private void PauseGame()
        {
            playerInputActionAsset.FindActionMap("Player").Disable();
            Time.timeScale = 0f;
            pauseMenuUI.SetActive(true);
            _isGamePaused = true;
        }

        public void ResumeGame()
        {
            playerInputActionAsset.FindActionMap("Player").Enable();
            Time.timeScale = 1f;
            pauseMenuUI.SetActive(false);
            _isGamePaused = false;
        }

        public void QuitGame()
        {
            Time.timeScale = 1f;
            _gameSession.QuitToMainMenu();
        }
    }
}