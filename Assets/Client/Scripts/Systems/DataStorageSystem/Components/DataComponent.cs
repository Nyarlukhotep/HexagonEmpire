namespace Client.Scripts.Systems.DataStorageSystem
{
	/// <summary>
	/// Wrapper for working with data in other systems
	/// </summary>
	/// <typeparam name="T">Any data with type IData</typeparam>
	public struct DataComponent<T> where T: IData
	{
		public T Data;
	}
}