namespace Output4Epam.Providers
{
	using System;
	using System.Web.Security;

	public class Output4EpamRoleProvider : RoleProvider
	{
		private string applicationName;

		public override string ApplicationName
		{
			get
			{
				return "Output4Epam";
			}

			set
			{
				this.applicationName = value; // TODO to ask
			}
		}

		public override void AddUsersToRoles(string[] usernames, string[] roleNames)
		{
			throw new NotImplementedException();
		}

		public override void CreateRole(string roleName)
		{
			throw new NotImplementedException();
		}

		public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
		{
			throw new NotImplementedException();
		}

		public override string[] FindUsersInRole(string roleName, string usernameToMatch)
		{
			throw new NotImplementedException();
		}

		public override string[] GetAllRoles()
		{
			throw new NotImplementedException();
		}

		public override string[] GetRolesForUser(string username)
		{
			return LogicProvider.RegUserLogic.GetRolesForUser(username);
		}

		public override string[] GetUsersInRole(string roleName)
		{
			throw new NotImplementedException();
		}

		public override bool IsUserInRole(string username, string roleName)
		{
			return LogicProvider.RegUserLogic.IsUserInRole(username, roleName);
		}

		public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
		{
			throw new NotImplementedException();
		}

		public override bool RoleExists(string roleName)
		{
			throw new NotImplementedException();
		}
	}
}