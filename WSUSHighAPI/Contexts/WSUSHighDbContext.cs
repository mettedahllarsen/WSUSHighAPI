using Microsoft.EntityFrameworkCore;
using WSUSHighAPI.Models;

namespace WSUSHighAPI.Contexts
{
	public class WSUSHighDbContext : DbContext
	{
		public WSUSHighDbContext(DbContextOptions<WSUSHighDbContext> options) : base(options) { }
		public DbSet<Computer> Computers { get; set; }
	}
}
