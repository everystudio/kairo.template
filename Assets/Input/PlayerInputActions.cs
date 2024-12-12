//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.5.1
//     from Assets/Input/PlayerInputActions.inputactions
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

public partial class @PlayerInputActions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""Idle"",
            ""id"": ""80c05377-75ac-4445-8166-dcecc1fe1a13"",
            ""actions"": [
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""888e5362-752a-4fa8-9f77-eb602a2a780d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""CursorPosition"",
                    ""type"": ""Value"",
                    ""id"": ""396b50aa-09dd-4b93-b85b-0a6df352a168"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Interaction"",
                    ""type"": ""Button"",
                    ""id"": ""86e7d954-7376-4574-bb9d-e5477a16b2b1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""90a2da2d-3b83-4c98-aa04-e69605643001"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9e623bc2-0d9d-46d6-8d70-ee9acbe144bc"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CursorPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ee6c848f-0c01-4ba0-b65f-524060e205d8"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interaction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Building"",
            ""id"": ""8dbb265c-c2ef-4f46-b7a6-d807b495ffc3"",
            ""actions"": [
                {
                    ""name"": ""CursorPosition"",
                    ""type"": ""Value"",
                    ""id"": ""ee97701a-405e-4e9c-97ea-8d80267c0400"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Build"",
                    ""type"": ""Button"",
                    ""id"": ""6cfb3527-b5bc-4c6c-ba58-8aceaa2c7c58"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""f7634178-c6f1-4300-9df3-d9ff44c1e7c4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""08e2ce25-e50a-4482-bdbf-ca4e869af2d2"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CursorPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6b68b928-0066-4419-a132-fb2a82ae8711"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Build"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6b462dbd-eaf3-47d1-b9e8-6a61a5c8186f"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Player"",
            ""id"": ""a077f846-02ae-456d-a40a-48e42d26cf4b"",
            ""actions"": [
                {
                    ""name"": ""OpenInventory"",
                    ""type"": ""Button"",
                    ""id"": ""e4668023-9398-4a47-8ece-029bd8fbe1a2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""2b486c12-0ddf-47fd-abd3-ab3e14f8ad67"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Interaction"",
                    ""type"": ""Button"",
                    ""id"": ""045b4fd4-6e5c-4c41-aad8-bf1beb122bb4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""CursorPosition"",
                    ""type"": ""Value"",
                    ""id"": ""437d79f4-c380-4ede-bc11-514ed2dfc0f1"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""CloseInventory"",
                    ""type"": ""Button"",
                    ""id"": ""d6dc505a-243b-409f-9807-aa2bb9bc13ee"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""bb719803-2f5c-4eeb-b47d-e2272e798994"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""127c75f2-d995-4110-ad93-f130dfa679fa"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""bcda0535-9509-4f41-919b-2d91cb077de2"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""b5908249-e30f-43a5-b86e-ad248c0cfe66"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""0186de52-20ea-48e1-b945-88b1b8c0009d"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""84e87388-9f9c-4e26-8336-1bc03a81ac3d"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""4e466ac3-0bca-49af-8255-5133553dbb99"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interaction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""41e3b6b1-49af-45fe-b17f-9e9586ee7414"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CursorPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1ff22fb7-9b3a-4056-9e08-d75c60842a45"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CloseInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""CameraMove"",
            ""id"": ""dfd82111-1408-44d7-8cc1-c9e4e12da3af"",
            ""actions"": [
                {
                    ""name"": ""MoveTrigger"",
                    ""type"": ""Button"",
                    ""id"": ""60bcc4dd-c73c-4e8d-9046-945e5e2717b8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""CameraMove"",
                    ""type"": ""Value"",
                    ""id"": ""84258c43-0aff-4ce3-93c5-f63670286d20"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5a86c44f-d206-481f-8c3e-758a1628ba55"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveTrigger"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e40439db-c1cb-4678-b82c-be82dce39510"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Idle
        m_Idle = asset.FindActionMap("Idle", throwIfNotFound: true);
        m_Idle_Cancel = m_Idle.FindAction("Cancel", throwIfNotFound: true);
        m_Idle_CursorPosition = m_Idle.FindAction("CursorPosition", throwIfNotFound: true);
        m_Idle_Interaction = m_Idle.FindAction("Interaction", throwIfNotFound: true);
        // Building
        m_Building = asset.FindActionMap("Building", throwIfNotFound: true);
        m_Building_CursorPosition = m_Building.FindAction("CursorPosition", throwIfNotFound: true);
        m_Building_Build = m_Building.FindAction("Build", throwIfNotFound: true);
        m_Building_Cancel = m_Building.FindAction("Cancel", throwIfNotFound: true);
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_OpenInventory = m_Player.FindAction("OpenInventory", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_Interaction = m_Player.FindAction("Interaction", throwIfNotFound: true);
        m_Player_CursorPosition = m_Player.FindAction("CursorPosition", throwIfNotFound: true);
        m_Player_CloseInventory = m_Player.FindAction("CloseInventory", throwIfNotFound: true);
        // CameraMove
        m_CameraMove = asset.FindActionMap("CameraMove", throwIfNotFound: true);
        m_CameraMove_MoveTrigger = m_CameraMove.FindAction("MoveTrigger", throwIfNotFound: true);
        m_CameraMove_CameraMove = m_CameraMove.FindAction("CameraMove", throwIfNotFound: true);
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

    // Idle
    private readonly InputActionMap m_Idle;
    private List<IIdleActions> m_IdleActionsCallbackInterfaces = new List<IIdleActions>();
    private readonly InputAction m_Idle_Cancel;
    private readonly InputAction m_Idle_CursorPosition;
    private readonly InputAction m_Idle_Interaction;
    public struct IdleActions
    {
        private @PlayerInputActions m_Wrapper;
        public IdleActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Cancel => m_Wrapper.m_Idle_Cancel;
        public InputAction @CursorPosition => m_Wrapper.m_Idle_CursorPosition;
        public InputAction @Interaction => m_Wrapper.m_Idle_Interaction;
        public InputActionMap Get() { return m_Wrapper.m_Idle; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(IdleActions set) { return set.Get(); }
        public void AddCallbacks(IIdleActions instance)
        {
            if (instance == null || m_Wrapper.m_IdleActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_IdleActionsCallbackInterfaces.Add(instance);
            @Cancel.started += instance.OnCancel;
            @Cancel.performed += instance.OnCancel;
            @Cancel.canceled += instance.OnCancel;
            @CursorPosition.started += instance.OnCursorPosition;
            @CursorPosition.performed += instance.OnCursorPosition;
            @CursorPosition.canceled += instance.OnCursorPosition;
            @Interaction.started += instance.OnInteraction;
            @Interaction.performed += instance.OnInteraction;
            @Interaction.canceled += instance.OnInteraction;
        }

        private void UnregisterCallbacks(IIdleActions instance)
        {
            @Cancel.started -= instance.OnCancel;
            @Cancel.performed -= instance.OnCancel;
            @Cancel.canceled -= instance.OnCancel;
            @CursorPosition.started -= instance.OnCursorPosition;
            @CursorPosition.performed -= instance.OnCursorPosition;
            @CursorPosition.canceled -= instance.OnCursorPosition;
            @Interaction.started -= instance.OnInteraction;
            @Interaction.performed -= instance.OnInteraction;
            @Interaction.canceled -= instance.OnInteraction;
        }

        public void RemoveCallbacks(IIdleActions instance)
        {
            if (m_Wrapper.m_IdleActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IIdleActions instance)
        {
            foreach (var item in m_Wrapper.m_IdleActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_IdleActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public IdleActions @Idle => new IdleActions(this);

    // Building
    private readonly InputActionMap m_Building;
    private List<IBuildingActions> m_BuildingActionsCallbackInterfaces = new List<IBuildingActions>();
    private readonly InputAction m_Building_CursorPosition;
    private readonly InputAction m_Building_Build;
    private readonly InputAction m_Building_Cancel;
    public struct BuildingActions
    {
        private @PlayerInputActions m_Wrapper;
        public BuildingActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @CursorPosition => m_Wrapper.m_Building_CursorPosition;
        public InputAction @Build => m_Wrapper.m_Building_Build;
        public InputAction @Cancel => m_Wrapper.m_Building_Cancel;
        public InputActionMap Get() { return m_Wrapper.m_Building; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BuildingActions set) { return set.Get(); }
        public void AddCallbacks(IBuildingActions instance)
        {
            if (instance == null || m_Wrapper.m_BuildingActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_BuildingActionsCallbackInterfaces.Add(instance);
            @CursorPosition.started += instance.OnCursorPosition;
            @CursorPosition.performed += instance.OnCursorPosition;
            @CursorPosition.canceled += instance.OnCursorPosition;
            @Build.started += instance.OnBuild;
            @Build.performed += instance.OnBuild;
            @Build.canceled += instance.OnBuild;
            @Cancel.started += instance.OnCancel;
            @Cancel.performed += instance.OnCancel;
            @Cancel.canceled += instance.OnCancel;
        }

        private void UnregisterCallbacks(IBuildingActions instance)
        {
            @CursorPosition.started -= instance.OnCursorPosition;
            @CursorPosition.performed -= instance.OnCursorPosition;
            @CursorPosition.canceled -= instance.OnCursorPosition;
            @Build.started -= instance.OnBuild;
            @Build.performed -= instance.OnBuild;
            @Build.canceled -= instance.OnBuild;
            @Cancel.started -= instance.OnCancel;
            @Cancel.performed -= instance.OnCancel;
            @Cancel.canceled -= instance.OnCancel;
        }

        public void RemoveCallbacks(IBuildingActions instance)
        {
            if (m_Wrapper.m_BuildingActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IBuildingActions instance)
        {
            foreach (var item in m_Wrapper.m_BuildingActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_BuildingActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public BuildingActions @Building => new BuildingActions(this);

    // Player
    private readonly InputActionMap m_Player;
    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
    private readonly InputAction m_Player_OpenInventory;
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_Interaction;
    private readonly InputAction m_Player_CursorPosition;
    private readonly InputAction m_Player_CloseInventory;
    public struct PlayerActions
    {
        private @PlayerInputActions m_Wrapper;
        public PlayerActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @OpenInventory => m_Wrapper.m_Player_OpenInventory;
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @Interaction => m_Wrapper.m_Player_Interaction;
        public InputAction @CursorPosition => m_Wrapper.m_Player_CursorPosition;
        public InputAction @CloseInventory => m_Wrapper.m_Player_CloseInventory;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @OpenInventory.started += instance.OnOpenInventory;
            @OpenInventory.performed += instance.OnOpenInventory;
            @OpenInventory.canceled += instance.OnOpenInventory;
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @Interaction.started += instance.OnInteraction;
            @Interaction.performed += instance.OnInteraction;
            @Interaction.canceled += instance.OnInteraction;
            @CursorPosition.started += instance.OnCursorPosition;
            @CursorPosition.performed += instance.OnCursorPosition;
            @CursorPosition.canceled += instance.OnCursorPosition;
            @CloseInventory.started += instance.OnCloseInventory;
            @CloseInventory.performed += instance.OnCloseInventory;
            @CloseInventory.canceled += instance.OnCloseInventory;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @OpenInventory.started -= instance.OnOpenInventory;
            @OpenInventory.performed -= instance.OnOpenInventory;
            @OpenInventory.canceled -= instance.OnOpenInventory;
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @Interaction.started -= instance.OnInteraction;
            @Interaction.performed -= instance.OnInteraction;
            @Interaction.canceled -= instance.OnInteraction;
            @CursorPosition.started -= instance.OnCursorPosition;
            @CursorPosition.performed -= instance.OnCursorPosition;
            @CursorPosition.canceled -= instance.OnCursorPosition;
            @CloseInventory.started -= instance.OnCloseInventory;
            @CloseInventory.performed -= instance.OnCloseInventory;
            @CloseInventory.canceled -= instance.OnCloseInventory;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // CameraMove
    private readonly InputActionMap m_CameraMove;
    private List<ICameraMoveActions> m_CameraMoveActionsCallbackInterfaces = new List<ICameraMoveActions>();
    private readonly InputAction m_CameraMove_MoveTrigger;
    private readonly InputAction m_CameraMove_CameraMove;
    public struct CameraMoveActions
    {
        private @PlayerInputActions m_Wrapper;
        public CameraMoveActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveTrigger => m_Wrapper.m_CameraMove_MoveTrigger;
        public InputAction @CameraMove => m_Wrapper.m_CameraMove_CameraMove;
        public InputActionMap Get() { return m_Wrapper.m_CameraMove; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraMoveActions set) { return set.Get(); }
        public void AddCallbacks(ICameraMoveActions instance)
        {
            if (instance == null || m_Wrapper.m_CameraMoveActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_CameraMoveActionsCallbackInterfaces.Add(instance);
            @MoveTrigger.started += instance.OnMoveTrigger;
            @MoveTrigger.performed += instance.OnMoveTrigger;
            @MoveTrigger.canceled += instance.OnMoveTrigger;
            @CameraMove.started += instance.OnCameraMove;
            @CameraMove.performed += instance.OnCameraMove;
            @CameraMove.canceled += instance.OnCameraMove;
        }

        private void UnregisterCallbacks(ICameraMoveActions instance)
        {
            @MoveTrigger.started -= instance.OnMoveTrigger;
            @MoveTrigger.performed -= instance.OnMoveTrigger;
            @MoveTrigger.canceled -= instance.OnMoveTrigger;
            @CameraMove.started -= instance.OnCameraMove;
            @CameraMove.performed -= instance.OnCameraMove;
            @CameraMove.canceled -= instance.OnCameraMove;
        }

        public void RemoveCallbacks(ICameraMoveActions instance)
        {
            if (m_Wrapper.m_CameraMoveActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ICameraMoveActions instance)
        {
            foreach (var item in m_Wrapper.m_CameraMoveActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_CameraMoveActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public CameraMoveActions @CameraMove => new CameraMoveActions(this);
    public interface IIdleActions
    {
        void OnCancel(InputAction.CallbackContext context);
        void OnCursorPosition(InputAction.CallbackContext context);
        void OnInteraction(InputAction.CallbackContext context);
    }
    public interface IBuildingActions
    {
        void OnCursorPosition(InputAction.CallbackContext context);
        void OnBuild(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
    }
    public interface IPlayerActions
    {
        void OnOpenInventory(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnInteraction(InputAction.CallbackContext context);
        void OnCursorPosition(InputAction.CallbackContext context);
        void OnCloseInventory(InputAction.CallbackContext context);
    }
    public interface ICameraMoveActions
    {
        void OnMoveTrigger(InputAction.CallbackContext context);
        void OnCameraMove(InputAction.CallbackContext context);
    }
}
