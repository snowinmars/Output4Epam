using Output4Epam.Entities;

namespace Output4Epam.BLL.Interface
{
	public interface IRegUserLogic : ILogic<RegUser>
	{
		bool Auth(string login, string password);
	}
}