using UnityEngine;

namespace Platformio.Loop
{
    /// <summary>
    /// Aggregates icons for a control scheme that is used for interactions with the Player character.
    /// </summary>
    [CreateAssetMenu(fileName = "NewControlScheme", menuName = "HUD/Control Scheme", order = 0)]
    public class ControlScheme : ScriptableObject
    {
        [SerializeField] public Sprite jumpButton;
        [SerializeField] public Sprite fireButton;
        [SerializeField] public Sprite menuButton;
    }
}