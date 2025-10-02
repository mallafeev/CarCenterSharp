using CarCenterDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenterDatabaseImplement
{
	public class CarCenterDatabase : DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (optionsBuilder.IsConfigured == false)
			{
				optionsBuilder.UseNpgsql(@"Host=localhost;Database=CarCenterDatabaseFull;Username=postgres;Password=postgres");
			}
			base.OnConfiguring(optionsBuilder);
			AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
			AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
		}
		public virtual DbSet<Order> Orders { set; get; }
		public virtual DbSet<Presale> Presales { set; get; }
		public virtual DbSet<OrderPresale> OrderPresales { set; get; }
		public virtual DbSet<Worker> Workers { set; get; }
		public virtual DbSet<Storekeeper> Storekeepers { set; get; }
		public virtual DbSet<Car> Cars { set; get; }
		public virtual DbSet<Request> Requests { set; get; }
		public virtual DbSet<Feature> Features { set; get; }
		public virtual DbSet<Bundling> Bundlings { set; get; }
		public virtual DbSet<CarBundling> CarBundlings { set; get; }
		public virtual DbSet<PresaleBundling> PresaleBundlings { set; get; }
	}
}
