namespace Scripts.ObjectPoolPattern
{
	public interface ICreation<T>
	{
		/// <summary>
		/// Возвращает вновь созданный объект
		/// </summary>
		/// <returns></returns>
		public T Create();
	}
}
