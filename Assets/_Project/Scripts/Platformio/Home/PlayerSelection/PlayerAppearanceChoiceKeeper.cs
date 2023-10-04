using Platformio.Player;

namespace Platformio.Home.PlayerSelection
{
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