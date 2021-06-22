namespace Client.Scripts.Systems.DataStorageSystem
{
	public interface IDataStorage<T> where T: IData
	{
		T Data { get; set; }
		
		void Save(IDataSaveProvider provider);
		
		T Load<T>(IDataLoadProvider provider);
	}
}