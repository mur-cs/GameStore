using Microsoft.EntityFrameworkCore;

namespace GameStore.Models
{
	public class Order
	{
		public int Id { get; set; }
		public string CustomerName { get; set; }
		public string Address { get; set; }
		public string State { get; set; }
		public string ZipCode { get; set; }
		public bool Shipped { get; set; }
		public int UserId { get; set; }
		public User? User { get; set; }
		public IEnumerable<OrderLine>? Lines { get; set; }

        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany()
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);  
        }
    }
}
