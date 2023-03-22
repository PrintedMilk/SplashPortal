using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Core.Input.Player.Controller
{
	public class ToggleAimCamera : MonoBehaviour
	{
		[SerializeField]
		private Transform moveCamera;

		[SerializeField]
		private Transform aimCamera;

		[SerializeField]
		private Image crossHair;

		public void ToggleCamera(bool isAiming)
		{
			if (isAiming)
			{
				moveCamera.gameObject.SetActive(false);
				aimCamera.gameObject.SetActive(true);
				crossHair.gameObject.SetActive(true);
			}
			else
			{
				moveCamera.gameObject.SetActive(true);
				aimCamera.gameObject.SetActive(false);
				crossHair.gameObject.SetActive(false);
			}
		}

	}
}