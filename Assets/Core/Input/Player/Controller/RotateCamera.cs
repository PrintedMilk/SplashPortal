using UnityEngine;

namespace Game.Scripts.Core.Input.Player.Controller
{
	public class RotateCamera : MonoBehaviour
	{
		[SerializeField]
		private Cinemachine.AxisState yAxis;

		[SerializeField]
		private Cinemachine.AxisState xAxis;

		[SerializeField]
		private Transform cameraLookAt;

		[SerializeField]
		private Cinemachine.CinemachineInputProvider inputAxisProvider;

		private void Awake()
		{
			xAxis.SetInputAxisProvider(0, inputAxisProvider);
			yAxis.SetInputAxisProvider(1, inputAxisProvider);
		}

		private void Update()
		{
			xAxis.Update(Time.deltaTime * 0.5f);
			yAxis.Update(Time.deltaTime * 0.5f);

			cameraLookAt.eulerAngles = new Vector3(yAxis.Value, xAxis.Value, 0f);
		}
	}
}