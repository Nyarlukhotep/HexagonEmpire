using UnityEngine;

namespace Client.Scripts.UnityComponents
{
	public class CommonSceneData : MonoBehaviour
	{
		[SerializeField] 
		private Camera mainCamera;
		[SerializeField] 
		private PopupText popupTextPrefab;
		[SerializeField] 
		private PlayerDataView playerDataView;

		public Camera Camera => mainCamera;

		public PopupText PopupTextPrefab => popupTextPrefab;

		public PlayerDataView PlayerDataView => playerDataView;
	}
}