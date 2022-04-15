using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace StackOverflow.Data
{
    public class StackOverflowDataContext : DbContext
    {
        private string _connectionString;

        public StackOverflowDataContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
        public DbSet<Question> Questions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<QuestionTags> QuestionTags { get; set; }
        public DbSet<Likes> Likes { get; set; }


 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<QuestionTags>()
                .HasKey(qt => new { qt.QuestionId, qt.TagId });
            modelBuilder.Entity<Likes>()
             .HasKey(l => new { l.UserId, l.QuestionId });
        }

      
     


    }
}
