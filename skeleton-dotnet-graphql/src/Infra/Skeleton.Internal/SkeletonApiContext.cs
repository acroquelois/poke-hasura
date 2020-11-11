using Microsoft.EntityFrameworkCore;
using Skeleton.Domain.Models;

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
    }
}
