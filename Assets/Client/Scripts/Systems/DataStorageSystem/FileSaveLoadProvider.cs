using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Client.Scripts.Systems.DataStorageSystem
{
	public class FileSaveLoadProvider : IDataSaveProvider, IDataLoadProvider
	{
		private string DataFolder => Application.persistentDataPath + "/Saves";
		
		public void Save<T>(T data)
		{
			var dataAsJson = JsonConvert.SerializeObject(data);
			var filePath = Path.Combine(DataFolder, $"{typeof(T).Name}.dta");

#if DEBUG
			Debug.Log($"Save file: {filePath}");
#endif			
            
			var folder = Path.GetDirectoryName(filePath);
			if (!Directory.Exists(folder))
				Directory.CreateDirectory(folder);

			File.WriteAllText(filePath, dataAsJson);
		}

		public T Load<T>()
		{
			var filePath = Path.Combine(DataFolder, $"{typeof(T).Name}.dta");
			
#if DEBUG
			Debug.Log($"Load file: {filePath}");
#endif

			if (!File.Exists(filePath))
				return Activator.CreateInstance<T>();
			
			var dataAsJson = File.ReadAllText(filePath);
			var data = JsonConvert.DeserializeObject<T>(dataAsJson);

			return data;
		}
	}
}