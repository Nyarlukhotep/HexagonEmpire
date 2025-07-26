namespace Client.Scripts.Systems.DataStorageSystem
{
	public interface IDataStorage<T> where T: IData
	{
		T Data { get; set; }
		
		void Save();
		
		T Load<T>();
	}
}