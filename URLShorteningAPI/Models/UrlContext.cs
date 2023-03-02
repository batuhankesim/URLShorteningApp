using Microsoft.EntityFrameworkCore;

namespace URLShorteningAPI.Models
{
    public class UrlContext : DbContext
    {
        public UrlContext(DbContextOptions<UrlContext> options) : base(options) { }

        public DbSet<UrlModel> UrlItems { get; set; } = null!;
    }
}
