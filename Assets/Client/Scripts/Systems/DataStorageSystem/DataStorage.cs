namespace Client.Scripts.Systems.DataStorageSystem
{
	public abstract class DataStorage<T> : IDataStorage<T> where T: class, IData
	{
		public T Data { get; set; }
		
		public void Save(IDataSaveProvider provider)
		{
			provider.Save(Data);
		}

		public T Load<T>(IDataLoadProvider provider)
		{
			return provider == null
				? default
				: provider.Load<T>();
		}
	}
}