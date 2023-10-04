using UnityEngine;

namespace Platformio.Character
{
    /// <summary>
    /// Aggregates a player avatar (that is shown along with the 'lives' counter)
    /// and the animation controller for various states of the player character.
    /// </summary>
    [CreateAssetMenu(fileName = "NewPlayerAppearance", menuName = "Theming/Player Appearance", order = 0)]
    public class PlayerAppearance : ScriptableObject
    {
        [SerializeField] private Sprite avatar;
        [SerializeField] private RuntimeAnimatorController animatorController;

        public Sprite Avatar => avatar;
        public RuntimeAnimatorController AnimatorController => animatorController;
    }
}