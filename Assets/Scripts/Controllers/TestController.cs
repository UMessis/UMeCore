namespace UMeGames.Controllers
{
    using Core.Attributes;
    using Core.InputManager;
    using Core.Logger;
    using Core.Services;
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class TestController : MonoBehaviour
    {
        private const string INPUT_MOVE_NAME = "Move";

        [SerializeField] private Animator animator;
        [SerializeField, AnimatorParameter(nameof(animator))] private int testParameter;

        private void Awake()
        {
            ServiceHub.GetService<CoreInputService>().Register(INPUT_MOVE_NAME, InputCallbackType.Started, OnMoveStarted);
        }

        private void OnDestroy()
        {
            ServiceHub.GetService<CoreInputService>().Unregister(INPUT_MOVE_NAME, InputCallbackType.Started, OnMoveStarted);
        }

        private void OnMoveStarted(InputAction.CallbackContext context)
        {
            this.Log($"Move started with value: {context.ReadValue<Vector2>()}");
        }
    }
}