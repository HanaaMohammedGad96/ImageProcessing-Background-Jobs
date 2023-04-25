using ImageProcessingService.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ImageProcessingService.Data;

public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

	public DbSet<Thumbnail> Thumbnails { get; set; }
}
