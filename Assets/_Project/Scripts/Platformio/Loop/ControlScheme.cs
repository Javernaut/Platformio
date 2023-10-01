using UnityEngine;

namespace Platformio.Loop
{
    [CreateAssetMenu(fileName = "NewControlScheme", menuName = "HUD/New Control Scheme", order = 0)]
    public class ControlScheme : ScriptableObject
    {
        [SerializeField] public Sprite jumpButton;
        [SerializeField] public Sprite fireButton;
        [SerializeField] public Sprite menuButton;
    }
}