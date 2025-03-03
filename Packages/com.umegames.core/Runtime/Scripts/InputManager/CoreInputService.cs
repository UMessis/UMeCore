namespace UMeGames.Core.InputManager
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Logger;
    using Services;
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

        public void RegisterInputCallback(string actionName, InputActionCallbackType actionCallbackType, Action<InputAction.CallbackContext> callback)
        {
            InputAction action = InputSystem.actions.FindAction(actionName);
            switch (actionCallbackType)
            {
                case InputActionCallbackType.Started:
                    this.Log("Registered callback to started action");
                    action.started += callback;
                    break;
                case InputActionCallbackType.Performed:
                    action.performed += callback;
                    break;
                case InputActionCallbackType.Canceled:
                    action.canceled += callback;
                    break;
                default:
                    this.LogError($"Unknown action callback type: {actionCallbackType}");
                    break;
            }
        }

        public void UnregisterInputCallback(string actionName, InputActionCallbackType actionCallbackType, Action<InputAction.CallbackContext> callback)
        {
            InputAction action = InputSystem.actions.FindAction(actionName);
            switch (actionCallbackType)
            {
                case InputActionCallbackType.Started:
                    action.started -= callback;
                    break;
                case InputActionCallbackType.Performed:
                    action.performed -= callback;
                    break;
                case InputActionCallbackType.Canceled:
                    action.canceled -= callback;
                    break;
                default:
                    this.LogError($"Unknown action callback type: {actionCallbackType}");
                    break;
            }
        }
    }
}