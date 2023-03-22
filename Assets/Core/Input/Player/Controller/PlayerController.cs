using Cysharp.Threading.Tasks;
using Game.Scripts.Player.Controller;
using UnityEngine;

namespace Game.Scripts.Core.Input.Player.Controller
{
	public class PlayerController : MonoBehaviour
	{
		[SerializeField]
		private PlayerAnimatorController playerAnimatorController;

		[SerializeField]
		private PlayerMovementActionService playerMovementService;

		[SerializeField]
		private PlayerActionService playerActionService;

		[SerializeField]
		private Transform currentCamera;

		[SerializeField]
		private Rigidbody playerRigidBody;

		[SerializeField]
		private float walkSpeed = 1;

		[SerializeField]
		private float runSpeed = 6;

		[SerializeField]
		private float jumpPower = 4;

		[SerializeField]
		private float rotationSmoothTimeWalking = 0.15f;

		[SerializeField]
		private float rotationSmoothTimeRunning = 0.1f;

		private float currentSpeed;

		private bool isJumping;
		private bool isGrounded = true;

		private bool isCursorLocked;

		private void OnEnable()
		{
			playerActionService.onLockCursor += HandleLockCursor;
		}

		private void OnDisable()
		{
			playerActionService.onLockCursor -= HandleLockCursor;
		}

		private void Start()
		{
			Cursor.lockState = CursorLockMode.Locked;
		}

		private void FixedUpdate()
		{
			GetPlayerSpeed();
			HandlePlayerMovement();
			//HandleCheckGround();
		}

		private void GetPlayerSpeed()
		{
			var isRunning = playerMovementService.isRunning;
			currentSpeed = isRunning ? runSpeed : walkSpeed;
			playerAnimatorController.SetRunningState(isRunning);
		}

		private void HandlePlayerMovement()
		{
			// Clamp movement to avoid diagonals moving faster. This function allows values between 0 - 1
			var clampedValue = Vector3.ClampMagnitude(playerMovementService.movePosition, 1f);

			// Rotation
			if (clampedValue.magnitude > 0.1f)
			{
				var rotationSpeed = playerMovementService.isRunning
					? rotationSmoothTimeRunning
					: rotationSmoothTimeWalking;

				MovementUtils.RotateTowardsTargetStrafing2DAxis(transform, currentCamera, rotationSpeed);
			}

			// Animation
			playerAnimatorController.SetMovementSpeed(playerMovementService.movePosition);

			// Movement
			var relativePosition = MovementUtils.MoveRelativeToCamera2DAxis(currentCamera, clampedValue);
			var speed = currentSpeed * Time.deltaTime;
			playerRigidBody.MovePosition(transform.position += relativePosition * speed);
		}

		private async void HandleJump()
		{
			if (isGrounded && !isJumping)
			{
				isGrounded = false;
				isJumping = true;

				playerAnimatorController.SetJumpingState(isJumping);
				playerAnimatorController.SetGroundedState(isGrounded);

				playerRigidBody.AddForce(Vector3.up * jumpPower * Time.fixedDeltaTime, ForceMode.Impulse);

				await UniTask.WaitUntil(() => isGrounded);
				//await UniTask.Delay(TimeSpan.FromSeconds(2));

				isGrounded = true;
				isJumping = false;

				playerAnimatorController.SetJumpingState(isJumping);
				playerAnimatorController.SetGroundedState(isGrounded);
			}
		}

		private void HandleCheckGround()
		{
			if (!isGrounded && isJumping)
			{
				isGrounded = Physics.CheckCapsule(transform.position, Vector3.down, 0.1f);
				Debug.DrawRay(transform.position, Vector3.down, Color.green);
			}
		}
		
		private void HandleLockCursor()
		{
			isCursorLocked = !isCursorLocked;
			Cursor.lockState = isCursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
}