namespace Client.Scripts.Systems.DataStorageSystem
{
	public interface ISaveLoadDataProvider
	{
		void Save<T>(T data);
		T Load<T>();
	}
}