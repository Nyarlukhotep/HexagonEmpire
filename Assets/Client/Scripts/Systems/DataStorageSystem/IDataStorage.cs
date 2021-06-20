namespace Client.Scripts.Systems.DataStorageSystem
{
	public interface IData {}

	public interface IDataSaveProvider
	{
		void Save<T>(T data);
	}

	public interface IDataLoadProvider
	{
		T Load<T>();
	}
	
	public interface IDataStorage<T> where T: IData
	{
		T Data { get; set; }
		
		void Save(IDataSaveProvider provider);
		
		T Load<T>(IDataLoadProvider provider);
	}
}