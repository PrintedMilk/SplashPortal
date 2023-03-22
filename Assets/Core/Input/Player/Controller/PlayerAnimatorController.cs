using UnityEngine;

namespace Game.Scripts.Player.Controller
{
	public class PlayerAnimatorController : MonoBehaviour
	{
		[SerializeField]
		private float walkingAnimationSmooth = 0.1f;
		
		[SerializeField]
		private float runningAnimationSmooth = 0.1f;

		[SerializeField]
		private Animator playerAnimator;

		private readonly int walkX = Animator.StringToHash("walkX");
		private readonly int walkZ = Animator.StringToHash("walkZ");
		private readonly int runX = Animator.StringToHash("runX");
		private readonly int runZ = Animator.StringToHash("runZ");
		private readonly int isRunningParameter = Animator.StringToHash("isRunning");
		private readonly int isJumpingParameter = Animator.StringToHash("isJumping");
		private readonly int isGroundedParameter = Animator.StringToHash("isGrounded");

		private bool isRunning;

		public void SetMovementSpeed(Vector2 movement)
		{
			if (isRunning)
			{
				SetRunSpeed(movement);
			}
			else
			{
				SetWalkSpeed(movement);
			}
		}

		private void SetRunSpeed(Vector2 runSpeed)
		{
			playerAnimator.SetFloat(runX, Mathf.Round(runSpeed.x), runningAnimationSmooth, Time.deltaTime);
			playerAnimator.SetFloat(runZ, Mathf.Round(runSpeed.y),  runningAnimationSmooth, Time.deltaTime);
		}

		private void SetWalkSpeed(Vector2 walkSpeed)
		{
			playerAnimator.SetFloat(walkX, Mathf.Round(walkSpeed.x), walkingAnimationSmooth, Time.deltaTime);
			playerAnimator.SetFloat(walkZ, Mathf.Round(walkSpeed.y), walkingAnimationSmooth, Time.deltaTime);
		}

		public void SetRunningState(bool isRunning)
		{
			this.isRunning = isRunning;
			playerAnimator.SetBool(isRunningParameter, isRunning);
		}

		public void SetJumpingState(bool isJumping)
		{
			playerAnimator.SetBool(isJumpingParameter, isJumping);
		}

		public void SetGroundedState(bool isGrounded)
		{
			playerAnimator.SetBool(isGroundedParameter, isGrounded);
		}
	}
}