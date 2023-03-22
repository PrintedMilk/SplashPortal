using UnityEngine;

namespace Game.Scripts.Player.Controller
{
	public static class MovementUtils
	{
		private static float currentAngle;
		private static float currentAngleVelocity;

		public static Quaternion RotateTowardsTarget2DAxis(Transform currentTransform, float rotationSmoothTime,
			Vector2 newPosition, Transform targetTransform)
		{
			var targetAngle = Mathf.Atan2(newPosition.x, newPosition.y)
				* Mathf.Rad2Deg + targetTransform.eulerAngles.y;

			currentAngle =
				Mathf.SmoothDampAngle(currentAngle, targetAngle, ref currentAngleVelocity, rotationSmoothTime);

			currentTransform.rotation = Quaternion.Euler(0, currentAngle, 0);

			var rotatedMovement = Quaternion.Euler(0f, targetAngle, 0f);
			return rotatedMovement;
		}

		public static Vector3 MoveRelativeToCamera2DAxis(Transform cameraTransform, Vector2 targetPosition)
		{
			var cameraForward = cameraTransform.forward;
			var cameraRight = cameraTransform.right;

			cameraForward.y = 0f;
			cameraRight.y = 0f;

			cameraForward = cameraForward.normalized;
			cameraRight = cameraRight.normalized;

			var relativePosition = (cameraForward * targetPosition.y) + (cameraRight * targetPosition.x);
			return relativePosition;
		}

		public static void RotateTowardsTargetStrafing2DAxis(Transform playerTransform, Transform cameraTransform, float turnSpeed)
		{
			var cameraYawl = cameraTransform.transform.rotation.eulerAngles.y;

			playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, Quaternion.Euler(0f, cameraYawl, 0f),
				turnSpeed * Time.fixedDeltaTime);
		}
	}
}