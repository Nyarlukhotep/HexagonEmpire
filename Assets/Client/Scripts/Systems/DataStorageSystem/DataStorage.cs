namespace Client.Scripts.Systems.DataStorageSystem
{
	public abstract class DataStorage<T> : IDataStorage<T> where T: class, IData
	{
		public T Data { get; set; }
		
		public virtual void Save(IDataSaveProvider provider)
		{
			provider.Save(Data);
		}

		public virtual  T Load<T>(IDataLoadProvider provider)
		{
			return provider == null
				? default
				: provider.Load<T>();
		}
	}
}