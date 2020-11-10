using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Skeleton.Domain.Models;
using Skeleton.Domain.Models.Users;

namespace Skeleton.Internal
{
    public class SkeletonApiContext : DbContext
    {
        public SkeletonApiContext(DbContextOptions<SkeletonApiContext> options)
            : base(options)
        {
        }
        public DbSet<Question> Questions { get; set;}
        public DbSet<QuestionAnswer> QuestionAnswers { get; set;}
        public DbSet<QuestionCategorie> QuestionCategories { get; set;}
        public DbSet<QuestionProposition> QuestionPropositions { get; set;}
        public DbSet<User> Users { get; set; }
    }
}
