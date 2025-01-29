using BookStoreMVC.Areas.Identity.Data;
using BookStoreMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStoreMVC.Data;

public class BookStoreMVCDBContext : IdentityDbContext<BookStoreMVCUser>
{
    public BookStoreMVCDBContext(DbContextOptions<BookStoreMVCDBContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<FavoriteVideo>()
            .HasKey(fv => new { fv.UserId, fv.VideoId });

        builder.Entity<FavoriteVideo>()
            .HasOne(fv => fv.User)
            .WithMany(u => u.FavoriteVideos)
            .HasForeignKey(fv => fv.UserId);

        builder.Entity<FavoriteVideo>()
            .HasOne(fv => fv.Video)
            .WithMany(u => u.FavoriteVideos)
            .HasForeignKey(fv => fv.VideoId);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<Video> Videos { get; set; }
    public DbSet<FavoriteVideo> FavoriteVideos { get; set; }
}
