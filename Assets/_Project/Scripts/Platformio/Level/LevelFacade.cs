using Cinemachine;
using UnityEngine;
using Zenject;

namespace Platformio.Level
{
    public class LevelFacade : MonoBehaviour
    {
        [SerializeField] private Collider2D cameraBoundingShape;
        [SerializeField] private Transform cameraFollowTarget;
        [SerializeField] private Animator cameraAnimatorTarget;

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
                cinemachineVirtualCamera.Follow = cameraFollowTarget;
            }
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        public class Factory : PlaceholderFactory<LevelFacade>
        {
        }
    }
}