namespace UMeGames
{
    using Core.Singleton;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.InputSystem;

    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(EventSystem))]
    public class InputManager : MonoSingleton<InputManager>
    {
        [SerializeField] private InputActionAsset inputActions;

        private IInputDispatcher dispatcher;

        private void OnDestroy()
        {
            foreach (InputActionMap actionMap in inputActions.actionMaps)
            {
                foreach (InputAction action in actionMap.actions)
                {
                    action.started -= OnActionStarted;
                    action.performed -= OnActionPerformed;
                    action.canceled -= OnActionCanceled;
                }
            }
        }

        public void Setup(IInputDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;

            foreach (InputActionMap actionMap in inputActions.actionMaps)
            {
                foreach (InputAction action in actionMap.actions)
                {
                    action.started += OnActionStarted;
                    action.performed += OnActionPerformed;
                    action.canceled += OnActionCanceled;
                }
            }
        }

        private void OnActionStarted(InputAction.CallbackContext context)
        {

        }

        private void OnActionPerformed(InputAction.CallbackContext context)
        {

        }

        private void OnActionCanceled(InputAction.CallbackContext context)
        {

        }
    }
}