using System;

namespace Output4Epam.Entities
{
	public enum LotTypes
	{
		None = 0,
		Other = 1,
	}

	public class Lot
	{
		public Lot(string title, string owner, string sity, int cost, LotTypes types = LotTypes.Other, DateTime postDate = default(DateTime))
		{
			this.Title = title;
			this.Owner = owner;
			this.Sity = sity;
			this.Cost = cost;
			this.Types = types;
			this.PostDate = postDate;
			this.Id = Guid.NewGuid();
		}

		public DateTime PostDate { get; set; }
		public Guid Id { get; set; }
		public int Cost { get; set; }
		public LotTypes Types { get; set; }
		public string Owner { get; set; }
		public string Sity { get; set; }
		public string Title { get; set; }
	}
}