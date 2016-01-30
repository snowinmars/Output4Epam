namespace Outpu4Epam.DAL.Interface
{
	using System.Collections.Generic;

	public interface IDao<T> : ICRUD<T>
	{
		/// <summary>
		/// Get all lots
		/// </summary>
		/// <returns></returns>
		IEnumerable<T> GetAll();
	}
}