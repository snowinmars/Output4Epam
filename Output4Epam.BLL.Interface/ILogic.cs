namespace Output4Epam.BLL.Interface
{
	using System;
	using System.Collections.Generic;

	public interface ILogic<T> : ICRUD<T>, IDisposable
	{
		/// <summary>
		/// Get all lots from database
		/// </summary>
		/// <returns></returns>
		IEnumerable<T> GetAll();

		// IEnumerable<T> Filter(string filter); // TODO filter method for ILogic
	}
}