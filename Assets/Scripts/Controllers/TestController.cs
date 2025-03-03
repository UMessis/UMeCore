namespace UMeGames.Controllers
{
    using Core.InputManager;
    using Core.Logger;
    using Core.Services;
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class TestController : MonoBehaviour
    {
        private const string INPUT_MOVE_NAME = "Move";

        private void Awake()
        {
            ServiceHub.GetService<CoreInputService>().RegisterInputCallback(INPUT_MOVE_NAME, InputActionCallbackType.Started, OnMoveStarted);
        }

        private void OnDestroy()
        {
            ServiceHub.GetService<CoreInputService>().UnregisterInputCallback(INPUT_MOVE_NAME, InputActionCallbackType.Started, OnMoveStarted);
        }

        private void OnMoveStarted(InputAction.CallbackContext context)
        {
            this.Log($"Move started with value: {context.ReadValue<Vector2>()}");
        }
    }
}