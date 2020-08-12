// GENERATED AUTOMATICALLY FROM 'Assets/Code/Player/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Basic"",
            ""id"": ""585adb3d-6164-4043-b2c5-12e60353829f"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""efad28d2-45cd-4f15-8586-df95e297d7ec"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""655795ae-f270-4cf5-9a21-a4783170e86b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""1e8d9a1e-c5e9-4f39-b895-c366a5bad909"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChangeDirection"",
                    ""type"": ""Value"",
                    ""id"": ""7b0b983e-75d2-494e-86e0-f459876c5d9e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f8434464-0bc4-43e1-8171-454f78ac2713"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1b70ddc6-b8b8-4b1f-8cac-8c7915dd3594"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6b23fd28-4409-4e55-bdcd-9994533e09ad"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d9b47758-aebd-4f10-8ae6-7fc5003bac80"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""UDLR"",
                    ""id"": ""fd5ae19a-c48d-474c-9f92-e1b2ac961d20"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""6adc0df8-3eb2-488c-95da-b29de496b3a8"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""72a7c0e9-ea0e-4bef-8d05-f53339ad297a"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""d509606a-bdec-43b9-a9df-05b4a182f0cb"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""aad8165f-7a2c-470b-b22f-0e61bd114fcf"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""DPad"",
                    ""id"": ""f012ffc4-6ca7-4696-96e4-fb0c3c4e2e4d"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""b99a4729-8f78-4121-87b2-5ddece331abd"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""afd2b598-d527-4ce5-8096-df8db1f5f78a"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""30997e3c-70d2-4d4c-9261-0ce585d1cb18"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""82402ca4-498f-4439-9551-97ec758387f7"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""LR"",
                    ""id"": ""b3eb5e87-a967-40f0-8843-74a719887693"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeDirection"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""294a804f-1d90-4722-876d-721a181fdaa6"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""ChangeDirection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""09799bf6-4763-4fad-8fd7-758a230a49c2"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""ChangeDirection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""a25b0ac0-5e4c-4eec-9566-c75340e12cf6"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeDirection"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""8db11527-2f0b-45ef-9f05-bbeaa9af9100"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ChangeDirection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""df39fa86-32ce-48b3-8416-76cf5ace3186"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ChangeDirection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Flying"",
            ""id"": ""bb784b2f-a723-4b70-b318-6dbee6c58e9d"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""e5071a7b-5bde-4d94-8323-a5741c6252d5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""UD"",
                    ""id"": ""e71828e3-3fc5-4ffe-aaaf-6d2feeb6d5fe"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Negative"",
                    ""id"": ""66e2a938-9de4-40cf-8738-38224455b629"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Positive"",
                    ""id"": ""860d8dfe-9493-4bc1-bffc-d4fd3f81cd29"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Swimming"",
            ""id"": ""9b4b3a4d-2002-40d9-959a-0b2c44ed189b"",
            ""actions"": [],
            ""bindings"": []
        },
        {
            ""name"": ""World"",
            ""id"": ""b63500e0-04a5-4ea3-8abe-6daefe049726"",
            ""actions"": [
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""d09dc682-c3b4-4831-9da4-a65999f3697a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PickDirection"",
                    ""type"": ""Button"",
                    ""id"": ""0f3a91c7-05b7-4586-a25b-57dd8e034d41"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e775faad-f99b-47de-862b-7c76c89d937d"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""73971538-f1b2-4e02-a789-d3e3ff75d45f"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PickDirection"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""77673cf6-a4c1-42ef-8b9b-94db49c79b1d"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""PickDirection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""d59b03f0-80c5-4b62-ba72-7fb21feae13b"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""PickDirection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""90411f10-5ed8-4af1-bbf7-9a27eb55e26a"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""PickDirection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""c04aa820-dd95-40d0-890e-59a8f0939319"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""PickDirection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""7995acff-a01a-4c7c-bc76-82cbc61a0520"",
            ""actions"": [
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""5209a13d-f311-486d-a9cb-4fe2bd4cb4fa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Navigate"",
                    ""type"": ""Button"",
                    ""id"": ""a7ff9c63-3f55-4df1-adbf-60c0ec3f0eab"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""023d33f3-e713-44a6-9f68-fc6860770400"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""928bebf5-d53e-4b0c-b931-60f51a7be88c"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""NP"",
                    ""id"": ""96d8492b-8d71-4671-a244-20628eb02d60"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""b8901ff5-5987-4f8d-b69e-3df3887a306d"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""c53059c6-a2ba-4ee0-b890-cf216a9fdda4"",
                    ""path"": ""<DualShockGamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Basic
        m_Basic = asset.FindActionMap("Basic", throwIfNotFound: true);
        m_Basic_Jump = m_Basic.FindAction("Jump", throwIfNotFound: true);
        m_Basic_Run = m_Basic.FindAction("Run", throwIfNotFound: true);
        m_Basic_Move = m_Basic.FindAction("Move", throwIfNotFound: true);
        m_Basic_ChangeDirection = m_Basic.FindAction("ChangeDirection", throwIfNotFound: true);
        // Flying
        m_Flying = asset.FindActionMap("Flying", throwIfNotFound: true);
        m_Flying_Move = m_Flying.FindAction("Move", throwIfNotFound: true);
        // Swimming
        m_Swimming = asset.FindActionMap("Swimming", throwIfNotFound: true);
        // World
        m_World = asset.FindActionMap("World", throwIfNotFound: true);
        m_World_Select = m_World.FindAction("Select", throwIfNotFound: true);
        m_World_PickDirection = m_World.FindAction("PickDirection", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_Select = m_UI.FindAction("Select", throwIfNotFound: true);
        m_UI_Navigate = m_UI.FindAction("Navigate", throwIfNotFound: true);
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

    // Basic
    private readonly InputActionMap m_Basic;
    private IBasicActions m_BasicActionsCallbackInterface;
    private readonly InputAction m_Basic_Jump;
    private readonly InputAction m_Basic_Run;
    private readonly InputAction m_Basic_Move;
    private readonly InputAction m_Basic_ChangeDirection;
    public struct BasicActions
    {
        private @PlayerControls m_Wrapper;
        public BasicActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_Basic_Jump;
        public InputAction @Run => m_Wrapper.m_Basic_Run;
        public InputAction @Move => m_Wrapper.m_Basic_Move;
        public InputAction @ChangeDirection => m_Wrapper.m_Basic_ChangeDirection;
        public InputActionMap Get() { return m_Wrapper.m_Basic; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BasicActions set) { return set.Get(); }
        public void SetCallbacks(IBasicActions instance)
        {
            if (m_Wrapper.m_BasicActionsCallbackInterface != null)
            {
                @Jump.started -= m_Wrapper.m_BasicActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_BasicActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_BasicActionsCallbackInterface.OnJump;
                @Run.started -= m_Wrapper.m_BasicActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_BasicActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_BasicActionsCallbackInterface.OnRun;
                @Move.started -= m_Wrapper.m_BasicActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_BasicActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_BasicActionsCallbackInterface.OnMove;
                @ChangeDirection.started -= m_Wrapper.m_BasicActionsCallbackInterface.OnChangeDirection;
                @ChangeDirection.performed -= m_Wrapper.m_BasicActionsCallbackInterface.OnChangeDirection;
                @ChangeDirection.canceled -= m_Wrapper.m_BasicActionsCallbackInterface.OnChangeDirection;
            }
            m_Wrapper.m_BasicActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @ChangeDirection.started += instance.OnChangeDirection;
                @ChangeDirection.performed += instance.OnChangeDirection;
                @ChangeDirection.canceled += instance.OnChangeDirection;
            }
        }
    }
    public BasicActions @Basic => new BasicActions(this);

    // Flying
    private readonly InputActionMap m_Flying;
    private IFlyingActions m_FlyingActionsCallbackInterface;
    private readonly InputAction m_Flying_Move;
    public struct FlyingActions
    {
        private @PlayerControls m_Wrapper;
        public FlyingActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Flying_Move;
        public InputActionMap Get() { return m_Wrapper.m_Flying; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(FlyingActions set) { return set.Get(); }
        public void SetCallbacks(IFlyingActions instance)
        {
            if (m_Wrapper.m_FlyingActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_FlyingActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_FlyingActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_FlyingActionsCallbackInterface.OnMove;
            }
            m_Wrapper.m_FlyingActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
            }
        }
    }
    public FlyingActions @Flying => new FlyingActions(this);

    // Swimming
    private readonly InputActionMap m_Swimming;
    private ISwimmingActions m_SwimmingActionsCallbackInterface;
    public struct SwimmingActions
    {
        private @PlayerControls m_Wrapper;
        public SwimmingActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputActionMap Get() { return m_Wrapper.m_Swimming; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SwimmingActions set) { return set.Get(); }
        public void SetCallbacks(ISwimmingActions instance)
        {
            if (m_Wrapper.m_SwimmingActionsCallbackInterface != null)
            {
            }
            m_Wrapper.m_SwimmingActionsCallbackInterface = instance;
            if (instance != null)
            {
            }
        }
    }
    public SwimmingActions @Swimming => new SwimmingActions(this);

    // World
    private readonly InputActionMap m_World;
    private IWorldActions m_WorldActionsCallbackInterface;
    private readonly InputAction m_World_Select;
    private readonly InputAction m_World_PickDirection;
    public struct WorldActions
    {
        private @PlayerControls m_Wrapper;
        public WorldActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Select => m_Wrapper.m_World_Select;
        public InputAction @PickDirection => m_Wrapper.m_World_PickDirection;
        public InputActionMap Get() { return m_Wrapper.m_World; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(WorldActions set) { return set.Get(); }
        public void SetCallbacks(IWorldActions instance)
        {
            if (m_Wrapper.m_WorldActionsCallbackInterface != null)
            {
                @Select.started -= m_Wrapper.m_WorldActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_WorldActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_WorldActionsCallbackInterface.OnSelect;
                @PickDirection.started -= m_Wrapper.m_WorldActionsCallbackInterface.OnPickDirection;
                @PickDirection.performed -= m_Wrapper.m_WorldActionsCallbackInterface.OnPickDirection;
                @PickDirection.canceled -= m_Wrapper.m_WorldActionsCallbackInterface.OnPickDirection;
            }
            m_Wrapper.m_WorldActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
                @PickDirection.started += instance.OnPickDirection;
                @PickDirection.performed += instance.OnPickDirection;
                @PickDirection.canceled += instance.OnPickDirection;
            }
        }
    }
    public WorldActions @World => new WorldActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_Select;
    private readonly InputAction m_UI_Navigate;
    public struct UIActions
    {
        private @PlayerControls m_Wrapper;
        public UIActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Select => m_Wrapper.m_UI_Select;
        public InputAction @Navigate => m_Wrapper.m_UI_Navigate;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @Select.started -= m_Wrapper.m_UIActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnSelect;
                @Navigate.started -= m_Wrapper.m_UIActionsCallbackInterface.OnNavigate;
                @Navigate.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnNavigate;
                @Navigate.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnNavigate;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
                @Navigate.started += instance.OnNavigate;
                @Navigate.performed += instance.OnNavigate;
                @Navigate.canceled += instance.OnNavigate;
            }
        }
    }
    public UIActions @UI => new UIActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IBasicActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnChangeDirection(InputAction.CallbackContext context);
    }
    public interface IFlyingActions
    {
        void OnMove(InputAction.CallbackContext context);
    }
    public interface ISwimmingActions
    {
    }
    public interface IWorldActions
    {
        void OnSelect(InputAction.CallbackContext context);
        void OnPickDirection(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnSelect(InputAction.CallbackContext context);
        void OnNavigate(InputAction.CallbackContext context);
    }
}
