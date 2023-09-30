using Cinemachine;
using Platformio.Player;
using UnityEngine;
using Zenject;

namespace Platformio.Level
{
    public class LevelFacade : MonoBehaviour
    {
        [SerializeField] private Collider2D cameraBoundingShape;
        [SerializeField] private Animator cameraAnimatorTarget;
        [SerializeField] private PlayerController playerController; 

        public void InitWith(
            CinemachineVirtualCamera[] cinemachineVirtualCameras,
            CinemachineConfiner2D[] cameraConfiners,
            CinemachineStateDrivenCamera camera)
        {
            camera.m_AnimatedTarget = cameraAnimatorTarget;
            foreach (var confiner in cameraConfiners)
            {
                confiner.m_BoundingShape2D = cameraBoundingShape;
            }

            foreach (var cinemachineVirtualCamera in cinemachineVirtualCameras)
            {
                cinemachineVirtualCamera.Follow = playerController.transform;
            }
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        public void Reload()
        {
            playerController.Reload();
        }

        public class Factory : PlaceholderFactory<LevelFacade>
        {
        }
    }
}