using DG.Tweening;
using Lean.Pool;
using TMPro;
using UnityEngine;

namespace Client.Scripts.UnityComponents
{
	[RequireComponent(typeof(TextMeshPro))]
	public class PopupText : MonoBehaviour, IPoolable
	{
		[SerializeField] 
		private TextMeshPro textComponent;

		public void Init(Vector2 position, string text)
		{
			textComponent.SetText(text);
			transform.position = position;
			
			transform
				.DOMoveY(position.y + 1.0f, 1.0f)
				.OnComplete(() => LeanPool.Despawn(this));
		}

		public void OnSpawn()
		{
		}

		public void OnDespawn()
		{
			textComponent.SetText(string.Empty);
		}
	}
}