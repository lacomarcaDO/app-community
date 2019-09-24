using Community.Backend.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Comunity.Models;
using System.Threading;

namespace Community.Backend.Database
{
    public class DatabaseContext : IdentityDbContext<User,Role,Guid>
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Category> Categoties { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<FeaturedEvent> FeaturedEvents { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<Sponsor> Sponsors { get; set; }
        public DbSet<SponsorLevel> SponsorLevels { get; set; }
    }
}
