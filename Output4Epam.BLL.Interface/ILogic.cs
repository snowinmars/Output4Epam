namespace Output4Epam.BLL.Interface
{
	using System.Collections.Generic;

	public interface ILogic<T> : ICRUD<T>
	{
		/// <summary>
		/// Get all lots from database
		/// </summary>
		/// <returns></returns>
		IEnumerable<T> GetAll();
	}
}