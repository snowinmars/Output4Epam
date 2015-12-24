using System;

namespace Output4Epam.Entities
{
	[Flags]
	public enum LotTypes
	{
		None = 0,
		Other = 1,
		Cloth = 2,
		Technic = 4,
		Fun = 8,
	}

	public class Lot
	{
		public Lot(string title, string owner, string sity, int cost, string info, LotTypes types = LotTypes.Other, DateTime postDate = default(DateTime), Guid g = default(Guid))
		{
			this.Title = title; // lt 200
			this.Owner = owner; // lt 200
			this.Sity = sity; // lt 100
			this.Cost = cost;
			this.Types = types;
			this.PostDate = postDate;
			this.Info = info;
			if (g == default(Guid))
			{
				g = Guid.NewGuid();
			}
			this.Id = g;
		}

		public string Info { get; set; }
		public DateTime PostDate { get; set; }
		public Guid Id { get; set; }
		public int Cost { get; set; }
		public LotTypes Types { get; set; }
		public string Owner { get; set; }
		public string Sity { get; set; }
		public string Title { get; set; }
	}
}