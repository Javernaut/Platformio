using System;
using Platformio.Loop;
using UnityEngine;

namespace Platformio
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenuUI;

        private PauseMenuInputActions _inputActions;

        private bool _isGamePaused;

        private void Awake()
        {
            _inputActions = new PauseMenuInputActions();
            _inputActions.PauseMenu.TogglePauseMenu.performed += _ => TogglePauseMenu();
            _inputActions.PauseMenu.ClosePauseMenu.performed += _ => ResumeGame();
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
            Time.timeScale = 0f;
            pauseMenuUI.SetActive(true);
            _isGamePaused = true;
        }

        public void ResumeGame()
        {
            Time.timeScale = 1f;
            pauseMenuUI.SetActive(false);
            _isGamePaused = false;
        }

        public void QuitGame()
        {
            Time.timeScale = 1f;
            // TODO Use injection here
            FindObjectOfType<GameSession>().QuitToMainMenu();
        }
    }
}