using UnityEngine;

namespace Platformio.Player
{
    // TODO Make better menu item settings
    [CreateAssetMenu]
    public class PlayerAppearance : ScriptableObject
    {
        [SerializeField] private Sprite avatar;
        [SerializeField] private RuntimeAnimatorController animatorController;

        public Sprite Avatar => avatar;
        public RuntimeAnimatorController AnimatorController => animatorController;
    }
}