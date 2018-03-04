using Lollapalooza.Services.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lollapalooza.Services.Model
{
    public class LollapaloozaContext : DbContext
    {
        public LollapaloozaContext(DbContextOptions<LollapaloozaContext> options) : base(options) { }
        public virtual DbSet<Show> Show { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserSchedule>().HasKey(x => new { x.UserIdentifier, x.ShowId });
            base.OnModelCreating(modelBuilder);
        }
    }
}
