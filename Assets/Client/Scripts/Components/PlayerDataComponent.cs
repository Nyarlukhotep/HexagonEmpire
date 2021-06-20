using System;
using Client.Scripts.Systems.DataStorageSystem;

namespace Client.Scripts.Components
{
	[Serializable]
	public class PlayerDataComponent : IData
	{
		public int currency;
	}
}