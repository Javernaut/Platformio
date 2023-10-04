using Platformio.Character;
using Platformio.Environment;
using Platformio.Sound;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Platformio.Level
{
    public class LevelFacade : MonoBehaviour
    {
        [SerializeField] private Collider2D cameraBoundingShape;
        [SerializeField] private Transform startPosition;
        private CinemachineConfiner2D[] _cameraConfiners;

        private PlayerController _playerController;
        [Inject] private StepSoundPlayer _stepSoundPlayer;
        [Inject] private EnvironmentThemeConfiguration _theme;

        public void InitWith(
            Image backgroundImage,
            CinemachineConfiner2D[] cameraConfiners,
            PlayerController controller)
        {
            backgroundImage.sprite = _theme.background;
            _cameraConfiners = cameraConfiners;
            UpdateConfiners(cameraBoundingShape);

            _playerController = controller;
            _playerController.transform.position = startPosition.position;
            _playerController.transform.localScale = startPosition.localScale;
            _playerController.OnStepMade = () => _stepSoundPlayer.PlayStepSound();
        }

        public void Destroy()
        {
            UpdateConfiners(null);
            Destroy(gameObject);
        }

        private void UpdateConfiners(Collider2D confinerCollider2D)
        {
            foreach (var confiner in _cameraConfiners)
            {
                confiner.BoundingShape2D = confinerCollider2D;
                confiner.InvalidateBoundingShapeCache();
            }
        }

        public void Reload()
        {
            _playerController.Reload(startPosition.position, startPosition.localScale);
        }

        public class Factory : PlaceholderFactory<LevelFacade>
        {
        }
    }
}