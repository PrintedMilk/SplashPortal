using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts.Core.Input.Player.Controller
{
	public class PlayerActionService : MonoBehaviour
	{
		[SerializeField]
		private InputActionReference shootAction;

		[SerializeField]
		private InputActionReference aimAction;

		[SerializeField]
		private InputActionReference lockCursorAction;

		public Action<bool> onShoot;
		public Action<bool> onAiming;
		public Action onChangeWeapon;
		public Action onLockCursor;

		private void OnEnable()
		{
			shootAction.action.Enable();
			aimAction.action.Enable();
			lockCursorAction.action.Enable();

			shootAction.action.performed += DoShootAction;
			shootAction.action.canceled += DoShootAction;

			aimAction.action.performed += DoAimAction;
			aimAction.action.canceled += DoAimAction;

			lockCursorAction.action.performed += LockCursorAction;
		}

		private void OnDisable()
		{
			shootAction.action.Disable();
			aimAction.action.Disable();
			lockCursorAction.action.Disable();

			shootAction.action.performed -= DoShootAction;
			shootAction.action.canceled -= DoShootAction;

			aimAction.action.performed -= DoAimAction;
			aimAction.action.canceled -= DoAimAction;

			lockCursorAction.action.performed -= LockCursorAction;
		}

		private void DoAimAction(InputAction.CallbackContext obj)
		{
			var state = obj.ReadValueAsButton();
			onAiming?.Invoke(state);
		}

		private void DoShootAction(InputAction.CallbackContext obj)
		{
			var shootIsPressed = obj.ReadValueAsButton();

			var isShooting = aimAction.action.IsPressed() && shootIsPressed;
			onShoot?.Invoke(isShooting);
		}

		private void DoChangeWeaponAction(InputAction.CallbackContext obj)
		{
			onChangeWeapon?.Invoke();
		}

		private void LockCursorAction(InputAction.CallbackContext obj)
		{
			onLockCursor?.Invoke();
		}
	}
}