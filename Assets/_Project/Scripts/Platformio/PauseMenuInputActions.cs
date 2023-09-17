//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/_Project/PauseMenu/PauseMenuInputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Platformio
{
    public partial class @PauseMenuInputActions: IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @PauseMenuInputActions()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""PauseMenuInputActions"",
    ""maps"": [
        {
            ""name"": ""PauseMenu"",
            ""id"": ""2145a998-dbd4-49f2-90b5-9b10d592b13b"",
            ""actions"": [
                {
                    ""name"": ""TogglePauseMenu"",
                    ""type"": ""Button"",
                    ""id"": ""cbc2f58b-8dde-4bf6-9760-54f2e9a2513a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ClosePauseMenu"",
                    ""type"": ""Button"",
                    ""id"": ""5d010fb2-ac04-4984-8215-94b30ac9045e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ebeba1a7-4d01-447f-bb5d-81ec0b3f48cf"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TogglePauseMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e7bbf9e6-6cfe-4d48-b7de-af04712e25b1"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TogglePauseMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a826cac6-502d-444f-880f-a5f4f5e1ec34"",
                    ""path"": ""<Gamepad>/{Cancel}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ClosePauseMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // PauseMenu
            m_PauseMenu = asset.FindActionMap("PauseMenu", throwIfNotFound: true);
            m_PauseMenu_TogglePauseMenu = m_PauseMenu.FindAction("TogglePauseMenu", throwIfNotFound: true);
            m_PauseMenu_ClosePauseMenu = m_PauseMenu.FindAction("ClosePauseMenu", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        public IEnumerable<InputBinding> bindings => asset.bindings;

        public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
        {
            return asset.FindAction(actionNameOrId, throwIfNotFound);
        }

        public int FindBinding(InputBinding bindingMask, out InputAction action)
        {
            return asset.FindBinding(bindingMask, out action);
        }

        // PauseMenu
        private readonly InputActionMap m_PauseMenu;
        private List<IPauseMenuActions> m_PauseMenuActionsCallbackInterfaces = new List<IPauseMenuActions>();
        private readonly InputAction m_PauseMenu_TogglePauseMenu;
        private readonly InputAction m_PauseMenu_ClosePauseMenu;
        public struct PauseMenuActions
        {
            private @PauseMenuInputActions m_Wrapper;
            public PauseMenuActions(@PauseMenuInputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @TogglePauseMenu => m_Wrapper.m_PauseMenu_TogglePauseMenu;
            public InputAction @ClosePauseMenu => m_Wrapper.m_PauseMenu_ClosePauseMenu;
            public InputActionMap Get() { return m_Wrapper.m_PauseMenu; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PauseMenuActions set) { return set.Get(); }
            public void AddCallbacks(IPauseMenuActions instance)
            {
                if (instance == null || m_Wrapper.m_PauseMenuActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_PauseMenuActionsCallbackInterfaces.Add(instance);
                @TogglePauseMenu.started += instance.OnTogglePauseMenu;
                @TogglePauseMenu.performed += instance.OnTogglePauseMenu;
                @TogglePauseMenu.canceled += instance.OnTogglePauseMenu;
                @ClosePauseMenu.started += instance.OnClosePauseMenu;
                @ClosePauseMenu.performed += instance.OnClosePauseMenu;
                @ClosePauseMenu.canceled += instance.OnClosePauseMenu;
            }

            private void UnregisterCallbacks(IPauseMenuActions instance)
            {
                @TogglePauseMenu.started -= instance.OnTogglePauseMenu;
                @TogglePauseMenu.performed -= instance.OnTogglePauseMenu;
                @TogglePauseMenu.canceled -= instance.OnTogglePauseMenu;
                @ClosePauseMenu.started -= instance.OnClosePauseMenu;
                @ClosePauseMenu.performed -= instance.OnClosePauseMenu;
                @ClosePauseMenu.canceled -= instance.OnClosePauseMenu;
            }

            public void RemoveCallbacks(IPauseMenuActions instance)
            {
                if (m_Wrapper.m_PauseMenuActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(IPauseMenuActions instance)
            {
                foreach (var item in m_Wrapper.m_PauseMenuActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_PauseMenuActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public PauseMenuActions @PauseMenu => new PauseMenuActions(this);
        public interface IPauseMenuActions
        {
            void OnTogglePauseMenu(InputAction.CallbackContext context);
            void OnClosePauseMenu(InputAction.CallbackContext context);
        }
    }
}
