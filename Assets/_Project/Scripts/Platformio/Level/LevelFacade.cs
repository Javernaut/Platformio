using Cinemachine;
using Platformio.Player;
using Platformio.Sound;
using UnityEngine;
using Zenject;

namespace Platformio.Level
{
    public class LevelFacade : MonoBehaviour
    {
        [Inject] private StepSoundPlayer _stepSoundPlayer;
        
        [SerializeField] private Collider2D cameraBoundingShape;
        [SerializeField] private Transform startPosition;

        private PlayerController _playerController;

        public void InitWith(CinemachineConfiner2D[] cameraConfiners, PlayerController controller)
        {
            foreach (var confiner in cameraConfiners)
            {
                confiner.m_BoundingShape2D = cameraBoundingShape;
            }

            _playerController = controller;
            _playerController.transform.position = startPosition.position;
            _playerController.transform.localScale = startPosition.localScale;
            _playerController.OnStepMade = () => _stepSoundPlayer.PlayStepSound();
        }

        public void Destroy()
        {
            Destroy(gameObject);
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