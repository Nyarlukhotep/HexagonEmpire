namespace Client.Scripts.Systems.DataStorageSystem
{
	public interface IDataLoadProvider
	{
		T Load<T>();
	}
}