using Microsoft.EntityFrameworkCore;
using DemoApp.Models;

public class DemoContext : DbContext 
{

    public DbSet<User> Users { get; set; }
    public DbSet<Job> Jobs { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
    {
        optionsBuilder.UseSqlite("Filename=./DemoApp.sqlite");
    }
}