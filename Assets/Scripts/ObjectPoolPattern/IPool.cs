namespace Scripts.ObjectPoolPattern
{
	public interface IPool<T>
	{
		public ICreation<T> Creator
		{
			get;
			set;
		}

		public T GetFree();
		public void SetFree(T toSetFree);
	}
}
