using Newtonsoft.Json;
using UnityEngine;

namespace Client.Scripts.Systems.DataStorageSystem
{
	public class PlayerPrefsSaveLoadDataProvider : ISaveLoadDataProvider
	{
		private const string DATA_NAME = "data";
		
		public void Save<T>(T data)
		{
			var dataAsJson = JsonConvert.SerializeObject(data);
            
			PlayerPrefs.SetString(DATA_NAME, dataAsJson);
			PlayerPrefs.Save();
		}

		public T Load<T>()
		{
			var dataAsJson = PlayerPrefs.GetString(DATA_NAME, default);
			var data = JsonConvert.DeserializeObject<T>(dataAsJson);

			return data;
		}
	}
}