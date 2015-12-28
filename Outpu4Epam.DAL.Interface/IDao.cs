namespace Outpu4Epam.DAL.Interface
{
	using System;
	using System.Collections.Generic;

	public interface IDao<T> : ICRUD<T>, IDisposable
	{
		/// <summary>
		/// Get all lots
		/// </summary>
		/// <returns></returns>
		IEnumerable<T> GetAll();

		// IEnumerable<T> Filter(string filter); // TODO filter method for ILogic
	}
}