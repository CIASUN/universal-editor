using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using UEdit.Models;

namespace UEdit.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ItemDetail> ItemDetails => Set<ItemDetail>();
    }
}
