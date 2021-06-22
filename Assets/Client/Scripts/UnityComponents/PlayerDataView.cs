using Client.Scripts.Components;
using Client.Scripts.Systems.DataStorageSystem;
using TMPro;
using UnityEngine;

namespace Client.Scripts.UnityComponents
{
	public class PlayerDataView : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI currencyText;

		public void UpdateData(PlayerData data)
		{
			currencyText.SetText(data.currency.ToString());
		}
	}
}