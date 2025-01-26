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
		/// DbSet representing collection of Order entities in db = Order table
		/// </summary>
		public DbSet<Order>? Orders { get; set; }
		/// <summary>
		/// DbSet representing collection of Product entities in db = Product table
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
					CustomerName = "Jan Dub",
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

			//testing data for OrderItems with product
			modelBuilder.Entity<OrderItem>().HasData(
				new OrderItem
				{
					OrderItemId = 1,
					OrderId = 1,	// to which order this OrderItem belongs to
					ProductId = 1,	// which product belongs to this orderLine
					Quantity = 1	//qunatity in the OrderLine
				},
				new OrderItem
				{
					OrderItemId = 2,
					OrderId = 2,
					ProductId = 2,
					Quantity = 2
				},
				new OrderItem
				{
					OrderItemId = 3,
					OrderId = 3,
					ProductId = 3,
					Quantity = 1
				}
			);

			// testing data for products
			modelBuilder.Entity<Product>().HasData(
				new Product
				{
					ProductId = 1,
					ProductName = "keyboard",
					Price = 450,
				},
				new Product
				{
					ProductId = 2,
					ProductName = "speaker Logitech",
					Price = 875,
				},
				new Product
				{
					ProductId = 3,
					ProductName = "headphones Niceboy",
					Price = 1290,
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
				.WithOne(oi => oi.Order)				// 1 item belongs to exactly 1 order
				.HasForeignKey(oi => oi.OrderId)       // OrderId is the foreight key in the OrderItem table
				.OnDelete(DeleteBehavior.Restrict); // prevents deletion of the order if has any product/orderItems

			//relationship 1 OrderItem has 1 Product / 1 product can have many OrderItems 
			modelBuilder
				.Entity<OrderItem>()
				.HasOne(oi => oi.Product)			// 1 orderItem has 1 product
				.WithMany()							// 1 product belongs to many OrderLines
				.HasForeignKey(oi => oi.ProductId) // ProductId is the foreight key in the OrderItem table
				.OnDelete(DeleteBehavior.Restrict); // prevents deletion of the OrderItem if has any products

			AddTestingData(modelBuilder);

			// prevents cascading deletion - Orders or Products cannot be deleted if they belong to OrderItem
			IEnumerable<IMutableForeignKey> cascadeFKs = modelBuilder.Model.GetEntityTypes()
				.SelectMany(type => type.GetForeignKeys())               // retrieve all foreighn keys
				.Where(foreignKey => !foreignKey.IsOwnership && foreignKey.DeleteBehavior == DeleteBehavior.Cascade);   //look for cascade behaviour

			foreach (IMutableForeignKey foreignKey in cascadeFKs)       // set all cascading foreight keys to restrict deletion
				foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
		}
	}
}
