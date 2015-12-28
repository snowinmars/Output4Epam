namespace Outpu4Epam.DAL.Interface
{
	using System;

	public interface ICRUD<T>
	{
		/// <summary>
		/// Add item to database.
		/// </summary>
		/// <param name="lot"></param>
		/// <returns></returns>
		bool Add(T item); 

		/// <summary>
		/// Get lot by its Id. If no such lot will be found, method return default(Lot)
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		T Get(Guid id); 

		/// <summary>
		/// Remove image by its Id.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		bool Remove(Guid id); 

		/// <summary>
		/// Set image by its Id. Not implemented yet
		/// </summary>
		/// <param name="lot"></param>
		void Set(T item);
	}
}