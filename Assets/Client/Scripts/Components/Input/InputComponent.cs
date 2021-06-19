using UnityEngine;

namespace Client.Scripts.Components
{
	public struct InputComponent
	{
		public Vector2 point;
		public Vector2 basePoint;
		public InputPhase phase;
		public int fingerId;

		public override string ToString()
		{
			return $"{nameof(InputComponent)} | Phase: {phase} | FingerId: {fingerId} | Point: {point} | BasePoint: {basePoint}";
		}
	}
}