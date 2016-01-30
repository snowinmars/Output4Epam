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
		bool Create(T item);

		/// <summary>
		/// Get lot by its Id. If no such lot will be found, method return default(Lot)
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		T Read(Guid id);

		/// <summary>
		/// Remove image by its Id.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		bool Delete(Guid id);

		/// <summary>
		/// Set image by its Id.
		/// </summary>
		/// <param name="lot"></param>
		void Update(T item);
	}
}