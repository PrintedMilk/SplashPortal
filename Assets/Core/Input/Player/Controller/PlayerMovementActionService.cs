using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts.Core.Input.Player.Controller
{
	public class PlayerMovementActionService : MonoBehaviour
	{
		[Header("Input Actions")]
		[SerializeField]
		private InputActionReference jumpAction;

		[SerializeField]
		private InputActionReference moveAction;

		[SerializeField]
		private InputActionReference sprintAction;

		[SerializeField]
		private InputActionReference rotateAction;

		[HideInInspector]
		public Vector2 movePosition;

		[HideInInspector]
		public bool isRunning;

		public Action onJump;

		private void OnEnable()
		{
			jumpAction.action.Enable();
			moveAction.action.Enable();
			sprintAction.action.Enable();
			rotateAction.action.Enable();

			jumpAction.action.performed += DoJumpAction;
		}

		private void OnDisable()
		{
			jumpAction.action.Disable();
			moveAction.action.Disable();
			sprintAction.action.Disable();
			rotateAction.action.Disable();
		}

		private void Update()
		{
			movePosition = moveAction.action.ReadValue<Vector2>();
			isRunning = sprintAction.action.IsPressed() && movePosition != Vector2.zero;
		}

		private void DoJumpAction(InputAction.CallbackContext obj)
		{
			onJump?.Invoke();
		}
	}
}