using Platformio.Character;

namespace Platformio.Home.PlayerSelection
{
    /// <summary>
    /// Keeps track of the last chosen <see cref="PlayerAppearance"/> during the Player Selection Window.
    /// </summary>
    public class PlayerAppearanceChoiceKeeper
    {
        private PlayerAppearance _playerAppearance;

        public void SetChoice(PlayerAppearance playerAppearance)
        {
            _playerAppearance = playerAppearance;
        }

        public PlayerAppearance GetChoice()
        {
            return _playerAppearance;
        }
    }
}