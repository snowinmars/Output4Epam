namespace Outpu4Epam.DAL.Interface
{
	public interface IRegUserDao<RegUser> : IDao<RegUser>
	{
		RegUser GetByLogin(string login);
	}
}