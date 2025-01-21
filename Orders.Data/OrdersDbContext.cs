using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Orders.Data.Models;

namespace Orders.Data
{
	/// <summary>
	/// db context for managing Order and Product entities
	/// inherints from DbContext -  EF functionalities for db operations
	/// </summary>
	public class OrdersDbContext : DbContext
	{
		/// <summary>
		/// DbSet representing collection of Order entities in db
		/// </summary>
		public DbSet<Order>? Orders { get; set; }
		/// <summary>
		/// DbSet representing collection of Product entities in db
		/// </summary>
		public DbSet<Product> Products { get; set; }
		public OrdersDbContext(DbContextOptions<OrdersDbContext> options) : base(options)
		{ }

		/// <summary>
		/// testing date for testing purposes
		/// </summary>
		/// <param name="modelBuilder"></param>
		private void AddTestingData(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Order>().HasData(
				new Order
				{
					OrderId = 1,
					OrderNumber = 1001,
					CustomerName = "Jan Novak",
					Issued = new DateTime(2025, 01, 18),
					State = OrderState.New,
				},
				new Order
				{
					OrderId = 2,
					OrderNumber = 1002,
					CustomerName = "Pavel Briza",
					Issued = new DateTime(2025, 01, 05),
					State = OrderState.Paid,
				},
				new Order
				{
					OrderId = 3,
					OrderNumber = 1003,
					CustomerName = "Eva Pupavova",
					Issued = new DateTime(2024, 12, 06),
					State = OrderState.Cancelled,
				}
			);

			// testing data (related to orders via OrderId)
			modelBuilder.Entity<Product>().HasData(
				new Product
				{
					ProductId = 1,
					Name = "keyboard",
					Quantity = 3,
					Price = 450,
					OrderId = 1  // OrderId to which this belongs
				},
				new Product
				{
					ProductId = 2,
					Name = "speaker Logitech",
					Quantity = 2,
					Price = 875,
					OrderId = 2 
				},
				new Product
				{
					ProductId = 3,
					Name = "headphones Niceboy",
					Quantity = 1,
					Price = 1290,
					OrderId = 3  
				}
			);
		}
		/// <summary>
		/// set up of the relationships in model, using ModelBuilder
		/// </summary>
		/// <param name="modelBuilder"></param>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// as EF does not know how to map decimal of Price to the SQl, specification added
			modelBuilder.Entity<Product>()
				.Property(p => p.Price)
				.HasPrecision(10, 2);  // Precision of 10 digits, 2 digits after decimal

			//relationship Order(1) & OrderItems(many) / 1 item belongs to exactly one order
			modelBuilder
				.Entity<Order>()
				.HasMany(o => o.OrderItems)			// 1 order has many items
				.WithOne(p => p.Order)				// 1 item belongs to exactly 1 order
				.HasForeignKey(p => p.OrderId);		// OrderId is the foreight key in the OrderItem/Product table 

			AddTestingData(modelBuilder);

			// prevents cascading deletion
			IEnumerable<IMutableForeignKey> cascadeFKs = modelBuilder.Model.GetEntityTypes()
				.SelectMany(type => type.GetForeignKeys())               // retrieve all foreighn keys
				.Where(foreignKey => !foreignKey.IsOwnership && foreignKey.DeleteBehavior == DeleteBehavior.Cascade);   //look for cascade behaviour

			foreach (IMutableForeignKey foreignKey in cascadeFKs)       // set all cascading foreight keys to restrict deletion
				foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
		}
	}
}
