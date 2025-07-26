namespace Client.Scripts.Systems.DataStorageSystem
{
	public abstract class DataStorage<T> : IDataStorage<T> where T: class, IData
	{
		protected readonly ISaveLoadDataProvider _saveLoadProvider;
		
		public T Data { get; set; }

		protected DataStorage(ISaveLoadDataProvider saveLoadProvider)
		{
			_saveLoadProvider = saveLoadProvider;
		}
		
		public virtual void Save()
		{
			_saveLoadProvider.Save(Data);
		}

		public virtual T Load<T>()
		{
			return _saveLoadProvider == null
				? default
				: _saveLoadProvider.Load<T>();
		}
	}
}