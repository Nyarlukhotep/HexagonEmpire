namespace Client.Scripts.Systems.DataStorageSystem
{
	public interface IDataSaveProvider
	{
		void Save<T>(T data);
	}
}