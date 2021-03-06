﻿namespace Output4Epam.BLL.Core
{
	using Output4Epam.Entities;
	using System;
	using System.IO;
	using System.Text.RegularExpressions;

	internal static class Validate
	{
		internal static void V_colorsheme(string colorsheme)
		{
			// TODO
		}

		internal static void V_image(Stream image)
		{
			if (image == null)
			{
				throw new ArgumentException("Image is null");
			}

			long length = image.Length;

			if (length > Common.Common.MaxImageSizeInBytes)
			{
				throw new ArgumentException("Too big file");
			}
		}

		internal static void V_login(string login)
		{
			if (String.IsNullOrEmpty(login))
			{
				throw new ArgumentException("Login is empty");
			}

			if (login.Length > Common.Common.MaxLoginLength)
			{
				throw new ArgumentException($"Login is too long: max length - {Common.Common.MaxLoginLength}");
			}

			if (login.Length < Common.Common.MinLoginLength)
			{
				throw new ArgumentException($"Login is too short: min length - {Common.Common.MinLoginLength}");
			}

			string regexQuery = Common.Common.LoginRegex;
			Regex regex = new Regex(regexQuery);
			Match match = regex.Match(login);

			if (!match.Success)
			{
				throw new ArgumentException("Bad login: use only english letters and _");
			}
		}

		internal static void V_lot(Lot lot)
		{
			if (lot == null)
			{
				throw new ArgumentException("lot is null");
			}

			V_positiveNumber(lot.Cost, canBeZero: false);

			if (lot.Info.Length > Common.Common.MaxInfoLength)
			{
				throw new ArgumentException($"Lot info is too long: max length - {Common.Common.MaxInfoLength}");
			}

			if (lot.Info.Length < Common.Common.MinInfoLength)
			{
				throw new ArgumentException($"Lot info is too short: min length - {Common.Common.MinInfoLength}");
			}

			V_login(lot.Owner);

			DateTime dt = DateTime.Now;

			if (lot.Postdate > dt)
			{
				throw new ArgumentException($"Time paradox: now is {dt}, and lot postdate is {lot.Postdate}");
			}

			if (lot.City.Length > Common.Common.MaxSityLength)
			{
				throw new ArgumentException($"Lot sity name is too long: max length - {Common.Common.MaxSityLength}");
			}

			if (lot.City.Length < Common.Common.MinSityLength)
			{
				throw new ArgumentException($"Lot sity name is too short: min length - {Common.Common.MinSityLength}");
			}

			if (lot.Title.Length > Common.Common.MaxTitleLength)
			{
				throw new ArgumentException($"Lot title is too long: max length - {Common.Common.MaxTitleLength}");
			}

			if (lot.Title.Length < Common.Common.MinTitleLength)
			{
				throw new ArgumentException($"Lot title is too short: max length - {Common.Common.MinTitleLength}");
			}
		}

		internal static void V_password(string password)
		{
			if (String.IsNullOrEmpty(password))
			{
				throw new ArgumentException("Login is empty");
			}

			string regexQuery = Common.Common.PasswordRegex;
			Regex regex = new Regex(regexQuery);
			Match match = regex.Match(password);

			if (password.Length > Common.Common.MaxPasswordLength)
			{
				throw new ArgumentException($"Password is too long: max length - {Common.Common.MaxPasswordLength}");
			}

			if (password.Length < Common.Common.MinPasswordLength)
			{
				throw new ArgumentException($"Password is too short: min length - {Common.Common.MinPasswordLength}");
			}

			if (!match.Success)
			{
				throw new ArgumentException("Bad password: use only english letters, numbers and any of `~!@#$%^&*()-=_+\\|/?.>,<':;]");
			}
		}

		internal static void V_positiveNumber(int summ, bool canBeZero)
		{
			if (canBeZero)
			{
				if (summ < 0)
				{
					throw new ArgumentException("Summ must be positive");
				}
			}
			else
			{
				if (summ <= 0)
				{
					throw new ArgumentException("Summ must be positive");
				}
			}
		}

		internal static void V_regUser(RegUser regUser)
		{
			if (regUser == null)
			{
				throw new ArgumentException("regUser is null");
			}

			V_login(regUser.Login);

			if (regUser.Money < 0)
			{
				throw new ArgumentException("regUser must have non-negative money");
			}
		}

		internal static void V_role(string roleName)
		{
			if (String.IsNullOrEmpty(roleName))
			{
				throw new ArgumentException("Role is empty");
			}
		}

		internal static void V_role(RoleScrolls role)
		{
			if (role == default(RoleScrolls))
			{
				throw new ArgumentException("Role is empty");
			}
		}
	}
}