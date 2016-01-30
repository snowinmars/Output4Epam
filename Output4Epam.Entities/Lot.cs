namespace Output4Epam.Entities
{
	using System;

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
		public Lot(
			string title,
			string owner,
			string city,
			int cost,
			string info,
			LotTypes types = LotTypes.Other,
			DateTime postdate = default(DateTime),
			Guid id = default(Guid),
			string boughtBy = "")
		{
			this.Title = title;
			this.Owner = owner;
			this.City = city;
			this.Cost = cost;
			this.Types = types;
			this.Postdate = postdate;
			this.Info = info;
			this.BoughtBy = boughtBy;
			if (id == default(Guid))
			{
				id = Guid.NewGuid();
			}

			this.Id = id;
		}

		public string BoughtBy { get; set; }

		public int Cost { get; set; }

		public Guid Id { get; set; }

		public string Info { get; set; }

		public string Owner { get; set; }

		public DateTime Postdate { get; set; }

		public string City { get; set; }

		public string Title { get; set; }

		public LotTypes Types { get; set; }
	}
}