using UnityEngine.UI;

namespace Platformio
{
    public class SelectionStateExposingButton : Button
    {
        public delegate void NewPublicSelectionStateHandler(PublicSelectionState newState);

        public PublicSelectionState CurrentPublicSelectionState => From(currentSelectionState);

        public event NewPublicSelectionStateHandler OnNewPublicSelectionState;

        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            base.DoStateTransition(state, instant);
            OnNewPublicSelectionState?.Invoke(CurrentPublicSelectionState);
        }

        private static PublicSelectionState From(SelectionState state)
        {
            return state switch
            {
                SelectionState.Disabled => PublicSelectionState.Disabled,
                SelectionState.Highlighted => PublicSelectionState.Highlighted,
                SelectionState.Pressed => PublicSelectionState.Pressed,
                SelectionState.Selected => PublicSelectionState.Selected,
                // The rest is treated as Normal
                _ => PublicSelectionState.Normal
            };
        }
    }

    public enum PublicSelectionState
    {
        Normal,
        Highlighted,
        Pressed,
        Selected,
        Disabled
    }
}