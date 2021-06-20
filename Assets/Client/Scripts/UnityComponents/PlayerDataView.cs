using Client.Scripts.Components;
using TMPro;
using UnityEngine;

namespace Client.Scripts.UnityComponents
{
	public class PlayerDataView : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI currencyText;

		public void UpdateData(PlayerDataComponent data)
		{
			currencyText.SetText(data.currency.ToString());
		}
	}
}