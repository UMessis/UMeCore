namespace UMeGames.Core.InputManager
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Logger;
    using Services;
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class CoreInputService : IService
    {
        public List<Type> Dependencies => null;

        public IEnumerator Initialize(List<IService> dependencies)
        {
            yield break;
        }

        public void Dispose()
        {
        }

        public void Register(string actionName, InputCallbackType callbackType, Action<InputAction.CallbackContext> callback)
        {
            InputAction action = InputSystem.actions.FindAction(actionName);

            if (action == null)
            {
                this.LogError($"Action [{actionName}] not found in any action map");
                return;
            }

            switch (callbackType)
            {
                case InputCallbackType.Started:
                    action.started += callback;
                    break;
                case InputCallbackType.Performed:
                    action.performed += callback;
                    break;
                case InputCallbackType.Canceled:
                    action.canceled += callback;
                    break;
                default:
                    this.LogError($"Unknown action callback type: {callbackType}");
                    break;
            }
        }

        public void Unregister(string actionName, InputCallbackType callbackType, Action<InputAction.CallbackContext> callback)
        {
            InputAction action = InputSystem.actions.FindAction(actionName);

            if (action == null)
            {
                this.LogError($"Action [{actionName}] not found in any action map");
                return;
            }

            switch (callbackType)
            {
                case InputCallbackType.Started:
                    action.started -= callback;
                    break;
                case InputCallbackType.Performed:
                    action.performed -= callback;
                    break;
                case InputCallbackType.Canceled:
                    action.canceled -= callback;
                    break;
                default:
                    this.LogError($"Unknown action callback type: {callbackType}");
                    break;
            }
        }

        public static void SetCursor(CursorLockMode lockMode, bool visible)
        {
            Cursor.lockState = lockMode;
            Cursor.visible = visible;
        }
    }
}